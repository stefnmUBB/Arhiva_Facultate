package ro.ubbcluj.cs.stefnmubb.festivalsellpoint.repo;

import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.domain.Spectacol;

import java.time.LocalDateTime;

public interface ISpectacolRepo extends IRepo<Integer, Spectacol> {
    Iterable<Spectacol> getBetweenDates(LocalDateTime start, LocalDateTime end) throws EntityRepoException;
}
