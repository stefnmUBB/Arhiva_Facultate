package ro.ubbcluj.cs.stefnmubb.festivalsellpoint.server;

import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.domain.Angajat;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.domain.Spectacol;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.service.observer.Observable;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.service.observer.Observer;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.service.BiletReservationException;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.service.IAppService;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.service.ServiceException;

import java.time.LocalDateTime;
import java.util.HashMap;

public class ServiceImplementation implements IAppService {
    private final IAppService innerService;
    private final INotificationService notificationService;

    public ServiceImplementation(IAppService innerService, INotificationService notifService) {
        this.innerService = innerService;
        this.notificationService = notifService;
    }

    @Override
    public synchronized Angajat loginAngajat(String username, String password, Observer client) throws ServiceException {
        var angajat = innerService.loginAngajat(username, password, client);
        System.out.println("Login angajat");
        if(angajat==null)
            System.out.println("Angajat null");
        if(client==null)
            System.out.println("Client null");
        if(angajat!=null && client!=null){
            if(angajatiObs.containsKey(username)){
                throw new ServiceException("User already logged");
            }
            angajatiObs.put(username, client);
        }
        return angajat;
    }

    @Override
    public synchronized void registerAngajat(String username, String password, String email) throws ServiceException {
        innerService.registerAngajat(username, password, email);
    }

    @Override
    public synchronized Iterable<Spectacol> getAllSpectacole() throws ServiceException {
        return innerService.getAllSpectacole();
    }

    @Override
    public synchronized Iterable<Spectacol> filterSpectacole(LocalDateTime startDate, LocalDateTime endDate) throws ServiceException {
        return innerService.filterSpectacole(startDate, endDate);
    }

    @Override
    public synchronized Iterable<Spectacol> filterSpectacole(LocalDateTime day) throws ServiceException {
        return innerService.filterSpectacole(day);
    }

    @Override
    public void reserveBilet(Spectacol spectacol, String cumparatorName, int seats) throws BiletReservationException, ServiceException {
        innerService.reserveBilet(spectacol, cumparatorName, seats);
        //System.out.println("Observers count = "+angajatiObs.size());
        //angajatiObs.values().forEach(o->o.updatedSpectacol(spectacol));
        notificationService.updatedSpectacol(spectacol);
    }

    @Override
    public synchronized void logout(Angajat angajat) {
        angajatiObs.remove(angajat.getUsername());
        innerService.logout(angajat);
    }

    HashMap<String, Observer> angajatiObs = new HashMap<>();
}
