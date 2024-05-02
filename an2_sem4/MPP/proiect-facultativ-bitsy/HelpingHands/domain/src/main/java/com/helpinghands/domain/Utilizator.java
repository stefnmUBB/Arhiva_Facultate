package com.helpinghands.domain;

import com.fasterxml.jackson.annotation.JsonIgnoreProperties;

import javax.persistence.*;

@Entity
@Inheritance(strategy = InheritanceType.SINGLE_TABLE)
@JsonIgnoreProperties({"hibernateLazyInitializer"})
@Table(name="Users")
@DiscriminatorValue("0")
@DiscriminatorColumn(name="userType")
public class Utilizator implements IEntity {
    private String username;
    private String password;
    private String email;
    private String nume;
    private String prenume;
    @Id
    @Column(name="id")
    @GeneratedValue(strategy=GenerationType.IDENTITY)
    private Integer id;

    public Utilizator(){}

    public Utilizator(String username, String password, String email, String nume, String prenume) {
        this.username = username;
        this.password = password;
        this.email = email;
        this.nume = nume;
        this.prenume = prenume;
    }

    public String getUsername() {
        return username;
    }

    public void setUsername(String username) {
        this.username = username;
    }

    public String getPassword() {
        return password;
    }

    public void setPassword(String password) {
        this.password = password;
    }

    public String getEmail() {
        return email;
    }

    public void setEmail(String email) {
        this.email = email;
    }

    public String getNume() {
        return nume;
    }

    public void setNume(String nume) {
        this.nume = nume;
    }

    public String getPrenume() {
        return prenume;
    }

    public void setPrenume(String prenume) {
        this.prenume = prenume;
    }
    public Integer getId() {return id;}
    public void setId(Integer id) {this.id=id;}

    @Override
    public String toString() {
        return "Utilizator{" +
                "username='" + username + '\'' +
                ", password='" + password + '\'' +
                ", nume='" + nume + '\'' +
                ", prenume='" + prenume + '\'' +
                '}';
    }
}
