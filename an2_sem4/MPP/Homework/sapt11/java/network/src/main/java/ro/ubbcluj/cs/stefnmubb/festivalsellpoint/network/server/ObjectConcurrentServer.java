package ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.server;

import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.client.ClientObjectWorker;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.service.IAppService;

import java.net.Socket;

public class ObjectConcurrentServer extends ConcurrentServer {
    private final IAppService server;
    public ObjectConcurrentServer(int port, IAppService service) {
        super(port);
        this.server=service;
    }

    @Override
    protected Thread createWorker(Socket client) {
        ClientObjectWorker worker=new ClientObjectWorker(server, client);
        return new Thread(worker);
    }
}
