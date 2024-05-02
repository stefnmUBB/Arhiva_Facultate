package com.helpinghands.domain;

import javax.persistence.DiscriminatorValue;
import javax.persistence.Entity;

@Entity
@DiscriminatorValue("1")
public class Admin extends Utilizator {
    public Admin(){}
    public Admin(String username, String password, String email, String nume, String prenume) {
        super(username, password, email, nume, prenume);
    }
}
