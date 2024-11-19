package pizzashop.repository;

import pizzashop.model.Payment;
import pizzashop.model.PaymentType;

import java.io.*;
import java.util.ArrayList;
import java.util.List;
import java.util.StringTokenizer;

public class PaymentRepository implements IPaymentRepository {
    private String filename;
    private List<Payment> paymentList;

    public PaymentRepository(String filename) throws IOException {
        this.filename = filename;
        this.paymentList = new ArrayList<>();
        readPayments();
    }

    private void readPayments() throws IOException {
        File file = new File(filename);
        try(BufferedReader br = new BufferedReader(new FileReader(file))) {
            String line = null;
            while((line=br.readLine())!=null){
                Payment payment=getPayment(line);
                paymentList.add(payment);
            }
        }
    }

    private Payment getPayment(String line){
        try{
            Payment item=null;
            if (line==null|| line.equals("")) return null;
            StringTokenizer st=new StringTokenizer(line, ",");
            int tableNumber= Integer.parseInt(st.nextToken());
            String type= st.nextToken();
            double amount = Double.parseDouble(st.nextToken());
            item = new Payment(tableNumber, PaymentType.valueOf(type), amount);
            return item;
        }
        catch (Exception e){
            throw new IllegalArgumentException(line+" => "+e.getMessage());
        }
    }

    public void add(Payment payment){
        paymentList.add(payment);
        writeAll();
    }

    public List<Payment> getAll(){
        return paymentList;
    }

    public void writeAll(){
        File file = new File(filename);
        try(BufferedWriter bw = new BufferedWriter(new FileWriter(file))) {
            for (Payment p:paymentList) {
                System.out.println(p.toString());
                bw.write(p.toString());
                bw.newLine();
            }
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
}