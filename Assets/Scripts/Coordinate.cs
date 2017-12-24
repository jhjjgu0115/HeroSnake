using System;

[Serializable]
public struct Coordinate
{
    public int x;
    public int y;
    public Coordinate(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    public Coordinate(Coordinate coordinate)
    {
        x = coordinate.x;
        y = coordinate.y;
    }
    public static Coordinate Zero
    {
        get
        {
            return new Coordinate(0, 0);
        }
    }


    public static Coordinate operator +(Coordinate a, Coordinate b)
    {
        return new Coordinate(a.x + b.x, a.y + b.y);
    }
    public static Coordinate operator -(Coordinate a, Coordinate b)
    {
        return new Coordinate(a.x - b.x, a.y - b.y);
    }
    public static Coordinate operator -(Coordinate a)
    {
        return new Coordinate(-a.x, -a.y);
    }
    public static Coordinate operator *(Coordinate a, int b)
    {
        return new Coordinate(a.x * b, a.y * b);
    }
    public static Coordinate operator *(int a, Coordinate b)
    {
        return new Coordinate(b.x * a, b.y * a);
    }
    public override string ToString()
    {
        return "(" + x + "," + y + ")";
    }
}