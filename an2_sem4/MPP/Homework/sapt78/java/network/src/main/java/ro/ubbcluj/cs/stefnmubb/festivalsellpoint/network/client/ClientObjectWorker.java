package ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.client;

import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.domain.Spectacol;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.dto.DTOFactory;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.objectprotocol.*;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.service.BiletReservationException;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.service.IAppService;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.service.ServiceException;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.service.observer.Observer;

import java.io.IOException;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.net.Socket;
import java.util.stream.StreamSupport;

public class ClientObjectWorker implements Observer, Runnable {
    private final IAppService server;
    private final Socket connection;
    private final ObjectInputStream input;
    private final ObjectOutputStream output;
    private volatile boolean connected;

    public ClientObjectWorker(IAppService server, Socket connection) {
        this.server = server;
        this.connection = connection;

        ObjectInputStream in=null;
        ObjectOutputStream out=null;

        try{
            out=new ObjectOutputStream(connection.getOutputStream());
            out.flush();
            in=new ObjectInputStream(connection.getInputStream());
            connected=true;
        } catch (IOException e) {
            throw new RuntimeException(e);
        }
        input=in;
        output=out;
    }

    @Override
    public void run() {
        while(connected){
            try{
                Object request = input.readObject();
                IResponse response=handleRequest((IRequest)request);
                if (response!=null){
                    sendResponse(response);
                }
            } catch (IOException e) {
                throw new RuntimeException(e);
            }catch (ClassNotFoundException e){
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
                var spectacol = DTOFactory.fromDTO(((ReserveBiletRequest) request).getSpectacol());
                var cumparator = ((ReserveBiletRequest) request).getCumparatorName();
                var seats = ((ReserveBiletRequest) request).getSeats();
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
        synchronized (output) {
            output.writeObject(response);
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
