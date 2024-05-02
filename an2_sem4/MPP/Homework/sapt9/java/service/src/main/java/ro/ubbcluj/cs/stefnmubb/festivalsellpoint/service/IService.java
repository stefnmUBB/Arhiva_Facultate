package ro.ubbcluj.cs.stefnmubb.festivalsellpoint.service;

import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.domain.Entity;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.repo.EntityRepoException;

public interface IService<ID, E extends Entity<ID>> {
    void add(E e) throws EntityRepoException;
    void update(E e) throws EntityRepoException;
    void remove(ID id) throws EntityRepoException;
    E getById(ID id) throws EntityRepoException;
    Iterable<E> getAll() throws EntityRepoException;
}
