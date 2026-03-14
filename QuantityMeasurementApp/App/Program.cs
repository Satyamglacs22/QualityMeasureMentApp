using System;
using QuantityMeasurementApp.Model;

namespace QuantityMeasurementApp.App
{
    class Program
    {
        static void Main()
        {
            var length1 = new Quantity<LengthUnit>(10, LengthUnit.Feet);
            var length2 = new Quantity<LengthUnit>(6, LengthUnit.Inches);

            Console.WriteLine(length1.Subtract(length2, LengthUnit.Feet));

            var weight1 = new Quantity<WeightUnit>(10, WeightUnit.Kilogram);
            var weight2 = new Quantity<WeightUnit>(5, WeightUnit.Kilogram);

            Console.WriteLine(weight1.Divide(weight2));

            var volume1 = new Quantity<VolumeUnit>(5, VolumeUnit.Litre);
            var volume2 = new Quantity<VolumeUnit>(500, VolumeUnit.Millilitre);

            Console.WriteLine(volume1.Subtract(volume2, VolumeUnit.Litre));
        }
    }
}