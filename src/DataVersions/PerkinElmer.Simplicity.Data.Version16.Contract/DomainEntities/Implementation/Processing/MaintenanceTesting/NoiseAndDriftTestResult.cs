using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Processing.MaintenanceTesting;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Implementation.Processing.MaintenanceTesting
{
	public class NoiseAndDriftTestResult : INoiseAndDriftTestResult
	{
		public bool IsDataValidForTest { get; set; }
		public string ErrorMessage { get; set; }

		public double ShortTermNoise { get; set; }
		public double LongTermNoise { get; set; }
		public double Drift { get; set; }
		public double Wanderation { get; set; }
	}
}
