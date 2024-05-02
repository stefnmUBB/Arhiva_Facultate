import model.ComputerRepairRequest;
import org.junit.jupiter.api.DisplayName;
import org.junit.jupiter.api.Test;
import repository.RequestRepository;

import java.time.LocalDate;
import java.time.LocalDateTime;

import static org.junit.jupiter.api.Assertions.assertEquals;


public class ComputerRepairRequestTest {
    
    @Test
    @DisplayName("Test1 - ComputerRepairRequest create")
    public void Test1() {
        ComputerRepairRequest crr = new ComputerRepairRequest();
        assertEquals("", crr.getOwnerName());
        assertEquals("", crr.getOwnerAddress());
    }

    @Test
    @DisplayName("Test2 - ComputerRepairRequest get/set")
    public void Test2() {
        ComputerRepairRequest crr = new ComputerRepairRequest();
        crr.setOwnerName("Stefan");
        crr.setOwnerAddress("Strada Ramurelei, 15");
        crr.setDate("08/03/2023");
        crr.setModel("Thinkpad");
        crr.setPhoneNumber("0700123456");
        crr.setID(1);

        assertEquals("Stefan", crr.getOwnerName());
        assertEquals("Strada Ramurelei, 15", crr.getOwnerAddress());
        assertEquals("08/03/2023", crr.getDate());
        assertEquals("Thinkpad", crr.getModel());
        assertEquals("0700123456", crr.getPhoneNumber());
        assertEquals(1, crr.getID());
    }

    @Test
    @DisplayName("Test3 - repo add")
    public void Test3(){
        RequestRepository repo = new RequestRepository();
        assertEquals(0, repo.getAll().size());

        var req1 = new ComputerRepairRequest();
        req1.setOwnerName("Stefan");
        req1.setOwnerAddress("Strada Ramurelei, 15");
        req1.setDate("08/03/2023");
        req1.setModel("Thinkpad");
        req1.setPhoneNumber("0700123456");
        req1.setID(1);
        repo.add(req1);
        assertEquals(1, repo.getAll().size());
        assertEqualsReq(req1, repo.findById(1));

        var req2 = new ComputerRepairRequest();
        req2.setOwnerName("Stefan");
        req2.setOwnerAddress("Strada Ramurelei, 15");
        req2.setDate("08/03/2023");
        req2.setModel("Thinkpad");
        req2.setPhoneNumber("0700123456");
        req2.setID(2);
        repo.add(req2);
        assertEquals(2, repo.getAll().size());
        assertEqualsReq(req2, repo.findById(2));

        try {
            repo.add(req1);
            assert(false);
        }
        catch (RuntimeException e){
            assert(true);
        }
    }

    private void assertEqualsReq(ComputerRepairRequest c1, ComputerRepairRequest c2){
        assertEquals(c1.getOwnerName(), c2.getOwnerName());
        assertEquals(c1.getOwnerAddress(), c2.getOwnerAddress());
        assertEquals(c1.getDate(), c2.getDate());
        assertEquals(c1.getModel(), c2.getModel());
        assertEquals(c1.getPhoneNumber(), c2.getPhoneNumber());
        assertEquals(c1.getID(), c2.getID());
    }
}
