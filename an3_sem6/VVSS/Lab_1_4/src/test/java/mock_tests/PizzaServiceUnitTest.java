package mock_tests;

import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.mockito.InjectMocks;
import org.mockito.Mock;
import org.mockito.Mockito;
import org.mockito.MockitoAnnotations;
import pizzashop.model.Payment;
import pizzashop.model.PaymentType;
import pizzashop.repository.IPaymentRepository;
import pizzashop.service.PizzaService;
import pizzashop.utils.ListUtils;

import java.util.Arrays;

import static org.mockito.Mockito.never;
import static org.mockito.Mockito.times;

public class PizzaServiceUnitTest {

    @Mock
    private IPaymentRepository paymentRepository;

    @InjectMocks
    private PizzaService pizzaService;

    @BeforeEach
    public void setUp() {
        MockitoAnnotations.initMocks(this);
    }

    @Test
    public void test_getPayments() {
        Payment p1 = new Payment(5, PaymentType.CARD, 10.5);

        Mockito.when(paymentRepository.getAll()).thenReturn(ListUtils.of(p1));
        Assertions.assertEquals(ListUtils.of(p1), pizzaService.getPayments());
        Mockito.verify(paymentRepository, times(1)).getAll();
    }

    @Test
    public void test_addPayment() {
        Payment p1 = new Payment(5, PaymentType.CARD, 10.5);
        Payment p2 = new Payment(7, PaymentType.CASH, 42.0);
        Mockito.when(paymentRepository.getAll()).thenReturn(Arrays.asList(p1));
        Mockito.doNothing().when(paymentRepository).add(p2);
        pizzaService.addPayment(p2.getTableNumber(), p2.getType(), p2.getAmount());
        //pizzaService.addPayment(p2);
        Mockito.verify(paymentRepository, times(1)).add(p2);
        Mockito.verify(paymentRepository, never()).getAll();

        //assert examples
        assert true;
        Assertions.assertEquals(1, pizzaService.getPayments().size());
        Mockito.verify(paymentRepository, times(1)).getAll();
    }
}
