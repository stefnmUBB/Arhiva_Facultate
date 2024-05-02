package ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.dto;

import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.domain.Angajat;

public class AngajatDTO extends EntityDTO {
    private String username;
    private String password;

    public AngajatDTO() {}

    public AngajatDTO(String username, String password) {
        this.username = username;
        this.password = password;
    }

    public String getUsername() {
        return username;
    }

    public String getPassword() {
        return password;
    }

    public static AngajatDTO fromAngajat(Angajat angajat){
        var a = new AngajatDTO(angajat.getUsername(), angajat.getPassword());
        a.setId(angajat.getId());
        return a;
    }

    public Angajat toAngajat(){
        var a = new Angajat(getUsername(), getPassword(), "dummy@notusedemail.com");
        a.setId(getId());
        return a;
    }

    @Override
    public String toString() {
        return "AngajatDTO{" +
                "username='" + username + '\'' +
                ", password='" + password + '\'' +
                '}';
    }
}
