namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Processing
{
	public enum BaselineCorrectionType
	{
		None = 0,
		PeakStart = 1,
		SelectedSpectrum = 2,
		AverageOfRange = 3,
		InterpolatedPeakStartAndEnd = 4,
		InterpolatedSelectedPoints = 5
	}
}