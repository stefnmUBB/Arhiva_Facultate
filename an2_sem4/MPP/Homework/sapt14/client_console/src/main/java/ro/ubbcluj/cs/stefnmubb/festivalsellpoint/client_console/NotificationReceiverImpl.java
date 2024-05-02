package ro.ubbcluj.cs.stefnmubb.festivalsellpoint.client_console;

import org.springframework.jms.core.JmsOperations;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.ams.INotificationReceiver;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.ams.INotificationSubscriber;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.ams.Notification;

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
            Notification notif=(Notification)jmsOperations.receiveAndConvert();
            System.out.println("Received Notification... "+notif);
            subscriber.notificationReceived(notif);
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

