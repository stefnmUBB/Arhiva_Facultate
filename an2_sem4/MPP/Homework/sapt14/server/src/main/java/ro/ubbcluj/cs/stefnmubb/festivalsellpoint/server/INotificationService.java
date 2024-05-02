package ro.ubbcluj.cs.stefnmubb.festivalsellpoint.server;

import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.domain.Spectacol;

public interface INotificationService {
    void updatedSpectacol(Spectacol spectacol);
}
