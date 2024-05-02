package Lab1_P4;

public class Rectangle {
    public int width;
    public int height;

    public void set(int x, int y)
    {
        width=x;
        height=y;
    }

    public int area() {
        return width*height;
    }
}
