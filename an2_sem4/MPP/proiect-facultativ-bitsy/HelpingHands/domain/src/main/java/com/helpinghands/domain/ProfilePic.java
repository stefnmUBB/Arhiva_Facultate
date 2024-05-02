package com.helpinghands.domain;

import javax.persistence.*;

@Entity
@Table(name="ProfilePics")
public class ProfilePic implements IEntity {
    @Id
    private Integer id;

    @Column(name="path")
    private String path;

    public String getPath() {
        return path;
    }

    public void setPath(String path) {
        this.path = path;
    }

    public ProfilePic(){}

    public ProfilePic(String path) {
        this.path = path;
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
