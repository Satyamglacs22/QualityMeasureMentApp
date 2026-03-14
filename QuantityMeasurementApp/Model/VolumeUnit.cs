using System;

namespace QuantityMeasurementApp.Model
{
    public enum VolumeUnit
    {
        Litre,
        Millilitre,
        Gallon
    }

    public static class VolumeUnitHelper
    {
        public static double ToBase(double value, VolumeUnit unit)
        {
            switch (unit)
            {
                case VolumeUnit.Litre:
                    return value;

                case VolumeUnit.Millilitre:
                    return value * 0.001;

                case VolumeUnit.Gallon:
                    return value * 3.78541;

                default:
                    throw new ArgumentException("Invalid volume unit");
            }
        }

        public static double FromBase(double baseValue, VolumeUnit unit)
        {
            switch (unit)
            {
                case VolumeUnit.Litre:
                    return baseValue;

                case VolumeUnit.Millilitre:
                    return baseValue / 0.001;

                case VolumeUnit.Gallon:
                    return baseValue / 3.78541;

                default:
                    throw new ArgumentException("Invalid volume unit");
            }
        }
    }
}