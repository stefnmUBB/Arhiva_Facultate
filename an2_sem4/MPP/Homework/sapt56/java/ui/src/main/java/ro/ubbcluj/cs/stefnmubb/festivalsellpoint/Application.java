package ro.ubbcluj.cs.stefnmubb.festivalsellpoint;

import javafx.fxml.FXMLLoader;
import javafx.scene.Scene;
import javafx.stage.Stage;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.controller.Controller;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.controller.LoginViewController;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.controller.Utils;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.domain.Spectacol;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.repo.AngajatDbRepo;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.repo.BiletDbRepo;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.repo.EntityRepoException;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.repo.SpectacolDbRepo;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.service.AngajatService;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.service.AppService;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.service.BiletService;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.service.SpectacolService;

import java.io.FileReader;
import java.io.IOException;
import java.time.LocalDateTime;
import java.util.Properties;
import java.util.Random;

public class Application extends javafx.application.Application {
    @Override
    public void start(Stage stage) {
        var props = loadProperties();
        var angajatRepo = new AngajatDbRepo(props);
        var spectacolRepo =new SpectacolDbRepo(props);
        var biletRepo =new BiletDbRepo(props, spectacolRepo);

        var angajatService = new AngajatService(angajatRepo);
        var spectacolService = new SpectacolService(spectacolRepo);
        var biletService = new BiletService(biletRepo);

        var appService = new AppService(angajatService, biletService, spectacolService);

        stage = Utils.createLoginWindow(appService);
        stage.show();
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
