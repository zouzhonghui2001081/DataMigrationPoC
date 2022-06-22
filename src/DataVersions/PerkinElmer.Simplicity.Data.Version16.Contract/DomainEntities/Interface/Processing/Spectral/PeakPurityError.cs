namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Processing.Spectral
{
    public enum PeakPurityError
    {
        None = SpectralError.None,
        TimeOutOf3DDataRange = SpectralError.TimeOutOf3DDataRange,
        BaselineTimeOutOf3DDataRange = SpectralError.BaselineTimeOutOf3DDataRange,
        AboveThreshold,
        TooLittleDataPoints,
		IncompatibleWavelengthRange,
        NotApplicable,
       
    }
}
