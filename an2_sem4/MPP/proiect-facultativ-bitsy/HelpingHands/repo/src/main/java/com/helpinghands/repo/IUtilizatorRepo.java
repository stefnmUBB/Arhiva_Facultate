package com.helpinghands.repo;

import com.helpinghands.domain.Utilizator;

public interface IUtilizatorRepo<U extends Utilizator> extends IRepo<U> {
    U findByCredentials(String username, String password);
}
