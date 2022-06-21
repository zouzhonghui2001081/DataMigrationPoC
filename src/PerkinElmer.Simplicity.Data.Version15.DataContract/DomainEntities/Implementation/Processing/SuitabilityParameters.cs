// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;
// using PerkinElmer.Domain.Contracts.Processing;
//
// namespace PerkinElmer.Domain.Implementation.Processing
// {
// 	public class SuitabilityParameters : ISuitabilityParameters
// 	{
// 		public object Clone()
// 		{
// 			var suitabilityParameters = new SuitabilityParameters()
// 			{
// 				AnalyzeMode = AnalyzeMode,
// 				SuitabilityLimits = SuitabilityLimits,
// 				SignalToNoiseEndTime = SignalToNoiseEndTime,
// 				SignalToNoiseStartTime = SignalToNoiseStartTime,
// 				TailingFactorCalculation = TailingFactorCalculation,
// 				ColumnLength = ColumnLength,
// 				NumberOfSigmas = NumberOfSigmas,
// 				EfficiencyReporting = EfficiencyReporting,
// 				ComplianceStandard = ComplianceStandard
// 			};
//
// 			return suitabilityParameters;
// 		}
//
// 		public SuitabilityComplianceStandard ComplianceStandard { get; set; }
// 		public EfficiencyReportingMode EfficiencyReporting { get; set; }
// 		public double ColumnLength { get; set; }
// 		public double SignalToNoiseStartTime { get; set; }
// 		public double SignalToNoiseEndTime { get; set; }
// 		public int NumberOfSigmas { get; set; }
// 		public AnalyzeMode AnalyzeMode { get; set; }
// 		public TailingFactorCalculationMode TailingFactorCalculation { get; set; }
// 		public ISuitabilityLimits SuitabilityLimits { get; set; }
//
// 	    public virtual bool Equals(ISuitabilityParameters other)
// 	    {
// 	        if (other == null) return false;
// 	        if (other.GetType() != GetType()) return false;
//
//             return ComplianceStandard == other.ComplianceStandard &&
// 	               EfficiencyReporting == other.EfficiencyReporting &&
// 	               ColumnLength.Equals(other.ColumnLength) &&
// 	               SignalToNoiseStartTime.Equals(other.SignalToNoiseStartTime) &&
// 	               SignalToNoiseEndTime.Equals(other.SignalToNoiseEndTime) &&
// 	               NumberOfSigmas == other.NumberOfSigmas &&
// 	               AnalyzeMode == other.AnalyzeMode &&
// 	               TailingFactorCalculation == other.TailingFactorCalculation &&
// 	               Equals(SuitabilityLimits, other.SuitabilityLimits);
//         }
//
// 	    public override bool Equals(object obj)
// 	    {
// 	        if (ReferenceEquals(null, obj)) return false;
// 	        if (ReferenceEquals(this, obj)) return true;
// 	        if (obj.GetType() != this.GetType()) return false;
// 	        return Equals((SuitabilityParameters) obj);
// 	    }
// 	}
// }
