using System.Collections.Generic;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Processing.MaintenanceTesting;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.Implementation.Processing.MaintenanceTesting
{
	public class RamanScatteringTestResult : IRamanScatteringTestResult
	{
		public bool IsDataValidForTest { get; set; }
		public string ErrorMessage { get; set; }

		public double? PeakIntensityFromFirmware { get; set; }
		public double? SignalToNoiseRatioFromFirmware { get; set; }
        public IList<double> SpectrumWavelengths { get; set; }
        public IList<double> SpectrumIntensities { get; set; }
        public double? HorizontalBaselineIntensity { get; set; }
        public double? TangentialBaselineSlope { get; set; }
        public double? TangentialBaselineIntercept { get; set; }
        public double? PeakIntensityFromHorizontalBaseline { get; set; }
        public double? PeakIntensityFromTangentialBaseline { get; set; }
        public double? PeakWavelength { get; set; }
		public double? GlobalSignalToNoiseRatio { get; set; }
		public double? ShortTermNoise { get; set; }
		public double? TangentialSignalToNoiseRatio { get; set; }
        public string FlowCellType { get; set; }
    }
}