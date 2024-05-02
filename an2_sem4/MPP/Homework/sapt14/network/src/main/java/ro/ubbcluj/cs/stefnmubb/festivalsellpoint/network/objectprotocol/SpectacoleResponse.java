package ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.objectprotocol;

import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.domain.Spectacol;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.dto.DTOFactory;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.dto.SpectacolDTO;

import java.util.Arrays;
import java.util.List;

public class SpectacoleResponse implements IResponse {
    private final SpectacolDTO[] spectacole;

    protected SpectacoleResponse(Spectacol[] spectacole){
        this.spectacole = DTOFactory.getDTO(Arrays.asList(spectacole));
    }

    public List<SpectacolDTO> getSpectacole() {
        return Arrays.asList(spectacole);
    }
}
