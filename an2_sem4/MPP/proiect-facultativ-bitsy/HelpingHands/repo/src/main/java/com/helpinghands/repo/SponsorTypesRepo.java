package com.helpinghands.repo;

import com.helpinghands.domain.Eveniment;
import com.helpinghands.domain.Participant;
import com.helpinghands.domain.Post;
import com.helpinghands.domain.SponsorType;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;

import java.util.concurrent.atomic.AtomicReference;

public class SponsorTypesRepo extends AbstractRepo<SponsorType> implements ISponsorTypeRepo{

    private final static Logger logger = LogManager.getLogger(EvenimentRepo.class);
    public SponsorTypesRepo() {
        super(SponsorType.class);
    }


    @Override
    public SponsorType getByName(String name) {
        String query = "SELECT st FROM SponsorType st WHERE st.name = :name";

        AtomicReference<SponsorType> result = new AtomicReference<>();
        Session.doTransaction((session, tx) -> {
            result.set(session.createQuery(query, SponsorType.class)
                    .setParameter("name", name)
                    .uniqueResult());
            tx.commit();
        });

        return result.get();
    }


}
