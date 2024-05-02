package ro.ubbcluj.cs.stefnmubb.festivalsellpoint;

import org.springframework.context.support.ClassPathXmlApplicationContext;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.ams.AMSRpcProxy;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.service.BiletReservationException;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.service.ServiceException;

import java.time.LocalDateTime;

public class Main {
    public static void main(String[] args) throws ServiceException, BiletReservationException {
        ClassPathXmlApplicationContext context = new ClassPathXmlApplicationContext("spring-client_console.xml");
        var server =context.getBean("server", AMSRpcProxy.class);
        var receiver =context.getBean("notificationReceiver",NotificationReceiverImpl.class);

        //server.loginAngajat("stefan","1234",null);
        receiver.start(null);
        var s=new ro.ubbcluj.cs.stefnmubb.festivalsellpoint.domain.Spectacol("artist-3", LocalDateTime.now(),"locatie0",40,0);
        s.setId(20);
        //server.reserveBilet(s,"a",1);
    }
}
