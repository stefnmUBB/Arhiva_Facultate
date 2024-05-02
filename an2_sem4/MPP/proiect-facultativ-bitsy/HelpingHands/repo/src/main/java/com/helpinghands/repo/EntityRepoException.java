package com.helpinghands.repo;

public class EntityRepoException extends Exception {
    public EntityRepoException() {
        super();
    }
    public EntityRepoException(String message) {
        super(message);
    }

    public EntityRepoException(Exception e) {
        super(e);
    }
}
