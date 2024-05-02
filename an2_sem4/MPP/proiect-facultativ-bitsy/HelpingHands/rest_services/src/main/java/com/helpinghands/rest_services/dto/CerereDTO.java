package com.helpinghands.rest_services.dto;

import com.helpinghands.domain.CerereSponsor;

public class CerereDTO {
    private Integer id;
    private Integer volId;
    private String cifFirma;
    private String telefon;
    private String adresa;
    private String numeFirma;
    private String sponsorType;
    private String status;

    public CerereDTO(Integer id, Integer volId, String cifFirma, String sponsorType, String telefon, String adresa, String numeFirma,String status) {
        this.id = id;
        this.volId = volId;
        this.cifFirma = cifFirma;
        this.telefon = telefon;
        this.adresa = adresa;
        this.numeFirma = numeFirma;
        this.sponsorType = sponsorType;
        this.status = status;
    }
    public CerereDTO(CerereSponsor cerereSponsor){
        this(cerereSponsor.getId(),cerereSponsor.getVolunteer().getId(),cerereSponsor.getCifFirma(),cerereSponsor.getSponsorType().getName(),cerereSponsor.getTelefon(),cerereSponsor.getAdresaSediului(),cerereSponsor.getNumeFirma(),cerereSponsor.getStatus());
    }

    public Integer getId() {
        return id;
    }

    public void setId(Integer id) {
        this.id = id;
    }

    public Integer getVolId() {
        return volId;
    }

    public void setVolId(Integer volId) {
        this.volId = volId;
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

    public String getAdresa() {
        return adresa;
    }

    public void setAdresa(String adresa) {
        this.adresa = adresa;
    }

    public String getNumeFirma() {
        return numeFirma;
    }

    public void setNumeFirma(String numeFirma) {
        this.numeFirma = numeFirma;
    }

    public String getSponsorType() {
        return sponsorType;
    }

    public void setSponsorType(String sponsorType) {
        this.sponsorType = sponsorType;
    }

    public String getStatus() {
        return status;
    }

    public void setStatus(String status) {
        this.status = status;
    }
}
