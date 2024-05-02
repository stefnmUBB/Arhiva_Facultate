package ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.objectprotocol;

import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.dto.AngajatDTO;

public class LoginAngajatResponse implements IResponse {
    private AngajatDTO angajat;

    public AngajatDTO getAngajat() {
        return angajat;
    }

    public LoginAngajatResponse(){}

    public LoginAngajatResponse(AngajatDTO angajat) {
        this.angajat = angajat;
    }


    @Override
    public String toString() {
        return "LoginAngajatResponse{" +
                "angajat=" + angajat +
                '}';
    }
}
