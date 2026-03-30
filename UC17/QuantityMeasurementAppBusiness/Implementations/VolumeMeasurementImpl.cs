using QuantityMeasurementAppBusiness.Interfaces;
using QuantityMeasurementAppModels.Enums;

namespace QuantityMeasurementAppBusiness.Implementations
{
    /// <summary>
    /// IMeasurable implementation for volume units.
    /// Base unit is Litre.
    /// </summary>
    public class VolumeMeasurementImpl : IMeasurable
    {
        private readonly VolumeUnit _selectedUnit;

        private static readonly Dictionary<VolumeUnit, double> FactorMap = new()
        {
            { VolumeUnit.Litre,      1.0     },
            { VolumeUnit.Millilitre, 0.001   },
            { VolumeUnit.Gallon,     3.78541 }
        };

        public VolumeMeasurementImpl(VolumeUnit unitType)
        {
            _selectedUnit = unitType;
        }

        public double ToBaseFactor()
        {
            if (!FactorMap.TryGetValue(_selectedUnit, out double factor))
                throw new ArgumentException($"Unrecognised volume unit: {_selectedUnit}");
            return factor;
        }

        public double Normalise(double raw)       => raw * ToBaseFactor();
        public double Denormalise(double baseVal) => baseVal / ToBaseFactor();
        public string Label()    => _selectedUnit.ToString();
        public string Category() => "Volume";
        public bool   AllowsArithmetic() => true;
        public void   AssertOperationAllowed(string operationName) { /* all ops permitted */ }

        public IMeasurable FromLabel(string label)
        {
            var parsed = Enum.Parse<VolumeUnit>(label, ignoreCase: true);
            return new VolumeMeasurementImpl(parsed);
        }
    }
}