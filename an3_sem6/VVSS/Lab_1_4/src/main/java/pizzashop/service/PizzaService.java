package pizzashop.service;

import pizzashop.model.Order;
import pizzashop.model.Payment;
import pizzashop.model.PaymentType;
import pizzashop.repository.IPaymentRepository;
import pizzashop.repository.MenuRepository;

import java.util.List;

public class PizzaService {
    private final MenuRepository menuRepo;
    private final IPaymentRepository payRepo;

    public PizzaService(MenuRepository menuRepo, IPaymentRepository payRepo) {
        this.menuRepo = menuRepo;
        this.payRepo = payRepo;
    }

    public static double getTotalAmountStatic(List<Payment> l, PaymentType type) {
        double total = 0.0f;
        // spargem if-ul in doua ca sa facem CC=5
        if (l == null)
            throw new IllegalArgumentException();
        if (l.isEmpty()) return total;
        for (Payment p : l) {
            if (p.getType().equals(type))
                total += p.getAmount();
        }
        return total;
    }

    public List<Order> getMenuData() {
        return menuRepo.getMenu();
    }

    public List<Payment> getPayments() {
        return payRepo.getAll();
    }

    public void addPayment(int table, PaymentType type, double amount) {
        if (table < 1 || table > 8)
            throw new IllegalArgumentException();
        if (amount <= 0)
            throw new IllegalArgumentException();
        Payment payment = new Payment(table, type, amount);
        payRepo.add(payment);
    }

    public double getTotalAmount(PaymentType type) {
        return getTotalAmountStatic(this.getPayments(), type);
    }

}