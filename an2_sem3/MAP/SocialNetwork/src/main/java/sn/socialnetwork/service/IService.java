package sn.socialnetwork.service;

import sn.socialnetwork.domain.Entity;
import sn.socialnetwork.repo.EntityAlreadyExistsException;
import sn.socialnetwork.reports.AbstractReport;

import java.util.function.Predicate;

public interface IService<ID, E extends Entity<ID>> {

    /**
     * Adds new rntity to the sn.socialnetwork.repo
     * @param entity entity to add
     * @return addition report containing added entity
     * @throws EntityAlreadyExistsException if entity id is already found to the sn.socialnetwork.repo
     */
    AbstractReport add(E entity) throws EntityAlreadyExistsException;

    /**
     * updates existing entity by id
     * @param entity entity to add
     * @return update report containing updated entity
     */
    AbstractReport update(E entity);

    /**
     * removes entity by id
     * @param id id of entity to remove
     * @return remove report containing removed entity
     * @throws EntityIdNotFoundException id the entity to remove was not found
     */
    AbstractReport remove(ID id) throws EntityIdNotFoundException;

    /**
     * checks if there is at least one entity that satisfies the predicate
     * @param pred
     * @return true if predicate is satisfied
     */
    boolean exists(Predicate<E> pred);

    /**
     * @return all entities
     */
    public Iterable<E> getAll();

    /**
     * @return new unique id for an entity to be added
     */
    ID generateNewId();

    E getById(ID id);
}
