package Lab1_P4;

public class Cuboid extends Rectangle {
    public int depth;

    public void set(int x, int y, int z)
    {
        this.set(x,y);
        depth=z;
    }

    public int volume()
    {
        return area()*depth;
    }
}
