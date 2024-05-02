import org.junit.jupiter.api.DisplayName;
import org.junit.jupiter.api.Test;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.domain.Angajat;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.domain.Spectacol;

import java.time.LocalDateTime;

import static org.junit.jupiter.api.Assertions.assertEquals;

public class TestEntities {
    @Test
    @DisplayName("Test create Angajat")
    public void TestCreateAngajat(){
        Angajat a = new Angajat("Alex","1234","x@y.com");
        assertEquals("x@y.com",a.getEmail());
        assertEquals("Alex",a.getUsername());
        assertEquals("1234",a.getPassword());
    }

    @Test
    @DisplayName("Test create Spectacol")
    public void TestCreateSpectacol(){
        var data = LocalDateTime.of(2023,3,15, 10,0,0);
        var s = new Spectacol("The Motans", data, "Cluj", 10, 5);
        assertEquals("The Motans", s.getArtist());
        assertEquals(LocalDateTime.of(2023,3,15,10,0,0), s.getData());
        assertEquals("Cluj", s.getLocatie());
        assertEquals(10, s.getNrLocuriDisponibile());
        assertEquals(5, s.getNrLocuriVandute());
    }
}
