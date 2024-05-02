package com.helpinghands.domain;

public class ChatRoom implements IEntity {
    Eveniment eveniment;
    boolean isWithSponsor;

    public ChatRoom() {}

    public ChatRoom(Eveniment eveniment, boolean isWithSponsor) {
        this.eveniment = eveniment;
        this.isWithSponsor = isWithSponsor;
    }

    public Eveniment getEveniment() {
        return eveniment;
    }

    public void setEveniment(Eveniment eveniment) {
        this.eveniment = eveniment;
    }

    public boolean getIsWithSponsor() {
        return isWithSponsor;
    }

    public void setIsWithSponsor(boolean withSponsor) {
        isWithSponsor = withSponsor;
    }

    private Integer id;
    public Integer getId() {return id;}
    public void setId(Integer id) {this.id=id;}
}
