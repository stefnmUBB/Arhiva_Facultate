package ro.ubbcluj.cs.stefnmubb.festivalsellpoint;

import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.JsonMappingException;
import com.fasterxml.jackson.databind.ObjectMapper;
import org.springframework.jms.core.JmsOperations;
import org.springframework.jms.core.JmsTemplate;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.ams.INotificationReceiver;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.ams.INotificationSubscriber;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.ams.Notification;

import javax.jms.JMSException;
import javax.jms.TextMessage;
import java.util.HashMap;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;
import java.util.concurrent.TimeUnit;

public class NotificationReceiverImpl implements INotificationReceiver {
    private final JmsOperations jmsOperations;
    private boolean running;
    public NotificationReceiverImpl(JmsOperations operations) {
        jmsOperations=operations;
    }
    ExecutorService service;
    INotificationSubscriber subscriber;
    @Override
    public void start(INotificationSubscriber subscriber) {
        System.out.println("Starting notification receiver ...");
        running=true;

        this.subscriber=subscriber;
        service = Executors.newSingleThreadExecutor();
        service.submit(this::run);
    }

    private void run(){
        while(running){
            //System.out.println("Notif running");
            var x=jmsOperations.receive();
            System.out.println(x);
            try {
                ObjectMapper objectMapper = new ObjectMapper().findAndRegisterModules();
                var text=((TextMessage)x).getText();
                System.out.println(text);
                var n=objectMapper.readValue(text, HashMap.class);
                System.out.println(n);

                var not=new Notification((int)n.get("spectacolId"),(int)n.get("nrLocuriDisponibile"),
                        (int)n.get("nrLocuriVandute"));
                System.out.println(not);
                System.out.println("Received Notification... "+not);
                subscriber.notificationReceived(not);

                //System.out.println(((TextMessage)x).getText());
            } catch (JMSException | JsonProcessingException e) {
                throw new RuntimeException(e);
            }


            // Notification notif=(Notification)jmsOperations.receiveAndConvert();
            // ^^^^^^^^^^^^^ freezes!!!
        }
    }


    @Override
    public void stop() {
        running=false;
        try {
            service.awaitTermination(100, TimeUnit.MILLISECONDS);
            service.shutdown();
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
        System.out.println("Stopped notification receiver");
    }
}

