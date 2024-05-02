package ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.ams;

import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.domain.Spectacol;

public interface IClient {
    void userLoggedIn(String name);
    void userLoggedOut(String name);
    void spectacolUpdated(Spectacol s);
}
