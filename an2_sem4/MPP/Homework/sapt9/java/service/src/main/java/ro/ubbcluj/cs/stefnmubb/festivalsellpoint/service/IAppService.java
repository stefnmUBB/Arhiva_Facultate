package ro.ubbcluj.cs.stefnmubb.festivalsellpoint.service;

import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.domain.Angajat;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.domain.Spectacol;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.repo.EntityRepoException;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.service.observer.Observable;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.service.observer.Observer;

import java.time.LocalDateTime;

public interface IAppService {
    Angajat loginAngajat(String username, String password, Observer client) throws ServiceException;
    void registerAngajat(String username, String password, String email) throws ServiceException;

    Iterable<Spectacol> getAllSpectacole() throws ServiceException;

    Iterable<Spectacol> filterSpectacole(LocalDateTime startDate, LocalDateTime endDate) throws ServiceException;

    Iterable<Spectacol> filterSpectacole(LocalDateTime day) throws ServiceException;

    void reserveBilet(Spectacol spectacol, String cumparatorName, int seats) throws BiletReservationException, ServiceException;

    void logout(Angajat angajat);
}
