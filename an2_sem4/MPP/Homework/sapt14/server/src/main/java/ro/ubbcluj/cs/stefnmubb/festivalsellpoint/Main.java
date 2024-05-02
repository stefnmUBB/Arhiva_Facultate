package ro.ubbcluj.cs.stefnmubb.festivalsellpoint;

import org.springframework.context.support.ClassPathXmlApplicationContext;
//import org.apache.xbean.spring.context.ClassPathXmlApplicationContext;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.ams.AMSConcurrentServer;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.server.ServerException;

public class Main {
    public static void main(String[] args) throws ServerException {
        ClassPathXmlApplicationContext context = new ClassPathXmlApplicationContext("spring-server.xml");
        var server=context.getBean("server",AMSConcurrentServer.class);
        server.start();
    }
}