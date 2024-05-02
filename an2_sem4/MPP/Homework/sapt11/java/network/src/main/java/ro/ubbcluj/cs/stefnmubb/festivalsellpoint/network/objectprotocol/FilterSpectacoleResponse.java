package ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.objectprotocol;

import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.domain.Spectacol;

public class FilterSpectacoleResponse extends SpectacoleResponse{
    public FilterSpectacoleResponse(){}
    public FilterSpectacoleResponse(Spectacol[] spectacole) {
        super(spectacole);
    }
}
