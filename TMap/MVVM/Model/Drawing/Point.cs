using System;
using System.Diagnostics.CodeAnalysis;

namespace TMap.MVVM.Model.Drawing;

public struct Point
{
    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    public Point(int value)
    {
        X = Y = value;
    }

    public int X { get; set; }
    public int Y { get; set; }

    public static bool operator <(Point a, Point b)
    {
        return a.X < b.X || a.Y < b.Y;
    }
    public static bool operator >(Point a, Point b)
    {
        return a.X > b.X || a.Y > b.Y;
    }
    public static bool operator != (Point a, Point b)
    {
        return a.X != b.X || a.Y != b.Y;
    }
    public static bool operator ==(Point a, Point b)
    {
        return !(a != b);
    }

    public readonly override bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj is not null && obj is Point p)
        {
            return p.X == X && p.Y == Y;
        }

        return false;
    }
    public override readonly int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }
}