package com.helpinghands.rest_services.dto;

import java.time.LocalDateTime;

public class PostVolDTO {
    private Integer id;
    private Integer userId;
    private String username;
    private EvenimentNoParticipantsDTO evenimentDTO;
    private String descriere;
    private LocalDateTime data;

    public PostVolDTO(Integer id, Integer userId, String username, EvenimentNoParticipantsDTO evenimentDTO, String descriere, LocalDateTime data) {
        this.id = id;
        this.userId = userId;
        this.username = username;
        this.evenimentDTO = evenimentDTO;
        this.descriere = descriere;
        this.data = data;
    }


    public void setEvenimentDTO(EvenimentNoParticipantsDTO evenimentDTO) {
        this.evenimentDTO = evenimentDTO;
    }

    public LocalDateTime getData() {
        return data;
    }

    public void setData(LocalDateTime data) {
        this.data = data;
    }

    public Integer getId() {
        return id;
    }

    public void setId(Integer id) {
        this.id = id;
    }

    public Integer getUserId() {
        return userId;
    }

    public String getUsername() {
        return username;
    }

    public void setUsername(String username) {
        this.username = username;
    }

    public void setUserId(Integer userId) {
        this.userId = userId;
    }

    public EvenimentNoParticipantsDTO getEvenimentDTO() {
        return evenimentDTO;
    }

    public void EvenimentNoParticipantsDTO(EvenimentNoParticipantsDTO evenimentDTO) {
        this.evenimentDTO = evenimentDTO;
    }

    public String getDescriere() {
        return descriere;
    }

    public void setDescriere(String descriere) {
        this.descriere = descriere;
    }
}
