package com.helpinghands.domain;

import javax.persistence.*;

@Entity
@Table(name="Participants")
public class Participant implements IEntity {
    @ManyToOne(targetEntity = Voluntar.class,
            cascade = { CascadeType.ALL }, fetch = FetchType.EAGER)
    @JoinColumn(name="idVoluntar")
    private Voluntar voluntar;

    @Column(name="isOrganizer")
    private boolean organizer;
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Integer id;

    public Participant(){}

    @Override
    public String toString() {
        return "Participant{" +
                "voluntar=" + voluntar +
                ", organizer=" + organizer +
                ", id=" + id +
                '}';
    }

    public Participant(Voluntar voluntar, boolean organizer) {
        this.voluntar = voluntar;
        this.organizer = organizer;
    }
    public Voluntar getVoluntar() {
        return voluntar;
    }

    public void setVoluntar(Voluntar voluntar) {
        this.voluntar = voluntar;
    }
    public boolean isOrganizer() {
        return organizer;
    }

    public void setOrganizer(boolean organizer) {
        this.organizer = organizer;
    }

    public Integer getId() {return id;}
    public void setId(Integer id) {this.id=id;}
}
