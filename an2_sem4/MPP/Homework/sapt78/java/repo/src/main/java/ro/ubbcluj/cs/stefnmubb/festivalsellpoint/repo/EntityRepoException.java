package ro.ubbcluj.cs.stefnmubb.festivalsellpoint.repo;

public class EntityRepoException extends Exception {
    public EntityRepoException(String msg) {
        super(msg);
    }

    public EntityRepoException(Exception e) {
        super("Inner exception thrown:\n"+e.getMessage());
    }
}
