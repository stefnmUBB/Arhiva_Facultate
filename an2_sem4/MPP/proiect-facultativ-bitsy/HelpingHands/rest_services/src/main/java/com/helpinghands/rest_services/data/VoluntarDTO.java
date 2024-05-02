package com.helpinghands.rest_services.data;

import com.helpinghands.domain.Interest;
import com.helpinghands.domain.Voluntar;

public class VoluntarDTO extends UserDTO {
    private String[] interests;
    private int xpPoints;
    private Boolean isSponsor;
    private Boolean isActiveSponsor;

    public void setXpPoints(int xpPoints) {
        this.xpPoints = xpPoints;
    }

    public void setSponsor(Boolean sponsor) {
        isSponsor = sponsor;
    }

    public void setActiveSponsor(Boolean activeSponsor) {
        isActiveSponsor = activeSponsor;
    }

    public int getXpPoints() {
        return xpPoints;
    }

    public Boolean getSponsor() {
        return isSponsor;
    }

    public Boolean getActiveSponsor() {
        return isActiveSponsor;
    }

    public String[] getInterests() {
        return interests;
    }

    public void setInterests(String[] interests) {
        this.interests = interests;
    }

    public static VoluntarDTO fromVoluntar(Voluntar v){
        var r=new VoluntarDTO();
        r.setUsername(v.getUsername());
        r.setEmail(v.getEmail());
        r.setNume(v.getNume());
        r.setPrenume(v.getPrenume());
        r.setId(v.getId());
        r.setInterests(v.getInterests().stream().map(Interest::getName).toArray(String[]::new));
        r.setXpPoints(v.getXpPoints());
        r.setSponsor(v.isSponsor());
        r.setActiveSponsor(v.isActiveSponsor());
        return r;
    }
}
