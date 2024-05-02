package com.helpinghands.repo;

import com.helpinghands.domain.IEntity;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;

import java.util.List;
import java.util.concurrent.atomic.AtomicReference;

public class AbstractRepo<E extends IEntity> implements IRepo<E> {
    private final static Logger logger = LogManager.getLogger(AbstractRepo.class);
    protected final Class<E> entityType;

    public AbstractRepo(Class<E> entityType){
        this.entityType = entityType;
    }

    @Override
    public E add(E e) {
        logger.trace("");
        logger.info("Adding {}",e);
        Session.doTransaction((session, tx)->{
            int id = (int)session.save(e);
            tx.commit();
            e.setId(id);
        });
        logger.info("Ok:{}",e);
        logger.traceExit();
        return e;
    }

    @Override
    public E update(E e) {
        logger.trace("");
        logger.info("Updating {}",e);
        Session.doTransaction((session, tx)->{
            session.update(e);
            tx.commit();
        });
        logger.info("Updated {}",e);
        logger.traceExit();
        return e;
    }

    @Override
    public void remove(E e) {
        logger.trace("");
        logger.info("Removing {}",e);
        Session.doTransaction((session, tx)->{
            session.remove(e);
            tx.commit();
        });
        logger.info("Removal done");
        logger.traceExit();
    }

    @Override
    public Iterable<E> getAll() {
        logger.trace("");
        logger.info("Getting all");
        AtomicReference<List<E>> result = new AtomicReference<>();
        Session.doTransaction((session, tx)->{
            var cb = session.getCriteriaBuilder();
            var cq = cb.createQuery(entityType);
            var rt = cq.from(entityType);
            result.set(session.createQuery(cq.select(rt)).getResultList());
            tx.commit();
        });
        logger.info("Got all");
        logger.traceExit();
        return result.get();
    }

    @Override
    public E getById(Integer id) {
        logger.trace("");
        logger.info("Getting by id {} {}",id, entityType);
        AtomicReference<E> result=new AtomicReference<>();
        result.set(null);
        Session.doTransaction((session, tx)->{
            result.set(entityType.cast(session.get(entityType, id)));
            tx.commit();
        });
        logger.info("Got by id {}",result.get());
        logger.traceExit();
        return result.get();
    }
}
