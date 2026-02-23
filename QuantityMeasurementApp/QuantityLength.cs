using System;

public class QuantityLength
{
    private readonly double value;
    private readonly LengthUnit unit;

    // Conversion factors
    private const double INCH_TO_FEET = 1.0 / 12.0;

    // Tolerance for floating comparison
    private const double EPSILON = 0.000001;

    public QuantityLength(double value, LengthUnit unit)
    {
        if (double.IsNaN(value) || double.IsInfinity(value))
            throw new ArgumentException("Invalid numeric value");

        this.value = value;
        this.unit = unit;
    }

    // Convert to Feet (Base Unit)
    private double ToFeet()
    {
        switch (unit)
        {
            case LengthUnit.Feet:
                return value;

            case LengthUnit.Inch:
                return value * INCH_TO_FEET;

            default:
                throw new ArgumentException("Invalid Unit");
        }
    }

    // Override Equals (Nullable Safe)
    public override bool Equals(object? obj)
    {
        // Same reference
        if (ReferenceEquals(this, obj))
            return true;

        // Null + Type check
        if (obj is not QuantityLength other)
            return false;

        // Convert both to feet
        double thisFeet = this.ToFeet();
        double otherFeet = other.ToFeet();

        // Safe comparison with tolerance
        return Math.Abs(thisFeet - otherFeet) < EPSILON;
    }

    // Override GetHashCode
    public override int GetHashCode()
    {
        return ToFeet().GetHashCode();
    }

    // For display
    public override string ToString()
    {
        return $"Quantity({value}, {unit})";
    }
}