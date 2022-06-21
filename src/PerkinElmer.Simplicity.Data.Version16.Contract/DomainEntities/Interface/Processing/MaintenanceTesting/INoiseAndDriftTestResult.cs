namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Processing.MaintenanceTesting
{
	public interface INoiseAndDriftTestResult
	{
		bool IsDataValidForTest { get; set; }
		string ErrorMessage { get; set; }

		double ShortTermNoise { get; set; }
		double LongTermNoise { get; set; }
		double Drift { get; set; }
		double Wanderation { get; set; }
	}
}
