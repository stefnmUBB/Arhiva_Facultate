package Lab1_P4;
// 4. Implement a simple inheritance using Square
// ( length, width, area() ) and Cube classes and
// then calculate the area of a cube.

public class Main {
    public static void main(String[] args)
    {
        Cuboid c=new Cuboid();
        c.set(10,20,30);
        System.out.println("Area   = " + c.area());
        System.out.println("Volume = " + c.volume());
    }
}
