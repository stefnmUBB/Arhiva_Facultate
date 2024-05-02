package Lab1_P5;

public class Employee {
    private String name;
    private int age;
    private int salary;
    private String position;

    public void setName(String name)
    {
        this.name=name;
    }

    public void setAge(int age)
    {
        this.age=age;
    }

    public void setSalary(int salary)
    {
        this.salary=salary;
    }

    public void setPosition(String position)
    {
        this.position=position;
    }

    public String toString() {
        return name+" "+age+" "+salary+" "+position;
    }
}
