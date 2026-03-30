using QuantityMeasurementAppBusiness.Interfaces;
using QuantityMeasurementAppModels.Enums;

namespace QuantityMeasurementAppBusiness.Implementations
{
    /// <summary>
    /// IMeasurable implementation for temperature units.
    /// Base unit is Celsius. Temperature does NOT support arithmetic operations.
    /// Conversion uses formula delegates rather than a simple multiplication factor.
    /// </summary>
    public class TemperatureMeasurementImpl : IMeasurable
    {
        private readonly TemperatureUnit _selectedUnit;

        private static readonly Dictionary<TemperatureUnit, Func<double, double>> ToCelsius = new()
        {
            { TemperatureUnit.Celsius,    t => t               },
            { TemperatureUnit.Fahrenheit, t => (t - 32) * 5.0 / 9.0 },
            { TemperatureUnit.Kelvin,     t => t - 273.15      }
        };

        private static readonly Dictionary<TemperatureUnit, Func<double, double>> FromCelsius = new()
        {
            { TemperatureUnit.Celsius,    c => c               },
            { TemperatureUnit.Fahrenheit, c => c * 9.0 / 5.0 + 32 },
            { TemperatureUnit.Kelvin,     c => c + 273.15      }
        };

        public TemperatureMeasurementImpl(TemperatureUnit unitType)
        {
            _selectedUnit = unitType;
        }

        public double ToBaseFactor() => 1.0;

        public double Normalise(double raw)
        {
            if (!ToCelsius.TryGetValue(_selectedUnit, out var convert))
                throw new ArgumentException($"Unrecognised temperature unit: {_selectedUnit}");
            return convert(raw);
        }

        public double Denormalise(double baseVal)
        {
            if (!FromCelsius.TryGetValue(_selectedUnit, out var convert))
                throw new ArgumentException($"Unrecognised temperature unit: {_selectedUnit}");
            return convert(baseVal);
        }

        public string Label()    => _selectedUnit.ToString();
        public string Category() => "Temperature";
        public bool   AllowsArithmetic() => false;

        public void AssertOperationAllowed(string operationName)
        {
            throw new NotSupportedException(
                $"{operationName} is not supported for temperature measurements.");
        }

        public IMeasurable FromLabel(string label)
        {
            var parsed = Enum.Parse<TemperatureUnit>(label, ignoreCase: true);
            return new TemperatureMeasurementImpl(parsed);
        }
    }
}