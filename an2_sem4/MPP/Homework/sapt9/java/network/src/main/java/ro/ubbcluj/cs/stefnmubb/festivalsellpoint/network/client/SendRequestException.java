package ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.client;

import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.service.ServiceException;

public class SendRequestException extends ServiceException {
    public SendRequestException(String s) {
        super(s);
    }
}
