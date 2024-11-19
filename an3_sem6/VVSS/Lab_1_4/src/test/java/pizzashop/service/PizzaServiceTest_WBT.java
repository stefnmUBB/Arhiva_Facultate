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

public class PizzaServiceTest_WBT {
    private PaymentType type;
    private List<Payment> l;
    private double total;
    PizzaService srv;

    static String filename = "test_payments.txt";

    @BeforeEach
    void setUp() {
    }

    @AfterEach
    void tearDown() {
        // GARBAGE COLLECTOR
    }

    @Test
    void getTotalAmount_F02_TC01() {
        l = null;
        type = PaymentType.CASH;
        total = 0;
        try
        {
            double amount = PizzaService.getTotalAmountStatic(l, type);
        }
        catch (IllegalArgumentException e){
            Assertions.assertTrue(true);
            return;
        }
        Assertions.fail();
    }

    @Test
    void getTotalAmount_F02_TC02() {
        l = ListUtils.of();
        type = PaymentType.CASH;
        total = 0;
        Assertions.assertEquals(PizzaService.getTotalAmountStatic(l, type), total);
    }

    @Test
    void getTotalAmount_F02_TC03() {
        // ??
        l = ListUtils.of();
        type = PaymentType.CASH;
        total = 0;
        Assertions.assertEquals(PizzaService.getTotalAmountStatic(l, type), total);
    }

    @Test
    void getTotalAmount_F02_TC04() {
        l = ListUtils.of(new Payment(1, PaymentType.CASH, 5));
        type = PaymentType.CARD;
        total = 0;
        Assertions.assertEquals(PizzaService.getTotalAmountStatic(l, type), total);
    }

    @Test
    void getTotalAmount_F02_TC05() {
        l = ListUtils.of(
                new Payment(1, PaymentType.CARD, 2),
                new Payment(1, PaymentType.CARD, 3));
        type = PaymentType.CARD;
        total = 5;
        Assertions.assertEquals(PizzaService.getTotalAmountStatic(l, type), total);
    }


}
