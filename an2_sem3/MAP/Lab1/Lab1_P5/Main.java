package Lab1_P5;
// 5. Create an Employee class (Name, Age, Salary, Position).
// Implement methods to set the values. Create 2 employees and display the objects

public class Main {
    public static void main(String[] args)
    {
        Employee e1=new Employee();
        e1.setAge(25);
        e1.setName("Lin");
        e1.setSalary(5000);
        e1.setPosition("Manager");

        Employee e2 = new Employee();
        e2.setAge(40);
        e2.setName("Red");
        e2.setSalary(4121);
        e2.setPosition("Accountant");

        System.out.println(e1);
        System.out.println(e2);
    }
}
