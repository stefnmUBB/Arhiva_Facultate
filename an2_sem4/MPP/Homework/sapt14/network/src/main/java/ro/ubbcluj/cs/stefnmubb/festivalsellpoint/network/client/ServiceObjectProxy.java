package ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.client;

import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.domain.Angajat;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.domain.Spectacol;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.dto.DTOFactory;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.objectprotocol.*;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.service.BiletReservationException;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.service.IAppService;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.service.ServiceException;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.service.observer.Observable;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.service.observer.Observer;

import java.time.LocalDateTime;
import java.util.Arrays;

public class ServiceObjectProxy extends ServiceObjectProxyBase implements IAppService {
    public ServiceObjectProxy(String host, int port) {
        super(host, port);
    }

    Observer client;

    @Override
    public Angajat loginAngajat(String username, String password, Observer client) throws ServiceException {
        initializeConnection();
        sendRequest(new LoginAngajatRequest(username, password));
        var response = readResponse();
        this.client=client;

        Angajat angajat = null;
        if(response instanceof LoginAngajatResponse){
            System.out.println("Hey");
            angajat = DTOFactory.fromDTO(((LoginAngajatResponse)response).getAngajat());
        }
        else if(response instanceof ErrorResponse){
            closeConnection();
            throw new ServiceException("Failed to login Angajat");
        }
        else {
            System.out.println("What");
            closeConnection();
            throw new ServiceException("Failed to login Angajat");
        }
        return angajat;
    }

    @Override
    public void registerAngajat(String username, String password, String email) throws ServiceException {
        // not used
    }
    @Override
    public Iterable<Spectacol> getAllSpectacole() throws ServiceException {
        testConnectionOpen();
        sendRequest(new GetAllSpectacoleRequest());
        var response = readResponse();
        Iterable<Spectacol> spectacole=null;
        if(response instanceof GetAllSpectacoleResponse){
            spectacole = ()->Arrays.stream(DTOFactory.fromDTO(((GetAllSpectacoleResponse)response)
                            .getSpectacole()))
                    .iterator();
        }
        else {
            throw new ServiceException("Expected Spectacole, received " + response.getClass().getName());
        }
        return spectacole;
    }

    @Override
    public Iterable<Spectacol> filterSpectacole(LocalDateTime startDate, LocalDateTime endDate) throws ServiceException {
        // not used
        return null;
    }

    @Override
    public Iterable<Spectacol> filterSpectacole(LocalDateTime day) throws ServiceException {
        testConnectionOpen();
        sendRequest(new FilterSpectacoleRequest(day));
        var response = readResponse();
        Iterable<Spectacol> spectacole=null;
        if(response instanceof FilterSpectacoleResponse){
            spectacole = ()->Arrays.stream(DTOFactory.fromDTO(((FilterSpectacoleResponse)response)
                            .getSpectacole()))
                    .iterator();
        }
        else {
            throw new ServiceException("Expected Spectacole, received " + response.getClass().getName());
        }
        return spectacole;
    }

    @Override
    public void reserveBilet(Spectacol spectacol, String cumparatorName, int seats) throws BiletReservationException, ServiceException {
        testConnectionOpen();
        sendRequest(new ReserveBiletRequest(DTOFactory.getDTO(spectacol), cumparatorName, seats));
        var response = readResponse();
        Iterable<Spectacol> spectacole=null;
        if(!(response instanceof ReserveBiletResponse)){
            throw new ServiceException("Expected ReserveBiletResponse, received:\n " + response);
        }
    }

    @Override
    public void logout(Angajat angajat) {
        closeConnection();
    }

    @Override
    protected void handleUpdate(UpdateResponse update){
        if (update instanceof UpdatedSpectacolResponse){
            var spectacol = ((UpdatedSpectacolResponse) update).getSpectacol();
            System.out.println("Handle:Updated spectacol "+spectacol);
            try {
                client.updatedSpectacol(DTOFactory.fromDTO(spectacol));
            } catch (Exception e) {
                e.printStackTrace();
            }
        }
    }
}
