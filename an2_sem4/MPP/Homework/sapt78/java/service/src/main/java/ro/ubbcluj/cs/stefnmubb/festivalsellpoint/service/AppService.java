package ro.ubbcluj.cs.stefnmubb.festivalsellpoint.service;

import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.domain.Angajat;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.domain.Bilet;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.domain.Spectacol;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.repo.EntityRepoException;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.service.observer.Observable;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.service.observer.Observer;

import java.time.LocalDateTime;

public class AppService implements IAppService {
    private final IAngajatService angajatService;
    private final IBiletService biletService;
    private final ISpectacolService spectacolService;

    public AppService(IAngajatService angajatService, IBiletService biletService, ISpectacolService spectacolService) {
        this.angajatService = angajatService;
        this.biletService = biletService;
        this.spectacolService = spectacolService;
    }

    @Override
    public Angajat loginAngajat(String username, String password, Observer client) throws ServiceException {
        try {
            return angajatService.login(username, password);
        } catch (EntityRepoException e) {
            throw new ServiceException(e);
        }
    }

    @Override
    public void registerAngajat(String username, String password, String email) throws ServiceException {
        try {
            angajatService.register(new Angajat(username, password, email));
        } catch (EntityRepoException e) {
            throw new ServiceException(e);
        }
    }

    @Override
    public Iterable<Spectacol> getAllSpectacole() throws ServiceException {
        try {
            return spectacolService.getAll();
        } catch (EntityRepoException e) {
            throw new ServiceException(e);
        }
    }

    @Override
    public Iterable<Spectacol> filterSpectacole(LocalDateTime startDate, LocalDateTime endDate) throws ServiceException {
        try {
            return spectacolService.getBetweenDates(startDate, endDate);
        } catch (EntityRepoException e) {
            throw new ServiceException(e);
        }
    }

    @Override
    public Iterable<Spectacol> filterSpectacole(LocalDateTime day) throws ServiceException {
        var y = day.getYear();
        var m = day.getMonth();
        var d = day.getDayOfMonth();
        return filterSpectacole(
                LocalDateTime.of(y,m,d,0,0,0),
                LocalDateTime.of(y,m,d,23,59,59));
    }

    @Override
    public void reserveBilet(Spectacol spectacol, String cumparatorName, int seats) throws BiletReservationException, ServiceException {
        var bilet = new Bilet(cumparatorName, seats, spectacol);

        if(seats > spectacol.getNrLocuriDisponibile())
        {
            throw new BiletReservationException("More seats requested than there are provided");
        }
        try {
            biletService.add(bilet);
        } catch (EntityRepoException e) {
            throw new ServiceException(e);
        }

        spectacol.setNrLocuriDisponibile(spectacol.getNrLocuriDisponibile() - seats);
        spectacol.setNrLocuriVandute(spectacol.getNrLocuriVandute()+seats);
        try {
            spectacolService.update(spectacol);
        } catch (EntityRepoException e) {
            throw new ServiceException(e);
        }
    }

    @Override
    public void logout(Angajat angajat) {

    }


}
