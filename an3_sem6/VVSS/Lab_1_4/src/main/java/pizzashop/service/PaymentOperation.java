package pizzashop.service;

public interface PaymentOperation {
     void cardPayment();
     void cashPayment();
     void cancelPayment();
}