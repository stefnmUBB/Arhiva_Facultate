package com.helpinghands.domain;

import javax.persistence.*;

@Entity
@Table(name="Interests")
public class Interest implements IEntity {
    public Interest(){}
    public Interest(String name){
        this.name=name;
    }

    @Column(name="name")
    private String name;

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private int id;

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    @Override
    public String toString() {
        return "Interest{" +
                "name='" + name + '\'' +
                '}';
    }

    @Override
    public Integer getId() {
        return id;
    }

    @Override
    public void setId(Integer id) {
        this.id=id;
    }
}
