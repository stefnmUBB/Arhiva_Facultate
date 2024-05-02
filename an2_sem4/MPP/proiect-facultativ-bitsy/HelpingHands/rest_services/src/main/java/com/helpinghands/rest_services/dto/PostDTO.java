package com.helpinghands.rest_services.dto;

import com.helpinghands.domain.Eveniment;
import com.helpinghands.domain.Interest;
import com.helpinghands.domain.Participant;
import com.helpinghands.domain.Post;

import java.time.LocalDateTime;

public class PostDTO {
    private Integer id;
    private String descriere;
    private Integer idEv;
    private Integer idUser;
    private LocalDateTime data;

    public PostDTO(Integer id, String descriere, Integer idEv, Integer idUser, LocalDateTime data) {
        this.id = id;
        this.descriere = descriere;
        this.idEv = idEv;
        this.idUser = idUser;
        this.data = data;
    }

    public Integer getId() {
        return id;
    }

    public void setId(Integer id) {
        this.id = id;
    }

    public String getDescriere() {
        return descriere;
    }

    public void setDescriere(String descriere) {
        this.descriere = descriere;
    }

    public Integer getIdEv() {
        return idEv;
    }

    public void setIdEv(Integer idEv) {
        this.idEv = idEv;
    }

    public Integer getIdUser() {
        return idUser;
    }

    public void setIdUser(Integer idUser) {
        this.idUser = idUser;
    }

}
