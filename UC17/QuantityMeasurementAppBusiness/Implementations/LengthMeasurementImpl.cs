using QuantityMeasurementAppBusiness.Interfaces;
using QuantityMeasurementAppModels.Enums;

namespace QuantityMeasurementAppBusiness.Implementations
{
    /// <summary>
    /// IMeasurable implementation for length units.
    /// Base unit is Feet. All other units are expressed as their equivalent in Feet.
    /// </summary>
    public class LengthMeasurementImpl : IMeasurable
    {
        private readonly LengthUnit _selectedUnit;

        private static readonly Dictionary<LengthUnit, double> FactorMap = new()
        {
            { LengthUnit.Feet,       1.0        },
            { LengthUnit.Inch,       1.0 / 12.0 },
            { LengthUnit.Yard,       3.0        },
            { LengthUnit.Centimeter, 0.0328084  }
        };

        public LengthMeasurementImpl(LengthUnit unitType)
        {
            _selectedUnit = unitType;
        }

        public double ToBaseFactor()
        {
            if (!FactorMap.TryGetValue(_selectedUnit, out double factor))
                throw new ArgumentException($"Unrecognised length unit: {_selectedUnit}");
            return factor;
        }

        public double Normalise(double raw)   => raw * ToBaseFactor();
        public double Denormalise(double baseVal) => baseVal / ToBaseFactor();
        public string Label()    => _selectedUnit.ToString();
        public string Category() => "Length";
        public bool   AllowsArithmetic() => true;
        public void   AssertOperationAllowed(string operationName) { /* all ops permitted */ }

        public IMeasurable FromLabel(string label)
        {
            var parsed = Enum.Parse<LengthUnit>(label, ignoreCase: true);
            return new LengthMeasurementImpl(parsed);
        }
    }
}