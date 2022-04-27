using System.Collections.Generic;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Processing.Spectral
{
	public class WavelengthMaxParameters : SpectralParametersBase
	{
	    public IList<double> Times { get; set; }
	}
}