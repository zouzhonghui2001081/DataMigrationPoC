namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Processing.Spectral
{
    public enum AbsorbanceRatioError
    {
        None,
        TimeOutOf3DDataRange,
        BaselineTimeOutOf3DDataRange,
        BelowThreshold,
        AbsorbanceBIsZero //only possible when threshold is zero
    }
}