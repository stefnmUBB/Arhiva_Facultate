package ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.objectprotocol;

public class LoginAngajatRequest implements IRequest {
    private String username;
    private String password;

    public LoginAngajatRequest() {
    }

    public String getPassword() {
        return password;
    }

    public String getUsername() {
        return username;
    }

    public LoginAngajatRequest(String username, String password) {
        this.username = username;
        this.password = password;
    }
}
