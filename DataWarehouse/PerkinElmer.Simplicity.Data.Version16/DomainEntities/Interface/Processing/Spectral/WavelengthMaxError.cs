namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Processing.Spectral
{
    public enum WavelengthMaxError
    {
        None = SpectralError.None,
        TimeOutOf3DDataRange = SpectralError.TimeOutOf3DDataRange,
        BaselineTimeOutOf3DDataRange = SpectralError.BaselineTimeOutOf3DDataRange,
		NoDataPointsAboveThreshold,
	    IncompatibleWavelengthRange
    }
}
