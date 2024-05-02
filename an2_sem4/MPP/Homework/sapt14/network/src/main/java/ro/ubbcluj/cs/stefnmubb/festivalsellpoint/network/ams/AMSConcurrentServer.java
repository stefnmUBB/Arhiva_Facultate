package ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.ams;

import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.server.ConcurrentServer;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.service.IAppService;

import java.net.Socket;

public class AMSConcurrentServer extends ConcurrentServer {
    private IAppService server;
    public AMSConcurrentServer(int port, IAppService chatServer) {
        super(port);
        this.server = chatServer;
        System.out.println("AMSConcurrentServer port "+port);
    }

    @Override
    protected Thread createWorker(Socket client) {
        var worker=new AMSRpcReflectionWorker(server, client);
        return new Thread(worker);
    }
}