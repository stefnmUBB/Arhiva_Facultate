import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.domain.Spectacol;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.rest_server.rest.client.SpectacolClient;

import java.time.LocalDateTime;
import java.util.Arrays;

public class TestClientSpectacol {
    public static void main(String[] args){
        var client=new SpectacolClient();

        System.out.println("\nGet By Id");
        var s1=client.getById(2);
        System.out.println(s1);

        System.out.println("\nCreate**");
        var s=client.create(new Spectacol("Magician13"
                , LocalDateTime.now(),"Cluj",5,6));
        System.out.println(s);

        System.out.println("\nGet ALL");
        Arrays.stream(client.getAll()).toList().forEach(System.out::println);

        System.out.println("\nDelete");
        System.out.println("Deleting with id="+s.getId());
        client.delete(s.getId());

        System.out.println("\nUpdate");
        s1.setNrLocuriDisponibile(15);
        client.update(s1);
        System.out.println(client.getById(1));
        s1.setNrLocuriDisponibile(17);
        client.update(s1);
        System.out.println(client.getById(1));


    }
}
