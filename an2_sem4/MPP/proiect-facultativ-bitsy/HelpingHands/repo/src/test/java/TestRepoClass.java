import com.helpinghands.domain.Admin;
import com.helpinghands.domain.Eveniment;
import com.helpinghands.domain.ProfilePic;
import com.helpinghands.domain.Voluntar;
import com.helpinghands.repo.*;
import junit.framework.TestCase;

import java.io.IOException;
import java.time.LocalDateTime;
import java.util.Set;

public class TestRepoClass extends TestCase {
    static SponsorTypesRepo ppRepo=new SponsorTypesRepo();
    static CerereSponsorRepo ccRepo=new CerereSponsorRepo();
    public static void main(String[] args) throws IOException {
        System.out.println(ccRepo.getById(3));
        System.out.println(ppRepo.getById(1));


        /*var user = voluntarRepo.findByCredentials("user1","0000");

        eventRepo.add(new Eveniment("ev1","d1", LocalDateTime.now(), LocalDateTime.now()
        ,"ala",user, "PENDING"));*/
    }
}
