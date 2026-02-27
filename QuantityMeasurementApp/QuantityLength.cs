using System;

public class QuantityLength
{
    private readonly double value;
    private readonly LengthUnit unit;

    // Conversion factors (to base unit: FEET)
    private const double INCH_TO_FEET = 1.0 / 12.0;
    private const double CM_TO_FEET = 1.0 / 30.48;
    private const double METER_TO_FEET = 3.28084;

    private const double EPSILON = 0.000001;

    public QuantityLength(double value, LengthUnit unit)
    {
        if (double.IsNaN(value) || double.IsInfinity(value))
            throw new ArgumentException("Invalid numeric value");

        this.value = value;
        this.unit = unit;
    }

    // Convert everything to FEET (Base Unit)
    private double ToFeet()
    {
        return unit switch
        {
            LengthUnit.Feet => value,
            LengthUnit.Inch => value * INCH_TO_FEET,
            LengthUnit.Centimeter => value * CM_TO_FEET,
            LengthUnit.Meter => value * METER_TO_FEET,
            _ => throw new ArgumentException("Unsupported unit")
        };
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj))
            return true;

        if (obj is not QuantityLength other)
            return false;

        double thisFeet = this.ToFeet();
        double otherFeet = other.ToFeet();

        return Math.Abs(thisFeet - otherFeet) < EPSILON;
    }

    public override int GetHashCode()
    {
        return ToFeet().GetHashCode();
    }

    public override string ToString()
    {
        return $"Quantity({value}, {unit})";
    }
}