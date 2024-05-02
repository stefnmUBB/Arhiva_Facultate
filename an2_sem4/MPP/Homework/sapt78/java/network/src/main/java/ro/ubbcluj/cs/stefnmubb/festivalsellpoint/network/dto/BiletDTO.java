package ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.dto;

import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.domain.Entity;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.domain.Spectacol;

public class BiletDTO extends EntityDTO {
    private final String numeCumparator;
    private final int nrLocuri;
    private final SpectacolDTO spectacol;

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
