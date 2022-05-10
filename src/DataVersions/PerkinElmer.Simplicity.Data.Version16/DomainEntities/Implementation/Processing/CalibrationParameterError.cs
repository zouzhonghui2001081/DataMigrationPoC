using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Processing;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Implementation.Processing
{
    class CalibrationParameterError : ICalibrationParameterError
    {
        public string ParameterName { get; set; }
        public string ErrorCode { get; set; }
    }
}
