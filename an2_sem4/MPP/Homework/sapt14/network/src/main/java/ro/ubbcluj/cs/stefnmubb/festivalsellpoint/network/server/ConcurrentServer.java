package ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.server;

import java.net.Socket;

public abstract class ConcurrentServer extends AbstractServer {
    public ConcurrentServer(int port) {
        super(port);
    }

    @Override
    protected void processRequest(Socket client) {
        Thread tw=createWorker(client);
        tw.start();
    }

    protected abstract Thread createWorker(Socket client);
}
