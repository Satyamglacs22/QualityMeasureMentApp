using System;

class Program
{
    static void Main(string[] args)
    {
        var q1 = new QuantityLength(1.0, LengthUnit.Feet);
        var q2 = new QuantityLength(12.0, LengthUnit.Inch);

        Console.WriteLine(q1 + " == " + q2);
        Console.WriteLine("Equal: " + q1.Equals(q2));

        Console.WriteLine();

        var q3 = new QuantityLength(1.0, LengthUnit.Meter);
        var q4 = new QuantityLength(100.0, LengthUnit.Centimeter);

        Console.WriteLine(q3 + " == " + q4);
        Console.WriteLine("Equal: " + q3.Equals(q4));

        Console.ReadLine();
    }
}