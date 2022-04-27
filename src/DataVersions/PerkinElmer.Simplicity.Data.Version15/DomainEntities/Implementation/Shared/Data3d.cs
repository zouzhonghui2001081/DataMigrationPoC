using System.Collections.Generic;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.Implementation.Shared
{
	public class Data3d : IData3d
	{
		public IList<double> Times { get; set; }
		public IList<double> Wavelengths { get; set; }
		public IList<IList<double>> Intensities { get; set; }
	}
}