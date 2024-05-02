package ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.objectprotocol;

import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.dto.SpectacolDTO;

public class ReserveBiletRequest implements IRequest {
    SpectacolDTO spectacol;
    String cumparatorName;
    int seats;

    public SpectacolDTO getSpectacol() {
        return spectacol;
    }

    public String getCumparatorName() {
        return cumparatorName;
    }

    public int getSeats() {
        return seats;
    }

    public ReserveBiletRequest(SpectacolDTO spectacol, String cumparatorName, int seats) {
        this.spectacol = spectacol;
        this.cumparatorName = cumparatorName;
        this.seats = seats;
    }
}
