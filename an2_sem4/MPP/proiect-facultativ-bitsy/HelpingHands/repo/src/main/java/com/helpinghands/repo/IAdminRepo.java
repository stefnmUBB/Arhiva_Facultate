package com.helpinghands.repo;

import com.helpinghands.domain.Admin;
import com.helpinghands.domain.Voluntar;

public interface IAdminRepo extends IUtilizatorRepo<Admin> {

    public Admin changePassword(String username, String password);
}
