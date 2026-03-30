namespace QuantityMeasurementAppBusiness.Interfaces
{
    /// <summary>
    /// Defines the contract for any measurable unit type.
    /// Implementations handle unit-specific conversion and operation rules.
    /// </summary>
    public interface IMeasurable
    {
        /// <summary>Returns the factor used to normalise this unit to its base unit.</summary>
        double ToBaseFactor();

        /// <summary>Normalises a raw value into the shared base unit for this category.</summary>
        double Normalise(double raw);

        /// <summary>Denormalises a base-unit value back into this unit.</summary>
        double Denormalise(double baseVal);

        /// <summary>Returns a human-readable label for this unit.</summary>
        string Label();

        /// <summary>Returns the measurement category this unit belongs to (e.g. "Length").</summary>
        string Category();

        /// <summary>True when this unit permits arithmetic (add, subtract, divide).</summary>
        bool AllowsArithmetic();

        /// <summary>
        /// Throws <see cref="NotSupportedException"/> if the named operation is not permitted
        /// for this unit type; otherwise does nothing.
        /// </summary>
        void AssertOperationAllowed(string operationName);

        /// <summary>Resolves a unit label string to a concrete IMeasurable of the same category.</summary>
        IMeasurable FromLabel(string label);
    }
}