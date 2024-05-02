package sn.socialnetwork.repo;

import sn.socialnetwork.domain.Entity;

public interface IRepo<ID, E extends Entity<ID>> {

    /**
     * Adds a new entity to the sn.socialnetwork.repo
     * @param entity entity to be added
     * @return added entity
     * @throws EntityAlreadyExistsException if id of entity already appears in the sn.socialnetwork.repo
     */
    E add(E entity) throws EntityAlreadyExistsException;

    /**
     * Updates entity based on its id
     * @param entity new entity with modified properties
     * @return changed entity
     */
    E update(E entity);

    /**
     * Remove entity by id
     * @param id Id of entity to delete
     * @return removed entity
     */
    E remove(ID id);

    /**
     * gets entity with given Id
     * @param id id to search for
     * @return entity having specified id, or null if no entity with that id was found
     */
    E getById(ID id);

    /**
     * @return list of all entities
     */
    Iterable<E> getAll();
}
