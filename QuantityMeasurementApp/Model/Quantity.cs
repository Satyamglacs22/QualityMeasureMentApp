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
            this.value = value;
            this.unit = unit;
        }

        public double Value => value;
        public U Unit => unit;

        private static double ToBase(double value, U unit)
        {
            if (typeof(U) == typeof(LengthUnit))
                return LengthUnitHelper.ToBase(value, (LengthUnit)(object)unit);

            if (typeof(U) == typeof(WeightUnit))
                return WeightUnitHelper.ToBase(value, (WeightUnit)(object)unit);

            if (typeof(U) == typeof(VolumeUnit))
                return VolumeUnitHelper.ToBase(value, (VolumeUnit)(object)unit);

            throw new InvalidOperationException("Unsupported unit");
        }

        private static double FromBase(double baseValue, U unit)
        {
            if (typeof(U) == typeof(LengthUnit))
                return LengthUnitHelper.FromBase(baseValue, (LengthUnit)(object)unit);

            if (typeof(U) == typeof(WeightUnit))
                return WeightUnitHelper.FromBase(baseValue, (WeightUnit)(object)unit);

            if (typeof(U) == typeof(VolumeUnit))
                return VolumeUnitHelper.FromBase(baseValue, (VolumeUnit)(object)unit);

            throw new InvalidOperationException("Unsupported unit");
        }

        public Quantity<U> ConvertTo(U targetUnit)
        {
            double baseValue = ToBase(value, unit);
            double converted = FromBase(baseValue, targetUnit);

            return new Quantity<U>(converted, targetUnit);
        }

        public Quantity<U> Add(Quantity<U> other, U targetUnit)
        {
            double base1 = ToBase(value, unit);
            double base2 = ToBase(other.value, other.unit);

            double sum = base1 + base2;

            double result = FromBase(sum, targetUnit);

            return new Quantity<U>(result, targetUnit);
        }

        public override bool Equals(object obj)
        {
            if (obj is not Quantity<U> other)
                return false;

            double base1 = ToBase(value, unit);
            double base2 = ToBase(other.value, other.unit);

            return Math.Abs(base1 - base2) < EPSILON;
        }

        public override int GetHashCode()
        {
            return ToBase(value, unit).GetHashCode();
        }

        public override string ToString()
        {
            return $"Quantity({value}, {unit})";
        }
    }
}