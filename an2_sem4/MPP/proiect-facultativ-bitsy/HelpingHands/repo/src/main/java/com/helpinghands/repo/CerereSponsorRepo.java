package com.helpinghands.repo;

import com.helpinghands.domain.CerereSponsor;
import com.helpinghands.domain.SponsorType;
import com.helpinghands.domain.Voluntar;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;

import java.util.List;
import java.util.Objects;
import java.util.concurrent.atomic.AtomicReference;
import java.util.stream.StreamSupport;

public class CerereSponsorRepo extends AbstractRepo<CerereSponsor> implements ICerereSponsorRepo {
    private final static Logger logger = LogManager.getLogger(CerereSponsorRepo.class);
    public CerereSponsorRepo() {
        super(CerereSponsor.class);
    }
    @Override
    public CerereSponsor applyforSponsorship(CerereSponsor cerereSponsor) {
        return this.add(cerereSponsor);
    }

    @Override
    public Iterable<CerereSponsor> getAll() {
        logger.trace("");
        logger.info("Getting all");
        AtomicReference<List<CerereSponsor>> result = new AtomicReference<>();
        Session.doTransaction((session, tx)->{
            var cb = session.getCriteriaBuilder();
            var cq = cb.createQuery(entityType);
            var rt = cq.from(entityType);
            result.set(session.createQuery(cq.select(rt)).getResultList()
                    .stream().peek(c->{
                        System.out.println(c.getVolunteer());
                        var x=c.getVolunteer();
                        var y=c.getSponsorType();
                    }).toList());
            tx.commit();
        });
        logger.info("Got all");
        logger.traceExit();
        return result.get();

    }

    @Override
    public CerereSponsor getById(Integer id) {
        logger.trace("");
        logger.info("Getting by id {} {}",id, entityType);
        return  StreamSupport.stream(getAll().spliterator(), false)
                .filter(x-> Objects.equals(x.getId(), id))
                .findFirst()
                .orElse(null);

        /*logger.trace("");
        logger.info("Getting by id {} {}",id, entityType);
        AtomicReference<CerereSponsor> result=new AtomicReference<>();
        result.set(null);
        Session.doTransaction((session, tx)->{
            var c = entityType.cast(session.get(entityType, id));
            var v = c.getVolunteer();
            result.set(c);
            tx.commit();
        });
        logger.info("Got by id {}",result.get());
        logger.traceExit();
        return result.get();*/
    }
}
