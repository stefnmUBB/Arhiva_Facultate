package com.helpinghands.repo;

import com.helpinghands.domain.Eveniment;
import com.helpinghands.repo.data.EventOrderOption;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;

import java.util.concurrent.atomic.AtomicReference;

public class EvenimentRepo extends AbstractRepo<Eveniment> implements IEvenimentRepo{
    private final static Logger logger = LogManager.getLogger(EvenimentRepo.class);
    public EvenimentRepo() {
        super(Eveniment.class);
    }

    @Override
    public Eveniment[] getOrderedPaged(EventOrderOption orderOption, int page, int itemsPerPage) {
        String query = "from Eveniment order by id";
        if(orderOption==EventOrderOption.START_DATE){
            query = "from Eveniment order by startDate";
        } else if (orderOption==EventOrderOption.NAME) {
            query = "from Eveniment order by name";
        }

        String q=query;
        AtomicReference<Eveniment[]> result = new AtomicReference<>();
        Session.doTransaction((session, tx)->{
            result.set(session.createQuery(q, Eveniment.class)
                    .setFirstResult(page*itemsPerPage)
                    .setMaxResults(itemsPerPage)
                    .stream()
                    .filter(ev-> {
                        // to force foreign references to load
                        var x=ev.getInterests().size();
                        var y=ev.getParticipants().size();
                        var z=ev.getInitiator();
                        return true;
                    })
                    .toArray(Eveniment[]::new));
            tx.commit();
        });
        return result.get();
    }

    @Override
    public Eveniment[] getByInterestOrderedPaged(EventOrderOption orderOption, int page, int itemsPerPage, String interest) {
        String query = "select e from Eveniment e, Interest i where i in (from e.interests) and i.name=:interest order by e.id";
        if(orderOption==EventOrderOption.START_DATE){
            query = "select e from Eveniment e, Interest i where i in (from e.interests) and i.name=:interest order by e.startDate";
        } else if (orderOption==EventOrderOption.NAME) {
            query = "select e from Eveniment e, Interest i where i in (from e.interests) and i.name=:interest order by e.name";
        }

        String q=query;
        AtomicReference<Eveniment[]> result = new AtomicReference<>();
        Session.doTransaction((session, tx)->{
            System.out.println(session.createQuery(q, Eveniment.class).getQueryString());
            result.set(session.createQuery(q, Eveniment.class)
                    .setParameter("interest", interest)
                    .setFirstResult(page*itemsPerPage)
                    .setMaxResults(itemsPerPage)
                    .stream()
                    .filter(ev-> {
                        // to force foreign references to load
                        var x=ev.getInterests().size();
                        var y=ev.getParticipants().size();
                        var z=ev.getInitiator();
                        return true;
                    })
                    .toArray(Eveniment[]::new));
            tx.commit();
        });
        return result.get();
    }

    @Override
    public Eveniment[] getByOrganizer(int organizerId) {
        String query="select ev from Eveniment as ev inner join ev.participants as p " +
                "where p.voluntar.id=:volId " +
                "and p.organizer=:isOrg";

        AtomicReference<Eveniment[]> result = new AtomicReference<>();
        Session.doTransaction((session, tx)->{
            result.set(session.createQuery(query, Eveniment.class)
                    .setParameter("volId", organizerId)
                    .setParameter("isOrg", true)
                    .stream()
                    .filter(ev-> {
                        // to force foreign references to load
                        var x=ev.getInterests().size();
                        var y=ev.getParticipants().size();
                        var z=ev.getInitiator();
                        return true;
                    })
                    .toArray(Eveniment[]::new));
            tx.commit();
        });
        return result.get();
    }

    @Override
    public Eveniment[] getByVoluntar(int volId){
        String query="select ev from Eveniment as ev inner join ev.participants as p " +
                "where p.voluntar.id=:volId " +
                "and p.organizer=:isOrg";

        AtomicReference<Eveniment[]> result = new AtomicReference<>();
        Session.doTransaction((session, tx)->{
            result.set(session.createQuery(query, Eveniment.class)
                    .setParameter("volId", volId)
                    .setParameter("isOrg", false)
                    .stream()
                    .filter(ev-> {
                        // to force foreign references to load
                        var x=ev.getInterests().size();
                        var y=ev.getParticipants().size();
                        var z=ev.getInitiator();
                        return true;
                    })
                    .toArray(Eveniment[]::new));
            tx.commit();
        });
        return result.get();
    }
}
