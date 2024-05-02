package ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.ams;

public interface INotificationReceiver {
    void start(INotificationSubscriber subscriber);
    void stop();
}
