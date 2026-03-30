using QuantityMeasurementAppBusiness.Interfaces;
using QuantityMeasurementAppModels.Enums;

namespace QuantityMeasurementAppBusiness.Implementations
{
    /// <summary>
    /// IMeasurable implementation for weight units.
    /// Base unit is Kilogram.
    /// </summary>
    public class WeightMeasurementImpl : IMeasurable
    {
        private readonly WeightUnit _selectedUnit;

        private static readonly Dictionary<WeightUnit, double> FactorMap = new()
        {
            { WeightUnit.Kilogram, 1.0      },
            { WeightUnit.Gram,     0.001    },
            { WeightUnit.Pound,    0.453592 }
        };

        public WeightMeasurementImpl(WeightUnit unitType)
        {
            _selectedUnit = unitType;
        }

        public double ToBaseFactor()
        {
            if (!FactorMap.TryGetValue(_selectedUnit, out double factor))
                throw new ArgumentException($"Unrecognised weight unit: {_selectedUnit}");
            return factor;
        }

        public double Normalise(double raw)       => raw * ToBaseFactor();
        public double Denormalise(double baseVal) => baseVal / ToBaseFactor();
        public string Label()    => _selectedUnit.ToString();
        public string Category() => "Weight";
        public bool   AllowsArithmetic() => true;
        public void   AssertOperationAllowed(string operationName) { /* all ops permitted */ }

        public IMeasurable FromLabel(string label)
        {
            var parsed = Enum.Parse<WeightUnit>(label, ignoreCase: true);
            return new WeightMeasurementImpl(parsed);
        }
    }
}