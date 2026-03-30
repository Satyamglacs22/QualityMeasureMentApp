using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuantityMeasurementAppModels.Entities
{
    [Table("quantity_measurements")]
    public class QuantityMeasurementEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public long Id { get; set; }

        [Column("operation")]
        public string Operation { get; set; } = string.Empty;

        [Column("first_value")]
        public double FirstValue { get; set; }

        [Column("first_unit")]
        public string? FirstUnit { get; set; }

        [Column("second_value")]
        public double SecondValue { get; set; }

        [Column("second_unit")]
        public string? SecondUnit { get; set; }

        [Column("result_value")]
        public double ResultValue { get; set; }

        [Column("measurement_type")]
        public string? MeasurementType { get; set; }

        [Column("is_error")]
        public bool IsError { get; set; }

        [Column("error_message")]
        public string? ErrorMessage { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }

        // Required by EF Core
        public QuantityMeasurementEntity()
        {
            CreatedAt = UpdatedAt = DateTime.UtcNow;
        }

        // For two-operand operations (Add / Subtract / Compare / Divide)
        public static QuantityMeasurementEntity ForBinaryOp(
            string operation,
            double inputA, string unitA,
            double inputB, string unitB,
            double result,
            string category)
        {
            return new QuantityMeasurementEntity
            {
                Operation       = operation,
                FirstValue      = inputA,
                FirstUnit       = unitA,
                SecondValue     = inputB,
                SecondUnit      = unitB,
                ResultValue     = result,
                MeasurementType = category,
                IsError         = false
            };
        }

        // For single-operand operations (Convert)
        public static QuantityMeasurementEntity ForConvert(
            string operation,
            double inputVal, string inputUnit,
            double resultVal,
            string category)
        {
            return new QuantityMeasurementEntity
            {
                Operation       = operation,
                FirstValue      = inputVal,
                FirstUnit       = inputUnit,
                ResultValue     = resultVal,
                MeasurementType = category,
                IsError         = false
            };
        }

        // For error logging
        public static QuantityMeasurementEntity ForError(string operation, string errorMessage)
        {
            return new QuantityMeasurementEntity
            {
                Operation    = operation,
                IsError      = true,
                ErrorMessage = errorMessage
            };
        }

        // Keep original constructors for backward compatibility with existing service code
        public QuantityMeasurementEntity(
            string operation,
            double firstValue, string firstUnit,
            double secondValue, string secondUnit,
            double resultValue,
            string measurementType)
        {
            Operation       = operation;
            FirstValue      = firstValue;
            FirstUnit       = firstUnit;
            SecondValue     = secondValue;
            SecondUnit      = secondUnit;
            ResultValue     = resultValue;
            MeasurementType = measurementType;
            IsError         = false;
            CreatedAt       = UpdatedAt = DateTime.UtcNow;
        }

        public QuantityMeasurementEntity(
            string operation,
            double inputValue, string inputUnit,
            double resultValue,
            string measurementType)
        {
            Operation       = operation;
            FirstValue      = inputValue;
            FirstUnit       = inputUnit;
            ResultValue     = resultValue;
            MeasurementType = measurementType;
            IsError         = false;
            CreatedAt       = UpdatedAt = DateTime.UtcNow;
        }

        public QuantityMeasurementEntity(string operation, string errorMessage)
        {
            Operation    = operation;
            IsError      = true;
            ErrorMessage = errorMessage;
            CreatedAt    = UpdatedAt = DateTime.UtcNow;
        }

        public override string ToString()
        {
            return IsError
                ? $"[{Operation}] ERROR — {ErrorMessage}"
                : $"[{Operation}] {FirstValue} {FirstUnit} => {ResultValue}";
        }
    }
}