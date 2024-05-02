package ro.ubbcluj.cs.stefnmubb.festivalsellpoint;

import org.springframework.context.support.ClassPathXmlApplicationContext;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.ams.AMSRpcProxy;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.service.ServiceException;

import java.io.File;

public class Main {
    public static void main(String[] args) throws ServiceException {
        ClassPathXmlApplicationContext context = new ClassPathXmlApplicationContext("spring-client_console.xml");
        AMSRpcProxy server =context.getBean("server",AMSRpcProxy.class);

        var a=server.loginAngajat("stefan","1234", null);

        server.getAllSpectacole().forEach(System.out::println);

        System.out.println(a);
    }
}
