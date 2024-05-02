package ro.ubbcluj.cs.stefnmubb.festivalsellpoint;

import javafx.stage.Stage;
import org.springframework.context.support.ClassPathXmlApplicationContext;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.controller.Controller;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.controller.Utils;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.ams.AMSRpcProxy;
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
    public void start(Stage stage) throws ServiceException, BiletReservationException {
        ClassPathXmlApplicationContext context = new ClassPathXmlApplicationContext("spring-client_console.xml");
        var server =context.getBean("server",AMSRpcProxy.class);
        var receiver =context.getBean("notificationReceiver",NotificationReceiverImpl.class);

        /*server.loginAngajat("stefan","1234",null);
        receiver.start(null);
        var s=new ro.ubbcluj.cs.stefnmubb.festivalsellpoint.domain.Spectacol("artist-3",LocalDateTime.now(),"locatie0",40,0);
        s.setId(20);
        server.reserveBilet(s,"a",1);*/

        Utils.createLoginWindow(server,receiver).show();
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
