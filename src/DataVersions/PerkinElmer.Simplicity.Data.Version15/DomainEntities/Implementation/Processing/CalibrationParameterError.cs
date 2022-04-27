using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Processing;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.Implementation.Processing
{
    class CalibrationParameterError : ICalibrationParameterError
    {
        public string ParameterName { get; set; }
        public string ErrorCode { get; set; }
    }
}
