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

        // ---------------- ENUM FOR OPERATIONS ----------------

        private enum ArithmeticOperation
        {
            ADD,
            SUBTRACT,
            DIVIDE
        }

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

        // ---------------- CENTRAL VALIDATION ----------------

        private void ValidateArithmeticOperands(Quantity<U> other, U targetUnit, bool targetRequired)
        {
            if (other == null)
                throw new ArgumentException("Other quantity cannot be null");

            if (targetRequired && targetUnit == null)
                throw new ArgumentException("Target unit cannot be null");

            if (unit.GetType() != other.unit.GetType())
                throw new ArgumentException("Cannot operate on different measurement categories");

            if (double.IsNaN(other.value) || double.IsInfinity(other.value))
                throw new ArgumentException("Invalid numeric value");
        }

        // ---------------- CENTRAL ARITHMETIC ENGINE ----------------

        private double PerformBaseArithmetic(Quantity<U> other, ArithmeticOperation operation)
        {
            double base1 = this.ToBase();
            double base2 = other.ToBase();

            return operation switch
            {
                ArithmeticOperation.ADD => base1 + base2,
                ArithmeticOperation.SUBTRACT => base1 - base2,
                ArithmeticOperation.DIVIDE => base2 == 0
                        ? throw new ArithmeticException("Division by zero")
                        : base1 / base2,
                _ => throw new InvalidOperationException("Unsupported operation")
            };
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

        // ---------------- ADD ----------------

        public Quantity<U> Add(Quantity<U> other)
        {
            return Add(other, this.unit);
        }

        public Quantity<U> Add(Quantity<U> other, U targetUnit)
        {
            ValidateArithmeticOperands(other, targetUnit, true);

            double resultBase = PerformBaseArithmetic(other, ArithmeticOperation.ADD);

            double result = ConvertFromBase(resultBase, targetUnit);

            return new Quantity<U>(result, targetUnit);
        }

        // ---------------- SUBTRACT ----------------

        public Quantity<U> Subtract(Quantity<U> other)
        {
            return Subtract(other, this.unit);
        }

        public Quantity<U> Subtract(Quantity<U> other, U targetUnit)
        {
            ValidateArithmeticOperands(other, targetUnit, true);

            double resultBase = PerformBaseArithmetic(other, ArithmeticOperation.SUBTRACT);

            double result = ConvertFromBase(resultBase, targetUnit);

            return new Quantity<U>(result, targetUnit);
        }

        // ---------------- DIVIDE ----------------

        public double Divide(Quantity<U> other)
        {
            ValidateArithmeticOperands(other, default, false);

            return PerformBaseArithmetic(other, ArithmeticOperation.DIVIDE);
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