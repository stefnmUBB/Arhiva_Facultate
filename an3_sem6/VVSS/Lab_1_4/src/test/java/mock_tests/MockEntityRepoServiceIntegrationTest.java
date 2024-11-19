package mock_tests;

import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.mockito.Mock;
import org.mockito.Mockito;
import org.mockito.MockitoAnnotations;
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

public class MockEntityRepoServiceIntegrationTest {
    static String filename = "test_payments_integration.txt";
    private PizzaService pizzaService;
    @Mock
    private Payment p1;
    @Mock
    private Payment p2;

    @BeforeEach
    void setUp() {
        MockitoAnnotations.initMocks(this);

        // Configure mock objects
        Mockito.when(p1.getTableNumber()).thenReturn(1);
        Mockito.when(p1.getType()).thenReturn(PaymentType.CARD);
        Mockito.when(p1.getAmount()).thenReturn(12.0);
        Mockito.when(p1.toString()).thenReturn("1,CARD,12.0");

        Mockito.when(p2.getTableNumber()).thenReturn(2);
        Mockito.when(p2.getType()).thenReturn(PaymentType.CASH);
        Mockito.when(p2.getAmount()).thenReturn(22.4);
        Mockito.when(p2.toString()).thenReturn("2,CASH,22.4");

        // equals() method cannot be mocked (it is used by Mokito behind the scenes)

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
        List<Payment> expectedPayments = ListUtils.of(p1, p2);
        List<Payment> actualPayments = pizzaService.getPayments();

        Assertions.assertEquals(expectedPayments.size(), actualPayments.size());
        for (int i = 0; i < expectedPayments.size(); i++) {
            Assertions.assertTrue(comparePaymentWithMock(expectedPayments.get(i), actualPayments.get(i)));
        }
    }

    @Test
    void test_add() {
        Payment payment = Mockito.mock(Payment.class);
        Mockito.when(payment.getTableNumber()).thenReturn(1);
        Mockito.when(payment.getType()).thenReturn(PaymentType.CARD);
        Mockito.when(payment.getAmount()).thenReturn(10.0);

        pizzaService.addPayment(payment.getTableNumber(), payment.getType(), payment.getAmount());
        List<Payment> expectedPayments = ListUtils.of(p1, p2, payment);
        List<Payment> actualPayments = pizzaService.getPayments();

        Assertions.assertEquals(expectedPayments.size(), actualPayments.size());
        for (int i = 0; i < expectedPayments.size(); i++) {
            Assertions.assertTrue(comparePaymentWithMock(expectedPayments.get(i), actualPayments.get(i)));
        }
    }

    private boolean comparePaymentWithMock(Payment expected, Payment actual) {
        return expected.getTableNumber() == actual.getTableNumber() && expected.getType() == actual.getType() && Double.compare(expected.getAmount(), actual.getAmount()) == 0;
    }
}
