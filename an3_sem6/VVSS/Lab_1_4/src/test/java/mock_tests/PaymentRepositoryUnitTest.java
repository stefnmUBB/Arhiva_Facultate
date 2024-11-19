package mock_tests;

import org.junit.jupiter.api.AfterEach;
import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.mockito.Mock;
import org.mockito.Mockito;
import org.mockito.MockitoAnnotations;
import pizzashop.model.Payment;
import pizzashop.model.PaymentType;
import pizzashop.repository.PaymentRepository;

import java.io.BufferedWriter;
import java.io.File;
import java.io.FileWriter;
import java.io.IOException;
import java.util.List;

public class PaymentRepositoryUnitTest {
    static String filename = "test_payments.txt";
    private PaymentRepository paymentRepository;

    @Mock
    private Payment p1;
    @Mock
    private Payment p2;

    @BeforeEach
    void setUp() throws IOException {
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
        paymentRepository = new PaymentRepository(filename);
    }

    @AfterEach
    void tearDown() {
        File file = new File(filename);
        try (BufferedWriter bw = new BufferedWriter(new FileWriter(file))) {
            bw.write("1,CARD,12.0");
            bw.newLine();
            bw.write("2,CASH,22.4");
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    @Test
    void test_getAll() {
        List<Payment> payments = paymentRepository.getAll();
        Assertions.assertEquals(2, payments.size());
        // Assertions.assertEquals(new Payment(1, PaymentType.CARD, 12.0), payments.get(0));
        // Assertions.assertEquals(p1, payments.get(0));
        Assertions.assertEquals(p1.getTableNumber(), payments.get(0).getTableNumber());
        Assertions.assertEquals(p1.getType(), payments.get(0).getType());
        Assertions.assertEquals(p1.getAmount(), payments.get(0).getAmount());

        // Assertions.assertEquals(new Payment(2, PaymentType.CASH, 22.4), payments.get(1));
        // Assertions.assertEquals(p2, payments.get(1));
        Assertions.assertEquals(p2.getTableNumber(), payments.get(1).getTableNumber());
        Assertions.assertEquals(p2.getType(), payments.get(1).getType());
        Assertions.assertEquals(p2.getAmount(), payments.get(1).getAmount());
    }


    @Test
    void test_add() {
        Payment payment = Mockito.mock(Payment.class);
        Mockito.when(payment.getTableNumber()).thenReturn(1);
        Mockito.when(payment.getType()).thenReturn(PaymentType.CARD);
        Mockito.when(payment.getAmount()).thenReturn(10.0);

        // Payment payment = new Payment(1,PaymentType.CARD,10.0);
        paymentRepository.add(payment);
        List<Payment> payments = paymentRepository.getAll();
        Assertions.assertEquals(3, payments.size());
        // Assertions.assertEquals(new Payment(1, PaymentType.CARD, 12.0), payments.get(0));
        Assertions.assertEquals(p1.getTableNumber(), payments.get(0).getTableNumber());
        Assertions.assertEquals(p1.getType(), payments.get(0).getType());
        Assertions.assertEquals(p1.getAmount(), payments.get(0).getAmount());

        // Assertions.assertEquals(new Payment(2, PaymentType.CASH, 22.4), payments.get(1));
        Assertions.assertEquals(p2.getTableNumber(), payments.get(1).getTableNumber());
        Assertions.assertEquals(p2.getType(), payments.get(1).getType());
        Assertions.assertEquals(p2.getAmount(), payments.get(1).getAmount());
        Assertions.assertEquals(payment, payments.get(2));
    }
}
