package ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.dto;

import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.domain.Entity;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.domain.Spectacol;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.Constants;

import java.time.LocalDateTime;

public class SpectacolDTO extends EntityDTO {
    private final String artist;
    private String data;
    private final String locatie;
    private final int nrLocuriDisponibile;
    private final int nrLocuriVandute;

    public LocalDateTime getData() {
        return LocalDateTime.parse(data, Constants.DTO_FORMATTER);
    }

    public void setData(LocalDateTime data) {
        this.data = data.format(Constants.DTO_FORMATTER);
    }

    public SpectacolDTO(String artist, LocalDateTime data, String locatie, int nrLocuriDisponibile, int nrLocuriVandute) {
        this.artist = artist;
        this.data = data.format(Constants.DTO_FORMATTER);
        this.locatie = locatie;
        this.nrLocuriDisponibile = nrLocuriDisponibile;
        this.nrLocuriVandute = nrLocuriVandute;
    }

    public String getArtist() {
        return artist;
    }

    public String getLocatie() {
        return locatie;
    }

    public int getNrLocuriDisponibile() {
        return nrLocuriDisponibile;
    }

    public int getNrLocuriVandute() {
        return nrLocuriVandute;
    }

    public static SpectacolDTO fromSpectacol(Spectacol s){
        var sp = new SpectacolDTO(s.getArtist(), s.getData(), s.getLocatie(), s.getNrLocuriDisponibile(), s.getNrLocuriVandute());
        sp.setId(s.getId());
        return sp;
    }

    public Spectacol toSpectacol(){
        var sp=new Spectacol(getArtist(), getData(), getLocatie(), getNrLocuriDisponibile(), getNrLocuriVandute());
        sp.setId(getId());
        return sp;
    }
}
