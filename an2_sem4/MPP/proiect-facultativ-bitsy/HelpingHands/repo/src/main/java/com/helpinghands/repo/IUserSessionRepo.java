package com.helpinghands.repo;

import com.helpinghands.domain.UserSession;
import com.helpinghands.domain.Utilizator;

public interface IUserSessionRepo extends IRepo<UserSession> {
    String createToken(Utilizator user);
    void close(String token);
    UserSession findByToken(String token);
    String getRole(String token);
}
