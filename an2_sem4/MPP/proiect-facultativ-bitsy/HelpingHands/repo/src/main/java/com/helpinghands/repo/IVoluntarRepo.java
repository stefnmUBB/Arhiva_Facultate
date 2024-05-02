package com.helpinghands.repo;

import com.helpinghands.domain.Voluntar;

public interface IVoluntarRepo extends IUtilizatorRepo<Voluntar> {
    public Integer modifyPoints(Voluntar vol, Integer amount);

    public Voluntar changePassword(String username, String password);
}
