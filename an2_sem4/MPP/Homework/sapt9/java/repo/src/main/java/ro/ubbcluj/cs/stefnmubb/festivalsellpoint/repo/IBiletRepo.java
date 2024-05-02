package ro.ubbcluj.cs.stefnmubb.festivalsellpoint.repo;

import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.domain.Bilet;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.domain.Spectacol;

public interface IBiletRepo extends IRepo<Integer, Bilet> {
    Iterable<Bilet> getBySpectacol(Spectacol spectacol) throws EntityRepoException;
}
