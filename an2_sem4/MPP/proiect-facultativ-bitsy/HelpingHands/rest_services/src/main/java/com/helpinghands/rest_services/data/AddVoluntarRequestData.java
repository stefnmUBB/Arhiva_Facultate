package com.helpinghands.rest_services.data;

import com.helpinghands.domain.Voluntar;

public class AddVoluntarRequestData {
    private int idVoluntar;
    private String role;

    private String token;

    public String getToken() {
        return token;
    }

    public void setToken(String token) {
        this.token = token;
    }

    public int getIdVoluntar() {
        return idVoluntar;
    }

    public void setIdVoluntar(int idVoluntar) {
        this.idVoluntar = idVoluntar;
    }

    public String getRole() {
        return role;
    }

    public void setRole(String rol) {
        this.role = rol;
    }
}
