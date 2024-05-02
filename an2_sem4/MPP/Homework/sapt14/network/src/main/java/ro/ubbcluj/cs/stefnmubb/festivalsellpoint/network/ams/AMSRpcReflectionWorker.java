package ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.ams;

import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.domain.Spectacol;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.dto.AngajatDTO;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.dto.DTOFactory;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.objectprotocol.*;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.service.BiletReservationException;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.service.IAppService;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.service.ServiceException;

import java.io.IOException;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.lang.reflect.InvocationTargetException;
import java.lang.reflect.Method;
import java.net.Socket;
import java.util.stream.StreamSupport;

public class AMSRpcReflectionWorker implements Runnable {
    private IAppService server;
    private Socket connection;

    private ObjectInputStream input;
    private ObjectOutputStream output;
    private volatile boolean connected;
    public AMSRpcReflectionWorker(IAppService server, Socket connection) {
        this.server = server;
        this.connection = connection;
        try{
            output=new ObjectOutputStream(connection.getOutputStream());
            output.flush();
            input=new ObjectInputStream(connection.getInputStream());
            connected=true;
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    public void run() {
        while(connected){
            try {
                Object request=input.readObject();
                System.out.println("Received request");
                IResponse response=handleRequest((IRequest)request);
                if (response!=null){
                    sendResponse(response);
                }
            } catch (IOException|ClassNotFoundException e) {
                e.printStackTrace();
            }
            try {
                Thread.sleep(1000);
            } catch (InterruptedException e) {
                e.printStackTrace();
            }
        }
        try {
            input.close();
            output.close();
            connection.close();
        } catch (IOException e) {
            System.out.println("Error "+e);
        }
    }

    private static IResponse okResponse=new OkResponse();
    //  private static Response errorResponse=new Response.Builder().type(ResponseType.ERROR).build();
    private IResponse handleRequest(IRequest request){
        IResponse response=null;
        String handlerName="handle"+request.getClass().getSimpleName();
        System.out.println(handlerName);
        System.out.println("HandlerName "+handlerName);
        try {
            Method method=this.getClass().getDeclaredMethod(handlerName, IRequest.class);
            response=(IResponse)method.invoke(this,request);
            System.out.println("Method "+handlerName+ " invoked");
        } catch (NoSuchMethodException|InvocationTargetException|IllegalAccessException e) {
            e.printStackTrace();
        }
        return response;
    }

    private IResponse handleLoginAngajatRequest(IRequest request){
        var req = (LoginAngajatRequest)request;
        var username = req.getUsername();
        var password = req.getPassword();
        try{
            var angajat = server.loginAngajat(username, password, null /*???*/);
            return new LoginAngajatResponse(AngajatDTO.fromAngajat(angajat));
        }
        catch (ServiceException e){
            connected=false;
            return new ErrorResponse(e.getMessage());
        }
    }

    private IResponse handleGetAllSpectacoleRequest(IRequest request){
        try {
            return new GetAllSpectacoleResponse(
                    StreamSupport.stream(server.getAllSpectacole().spliterator(),false)
                            .toArray(Spectacol[]::new)
                    );
        } catch (ServiceException e) {
            System.out.println(e.getMessage());
            e.printStackTrace();
            return new ErrorResponse(e.getMessage());
        }
    }

    private IResponse handleFilterSpectacoleRequest(IRequest request){
        var req=(FilterSpectacoleRequest)request;
        var d=req.getDay();
        try {
            return new FilterSpectacoleResponse(StreamSupport.stream(server.filterSpectacole(d).spliterator(),false)
                    .toArray(Spectacol[]::new));
        } catch (ServiceException e) {
            System.out.println(e.getMessage());
            e.printStackTrace();
            return new ErrorResponse(e.getMessage());
        }
    }

    private IResponse handleReserveBiletRequest(IRequest request) {
        var req=(ReserveBiletRequest)request;
        var cump = req.getCumparatorName();
        var spec = DTOFactory.fromDTO(req.getSpectacol());
        var seats = req.getSeats();
        try {
            server.reserveBilet(spec, cump, seats);
            return new ReserveBiletResponse();
        } catch (BiletReservationException | ServiceException e) {
            System.out.println(e.getMessage());
            e.printStackTrace();
            return new ErrorResponse(e.getMessage());
        }
    }

    /*private Response handleSEND_MESSAGE_ALL(Request request){
        System.out.println("SendMessageAllRequest ...");
        MessageDTO mdto=(MessageDTO)request.data();
        Message message=DTOUtils.getFromDTO(mdto);
        try {
            server.sendMessageToAll(message);
            return okResponse;
        } catch (ChatException e) {
            return new Response.Builder().type(ResponseType.ERROR).data(e.getMessage()).build();
        }


    }

    private Response handleGET_LOGGED_USERS(Request request){
        System.out.println("GetLoggedFriends Request ...");
        try {
            User[] friends=server.getLoggedUsers();
            UserDTO[] frDTO=DTOUtils.getDTO(friends);
            return new Response.Builder().type(ResponseType.GET_LOGGED_USERS).data(frDTO).build();
        } catch (ChatException e) {
            return new Response.Builder().type(ResponseType.ERROR).data(e.getMessage()).build();
        }
    }*/

    private void sendResponse(IResponse response) throws IOException{
        System.out.println("sending response "+response);
        output.writeObject(response);
        output.flush();
    }
}
