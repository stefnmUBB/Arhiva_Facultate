package ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.dto;

import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.domain.Entity;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.domain.Spectacol;

public class BiletDTO extends EntityDTO {
    private String numeCumparator;
    private int nrLocuri;
    private SpectacolDTO spectacol;

    public BiletDTO(){}

    public BiletDTO(String numeCumparator, int nrLocuri, SpectacolDTO spectacol) {
        this.numeCumparator = numeCumparator;
        this.nrLocuri = nrLocuri;
        this.spectacol = spectacol;
    }

    public String getNumeCumparator() {
        return numeCumparator;
    }

    public int getNrLocuri() {
        return nrLocuri;
    }

    public SpectacolDTO getSpectacol() {
        return spectacol;
    }
}
