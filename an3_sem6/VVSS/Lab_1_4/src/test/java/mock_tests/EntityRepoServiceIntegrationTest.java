package mock_tests;

import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import pizzashop.model.Payment;
import pizzashop.model.PaymentType;
import pizzashop.repository.MenuRepository;
import pizzashop.repository.PaymentRepository;
import pizzashop.service.PizzaService;
import pizzashop.utils.ListUtils;

import java.io.BufferedWriter;
import java.io.File;
import java.io.FileWriter;
import java.io.IOException;
import java.util.List;

public class EntityRepoServiceIntegrationTest {
    static String filename = "test_payments_integration.txt";
    private PizzaService pizzaService;
    private Payment p1;
    private Payment p2;

    @BeforeEach
    void setUp() {
        p1 = new Payment(1, PaymentType.CARD, 12);
        p2 = new Payment(2, PaymentType.CASH, 22.4);
        List<Payment> payments = ListUtils.of(p1, p2);

        File file = new File(filename);
        BufferedWriter bw;
        try {
            bw = new BufferedWriter(new FileWriter(file));
            for (Payment p : payments) {
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
            PaymentRepository paymentRepository = new PaymentRepository("test_payments_integration.txt");
            pizzaService = new PizzaService(repoMenu, paymentRepository);
        } catch (Exception ex) {
            ex.printStackTrace();
        }
    }

    @Test
    void test_getAll() {
        Assertions.assertEquals(ListUtils.of(p1, p2), pizzaService.getPayments());
    }

    @Test
    void test_add() {
        Payment payment = new Payment(1, PaymentType.CARD, 10.0);
        pizzaService.addPayment(payment.getTableNumber(), payment.getType(), payment.getAmount());
        Assertions.assertEquals(ListUtils.of(p1, p2, payment), pizzaService.getPayments());
    }
}
