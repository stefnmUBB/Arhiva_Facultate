package ro.ubbcluj.cs.stefnmubb.festivalsellpoint.domain;

import java.time.LocalDate;
import java.time.LocalDateTime;

public class Spectacol extends Entity<Integer> {
    private String artist;
    private LocalDateTime data;
    private String locatie;
    private int nrLocuriDisponibile;
    private int nrLocuriVandute;

    public String getArtist() {
        return artist;
    }

    public void setArtist(String artist) {
        this.artist = artist;
    }

    public LocalDateTime getData() {
        return data;
    }

    public void setData(LocalDateTime data) {
        this.data = data;
    }

    public String getLocatie() {
        return locatie;
    }

    public void setLocatie(String locatie) {
        this.locatie = locatie;
    }

    public int getNrLocuriDisponibile() {
        return nrLocuriDisponibile;
    }

    public void setNrLocuriDisponibile(int nrLocuriDisponibile) {
        this.nrLocuriDisponibile = nrLocuriDisponibile;
    }

    public int getNrLocuriVandute() {
        return nrLocuriVandute;
    }

    public void setNrLocuriVandute(int nrLocuriVandute) {
        this.nrLocuriVandute = nrLocuriVandute;
    }

    public Spectacol(){}
    public Spectacol(String artist, LocalDateTime data, String locatie, int nrLocuriDisponibile, int nrLocuriVandute) {
        this.artist = artist;
        this.data = data;
        this.locatie = locatie;
        this.nrLocuriDisponibile = nrLocuriDisponibile;
        this.nrLocuriVandute = nrLocuriVandute;
    }

    @Override
    public String toString() {
        return "Spectacol{" +
                "artist='" + artist + '\'' +
                ", data=" + data +
                ", locatie='" + locatie + '\'' +
                ", nrLocuriDisponibile=" + nrLocuriDisponibile +
                ", nrLocuriVandute=" + nrLocuriVandute +
                '}';
    }
}
