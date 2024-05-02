package ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.ams;

import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.domain.Angajat;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.domain.Spectacol;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.dto.DTOFactory;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.dto.SpectacolDTO;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.objectprotocol.*;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.service.BiletReservationException;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.service.IAppService;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.service.ServiceException;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.service.observer.Observer;

import java.io.IOException;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.net.Socket;
import java.time.LocalDateTime;
import java.util.concurrent.BlockingQueue;
import java.util.concurrent.LinkedBlockingQueue;

public class AMSRpcProxy implements IAppService {
    private String host;
    private int port;
    private Observer client;
    private ObjectInputStream input;
    private ObjectOutputStream output;
    private Socket connection;

    private BlockingQueue<IResponse> qresponses;
    private volatile boolean finished;
    public AMSRpcProxy(String host, int port) {
        this.host = host;
        this.port = port;
        qresponses=new LinkedBlockingQueue<IResponse>();
    }



    /*@Override
    public void sendMessageToAll(Message message) throws ChatException {
        MessageDTO mdto=DTOUtils.getDTO(message);
        Request req=new Request.Builder().type(RequestType.SEND_MESSAGE_ALL).data(mdto).build();
        sendRequest(req);
        Response response=readResponse();
        if (response.type()== ResponseType.ERROR){
            String err=response.data().toString();
            throw new ChatException(err);
        }
    }*/

    /*public void logout(User user) throws ChatException {
        UserDTO udto=DTOUtils.getDTO(user);
        Request req=new Request.Builder().type(RequestType.LOGOUT).data(udto).build();
        sendRequest(req);
        Response response=readResponse();
        closeConnection();
        if (response.type()== ResponseType.ERROR){
            String err=response.data().toString();
            throw new ChatException(err);
        }
    }*/
    private void closeConnection() {
        finished=true;
        try {
            input.close();
            output.close();
            connection.close();
            client=null;
        } catch (IOException e) {
            e.printStackTrace();
        }

    }

    private void sendRequest(IRequest request) {
        try {
            output.writeObject(request);
            output.flush();
        } catch (IOException e) {
            throw new RuntimeException("Error sending object "+e);
        }

    }

    private IResponse readResponse() {
        IResponse response=null;
        try{
            /*synchronized (responses){
                responses.wait();
            }
            response = responses.remove(0);    */
            response=qresponses.take();

        } catch (InterruptedException e) {
            e.printStackTrace();
            throw new RuntimeException(e);
        }
        return response;
    }
    private void initializeConnection() {
        try {
            connection=new Socket(host,port);
            output=new ObjectOutputStream(connection.getOutputStream());
            output.flush();
            input=new ObjectInputStream(connection.getInputStream());
            finished=false;
            startReader();
        } catch (IOException e) {
            e.printStackTrace();
            throw new RuntimeException(e);
        }
    }
    private void startReader(){
        Thread tw=new Thread(new ReaderThread());
        tw.start();
    }

    @Override
    public Angajat loginAngajat(String username, String password, Observer client) throws ServiceException {
        initializeConnection();
        var req=new LoginAngajatRequest(username, password);
        sendRequest(req);
        var response=readResponse();
        if(response instanceof LoginAngajatResponse){
            this.client=client;
            return DTOFactory.fromDTO(((LoginAngajatResponse) response).getAngajat());
        }
        else
            throw new RuntimeException("Expected LoginAnajat got "+response.getClass().getName());
    }

    @Override
    public void registerAngajat(String username, String password, String email) throws ServiceException {

    }

    @Override
    public Iterable<Spectacol> getAllSpectacole() throws ServiceException {
        IRequest req=new GetAllSpectacoleRequest();
        sendRequest(req);
        IResponse response=readResponse();
        if(response instanceof GetAllSpectacoleResponse){
            return ((GetAllSpectacoleResponse) response).getSpectacole()
                    .stream().map(DTOFactory::fromDTO).toList();

        }
        else if(response instanceof ErrorResponse) {
            throw new ServiceException(((ErrorResponse) response).getMessage());
        }
        else throw new RuntimeException("Expected GetAllSpectacole, got "+response.getClass().getName());
    }

    @Override
    public Iterable<Spectacol> filterSpectacole(LocalDateTime startDate, LocalDateTime endDate) throws ServiceException {
        return null;
    }

    @Override
    public Iterable<Spectacol> filterSpectacole(LocalDateTime day) throws ServiceException {
        sendRequest(new FilterSpectacoleRequest(day));
        IResponse response=readResponse();
        if(response instanceof FilterSpectacoleResponse){
            return ((FilterSpectacoleResponse) response).getSpectacole()
                    .stream().map(DTOFactory::fromDTO).toList();

        }
        else if(response instanceof ErrorResponse) {
            throw new ServiceException(((ErrorResponse) response).getMessage());
        }
        else throw new RuntimeException("Expected FilterSpectacole, got "+response.getClass().getName());
    }

    @Override
    public void reserveBilet(Spectacol spectacol, String cumparatorName, int seats) throws BiletReservationException, ServiceException {
        sendRequest(new ReserveBiletRequest(SpectacolDTO.fromSpectacol(spectacol), cumparatorName, seats));
        IResponse response=readResponse();
        if(response instanceof ReserveBiletResponse){
            return;
        }
        else if(response instanceof ErrorResponse) {
            throw new ServiceException(((ErrorResponse) response).getMessage());
        }
        else throw new RuntimeException("Expected FilterSpectacole, got "+response.getClass().getName());
    }

    @Override
    public void logout(Angajat angajat) {

    }

    private class ReaderThread implements Runnable{
        public void run() {
            while(!finished){
                try {
                    Object response=input.readObject();
                    System.out.println("response received "+response);
                    if(response instanceof UpdatedSpectacolResponse){
                        //...
                    }
                    else {

                        try {
                            qresponses.put((IResponse)response);
                        } catch (InterruptedException e) {
                            e.printStackTrace();
                        }
                    }
                } catch (IOException|ClassNotFoundException e) {
                    System.out.println("Reading error "+e);
                }
            }
        }
    }
}
