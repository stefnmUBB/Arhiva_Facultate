package pizzashop.service;

import org.junit.jupiter.api.*;
import pizzashop.model.Payment;
import pizzashop.model.PaymentType;
import pizzashop.repository.MenuRepository;
import pizzashop.repository.PaymentRepository;
import pizzashop.utils.ListUtils;

import java.io.*;
import java.util.List;
import java.util.Objects;

class PizzaServiceTest_BBT {
    private int table;
    private PaymentType type;
    private double amount;
    PizzaService srv;

    static String filename = "test_payments.txt";

    @BeforeEach
    void setUp() {
        List<Payment> payments = ListUtils.of(new Payment(1,PaymentType.CARD,12),new Payment(2,PaymentType.CASH,22.4));

        File file = new File(filename);
        BufferedWriter bw = null;
        try {
            bw = new BufferedWriter(new FileWriter(file));
            for (Payment p:payments) {
                System.out.println(p.toString());
                bw.write(p.toString());
                bw.newLine();
            }
            bw.close();
        } catch (IOException e) {
            e.printStackTrace();
        }
        try {
            MenuRepository repoMenu = new MenuRepository("data/menu.txt");
            PaymentRepository payRepo = new PaymentRepository("test_payments.txt");
            srv = new PizzaService(repoMenu, payRepo);
        } catch (Exception ex) {
            ex.printStackTrace();
        }
    }

    @AfterEach
    void tearDown() {
        // GARBAGE COLLECTOR
    }

    @Test
    void addPayment_TC1_ECP() {
        table = 1;
        type = PaymentType.CASH;
        amount = 20.5;
        srv.addPayment(table,type,amount);
        checkAdd(table,type,amount);
    }

    @Test
    void addPayment_TC2_ECP() {
        table = 0;
        type = PaymentType.CARD;
        amount = -1;
        try {
            srv.addPayment(table, type, amount);
            Assertions.fail();
            //assert false;
        } catch (Exception ex) {
            Assertions.assertTrue(true);
            //assert true;
        }
    }

    @Test
    @Tag("ecp") // gives a tag to filter by it
    void addPayment_TC5_ECP() {
        table = 7;
        type = PaymentType.CASH;
        amount = 0.1;
        srv.addPayment(table,type,amount);
        checkAdd(table,type,amount);
    }

    @Test
    @DisplayName("TC6_ECP") // display the given name
    void addPayment_TC6_ECP() {
        table = 10;
        type = PaymentType.CARD;
        amount = 50;
        try {
            srv.addPayment(table, type, amount);
            assert false;
        } catch (Exception ex) {
            assert true;
        }
    }

    @Test
    @Timeout(10) // must be executed within 10s
    void addPayment_TC1_BVA() {
        table = 0;
        type = PaymentType.CASH;
        amount = 0;
        try {
            srv.addPayment(table, type, amount);
            assert false;
        } catch (Exception ex) {
            assert true;
        }
    }

    @Test
    @Order(1) // will be executed first
    void addPayment_TC2_BVA() {
        table = 9;
        type = PaymentType.CARD;
        amount = 1;
        try {
            srv.addPayment(table, type, amount);
            assert false;
        } catch (Exception ex) {
            assert true;
        }
    }

    @Test
    void addPayment_TC3_BVA() {
        table = 1;
        type = PaymentType.CARD;
        amount = Double.MAX_VALUE - 1;
        srv.addPayment(table,type,amount);
        checkAdd(table,type,amount);
    }

    @Test
    void addPayment_TC4_BVA() {
        table = 2;
        type = PaymentType.CARD;
        amount = Double.MAX_VALUE;
        srv.addPayment(table,type,amount);
        checkAdd(table,type,amount);
    }

    @Test
    @Disabled // this test will not run
    void addPayment_Disabled() {
        table = 2;
        type = PaymentType.CARD;
        amount = Double.MAX_VALUE;
        srv.addPayment(table,type,amount);
        checkAdd(table,type,amount);
    }
    void checkAdd(int table, PaymentType type, double amount) {
        File file = new File(filename);
        String lastLine = null;
        try(BufferedReader br = new BufferedReader(new FileReader(file))) {
            String line = null;
            while((line=br.readLine())!=null) {
                lastLine = line;
            }
        } catch (IOException e) {
            assert false;
        }
        assert Objects.equals(lastLine, new Payment(table, type, amount).toString());
    }
}