using QuantityMeasurementAppBusiness;
using QuantityMeasurementAppBusiness.Exceptions;
using QuantityMeasurementAppBusiness.Implementations;
using QuantityMeasurementAppBusiness.Interfaces;
using QuantityMeasurementAppModels.DTOs;
using QuantityMeasurementAppModels.Entities;
using QuantityMeasurementAppModels.Enums;
using QuantityMeasurementAppRepositories.Interfaces;
using QuantityMeasurementAppServices.Interfaces;

namespace QuantityMeasurementAppServices.Services
{
    public class QuantityMeasurementServiceImpl : IQuantityMeasurementService
    {
        private readonly IQuantityRecordRepository _store;

        public QuantityMeasurementServiceImpl(IQuantityRecordRepository store)
        {
            _store = store;
        }

        // ── Unit factory ──────────────────────────────────────────────────────

        private static IMeasurable BuildUnit(string category, string unitLabel)
        {
            return category.ToLowerInvariant() switch
            {
                "length"      => new LengthMeasurementImpl(
                                     Enum.Parse<LengthUnit>(unitLabel, true)),
                "weight"      => new WeightMeasurementImpl(
                                     Enum.Parse<WeightUnit>(unitLabel, true)),
                "volume"      => new VolumeMeasurementImpl(
                                     Enum.Parse<VolumeUnit>(unitLabel, true)),
                "temperature" => new TemperatureMeasurementImpl(
                                     Enum.Parse<TemperatureUnit>(unitLabel, true)),
                _             => throw new ArgumentException(
                                     $"Unknown measurement category: {category}")
            };
        }

        private static Quantity<IMeasurable> BuildQuantity(QuantityDTO dto)
        {
            var unit = BuildUnit(dto.MeasurementType, dto.UnitName);
            return new Quantity<IMeasurable>(dto.Value, unit);
        }

        // ── Operations ────────────────────────────────────────────────────────

        public QuantityDTO Add(QuantityDTO first, QuantityDTO second, string targetUnitLabel)
        {
            try
            {
                var q1     = BuildQuantity(first);
                var q2     = BuildQuantity(second);
                var outUnit = BuildUnit(first.MeasurementType, targetUnitLabel);

                var result = q1.Add(q2, outUnit);

                _store.Save(SuccessRecord(OperationType.Add.ToString(),
                    first, second, result.GetValue()));

                return new QuantityDTO(result.GetValue(), targetUnitLabel, first.MeasurementType);
            }
            catch (Exception ex)
            {
                _store.Save(ErrorRecord(OperationType.Add.ToString(), ex.Message));
                throw new QuantityMeasurementException($"Addition failed: {ex.Message}", ex);
            }
        }

        public QuantityDTO Subtract(QuantityDTO first, QuantityDTO second, string targetUnitLabel)
        {
            try
            {
                var q1      = BuildQuantity(first);
                var q2      = BuildQuantity(second);
                var outUnit = BuildUnit(first.MeasurementType, targetUnitLabel);

                var result = q1.Subtract(q2, outUnit);

                _store.Save(SuccessRecord(OperationType.Subtract.ToString(),
                    first, second, result.GetValue()));

                return new QuantityDTO(result.GetValue(), targetUnitLabel, first.MeasurementType);
            }
            catch (Exception ex)
            {
                _store.Save(ErrorRecord(OperationType.Subtract.ToString(), ex.Message));
                throw new QuantityMeasurementException($"Subtraction failed: {ex.Message}", ex);
            }
        }

        public double Divide(QuantityDTO first, QuantityDTO second)
        {
            try
            {
                var q1 = BuildQuantity(first);
                var q2 = BuildQuantity(second);

                double ratio = q1.Divide(q2);

                _store.Save(SuccessRecord(OperationType.Divide.ToString(),
                    first, second, ratio));

                return ratio;
            }
            catch (Exception ex)
            {
                _store.Save(ErrorRecord(OperationType.Divide.ToString(), ex.Message));
                throw new QuantityMeasurementException($"Division failed: {ex.Message}", ex);
            }
        }

        public bool Compare(QuantityDTO first, QuantityDTO second)
        {
            try
            {
                var q1   = BuildQuantity(first);
                var q2   = BuildQuantity(second);
                bool same = q1.Equals(q2);

                _store.Save(SuccessRecord(OperationType.Compare.ToString(),
                    first, second, same ? 1.0 : 0.0));

                return same;
            }
            catch (Exception ex)
            {
                _store.Save(ErrorRecord(OperationType.Compare.ToString(), ex.Message));
                throw new QuantityMeasurementException($"Comparison failed: {ex.Message}", ex);
            }
        }

        public QuantityDTO Convert(QuantityDTO source, string targetUnitLabel)
        {
            try
            {
                var q       = BuildQuantity(source);
                var outUnit = BuildUnit(source.MeasurementType, targetUnitLabel);
                var result  = q.ConvertTo(outUnit);

                _store.Save(new QuantityMeasurementEntity(
                    OperationType.Convert.ToString(),
                    source.Value, source.UnitName,
                    result.GetValue(),
                    source.MeasurementType));

                return new QuantityDTO(result.GetValue(), targetUnitLabel, source.MeasurementType);
            }
            catch (Exception ex)
            {
                _store.Save(ErrorRecord(OperationType.Convert.ToString(), ex.Message));
                throw new QuantityMeasurementException($"Conversion failed: {ex.Message}", ex);
            }
        }

        // ── Record builders ───────────────────────────────────────────────────

        private static QuantityMeasurementEntity SuccessRecord(
            string op, QuantityDTO a, QuantityDTO b, double result)
        {
            return new QuantityMeasurementEntity(
                op,
                a.Value, a.UnitName,
                b.Value, b.UnitName,
                result,
                a.MeasurementType);
        }

        private static QuantityMeasurementEntity ErrorRecord(string op, string message)
        {
            return new QuantityMeasurementEntity(op, message);
        }
    }
}