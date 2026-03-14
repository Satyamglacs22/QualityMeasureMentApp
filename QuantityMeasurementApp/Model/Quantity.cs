using System;

namespace QuantityMeasurementApp.Model
{
    public class Quantity<U> where U : Enum
    {
        private readonly double value;
        private readonly U unit;

        private const double EPSILON = 0.0001;

        public Quantity(double value, U unit)
        {
            if (unit == null)
                throw new ArgumentException("Unit cannot be null");

            if (double.IsNaN(value) || double.IsInfinity(value))
                throw new ArgumentException("Invalid numeric value");

            this.value = value;
            this.unit = unit;
        }

        public double Value => value;
        public U Unit => unit;

        // ---------------- BASE CONVERSION ----------------

        private static double ConvertToBase(double value, U unit)
        {
            if (unit is LengthUnit lu)
                return lu.ConvertToBaseUnit(value);

            if (unit is WeightUnit wu)
                return wu.ConvertToBaseUnit(value);

            if (unit is VolumeUnit vu)
                return vu.ConvertToBaseUnit(value);

            throw new InvalidOperationException("Unsupported unit type");
        }

        private static double ConvertFromBase(double baseValue, U unit)
        {
            if (unit is LengthUnit lu)
                return lu.ConvertFromBaseUnit(baseValue);

            if (unit is WeightUnit wu)
                return wu.ConvertFromBaseUnit(baseValue);

            if (unit is VolumeUnit vu)
                return vu.ConvertFromBaseUnit(baseValue);

            throw new InvalidOperationException("Unsupported unit type");
        }

        private double ToBase()
        {
            return ConvertToBase(value, unit);
        }

        // ---------------- CONVERSION ----------------

        public Quantity<U> ConvertTo(U targetUnit)
        {
            if (targetUnit == null)
                throw new ArgumentException("Target unit cannot be null");

            double baseValue = ToBase();
            double converted = ConvertFromBase(baseValue, targetUnit);

            return new Quantity<U>(converted, targetUnit);
        }

        // ---------------- ADDITION ----------------

        public Quantity<U> Add(Quantity<U> other)
        {
            return Add(other, this.unit);
        }

        public Quantity<U> Add(Quantity<U> other, U targetUnit)
        {
            ValidateOperand(other, targetUnit);

            double sum = ToBase() + other.ToBase();

            double result = ConvertFromBase(sum, targetUnit);

            return new Quantity<U>(result, targetUnit);
        }

        // ---------------- SUBTRACTION ----------------

        public Quantity<U> Subtract(Quantity<U> other)
        {
            return Subtract(other, this.unit);
        }

        public Quantity<U> Subtract(Quantity<U> other, U targetUnit)
        {
            ValidateOperand(other, targetUnit);

            double difference = ToBase() - other.ToBase();

            double result = ConvertFromBase(difference, targetUnit);

            return new Quantity<U>(result, targetUnit);
        }

        // ---------------- DIVISION ----------------

        public double Divide(Quantity<U> other)
        {
            if (other == null)
                throw new ArgumentException("Other quantity cannot be null");

            double divisor = other.ToBase();

            if (Math.Abs(divisor) < EPSILON)
                throw new ArithmeticException("Division by zero");

            return ToBase() / divisor;
        }

        // ---------------- VALIDATION ----------------

        private void ValidateOperand(Quantity<U> other, U targetUnit)
        {
            if (other == null)
                throw new ArgumentException("Other quantity cannot be null");

            if (targetUnit == null)
                throw new ArgumentException("Target unit cannot be null");
        }

        // ---------------- EQUALITY ----------------

        public override bool Equals(object obj)
        {
            if (obj is not Quantity<U> other)
                return false;

            return Math.Abs(ToBase() - other.ToBase()) < EPSILON;
        }

        public override int GetHashCode()
        {
            return ToBase().GetHashCode();
        }

        // ---------------- DISPLAY ----------------

        public override string ToString()
        {
            return $"Quantity({Math.Round(value, 4)}, {unit})";
        }
    }
}