package ro.ubbcluj.cs.stefnmubb.festivalsellpoint.repo;

import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.domain.Entity;

public interface IRepo <ID, E extends Entity<ID>> {
    void add(E e) throws EntityRepoException;
    void update(E e) throws EntityRepoException;
    void remove(ID id) throws EntityRepoException;
    E getById(ID id) throws EntityRepoException;
    Iterable<E> getAll() throws EntityRepoException;
}
