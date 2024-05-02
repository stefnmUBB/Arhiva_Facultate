package ro.ubbcluj.cs.stefnmubb.festivalsellpoint.server;

import org.springframework.jms.core.JmsOperations;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.domain.Spectacol;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.ams.Notification;

public class NotificationServiceImpl implements INotificationService {
    private final JmsOperations jmsOperations;

    public NotificationServiceImpl(JmsOperations jmsOperations) {
        this.jmsOperations = jmsOperations;
    }

    @Override
    public void updatedSpectacol(Spectacol spectacol) {
        System.out.println("Spectacol update noitification");
        Notification notif=new Notification(spectacol);
        System.out.println("Converting");
        jmsOperations.convertAndSend(notif);
        System.out.println("Sent message to ActiveMQ... " +notif);
    }
}
