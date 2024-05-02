package com.helpinghands.service.data;

import com.helpinghands.domain.Utilizator;

public class UserInfo{
    private String type; //admin, voluntar
    private Utilizator utilizator;

    public String getToken() {
        return token;
    }

    public void setToken(String token) {
        this.token = token;
    }

    private String token;

    public UserInfo(){}

    public UserInfo(String type, Utilizator utilizator, String token) {
        this.type = type;
        this.utilizator = utilizator;
        this.token=token;
    }

    public String getType() {
        return type;
    }

    public Utilizator getUtilizator() {
        return utilizator;
    }
}
