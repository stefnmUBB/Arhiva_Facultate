package ro.ubbcluj.cs.stefnmubb.festivalsellpoint.domain;
public class Bilet extends Entity<Integer> {
    private String numeCumparator;
    private int nrLocuri;

    private Spectacol spectacol;

    public Spectacol getSpectacol() {
        return spectacol;
    }

    public void setSpectacol(Spectacol spectacol) {
        this.spectacol = spectacol;
    }



    public String getNumeCumparator() {
        return numeCumparator;
    }

    public void setNumeCumparator(String numeCumparator) {
        this.numeCumparator = numeCumparator;
    }

    public int getNrLocuri() {
        return nrLocuri;
    }

    public void setNrLocuri(int nrLocuri) {
        this.nrLocuri = nrLocuri;
    }

    public Bilet(String numeCumparator, int nrLocuri) {
        this.numeCumparator = numeCumparator;
        this.nrLocuri = nrLocuri;
    }
}
