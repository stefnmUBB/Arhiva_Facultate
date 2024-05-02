package java.ro.ubbcluj.cs.stefnmubb.festivalsellpoint.repo;

import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.domain.Entity;

import java.ro.ubbcluj.cs.stefnmubb.festivalsellpoint.repo.exceptions.EntityRepoException;

public interface IRepo <ID, E extends Entity<ID>> {
    E add(E e) throws EntityRepoException;
    E update(E e) throws EntityRepoException;
    E remove(ID id) throws EntityRepoException;
    E getById(ID id) throws EntityRepoException;
    Iterable<E> getAll()throws EntityRepoException;
}
