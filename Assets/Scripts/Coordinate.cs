using System;
using UnityEngine;

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
    public Coordinate Rotate(float degree)
    {
        float sin = Mathf.Sin(degree * Mathf.Deg2Rad);
        float cos = Mathf.Cos(degree * Mathf.Deg2Rad);


        int tx = x;
        int ty = y;

        x = (int)Math.Round((cos * tx) - (sin * ty));
        y = (int)Math.Round((sin * tx) + (cos * ty));
        return new Coordinate(x, y);
    }
    /// <summary>
    /// Get Angle based on Zero position.
    /// </summary>
    /// <param name="vEnd"></param>
    /// <returns></returns>
    public int GetAngle(Coordinate vEnd)
    {
        Coordinate v = vEnd - Coordinate.Zero;

        return (int)(Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg);
    }
    public int GetAngle()
    {
        Coordinate v = this - Zero;

        return (int)(Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg);
    }
    public override string ToString()
    {
        return "(" + x + "," + y + ")";
    }
}