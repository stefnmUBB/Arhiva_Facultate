package ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.client;

import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.objectprotocol.IRequest;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.objectprotocol.IResponse;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.objectprotocol.UpdateResponse;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.service.ServiceException;

import java.io.EOFException;
import java.io.IOException;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.net.Socket;
import java.util.concurrent.BlockingQueue;
import java.util.concurrent.LinkedBlockingQueue;

public abstract class ServiceObjectProxyBase {
    private final String host;
    private final int port;
    private ObjectInputStream input;
    private ObjectOutputStream output;
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
            output=new ObjectOutputStream(connection.getOutputStream());
            output.flush();
            input=new ObjectInputStream(connection.getInputStream());
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
                    Object response=input.readObject();
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
                } catch (ClassNotFoundException e) {
                    System.out.println("Reading error "+e);
                    throw new RuntimeException(e);
                }
            }
            System.out.println("Reader thread stopped");
        }
    }

    protected void sendRequest(IRequest request) throws SendRequestException {
        try {
            output.writeObject(request);
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
