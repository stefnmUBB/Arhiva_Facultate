package com.helpinghands.repo;

import com.helpinghands.domain.Eveniment;
import com.helpinghands.domain.Voluntar;
import com.helpinghands.repo.data.EventOrderOption;

import java.util.concurrent.atomic.AtomicReference;

public class VoluntarRepo extends AbstractUtilizatorRepo<Voluntar> implements IVoluntarRepo {
    public VoluntarRepo() {
        super(Voluntar.class);
    }


    @Override
    public Integer modifyPoints(Voluntar vol, Integer amount) {
        int points = vol.getXpPoints() + amount;
        vol.setXpPoints(points);
        update(vol);
        return points;
    }

    @Override
    public Voluntar changePassword(String username, String password) {
        AtomicReference<Voluntar> voluntar = new AtomicReference<>();
        this.getAll().forEach(vol -> {
            if (vol.getUsername().equals(username)) {
                vol.setPassword(password);
                voluntar.set(vol);
                update(vol);
            }
        });
        return voluntar.get();
    }

}
