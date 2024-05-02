package com.helpinghands.repo;

import com.helpinghands.domain.IEntity;

public interface IRepo<E extends IEntity> {
    E add(E e);
    E update(E e);
    void remove(E e);
    Iterable<E> getAll();
    E getById(Integer id);
}
