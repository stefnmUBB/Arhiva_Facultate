package com.helpinghands.repo;

import com.helpinghands.domain.Interest;

import java.util.concurrent.atomic.AtomicReference;

public class InterestRepo extends AbstractRepo<Interest> implements IInterestRepo{
    public InterestRepo() {
        super(Interest.class);
    }
    public Interest getByName(String name){
        AtomicReference<Interest> result=new AtomicReference<>();
        Session.doTransaction((session, tx)->{
            result.set(session.createQuery("from Interest where name=:name", Interest.class)
                    .setParameter("name", name)
                    .setMaxResults(1)
                    .uniqueResult());
            tx.commit();
        });
        return result.get();
    }
}
