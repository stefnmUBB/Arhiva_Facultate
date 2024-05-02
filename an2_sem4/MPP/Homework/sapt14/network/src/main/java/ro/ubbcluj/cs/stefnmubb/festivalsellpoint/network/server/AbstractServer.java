package ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.server;

import java.io.IOException;
import java.net.*;

public abstract class AbstractServer {
    private final int port;
    private ServerSocket server=null;
    public AbstractServer(int port){
        this.port=port;
    }

    public void start() throws ServerException {
        try{
            server=new ServerSocket(port);
            while(true){
                System.out.println("Waiting for clients ...");
                Socket client=server.accept();
                System.out.println("Client connected ...");
                processRequest(client);
            }
        } catch (IOException e) {
            throw new ServerException("Starting server errror ",e);
        }finally {
            if(server!=null)
                stop();
        }
    }

    protected abstract void processRequest(Socket client);
    public void stop() throws ServerException {
        try {
            server.close();
        } catch (IOException e) {
            throw new ServerException("Closing server error ", e);
        }
    }
}
