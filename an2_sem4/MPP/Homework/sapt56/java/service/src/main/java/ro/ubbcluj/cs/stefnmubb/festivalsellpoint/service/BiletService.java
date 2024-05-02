package ro.ubbcluj.cs.stefnmubb.festivalsellpoint.service;

import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.domain.Bilet;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.domain.Spectacol;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.repo.EntityRepoException;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.repo.IBiletRepo;

public class BiletService extends AbstractService<Integer, Bilet, IBiletRepo>
    implements IBiletService {
    public BiletService(IBiletRepo repo) {
        super(repo);
    }

    @Override
    public Iterable<Bilet> getBySpectacol(Spectacol spectacol) throws EntityRepoException {
        return getRepo().getBySpectacol(spectacol);
    }
}
