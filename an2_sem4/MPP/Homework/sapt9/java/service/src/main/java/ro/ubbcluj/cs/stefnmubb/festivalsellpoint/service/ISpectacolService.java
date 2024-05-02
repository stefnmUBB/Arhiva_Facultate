package ro.ubbcluj.cs.stefnmubb.festivalsellpoint.service;

import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.domain.Spectacol;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.repo.EntityRepoException;

import java.time.LocalDateTime;

public interface ISpectacolService extends IService<Integer, Spectacol> {
    Iterable<Spectacol> getBetweenDates(LocalDateTime start, LocalDateTime end) throws EntityRepoException;
}
