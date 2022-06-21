using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Processing;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Implementation.Processing
{
	internal class SmoothParameters : ISmoothParameters
	{
		public SmoothType Function { get; set; }
		public int Width { get; set; }
		public int Passes { get; set; }
		public int Order { get; set; }
		public int Cycles { get; set; }
	}
}
