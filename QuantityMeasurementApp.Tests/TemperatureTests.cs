using System;
using QuantityMeasurementApp.Interfaces;

namespace QuantityMeasurementApp.Model
{
    public enum TemperatureUnit
    {
        CELSIUS,
        FAHRENHEIT,
        KELVIN
    }

    public static class TemperatureUnitExtensions
    {
        // Convert temperature to base unit (Celsius)
        public static double ConvertToBaseUnit(this TemperatureUnit unit, double value)
        {
            return unit switch
            {
                TemperatureUnit.CELSIUS => value,

                TemperatureUnit.FAHRENHEIT =>
                    (value - 32) * 5.0 / 9.0,

                TemperatureUnit.KELVIN =>
                    value - 273.15,

                _ => throw new InvalidOperationException("Unsupported temperature unit")
            };
        }

        // Convert from base unit (Celsius) to target unit
        public static double ConvertFromBaseUnit(this TemperatureUnit unit, double baseValue)
        {
            return unit switch
            {
                TemperatureUnit.CELSIUS => baseValue,

                TemperatureUnit.FAHRENHEIT =>
                    (baseValue * 9.0 / 5.0) + 32,

                TemperatureUnit.KELVIN =>
                    baseValue + 273.15,

                _ => throw new InvalidOperationException("Unsupported temperature unit")
            };
        }

        // Arithmetic operations NOT supported
        public static void ValidateOperationSupport(this TemperatureUnit unit, string operation)
        {
            throw new NotSupportedException(
                $"Temperature does not support {operation} operation."
            );
        }

        // Indicates arithmetic not supported
        public static bool SupportsArithmetic(this TemperatureUnit unit)
        {
            return false;
        }

        public static string GetUnitName(this TemperatureUnit unit)
        {
            return unit.ToString();
        }
    }
}