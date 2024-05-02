import com.google.common.collect.ArrayListMultimap;
import com.google.common.collect.Multimap;
import model.ComputerRepairRequest;
import repository.RequestRepository;

public class ComputerShopMain {
    public static void main(String[] args){
        RequestRepository repo = new RequestRepository();
        System.out.println("Adaugam in repo ...");
        repo.add(new ComputerRepairRequest(1,"A A","Address A","072222","Asus","13/10/2020","Broken display"));
        repo.add(new ComputerRepairRequest(2,"B B","Address B","072222","Acer","10/10/2020","Faulty keyboard"));

       // System.out.println("searching element with id 3 "+repo.findById(3));
        System.out.println("Afisam datele din repository ...");
        for(ComputerRepairRequest first: repo.findAll())
            System.out.println(first);

        System.out.println("Exemplu Multimap de la google.guava");
        Multimap<String,String> map= ArrayListMultimap.create();
        map.put("cheia1", "valoarea 1");
        map.put("cheia1", "valoarea 2");
        map.put("cheia2", "valoare noua");
        System.out.println(map);
    }

}
