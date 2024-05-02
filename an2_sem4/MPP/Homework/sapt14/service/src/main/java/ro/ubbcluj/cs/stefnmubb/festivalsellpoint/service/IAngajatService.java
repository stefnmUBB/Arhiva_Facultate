package ro.ubbcluj.cs.stefnmubb.festivalsellpoint.service;

import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.domain.Angajat;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.repo.EntityRepoException;

public interface IAngajatService extends IService<Integer, Angajat> {
    void register(Angajat angajat) throws EntityRepoException;
    Angajat login(String username, String password) throws EntityRepoException;
}
