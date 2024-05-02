package ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.dto;

import java.util.Arrays;

public class TestArrayDTO extends EntityDTO {
    private AngajatDTO[] angajati = new AngajatDTO[0];
    public TestArrayDTO(){}

    public TestArrayDTO(AngajatDTO... angajati){
        this.angajati = angajati;
    }

    @Override
    public String toString() {
        return "TestArrayDTO{" +
                "angajati=" + Arrays.toString(angajati) +
                ", id=" + getId() +
                '}';
    }
}
