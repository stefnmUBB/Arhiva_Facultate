package ro.ubbcluj.cs.stefnmubb.festivalsellpoint.service;

import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.domain.Angajat;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.domain.Spectacol;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.repo.EntityRepoException;

import java.time.LocalDateTime;

public interface IAppService {
    Angajat loginAngajat(String username, String password) throws EntityRepoException;
    void registerAngajat(String username, String password, String email) throws EntityRepoException;

    Iterable<Spectacol> getAllSpectacole() throws EntityRepoException;

    Iterable<Spectacol> filterSpectacole(LocalDateTime startDate, LocalDateTime endDate) throws EntityRepoException;

    Iterable<Spectacol> filterSpectacole(LocalDateTime day) throws EntityRepoException;

    void reserveBilet(Spectacol spectacol, String cumparatorName, int seats) throws BiletReservationException, EntityRepoException;
}
