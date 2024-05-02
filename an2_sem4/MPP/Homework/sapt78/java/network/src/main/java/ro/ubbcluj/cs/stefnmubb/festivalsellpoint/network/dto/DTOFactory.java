package ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.dto;

import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.domain.Angajat;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.domain.Spectacol;

import java.util.stream.StreamSupport;

public class DTOFactory {
    public static AngajatDTO getDTO(Angajat angajat) {
        return AngajatDTO.fromAngajat(angajat);
    }
    public static SpectacolDTO getDTO(Spectacol spectacol){
        return SpectacolDTO.fromSpectacol(spectacol);
    }
    public static SpectacolDTO[] getDTO(Iterable<Spectacol> spectacole) {
        return StreamSupport.stream(spectacole.spliterator(), false)
                .map(DTOFactory::getDTO)
                .toArray(SpectacolDTO[]::new);
    }

    public static Spectacol fromDTO(SpectacolDTO s){
        return s.toSpectacol();
    }

    public static Spectacol[] fromDTO(Iterable<SpectacolDTO> spectacole) {
        return StreamSupport.stream(spectacole.spliterator(), false)
                .map(DTOFactory::fromDTO)
                .toArray(Spectacol[]::new);
    }

    public static Angajat fromDTO(AngajatDTO angajat){
        return angajat.toAngajat();
    }
}
