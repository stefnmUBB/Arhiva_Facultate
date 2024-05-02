package ro.ubbcluj.cs.stefnmubb.festivalsellpoint;

import javafx.stage.Stage;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.controller.Controller;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.controller.Utils;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.client.ServiceObjectProxy;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.repo.AngajatDbRepo;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.repo.BiletDbRepo;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.repo.SpectacolDbRepo;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.service.*;

import java.io.FileReader;
import java.io.IOException;
import java.time.LocalDateTime;
import java.util.Properties;
import java.util.Random;

public class Application extends javafx.application.Application {
    @Override
    public void start(Stage stage) {
        var props= loadProperties();
        var ip = props.getProperty("ip");
        var port = Integer.parseInt(props.getProperty("port"));
        var server=new ServiceObjectProxy(ip, port);

        Utils.createLoginWindow(server).show();
        //server.closeConnection();
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

    public static void main(String[] args){
        Application.launch();
    }
}
