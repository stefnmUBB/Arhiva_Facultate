package com.helpinghands.repo;

import com.helpinghands.domain.Utilizator;

import java.util.concurrent.atomic.AtomicReference;

public class AbstractUtilizatorRepo<U extends Utilizator> extends AbstractRepo<U> implements IUtilizatorRepo<U> {
    public AbstractUtilizatorRepo(Class<U> entityType) {
        super(entityType);
    }

    @Override
    public U findByCredentials(String username, String password) {
        AtomicReference<U> result=new AtomicReference<>();
        Session.doTransaction((session, tx)->{
            result.set(session.createQuery("from "+entityType.getName()+" where username=:username and password=:password", entityType)
                    .setParameter("username", username)
                    .setParameter("password",password)
                    .setMaxResults(1)
                    .uniqueResult());
            tx.commit();
        });
        return result.get();
    }

}
