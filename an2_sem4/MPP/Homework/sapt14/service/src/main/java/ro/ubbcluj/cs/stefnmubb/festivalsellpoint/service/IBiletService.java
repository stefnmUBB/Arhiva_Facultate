package ro.ubbcluj.cs.stefnmubb.festivalsellpoint.service;

import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.domain.Bilet;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.domain.Spectacol;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.repo.EntityRepoException;

public interface IBiletService extends IService<Integer, Bilet> {
    Iterable<Bilet> getBySpectacol(Spectacol spectacol) throws EntityRepoException;
}
