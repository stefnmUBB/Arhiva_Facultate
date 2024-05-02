package ro.ubbcluj.cs.stefnmubb.festivalsellpoint;

import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.server.ObjectConcurrentServer;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.server.ServerException;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.repo.AngajatDbRepo;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.repo.BiletDbRepo;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.repo.SpectacolDbRepo;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.server.ServiceImplementation;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.service.*;

import java.io.FileReader;
import java.io.IOException;
import java.util.Properties;

public class Main {
    public static void main(String[] args) throws ServerException {
        var props = loadProperties();
        var angajatRepo = new AngajatDbRepo(props);
        var spectacolRepo =new SpectacolDbRepo(props);
        var biletRepo =new BiletDbRepo(props, spectacolRepo);

        var angajatService = new AngajatService(angajatRepo);
        var biletService = new BiletService(biletRepo);
        var spectacolService = new SpectacolService(spectacolRepo);

        var appService = new AppService(angajatService, biletService, spectacolService);
        var server = new ObjectConcurrentServer(15000, new ServiceImplementation(appService));
        server.start();
    }

    public static Properties loadProperties() {
        Properties props=new Properties();
        try {
            props.load(new FileReader("bd.properties"));
        } catch (IOException e) {
            System.out.println("Cannot find bd.config "+e);
        }
        return props;
    }
}