using System;
using QuantityMeasurementApp.Model;

namespace QuantityMeasurementApp.App
{
    class Program
    {
        static void Main()
        {
            // ---------------- LENGTH ----------------
            var length1 = new Quantity<LengthUnit>(1, LengthUnit.Feet);
            var length2 = new Quantity<LengthUnit>(12, LengthUnit.Inches);

            Console.WriteLine(length1.Equals(length2));

            var lengthResult = length1.Add(length2, LengthUnit.Feet);
            Console.WriteLine(lengthResult);

            // ---------------- WEIGHT ----------------
            var weight1 = new Quantity<WeightUnit>(1, WeightUnit.Kilogram);
            var weight2 = new Quantity<WeightUnit>(1000, WeightUnit.Gram);

            Console.WriteLine(weight1.Equals(weight2));

            var weightResult = weight1.Add(weight2, WeightUnit.Kilogram);
            Console.WriteLine(weightResult);

            // ---------------- VOLUME (UC11) ----------------
            var volume1 = new Quantity<VolumeUnit>(1, VolumeUnit.Litre);
            var volume2 = new Quantity<VolumeUnit>(1000, VolumeUnit.Millilitre);
            var volume3 = new Quantity<VolumeUnit>(1, VolumeUnit.Gallon);

            Console.WriteLine(volume1.Equals(volume2));

            var volumeResult = volume1.Add(volume2, VolumeUnit.Litre);
            Console.WriteLine(volumeResult);

            var gallonToLitre = volume3.ConvertTo(VolumeUnit.Litre);
            Console.WriteLine(gallonToLitre);
        }
    }
}