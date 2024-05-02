package chat.services.rest;

/**
 * Created by grigo on 5/11/17.
 */
public class ServiceException extends RuntimeException {
    public ServiceException(Exception e) {
        super(e);
    }

    public ServiceException(String message) {
        super(message);
    }
}
