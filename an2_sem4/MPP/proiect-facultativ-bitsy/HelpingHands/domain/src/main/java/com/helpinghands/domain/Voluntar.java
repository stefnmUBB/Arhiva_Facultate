package com.helpinghands.domain;

import org.hibernate.annotations.OnDelete;
import org.hibernate.annotations.OnDeleteAction;
import com.fasterxml.jackson.annotation.JsonIgnoreProperties;

import javax.persistence.*;
import java.util.HashSet;
import java.util.Set;

@Entity
@DiscriminatorValue("2")
public class Voluntar extends Utilizator {
    @ManyToMany(targetEntity = Interest.class,
            cascade = { CascadeType.ALL }, fetch = FetchType.EAGER)
    @JoinTable(name = "VoluntarInterests",
            joinColumns = { @JoinColumn(name = "idVoluntar") },
            inverseJoinColumns = {@JoinColumn(name = "idInterest")})
    @OnDelete(action = OnDeleteAction.NO_ACTION)
    Set<Interest> interests = new HashSet<>();
    int xpPoints;

    @Column(name="isSponsor")
    boolean sponsor;
    @Column(name="isActive")
    boolean activeSponsor;

    @ManyToMany(targetEntity = SponsorType.class,
            cascade = { CascadeType.ALL }, fetch = FetchType.EAGER)
    @JoinTable(name = "SponsorDataSponsorTypes",
            joinColumns = { @JoinColumn(name = "idVoluntar") },
            inverseJoinColumns = { @JoinColumn(name = "idSponsorType") })
    Set<SponsorType> sponsorTypes = new HashSet<>();

    @ManyToMany(targetEntity = Eveniment.class,
            cascade = { CascadeType.ALL }, fetch = FetchType.EAGER)
    @JoinTable(name = "SponsorDataSponsoredEvents",
            joinColumns = { @JoinColumn(name = "idVoluntar") },
            inverseJoinColumns = { @JoinColumn(name = "idEvent") })
    Set<Eveniment> sponsoredEvents = new HashSet<>();

    public Voluntar() { }

    public Voluntar(String username, String password, String email, String nume, String prenume,
                    int xpPoints, boolean isSponsor, Set<Interest> interests) {
        super(username, password, email, nume, prenume);
        this.xpPoints = xpPoints;
        this.sponsor = isSponsor;
        this.interests = interests;
    }

    public void setSponsor(boolean sponsor) {
        this.sponsor = sponsor;
    }

    public boolean isActiveSponsor() {
        return activeSponsor;
    }

    public void setActiveSponsor(boolean activeSponsor) {
        this.activeSponsor = activeSponsor;
    }

    public Set<SponsorType> getSponsorTypes() {
        return sponsorTypes;
    }

    public void setSponsorTypes(Set<SponsorType> sponsorTypes) {
        this.sponsorTypes = sponsorTypes;
    }

    public Set<Eveniment> getSponsoredEvents() {
        return sponsoredEvents;
    }

    public void setSponsoredEvents(Set<Eveniment> sponsoredEvents) {
        this.sponsoredEvents = sponsoredEvents;
    }

    public int getXpPoints() {
        return xpPoints;
    }

    public void setXpPoints(int xpPoints) {
        this.xpPoints = xpPoints;
    }

    public boolean isSponsor() {
        return sponsor;
    }

    public Set<Interest> getInterests() {
        return interests;
    }

    public void setInterests(Set<Interest> interests) {
        this.interests = interests;
    }


    @Override
    public String toString() {
        return "Voluntar{" +
                "username=" + getUsername() +
                ", nume=" + getNume() +
                ", interests=" + interests +
                ", xpPoints=" + xpPoints +
                ", sponsor=" + sponsor +
                ", activeSponsor=" + activeSponsor +
                ", sponsorTypes=" + sponsorTypes +
                ", sponsoredEvents=" + sponsoredEvents +
                ", id=" + getId() +
                '}';
    }
}
