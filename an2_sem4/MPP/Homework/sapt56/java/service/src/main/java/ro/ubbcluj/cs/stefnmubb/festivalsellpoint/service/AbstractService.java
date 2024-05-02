package ro.ubbcluj.cs.stefnmubb.festivalsellpoint.service;

import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.domain.Entity;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.repo.EntityRepoException;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.repo.IRepo;

public abstract class AbstractService<ID, E extends Entity<ID>, R extends IRepo<ID, E>>
    implements IService<ID, E> {
    private final R repo;

    public R getRepo(){
        return repo;
    }

    public AbstractService(R repo) {
        this.repo = repo;
    }

    @Override
    public void add(E e) throws EntityRepoException {
        repo.add(e);
    }

    @Override
    public void update(E e) throws EntityRepoException {
        repo.update(e);
    }

    @Override
    public void remove(ID id) throws EntityRepoException {
        repo.remove(id);
    }

    @Override
    public E getById(ID id) throws EntityRepoException {
        return repo.getById(id);
    }

    @Override
    public Iterable<E> getAll() throws EntityRepoException {
        return repo.getAll();
    }


}
