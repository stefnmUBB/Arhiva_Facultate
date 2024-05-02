package com.helpinghands.domain;

import javax.persistence.*;
import java.time.LocalDateTime;

@Entity
@Table(name="Posts")
public class Post implements IEntity {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Integer id;
    @Column(name="descriere")
    private String descriere;
    @Column(name="data")
    private LocalDateTime data;

    @ManyToOne(targetEntity = Eveniment.class,
            cascade = { CascadeType.MERGE }, fetch = FetchType.EAGER)
    @JoinColumn(name="idEveniment")
    Eveniment eveniment;

    @ManyToOne(targetEntity = Utilizator.class,
            cascade = { CascadeType.MERGE }, fetch = FetchType.EAGER)
    @JoinColumn(name="idUser")
    Utilizator author;

    public Post() {}

    public Post(String descriere, LocalDateTime data, Eveniment eveniment, Utilizator author) {
        this.descriere = descriere;
        this.data = data;
        this.eveniment = eveniment;
        this.author = author;
    }

    public String getDescriere() {
        return descriere;
    }

    public void setDescriere(String descriere) {
        this.descriere = descriere;
    }

    public LocalDateTime getData() {
        return data;
    }

    public void setData(LocalDateTime data) {
        this.data = data;
    }

    public Eveniment getEveniment() {
        return eveniment;
    }

    public void setEveniment(Eveniment eveniment) {
        this.eveniment = eveniment;
    }

    public Utilizator getAuthor() {
        return author;
    }

    public void setAuthor(Utilizator author) {
        this.author = author;
    }


    public Integer getId() {return id;}
    public void setId(Integer id) {this.id=id;}
}
