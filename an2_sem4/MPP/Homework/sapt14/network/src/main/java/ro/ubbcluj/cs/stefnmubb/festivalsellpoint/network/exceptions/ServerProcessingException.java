package ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.exceptions;

import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.repo.EntityRepoException;

public class ServerProcessingException extends Throwable {
    public ServerProcessingException(Exception e) {
        super(e);
    }
}
