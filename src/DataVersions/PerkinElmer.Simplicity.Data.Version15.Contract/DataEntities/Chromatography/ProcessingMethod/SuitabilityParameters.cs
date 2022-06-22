
namespace PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography.ProcessingMethod
{
    public class SuitabilityParameters
	{
		public long Id { get; set; }
		public long ChannelMethodId { get; set; }
		public short ComplianceStandard { get; set; }
		public short EfficiencyReporting { get; set; }
		public double ColumnLength { get; set; }
		public double SignalToNoiseStartTime { get; set; }
		public double SignalToNoiseEndTime { get; set; }
		public int NumberOfSigmas { get; set; }
		public short AnalyzeMode { get; set; }
		public short TailingFactorCalculation { get; set; }
		public bool AreaLimitIsUsed { get; set; }
		public double AreaLimitLowerLimit { get; set; }
		public double AreaLimitUpperLimit { get; set; }
		public double AreaLimitRelativeStDevPercent { get; set; }
		public bool HeightLimitIsUsed { get; set; }
		public double HeightLimitLowerLimit { get; set; }
		public double HeightLimitUpperLimit { get; set; }
		public double HeightLimitRelativeStDevPercent { get; set; }
		public bool NTanLimitIsUsed { get; set; }
		public double NTanLimitLowerLimit { get; set; }
		public double NTanLimitUpperLimit { get; set; }
		public double NTanLimitRelativeStDevPercent { get; set; }
		public bool NFoleyLimitIsUsed { get; set; }
		public double NFoleyLimitLowerLimit { get; set; }
		public double NFoleyLimitUpperLimit { get; set; }
		public double NFoleyLimitRelativeStDevPercent { get; set; }
		public bool TailingFactorSymmetryLimitIsUsed { get; set; }
		public double TailingFactorSymmetryLimitLowerLimit { get; set; }
		public double TailingFactorSymmetryLimitUpperLimit { get; set; }
		public double TailingFactorSymmetryLimitRelativeStDevPercent { get; set; }
		public bool UspResolutionLimitIsUsed { get; set; }
		public double UspResolutionLimitLowerLimit { get; set; }
		public double UspResolutionLimitUpperLimit { get; set; }
		public double UspResolutionLimitRelativeStDevPercent { get; set; }
		public bool KPrimeLimitIsUsed { get; set; }
		public double KPrimeLimitLowerLimit { get; set; }
		public double KPrimeLimitUpperLimit { get; set; }
		public double KPrimeLimitRelativeStDevPercent { get; set; }
		public bool ResolutionLimitIsUsed { get; set; }
		public double ResolutionLimitLowerLimit { get; set; }
		public double ResolutionLimitUpperLimit { get; set; }
		public double ResolutionLimitRelativeStDevPercent { get; set; }
		public bool AlphaLimitIsUsed { get; set; }
		public double AlphaLimitLowerLimit { get; set; }
		public double AlphaLimitUpperLimit { get; set; }
		public double AlphaLimitRelativeStDevPercent { get; set; }
		public bool SignalToNoiseLimitIsUsed { get; set; }
		public double SignalToNoiseLimitLowerLimit { get; set; }
		public double SignalToNoiseLimitUpperLimit { get; set; }
		public double SignalToNoiseLimitRelativeStDevPercent { get; set; }
		public bool PeakWidthLimitIsUsed { get; set; }
		public double PeakWidthLimitLowerLimit { get; set; }
		public double PeakWidthLimitUpperLimit { get; set; }
		public double PeakWidthLimitRelativeStDevPercent { get; set; }
	}
}
