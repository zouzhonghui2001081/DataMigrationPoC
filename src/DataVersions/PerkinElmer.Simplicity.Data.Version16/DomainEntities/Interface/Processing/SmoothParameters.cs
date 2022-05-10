namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Processing
{
	public class SmoothParameters : ISmoothParameters
	{
		public SmoothType Function { get; set; }
		public int Width { get; set; }
		public int Passes { get; set; }
		public int Order { get; set; }
		public int Cycles { get; set; }
	}
}
