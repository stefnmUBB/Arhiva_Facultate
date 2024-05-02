package ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.client;

import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.dto.AngajatDTO;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.dto.BiletDTO;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.dto.SpectacolDTO;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.objectprotocol.IRequest;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.objectprotocol.IResponse;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.objectprotocol.UpdateResponse;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.utils.Stringifier;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.service.ServiceException;

import java.io.*;
import java.net.Socket;
import java.nio.ByteBuffer;
import java.nio.charset.StandardCharsets;
import java.util.concurrent.BlockingQueue;
import java.util.concurrent.LinkedBlockingQueue;

public abstract class ServiceObjectProxyBase {
    private final String host;
    private final int port;
    private InputStream input;
    private OutputStream output;
    private Socket connection;
    private final BlockingQueue<IResponse> qresponses=new LinkedBlockingQueue<IResponse>();
    private volatile boolean finished;

    public ServiceObjectProxyBase(String host, int port) {
        this.host = host;
        this.port = port;
    }

    protected void initializeConnection()  {
        try {
            connection=new Socket(host,port);
            output=connection.getOutputStream();
            output.flush();
            input=connection.getInputStream();
            finished=false;
            startReader();
        } catch (IOException e) {
            throw new RuntimeException(e);
        }
    }
    private void startReader(){
        var readerThread = new Thread(new ServiceObjectProxyBase.ReaderThread());
        readerThread.start();
    }

    private class ReaderThread implements Runnable{
        public void run() {
            while(!finished){
                try {
                    System.out.println(AngajatDTO.class);
                    SpectacolDTO s=null;
                    BiletDTO b=null; // to overcome "Class not found" exception
                    String respStr = readString();
                    System.out.println("Received request : "+respStr);
                    IResponse response = Stringifier.decode(respStr, IResponse.class);
                    System.out.println("response received "+response);
                    if (response instanceof UpdateResponse){
                        handleUpdate((UpdateResponse)response);
                    }else{
                        try {
                            qresponses.put((IResponse)response);
                        } catch (InterruptedException e) {
                            e.printStackTrace();
                        }
                    }
                }catch (EOFException e) {
                    continue;
                } catch (IOException e) {
                    System.out.println("Reading error "+e);
                    throw new RuntimeException(e);
                }
            }
            System.out.println("Reader thread stopped");
        }
    }

    byte[] intToByteArray(int value) {
        byte[] result = new byte[4];
        result[0] = (byte) (value & 0xFF); value>>=8;
        result[1] = (byte) (value & 0xFF); value>>=8;
        result[2] = (byte) (value & 0xFF); value>>=8;
        result[3] = (byte) (value & 0xFF);
        return result;
    }
    private int byteArrayToInt(byte[] arr) {
        System.out.println("ARR = "+arr[0]+" "+arr[1]+" "+arr[2]+" "+arr[3]);
        return ((arr[0]&0xFF) | (((arr[1]&0xFF))<<8)
                | ((arr[2]&0xFF)<<16) | ((arr[3]&0xFF)<<24));
    }

    private void writeInt(int value) throws IOException {
        output.write(intToByteArray(value));
    }

    private static final char[] HEX_ARRAY = "0123456789ABCDEF".toCharArray();
    public static String bytesToHex(byte[] bytes) {
        char[] hexChars = new char[bytes.length * 2];
        for (int j = 0; j < bytes.length; j++) {
            int v = bytes[j] & 0xFF;
            hexChars[j * 2] = HEX_ARRAY[v >>> 4];
            hexChars[j * 2 + 1] = HEX_ARRAY[v & 0x0F];
        }
        return new String(hexChars);
    }

    private void writeString(String str) throws IOException {
        str=str.replace("\0","");
        var buffer = StandardCharsets.UTF_8.encode(str).array();

        System.out.println(buffer.length+" "+ str);
        System.out.println(bytesToHex(buffer));
        writeInt(buffer.length);
        output.write(buffer);
    }

    private String readString() throws IOException {
        int len = byteArrayToInt(input.readNBytes(4));
        byte[] buffer = input.readNBytes(len);
        return StandardCharsets.UTF_8.decode(ByteBuffer.wrap(buffer))
                .toString().replace("\0","");
    }


    protected void sendRequest(IRequest request) throws SendRequestException {

        String stringifiedRequest = Stringifier.encode(request);
        try {
            writeString(stringifiedRequest);
            output.flush();
        } catch (IOException e) {
            throw new SendRequestException("Error sending object "+e);
        }
    }

    protected IResponse readResponse() throws ReceiveResponseException {
        IResponse response=null;
        try{
            response=qresponses.take();
        } catch (InterruptedException e) {
            e.printStackTrace();
            throw new ReceiveResponseException("Error receiving object ");
        }
        return response;
    }

    protected void testConnectionOpen() throws ServiceException {
        if(connection==null){
            throw new ServiceException("Connection is not open");
        }
    }

    public void closeConnection() {
        System.out.println("Closing conn");
        finished=true;
        try{
            // give time for readerThread to stop
            // in order to avoid SocketExceptions
            Thread.sleep(1000);
        }
        catch (Exception e){
            throw new RuntimeException(e);
        }
        try {
            input.close();
            output.close();
            connection.close();
            connection=null;
            System.out.println("Closed conn");
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    protected abstract void handleUpdate(UpdateResponse update);
}
