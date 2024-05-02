package com.helpinghands.rest_services.data;

import com.helpinghands.domain.Interest;
import com.helpinghands.domain.Utilizator;
import com.helpinghands.domain.Voluntar;

public class UserDTO {
    private String username;
    private String email;
    private String nume;
    private String prenume;
    private int id;

    public void setUsername(String username) {
        this.username = username;
    }

    public void setEmail(String email) {
        this.email = email;
    }

    public void setNume(String nume) {
        this.nume = nume;
    }

    public void setPrenume(String prenume) {
        this.prenume = prenume;
    }

    public void setId(int id) {
        this.id = id;
    }

    public String getUsername() {
        return username;
    }

    public String getEmail() {
        return email;
    }

    public String getNume() {
        return nume;
    }

    public String getPrenume() {
        return prenume;
    }

    public int getId() {
        return id;
    }

    public static UserDTO fromUtilizator(Utilizator v){
        var r=new UserDTO();
        r.setUsername(v.getUsername());
        r.setEmail(v.getEmail());
        r.setNume(v.getNume());
        r.setPrenume(v.getPrenume());
        r.setId(v.getId());
        return r;
    }
}
