namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Processing
{
	public enum SmoothType
	{
		None,		
		Mean,		// mean == boxcar
		Gaussian,	// Gaussian
		SavGolay,	// Savitzky-Golay
		//Wavelet,  // not for GC
		//LowPass,  // not for GC
		//Envelope, // not for GC
		Median		// Median
	}
}
