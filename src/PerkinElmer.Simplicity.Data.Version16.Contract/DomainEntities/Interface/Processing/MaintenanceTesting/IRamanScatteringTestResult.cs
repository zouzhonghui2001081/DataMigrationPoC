using System.Collections.Generic;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Processing.MaintenanceTesting
{
	public interface IRamanScatteringTestResult
	{
		bool IsDataValidForTest { get; set; }
		string ErrorMessage { get; set; }
        double? PeakIntensityFromFirmware { get; set; }
		double? SignalToNoiseRatioFromFirmware { get; set; }

        IList<double> SpectrumWavelengths { get; set; }
        IList<double> SpectrumIntensities { get; set; }
        double? HorizontalBaselineIntensity { get; set; }
        double? TangentialBaselineSlope { get; set; }
        double? TangentialBaselineIntercept { get; set; }
        double? PeakIntensityFromHorizontalBaseline { get; set; }
        double? PeakIntensityFromTangentialBaseline { get; set; }
        double? PeakWavelength { get; set; }
		double? GlobalSignalToNoiseRatio { get; set; }
		double? ShortTermNoise { get; set; }
        double? TangentialSignalToNoiseRatio { get; set; }
    }
}
