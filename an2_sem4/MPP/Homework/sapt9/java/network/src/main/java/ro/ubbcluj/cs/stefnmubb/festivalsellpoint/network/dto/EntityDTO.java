package ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.dto;

import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.utils.IStringifiable;

import java.io.Serializable;

public class EntityDTO implements Serializable, IStringifiable {
    private int id;

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }
}
