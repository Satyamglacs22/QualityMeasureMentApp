using QuantityMeasurementAppModels.DTOs;
using QuantityMeasurementAppRepositories.Interfaces;
using QuantityMeasurementAppServices.Interfaces;

namespace QuantityMeasurementAppServices.Services
{
    public class QuantityWebServiceImpl : IQuantityWebService
    {
        private readonly IQuantityMeasurementService _calculator;
        private readonly IQuantityRecordRepository   _history;

        public QuantityWebServiceImpl(
            IQuantityMeasurementService calculator,
            IQuantityRecordRepository   history)
        {
            _calculator = calculator;
            _history    = history;
        }

        public QuantityMeasurementResponseDTO Compare(QuantityInputRequest request)
        {
            bool matched = _calculator.Compare(
                request.ThisQuantityDTO,
                request.ThatQuantityDTO);

            return BuildTwoOperandResponse(request, "Compare", matched.ToString());
        }

        public QuantityMeasurementResponseDTO Convert(ConvertRequest request)
        {
            QuantityDTO output = _calculator.Convert(
                request.ThisQuantityDTO,
                request.TargetUnit);

            return new QuantityMeasurementResponseDTO
            {
                ThisValue           = request.ThisQuantityDTO.Value,
                ThisUnit            = request.ThisQuantityDTO.UnitName,
                ThisMeasurementType = request.ThisQuantityDTO.MeasurementType,
                Operation           = "Convert",
                ResultValue         = output.Value,
                ResultUnit          = request.TargetUnit,
                IsError             = false
            };
        }

        public QuantityMeasurementResponseDTO Add(ArithmeticRequest request)
        {
            QuantityDTO output = _calculator.Add(
                request.ThisQuantityDTO,
                request.ThatQuantityDTO,
                request.TargetUnit);

            return BuildArithmeticResponse(request, "Add", output);
        }

        public QuantityMeasurementResponseDTO Subtract(ArithmeticRequest request)
        {
            QuantityDTO output = _calculator.Subtract(
                request.ThisQuantityDTO,
                request.ThatQuantityDTO,
                request.TargetUnit);

            return BuildArithmeticResponse(request, "Subtract", output);
        }

        public QuantityMeasurementResponseDTO Divide(QuantityInputRequest request)
        {
            double ratio = _calculator.Divide(
                request.ThisQuantityDTO,
                request.ThatQuantityDTO);

            var response = BuildTwoOperandResponse(request, "Divide", null);
            response.ResultValue = ratio;
            return response;
        }

        public List<QuantityMeasurementResponseDTO> GetHistoryByOperation(string operation)
            => QuantityMeasurementResponseDTO.FromEntityList(
                _history.GetByOperation(operation));

        public List<QuantityMeasurementResponseDTO> GetHistoryByType(string measurementType)
            => QuantityMeasurementResponseDTO.FromEntityList(
                _history.GetByMeasurementType(measurementType));

        public List<QuantityMeasurementResponseDTO> GetErrorHistory()
            => QuantityMeasurementResponseDTO.FromEntityList(
                _history.GetErrorHistory());

        public int GetOperationCount(string operation)
            => _history.GetOperationCount(operation);

        // ── Private helpers ───────────────────────────────────────────────────

        private static QuantityMeasurementResponseDTO BuildTwoOperandResponse(
            QuantityInputRequest request, string operation, string? resultStr)
        {
            return new QuantityMeasurementResponseDTO
            {
                ThisValue           = request.ThisQuantityDTO.Value,
                ThisUnit            = request.ThisQuantityDTO.UnitName,
                ThisMeasurementType = request.ThisQuantityDTO.MeasurementType,
                ThatValue           = request.ThatQuantityDTO.Value,
                ThatUnit            = request.ThatQuantityDTO.UnitName,
                ThatMeasurementType = request.ThatQuantityDTO.MeasurementType,
                Operation           = operation,
                ResultString        = resultStr,
                IsError             = false
            };
        }

        private static QuantityMeasurementResponseDTO BuildArithmeticResponse(
            ArithmeticRequest request, string operation, QuantityDTO output)
        {
            return new QuantityMeasurementResponseDTO
            {
                ThisValue             = request.ThisQuantityDTO.Value,
                ThisUnit              = request.ThisQuantityDTO.UnitName,
                ThisMeasurementType   = request.ThisQuantityDTO.MeasurementType,
                ThatValue             = request.ThatQuantityDTO.Value,
                ThatUnit              = request.ThatQuantityDTO.UnitName,
                ThatMeasurementType   = request.ThatQuantityDTO.MeasurementType,
                Operation             = operation,
                ResultValue           = output.Value,
                ResultUnit            = request.TargetUnit,
                ResultMeasurementType = request.ThisQuantityDTO.MeasurementType,
                IsError               = false
            };
        }
    }
}