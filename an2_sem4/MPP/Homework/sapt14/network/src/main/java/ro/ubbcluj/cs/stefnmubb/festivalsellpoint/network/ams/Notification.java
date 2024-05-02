package ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.ams;

import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.domain.Spectacol;

public class Notification {
    private int spectacolId;
    private int nrLocuriDisponibile;
    private int nrLocuriVandute;

    //private Spectacol spectacol;

    public Notification(Spectacol spectacol) {
        //this.spectacol = spectacol;
        this.spectacolId=spectacol.getId();
        this.nrLocuriDisponibile= spectacol.getNrLocuriDisponibile();
        this.nrLocuriVandute=spectacol.getNrLocuriVandute();
    }

    public Notification(int spectacolId, int nrLocuriDisponibile, int nrLocuriVandute) {
        this.spectacolId = spectacolId;
        this.nrLocuriDisponibile = nrLocuriDisponibile;
        this.nrLocuriVandute = nrLocuriVandute;
    }

    public int getSpectacolId() {
        return spectacolId;
    }

    public void setSpectacolId(int spectacolId) {
        this.spectacolId = spectacolId;
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

    //public Spectacol getSpectacol() {     return spectacol; }

    //public void setSpectacol(Spectacol spectacol) {this.spectacol = spectacol;}


    @Override
    public String toString() {
        return "Notification{" +
                "spectacolId=" + spectacolId +
                ", nrLocuriDisponibile=" + nrLocuriDisponibile +
                ", nrLocuriVandute=" + nrLocuriVandute +
                '}';
    }
}
