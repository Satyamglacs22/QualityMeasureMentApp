using System;

public class Feet
{
    // Private immutable field
    private readonly double value;

    // Constructor
    public Feet(double value)
    {
        this.value = value;
    }

    // Read-only property
    public double Value
    {
        get { return value; }
    }

    // Override Equals
   public override bool Equals(object obj)
{
    if (ReferenceEquals(this, obj))
        return true;

    if (obj == null || GetType() != obj.GetType())
        return false;

    Feet other = (Feet)obj;

    return this.value.Equals(other.value);
}

    // Override GetHashCode
    public override int GetHashCode()
    {
        return value.GetHashCode();
    }
}