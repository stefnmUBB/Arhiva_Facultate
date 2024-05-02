package ro.ubbcluj.cs.stefnmubb.festivalsellpoint.rest_server.services.rest;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.domain.Spectacol;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.repo.EntityRepoException;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.repo.SpectacolDbRepo;

import java.io.FileReader;
import java.io.IOException;
import java.util.Properties;
import java.util.stream.StreamSupport;

@CrossOrigin
@RestController
@RequestMapping("/festival/spectacole")
public class SpectacolController {
    private final SpectacolDbRepo spectacolDbRepo = new SpectacolDbRepo(loadProperties());

    @RequestMapping(method= RequestMethod.GET)
    public Spectacol[] getAll() throws EntityRepoException {
        System.out.println("Get all spectacole ...");
        return StreamSupport.stream(spectacolDbRepo.getAll().spliterator(),false)
                .toArray(Spectacol[]::new);
    }

    @ExceptionHandler(EntityRepoException.class)
    @ResponseStatus(HttpStatus.BAD_REQUEST)
    public String userError(EntityRepoException e) {
        return e.getMessage();
    }

    @RequestMapping(value = "/{id}", method = RequestMethod.GET)
    public ResponseEntity<?> getById(@PathVariable Integer id) throws EntityRepoException {
        System.out.println("Get by id "+id);
        var spectacol=spectacolDbRepo.getById(id);
        if (spectacol==null)
            return new ResponseEntity<String>("User not found",HttpStatus.NOT_FOUND);
        else
            return new ResponseEntity<Spectacol>(spectacol, HttpStatus.OK);
    }

    @RequestMapping(method = RequestMethod.POST)
    public Spectacol create(@RequestBody Spectacol spectacol) throws EntityRepoException {
        spectacolDbRepo.add(spectacol);
        return spectacol;
    }

    @RequestMapping(value = "/{id}", method = RequestMethod.PUT)
    public Spectacol update(@RequestBody Spectacol s) throws EntityRepoException {
        System.out.println("Updating user ...");
        spectacolDbRepo.update(s);
        return s;
    }

    @RequestMapping(value="/{id}", method= RequestMethod.DELETE)
    public ResponseEntity<?> delete(@PathVariable Integer id) {
        System.out.println("Deleting spectacol ... "+id);
        try {
            spectacolDbRepo.remove(id);
            return new ResponseEntity<Spectacol>(HttpStatus.OK);
        }catch (EntityRepoException ex){
            System.out.println("Ctrl Delete spectacol exception");
            System.out.println(ex.getMessage());
            return new ResponseEntity<>(ex.getMessage(),HttpStatus.BAD_REQUEST);
        }
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
