namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Processing
{
	public interface ISmoothParameters
    {
		SmoothType Function { get; set; }

		int Width { get; set; }     // number of points in smoothing function

		int Passes { get; set; }    // iterations

		int Order { get; set; }     // polynomial degree for Savitzky-Golay

		int Cycles { get; set; }    // number of sinusoid periods in a wavelet
	}
}