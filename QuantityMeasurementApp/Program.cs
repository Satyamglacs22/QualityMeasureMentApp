using System;

class Program
{
    static void Main(string[] args)
    {
        QuantityLength q1 =
            new QuantityLength(1.0, LengthUnit.Feet);

        QuantityLength q2 =
            new QuantityLength(12.0, LengthUnit.Inch);

        Console.WriteLine("Input: " + q1 + " and " + q2);
        Console.WriteLine("Output: Equal (" + q1.Equals(q2) + ")");

        Console.WriteLine();

        QuantityLength q3 =
            new QuantityLength(1.0, LengthUnit.Inch);

        QuantityLength q4 =
            new QuantityLength(1.0, LengthUnit.Inch);

        Console.WriteLine("Input: " + q3 + " and " + q4);
        Console.WriteLine("Output: Equal (" + q3.Equals(q4) + ")");

        Console.ReadLine();
    }
}