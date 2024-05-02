package ro.ubbcluj.cs.stefnmubb.festivalsellpoint.service;

import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.domain.Spectacol;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.repo.EntityRepoException;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.repo.ISpectacolRepo;

import java.time.LocalDateTime;

public class SpectacolService extends AbstractService<Integer, Spectacol, ISpectacolRepo>
    implements ISpectacolService{
    public SpectacolService(ISpectacolRepo repo) {
        super(repo);
    }

    @Override
    public Iterable<Spectacol> getBetweenDates(LocalDateTime start, LocalDateTime end) throws EntityRepoException {
        return getRepo().getBetweenDates(start, end);
    }
}
