package ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.client;

import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.service.ServiceException;

public class ReceiveResponseException extends ServiceException {
    public ReceiveResponseException(String msg) {
        super(msg);
    }
}
