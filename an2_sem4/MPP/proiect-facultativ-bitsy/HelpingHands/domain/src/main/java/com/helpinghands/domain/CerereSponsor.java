package com.helpinghands.domain;

import javax.persistence.*;

@Entity
@Table(name="CereriSponsor")
public class CerereSponsor implements IEntity {
    @ManyToOne(targetEntity = Voluntar.class,
            cascade = { CascadeType.MERGE }, fetch = FetchType.EAGER)
    @JoinColumn(name="idVoluntar")
    private Voluntar volunteer;
    @Column(name="cifFirma")
    private String cifFirma;
    @Column(name="telefon")
    private String telefon;
    @Column(name="adresaSediului")
    private String adresaSediului;
    @Column(name="numeFirma")
    private String numeFirma;
    @ManyToOne(targetEntity = SponsorType.class,
            cascade = { CascadeType.MERGE }, fetch = FetchType.EAGER)
    @JoinColumn(name="idSponsorType")
    private SponsorType sponsorType;

    @Column(name="status")
    private String status;

    public CerereSponsor(Voluntar volunteer, String cifFirma, String telefon, String adresaSediului, String numeFirma, SponsorType sponsorType) {
        this.volunteer = volunteer;
        this.cifFirma = cifFirma;
        this.telefon = telefon;
        this.adresaSediului = adresaSediului;
        this.numeFirma = numeFirma;
        this.sponsorType = sponsorType;
        this.status = "pending";
        //o sa avem pending, accepted si declined
    }

    public CerereSponsor(Voluntar volunteer, String cifFirma, String telefon, String adresaSediului, String numeFirma, SponsorType sponsorType, String status) {
        this.volunteer = volunteer;
        this.cifFirma = cifFirma;
        this.telefon = telefon;
        this.adresaSediului = adresaSediului;
        this.numeFirma = numeFirma;
        this.sponsorType = sponsorType;
        this.status = status;
        //o sa avem pending, accepted si declined
    }

    public CerereSponsor() {
    }

    public Voluntar getVolunteer() {
        return volunteer;
    }

    public void setVolunteer(Voluntar volunteer) {
        this.volunteer = volunteer;
    }

    public String getCifFirma() {
        return cifFirma;
    }

    public void setCifFirma(String cifFirma) {
        this.cifFirma = cifFirma;
    }

    public String getTelefon() {
        return telefon;
    }

    public void setTelefon(String telefon) {
        this.telefon = telefon;
    }

    public String getAdresaSediului() {
        return adresaSediului;
    }

    public void setAdresaSediului(String adresaSediului) {
        this.adresaSediului = adresaSediului;
    }

    public String getNumeFirma() {
        return numeFirma;
    }

    public void setNumeFirma(String numeFirma) {
        this.numeFirma = numeFirma;
    }

    public SponsorType getSponsorType() {
        return sponsorType;
    }

    public void setSponsorType(SponsorType sponsorType) {
        this.sponsorType = sponsorType;
    }

    public String getStatus() {
        return status;
    }

    public void setStatus(String status) {
        this.status = status;
    }

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Integer id;
    public Integer getId() {return id;}
    public void setId(Integer id) {this.id=id;}

    @Override
    public String toString() {
        return "CerereSponsor{" +
                "volunteer=" + volunteer +
                ", cifFirma='" + cifFirma + '\'' +
                ", telefon='" + telefon + '\'' +
                ", adresaSediului='" + adresaSediului + '\'' +
                ", numeFirma='" + numeFirma + '\'' +
                ", sponsorType=" + sponsorType +
                ", status='" + status + '\'' +
                ", id=" + id +
                '}';
    }
}