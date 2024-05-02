package ro.ubbcluj.cs.stefnmubb.festivalsellpoint.rest_server.start;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.context.annotation.ComponentScan;
import org.springframework.context.annotation.Configuration;

import java.io.File;

@ComponentScan(basePackages = "ro.ubbcluj.cs.stefnmubb.festivalsellpoint.rest_server")
@Configuration
@SpringBootApplication
public class StartRestServices {
    public static void main(String[] args) {
        SpringApplication.run(StartRestServices.class, args);
    }
}

