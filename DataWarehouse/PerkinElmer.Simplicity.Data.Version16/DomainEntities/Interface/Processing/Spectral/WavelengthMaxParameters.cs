using System.Collections.Generic;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Processing.Spectral
{
	public class WavelengthMaxParameters : SpectralParametersBase
	{
	    public IList<double> Times { get; set; }
	}
}