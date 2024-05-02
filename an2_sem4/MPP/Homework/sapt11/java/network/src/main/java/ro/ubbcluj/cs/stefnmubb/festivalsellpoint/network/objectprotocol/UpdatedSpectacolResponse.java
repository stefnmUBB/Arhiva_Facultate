package ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.objectprotocol;

import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.dto.SpectacolDTO;

public class UpdatedSpectacolResponse extends UpdateResponse{
    public UpdatedSpectacolResponse(){}

    SpectacolDTO spectacol;

    public SpectacolDTO getSpectacol() {
        return spectacol;
    }

    public UpdatedSpectacolResponse(SpectacolDTO spectacol) {
        this.spectacol = spectacol;
    }
}
