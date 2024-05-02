package com.helpinghands.domain;

import org.hibernate.annotations.Type;

import javax.persistence.*;
import java.time.LocalDateTime;
import java.util.HashSet;
import java.util.Set;

@Entity
@Table(name="Evenimente")
public class Eveniment implements IEntity {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Integer id;

    @Column(name="name")
    private String name;

    @Column(name="description")
    private String description;

    @Column(name="startDate")
    private LocalDateTime startDate;
    @Column(name="endDate")
    private LocalDateTime endDate;

    @Column(name="location")
    private String location;

    @ManyToMany(targetEntity = Interest.class,
            cascade = CascadeType.MERGE, fetch = FetchType.EAGER)
    @JoinTable(name = "EventInterests",
            joinColumns = { @JoinColumn(name = "idEvent") },
            inverseJoinColumns = { @JoinColumn(name = "idInterest") })
    Set<Interest> interests = new HashSet<>();

    @ManyToMany(targetEntity = Participant.class,
            cascade = { CascadeType.MERGE }, fetch = FetchType.EAGER)
    @JoinTable(name = "ParticipantsEvents",
            joinColumns = { @JoinColumn(name = "idEvent") },
            inverseJoinColumns = { @JoinColumn(name = "idParticipant") })
    Set<Participant> participants = new HashSet<Participant>();

    @ManyToOne(targetEntity = Voluntar.class,
            cascade = { CascadeType.MERGE }, fetch = FetchType.EAGER)
    @JoinColumn(name="idVoluntar")
    Voluntar initiator;

    @Column(name="status")
    String status; //  PENDING, ACTIVE, FINISHED, CANCELLED

    public Eveniment() { }

    public Eveniment(Eveniment value) {

    }

    @Override
    public String toString() {
        return "Eveniment{" +
                "id=" + id +
                ", name='" + name + '\'' +
                ", description='" + description + '\'' +
                ", startDate=" + startDate +
                ", endDate=" + endDate +
                ", location='" + location + '\'' +
                ", interests=" + interests +
                ", participants=" + participants +
                ", initiator=" + initiator +
                ", status='" + status + '\'' +
                '}';
    }

    public Eveniment(String name, String description, LocalDateTime startDate, LocalDateTime endDate, String location, Voluntar initiator, String status) {
        this.name = name;
        this.description = description;
        this.startDate = startDate;
        this.endDate = endDate;
        this.location = location;
        this.status = status;
        this.initiator = initiator;
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

    public Set<Interest> getInterests() {
        return interests;
    }

    public void setInterests(Set<Interest> interests) {
        this.interests = interests;
    }

    public Set<Participant> getParticipants() {
        return participants;
    }

    public void setParticipants(Set<Participant> participants) {
        this.participants = participants;
    }

    public Voluntar getInitiator() {
        return initiator;
    }

    public void setInitiator(Voluntar initiator) {
        this.initiator = initiator;
    }

    public String getStatus() {
        return status;
    }

    public void setStatus(String status) {
        this.status = status;
    }

    public Integer getId() {return id;}
    public void setId(Integer id) {this.id=id;}
}
