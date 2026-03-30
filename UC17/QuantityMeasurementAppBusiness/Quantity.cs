using QuantityMeasurementAppBusiness.Interfaces;
using QuantityMeasurementAppModels.Enums;

namespace QuantityMeasurementAppBusiness
{
    /// <summary>
    /// Represents a physical quantity — a numeric amount paired with a measurable unit.
    /// All arithmetic is performed in the normalised base unit to ensure cross-unit correctness.
    /// </summary>
    public class Quantity<TUnit> where TUnit : IMeasurable
    {
        private readonly double _amount;
        private readonly TUnit  _unit;

        private const double Tolerance = 0.0001;

        public Quantity(double amount, TUnit unit)
        {
            if (unit == null)
                throw new ArgumentException("A unit must be supplied.");

            if (double.IsNaN(amount) || double.IsInfinity(amount))
                throw new ArgumentException("Amount must be a finite number.");

            _amount = amount;
            _unit   = unit;
        }

        // ── Conversion ────────────────────────────────────────────────────────

        public Quantity<TUnit> ConvertTo(TUnit targetUnit)
        {
            if (targetUnit == null)
                throw new ArgumentException("Target unit must be supplied.");

            double inBase    = _unit.Normalise(_amount);
            double converted = targetUnit.Denormalise(inBase);
            return new Quantity<TUnit>(converted, targetUnit);
        }

        // ── Arithmetic ────────────────────────────────────────────────────────

        public Quantity<TUnit> Add(Quantity<TUnit> other, TUnit outputUnit)
        {
            _unit.AssertOperationAllowed("Addition");
            GuardOperands(other, outputUnit, requireTarget: true);
            double sum = ApplyOperation(other, ArithmeticOperation.ADD);
            return new Quantity<TUnit>(outputUnit.Denormalise(sum), outputUnit);
        }

        public Quantity<TUnit> Add(Quantity<TUnit> other)
        {
            _unit.AssertOperationAllowed("Addition");
            GuardOperands(other, _unit, requireTarget: true);
            double sum = ApplyOperation(other, ArithmeticOperation.ADD);
            return new Quantity<TUnit>(_unit.Denormalise(sum), _unit);
        }

        public Quantity<TUnit> Subtract(Quantity<TUnit> other, TUnit outputUnit)
        {
            _unit.AssertOperationAllowed("Subtraction");
            GuardOperands(other, outputUnit, requireTarget: true);
            double diff = ApplyOperation(other, ArithmeticOperation.SUBTRACT);
            return new Quantity<TUnit>(Math.Round(outputUnit.Denormalise(diff), 2), outputUnit);
        }

        public Quantity<TUnit> Subtract(Quantity<TUnit> other)
        {
            _unit.AssertOperationAllowed("Subtraction");
            GuardOperands(other, _unit, requireTarget: true);
            double diff = ApplyOperation(other, ArithmeticOperation.SUBTRACT);
            return new Quantity<TUnit>(Math.Round(_unit.Denormalise(diff), 2), _unit);
        }

        public double Divide(Quantity<TUnit> other)
        {
            _unit.AssertOperationAllowed("Division");
            GuardOperands(other, default, requireTarget: false);

            if (other._amount == 0)
                throw new DivideByZeroException("Divisor quantity cannot be zero.");

            return ApplyOperation(other, ArithmeticOperation.DIVIDE);
        }

        // ── Equality ──────────────────────────────────────────────────────────

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            if (obj is not Quantity<TUnit> other) return false;
            if (_unit.GetType() != other._unit.GetType()) return false;

            double myBase    = _unit.Normalise(_amount);
            double otherBase = other._unit.Normalise(other._amount);

            return Math.Abs(myBase - otherBase) <= Tolerance;
        }

        public override int GetHashCode()
        {
            double normalised = _unit.Normalise(_amount);
            double snapped    = Math.Round(normalised / Tolerance) * Tolerance;
            return snapped.GetHashCode();
        }

        public override string ToString() => $"{_amount} {_unit.Label()}";

        public double GetValue() => _amount;

        // ── Private helpers ───────────────────────────────────────────────────

        private void GuardOperands(Quantity<TUnit> other, TUnit? target, bool requireTarget)
        {
            if (other == null)
                throw new ArgumentException("The second operand must not be null.");

            if (_unit.GetType() != other._unit.GetType())
                throw new ArgumentException(
                    $"Cannot combine '{_unit.Category()}' with '{other._unit.Category()}'.");

            if (!double.IsFinite(_amount) || !double.IsFinite(other._amount))
                throw new ArgumentException("Both operands must hold finite values.");

            if (requireTarget && target == null)
                throw new ArgumentException("An output unit must be specified.");
        }

        private double ApplyOperation(Quantity<TUnit> other, ArithmeticOperation op)
        {
            double a = _unit.Normalise(_amount);
            double b = other._unit.Normalise(other._amount);

            return op switch
            {
                ArithmeticOperation.ADD      => a + b,
                ArithmeticOperation.SUBTRACT => a - b,
                ArithmeticOperation.DIVIDE   => a / b,
                _                            => throw new ArgumentException("Unrecognised operation.")
            };
        }
    }
}