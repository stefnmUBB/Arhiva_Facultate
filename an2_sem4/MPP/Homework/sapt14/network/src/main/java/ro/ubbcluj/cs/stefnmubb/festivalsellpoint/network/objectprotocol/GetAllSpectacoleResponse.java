package ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.objectprotocol;

import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.domain.Spectacol;


public class GetAllSpectacoleResponse extends SpectacoleResponse {
    public GetAllSpectacoleResponse(Spectacol[] spectacole) {
        super(spectacole);
    }
}
