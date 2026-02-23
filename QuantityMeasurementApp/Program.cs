using System;

class Program
{
    static void Main(string[] args)
    {
        // Feet Comparison
        bool feetResult = CheckFeetEquality(1.0, 1.0);

        Console.WriteLine("Input: 1.0 ft and 1.0 ft");
        Console.WriteLine("Output: Equal (" + feetResult + ")");

        Console.WriteLine();

        // Inch Comparison
        bool inchResult = CheckInchEquality(1.0, 1.0);

        Console.WriteLine("Input: 1.0 inch and 1.0 inch");
        Console.WriteLine("Output: Equal (" + inchResult + ")");

        
    }

    // Static Method for Feet Equality
    public static bool CheckFeetEquality(double v1, double v2)
    {
        Feet f1 = new Feet(v1);
        Feet f2 = new Feet(v2);

        return f1.Equals(f2);
    }

    // Static Method for Inch Equality
    public static bool CheckInchEquality(double v1, double v2)
    {
        Inch i1 = new Inch(v1);
        Inch i2 = new Inch(v2);

        return i1.Equals(i2);
    }
}