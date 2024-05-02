package sn.socialnetwork.repo;

import sn.socialnetwork.domain.Entity;
import sn.socialnetwork.domain.validators.IValidator;
import java.util.Map;
import java.util.TreeMap;

public class InMemoryRepo<ID, E extends Entity<ID>> implements IRepo<ID, E> {

    final Map<ID, E> entities = new TreeMap<>();
    final IValidator<E> validator;

    /**
     * Creates a new in memory sn.socialnetwork.repo for entities
     * @param validator entity validator
     */
    InMemoryRepo(IValidator<E> validator) {
        this.validator = validator;
    }

    @Override
    public E add(E entity) throws EntityAlreadyExistsException {
        if(entity.getId()==null) {
            throw new IllegalArgumentException("Entity id must not be null");
        }

        ID id = entity.getId();
        if(entities.get(id)!=null) {
            throw new EntityAlreadyExistsException();
        }
        validator.validate(entity);

        entities.put(id, entity);

        return entity;
    }

    @Override
    public E update(E entity) {
        if(entity.getId()==null) {
            throw new IllegalArgumentException("Entity id must not be null");
        }
        validator.validate(entity);
        entities.put(entity.getId(), entity);
        return entity;
    }

    @Override
    public E remove(ID id) {
        if(id==null) {
            return null;
        }
        E entity = entities.get(id);
        if(entity!=null){
            entities.remove(id);
        }
        return entity;
    }

    @Override
    public E getById(ID id) {
        return entities.get(id);
    }

    @Override
    public Iterable<E> getAll() {
        return entities.values();
    }
}
