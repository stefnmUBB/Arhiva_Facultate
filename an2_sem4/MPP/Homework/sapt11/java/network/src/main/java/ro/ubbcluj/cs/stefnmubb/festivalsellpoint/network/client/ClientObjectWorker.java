package ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.client;

import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.domain.Spectacol;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.dto.DTOFactory;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.objectprotocol.*;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.utils.Stringifier;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.service.BiletReservationException;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.service.IAppService;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.service.ServiceException;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.service.observer.Observer;

import java.io.*;
import java.net.Socket;
import java.nio.ByteBuffer;
import java.nio.charset.StandardCharsets;
import java.util.stream.StreamSupport;

public class ClientObjectWorker implements Observer, Runnable {
    private final IAppService server;
    private final Socket connection;
    private final InputStream input;
    private final OutputStream output;
    private volatile boolean connected;

    public ClientObjectWorker(IAppService server, Socket connection) {
        this.server = server;
        this.connection = connection;

        InputStream in=null;
        OutputStream out=null;

        try{
            out=connection.getOutputStream();
            out.flush();
            in=connection.getInputStream();
            connected=true;
        } catch (IOException e) {
            throw new RuntimeException(e);
        }
        input=in;
        output=out;
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

    @Override
    public void run() {
        while(connected){
            try{
                String reqStr = readString();
                System.out.println("Received request : "+reqStr);
                IRequest request = Stringifier.decode(reqStr, IRequest.class);

                IResponse response = handleRequest(request);
                if (response!=null){
                    sendResponse(response);
                }
            } catch (IOException e) {
                throw new RuntimeException(e);
            }
        }
        try {
            Thread.sleep(1000);
        } catch (InterruptedException e) {
            throw new RuntimeException(e);
        }
        try {
            input.close();
            output.close();
            connection.close();
        } catch (IOException e) {
            throw new RuntimeException(e);
        }
    }

    private IResponse handleRequest(IRequest request){
        System.out.println("Handling request"+request);

        IResponse response = null;
        if(request instanceof GetAllSpectacoleRequest){
            System.out.println("Handle GetAllSpectacoleReq");
            try {
                response = new GetAllSpectacoleResponse(StreamSupport
                        .stream(server.getAllSpectacole().spliterator(), false)
                        .toArray(Spectacol[]::new));
            } catch (Exception e) {
                response = new ErrorResponse(e.getMessage());
            }
        }
        if(request instanceof FilterSpectacoleRequest){
            System.out.println("Handle FilterSpectacoleReq");
            try {
                var spectacole = server.filterSpectacole(((FilterSpectacoleRequest) request).getDay());
                response = new FilterSpectacoleResponse(StreamSupport
                        .stream(spectacole.spliterator(), false)
                        .toArray(Spectacol[]::new));
            } catch (Exception e) {
                System.out.println(e.getMessage());
                e.printStackTrace();
                response = new ErrorResponse(e.getMessage());
            }
        }
        else if(request instanceof LoginAngajatRequest){
            System.out.println("Handle LoginAngajatReq");
            try {
                var angajat = server.loginAngajat(((LoginAngajatRequest) request).getUsername(),
                        ((LoginAngajatRequest) request).getPassword(), this);

                response = new LoginAngajatResponse(DTOFactory.getDTO(angajat));
            } catch (Exception e) {
                response = new ErrorResponse(e.getMessage());
            }
        }
        if(request instanceof ReserveBiletRequest){
            System.out.println("Handle ReserveBiletReq");
            try{
                var spectacol = DTOFactory.fromDTO(((ReserveBiletRequest) request).getBilet().getSpectacol());
                var cumparator = ((ReserveBiletRequest) request).getBilet().getNumeCumparator();
                var seats = ((ReserveBiletRequest) request).getBilet().getNrLocuri();
                System.out.println(server.getClass());
                server.reserveBilet(spectacol, cumparator, seats);
                response = new ReserveBiletResponse();
            } catch (ServiceException | BiletReservationException e) {
                response = new ErrorResponse(e.getMessage());
            }
        }
        if(response==null)
            response = new ErrorResponse("No response");
        return response;
    }

    private void sendResponse(IResponse response) throws IOException {
        System.out.println("sending response "+response);
        String stringifiedResponse = Stringifier.encode(response);

        synchronized (output) {
            writeString(stringifiedResponse);
            //output.writeObject(response);
            output.flush();
        }
    }

    @Override
    public void updatedSpectacol(Spectacol s) {
        System.out.println("Worder: Updated spectacol "+ s);
        var sDto = DTOFactory.getDTO(s);
        try {
            sendResponse(new UpdatedSpectacolResponse(sDto));
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
}
