namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Processing
{
	public class SuitabilityLimit
	{
		public bool IsUsed { get; set; }
		public double LowerLimit { get; set; }
		public double UpperLimit { get; set; }
		public double RelativeStDevPercent { get; set; }
	}
}