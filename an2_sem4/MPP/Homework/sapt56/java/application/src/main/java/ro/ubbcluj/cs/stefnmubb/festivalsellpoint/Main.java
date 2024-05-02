package ro.ubbcluj.cs.stefnmubb.festivalsellpoint;

import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.domain.Angajat;

import java.io.FileReader;
import java.io.IOException;

import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.domain.Bilet;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.domain.Entity;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.domain.Spectacol;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.repo.*;

import java.time.LocalDateTime;
import java.util.Properties;

public class Main {
    public static void main(String[] args) {
        var props = loadProperties();
        var angajatRepo = new AngajatDbRepo(props);
        var spectacolRepo =new SpectacolDbRepo(props);
        var biletRepo =new BiletDbRepo(props, spectacolRepo);
        try {
            biletRepo.add(new Bilet("Andrei", 15, spectacolRepo.getById(1)));
            show(angajatRepo);
            show(spectacolRepo);
            show(biletRepo);
        }
        catch (EntityRepoException e) {
            throw new RuntimeException(e);
        }
    }

    public static<ID, E extends Entity<ID>> void show(IRepo<ID,E> repo) throws EntityRepoException {
        repo.getAll().forEach(System.out::println);
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
}