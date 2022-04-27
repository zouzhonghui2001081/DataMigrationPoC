using System.Collections.Generic;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Processing.Spectral
{
	public class PeakPurityParameters : SpectralParametersBase
	{
	    public IList<(bool Applicable, int UpslopeIndex, int DownslopeIndex)> SlopeIndexes { get; set; }
    }
}