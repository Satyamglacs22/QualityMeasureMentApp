using System;

namespace QuantityMeasurementApp.Interfaces
{
    public interface IMeasurable
    {
        double ConvertToBaseUnit(double value);

        double ConvertFromBaseUnit(double baseValue);

        string GetUnitName();

        // Default: arithmetic allowed
        bool SupportsArithmetic()
        {
            return true;
        }

        // Default validation (can be overridden)
        void ValidateOperationSupport(string operation)
        {
            // By default all operations allowed
        }
    }
}