package com.helpinghands.repo;

import com.helpinghands.domain.Admin;
import com.helpinghands.domain.Voluntar;

import java.util.concurrent.atomic.AtomicReference;

public class AdminRepo extends AbstractUtilizatorRepo<Admin> implements IAdminRepo {
    public AdminRepo() {
        super(Admin.class);
    }

    @Override
    public Admin changePassword(String username, String password) {
        AtomicReference<Admin> admin = new AtomicReference<>();
        this.getAll().forEach(ad -> {
            if (ad.getUsername().equals(username)) {
                ad.setPassword(password);
                admin.set(ad);
                update(ad);
            }
        });
        return admin.get();
    }
}
