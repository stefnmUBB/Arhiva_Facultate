package com.helpinghands.rest_services.data;

import com.helpinghands.domain.Participant;

public class ParticipantDTO {
    public VoluntarDTO voluntar;
    public Boolean organizer;
    public int id;

    public VoluntarDTO getVoluntar() {
        return voluntar;
    }

    public void setVoluntar(VoluntarDTO voluntar) {
        this.voluntar = voluntar;
    }

    public Boolean getOrganizer() {
        return organizer;
    }

    public void setOrganizer(Boolean organizer) {
        this.organizer = organizer;
    }

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public static ParticipantDTO fromParticipant(Participant p){
        var r=new ParticipantDTO();
        r.setId(p.getId());
        r.setOrganizer(p.isOrganizer());
        r.setVoluntar(VoluntarDTO.fromVoluntar(p.getVoluntar()));
        return r;
    }
}
