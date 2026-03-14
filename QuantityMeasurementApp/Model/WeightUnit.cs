using System;

namespace QuantityMeasurementApp.Model
{
    public enum WeightUnit
    {
        Kilogram,
        Gram,
        Pound
    }

    public static class WeightUnitHelper
    {
        public static double ToBase(double value, WeightUnit unit)
        {
            switch (unit)
            {
                case WeightUnit.Kilogram:
                    return value;

                case WeightUnit.Gram:
                    return value * 0.001;

                case WeightUnit.Pound:
                    return value * 0.453592;

                default:
                    throw new ArgumentException("Invalid weight unit");
            }
        }

        public static double FromBase(double baseValue, WeightUnit unit)
        {
            switch (unit)
            {
                case WeightUnit.Kilogram:
                    return baseValue;

                case WeightUnit.Gram:
                    return baseValue / 0.001;

                case WeightUnit.Pound:
                    return baseValue / 0.453592;

                default:
                    throw new ArgumentException("Invalid weight unit");
            }
        }
    }
}