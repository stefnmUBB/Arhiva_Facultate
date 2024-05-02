package ro.ubbcluj.cs.stefnmubb.festivalsellpoint.repo;

import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.domain.Angajat;

public interface IAngajatRepo extends IRepo<Integer, Angajat> {
    Angajat findByCredentials(String username, String password) throws EntityRepoException;
}
