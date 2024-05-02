package ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.objectprotocol;

import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.dto.BiletDTO;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.dto.SpectacolDTO;

public class ReserveBiletRequest implements IRequest {
    BiletDTO bilet;

    public ReserveBiletRequest(){}


    public BiletDTO getBilet() {
        return this.bilet;
    }

    public ReserveBiletRequest(BiletDTO bilet) {
        this.bilet = bilet;
    }
}
