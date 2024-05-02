package com.helpinghands.rest_services.dto;

import com.helpinghands.domain.Eveniment;
import com.helpinghands.domain.Interest;
import com.helpinghands.domain.Participant;

import java.time.LocalDateTime;

public class EvenimentDTO {
    private Integer id;
    private String name;
    private String description;
    private LocalDateTime startDate;
    private LocalDateTime endDate;
    private String location;
    private String[] interests;
    private Participant[] participants;
    private Integer initiatorId;
    private String status;

    public Participant[] getParticipants() {
        return participants;
    }

    public void setParticipants(Participant[] participants) {
        this.participants = participants;
    }

    public Integer getId() {
        return id;
    }

    public void setId(Integer id) {
        this.id = id;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getDescription() {
        return description;
    }

    public void setDescription(String description) {
        this.description = description;
    }

    public LocalDateTime getStartDate() {
        return startDate;
    }

    public void setStartDate(LocalDateTime startDate) {
        this.startDate = startDate;
    }

    public LocalDateTime getEndDate() {
        return endDate;
    }

    public void setEndDate(LocalDateTime endDate) {
        this.endDate = endDate;
    }

    public String getLocation() {
        return location;
    }

    public void setLocation(String location) {
        this.location = location;
    }

    public String[] getInterests() {
        return interests;
    }

    public void setInterests(String[] interests) {
        this.interests = interests;
    }

    public Integer getInitiatorId() {
        return initiatorId;
    }

    public void setInitiatorId(Integer initiatorId) {
        this.initiatorId = initiatorId;
    }

    public String getStatus() {
        return status;
    }

    public void setStatus(String status) {
        this.status = status;
    }

    public static EvenimentDTO fromEveniment(Eveniment ev){
        var dto =new EvenimentDTO();
        dto.setId(ev.getId());
        dto.setName(ev.getName());
        dto.setDescription(ev.getDescription());
        dto.setInterests(ev.getInterests().stream().map(Interest::getName).toArray(String[]::new));
        dto.setStatus(ev.getStatus());
        dto.setLocation(ev.getLocation());
        dto.setInitiatorId(ev.getInitiator().getId());
        dto.setStartDate(ev.getStartDate());
        dto.setEndDate(ev.getEndDate());
        dto.setParticipants(ev.getParticipants().toArray(Participant[]::new));
        return dto;
    }
}
