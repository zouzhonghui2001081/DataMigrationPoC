namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Acquisition
{
	public enum SampleType
	{
        // TODO: List of samples to be defined and finalized
	    Unknown,
	    Standard,
		StandardReplace,
		BracketNonOverlapped,
		BracketOverlapped,
		QualityControl,
		Solvent,
	    DoubleBlank,
	    Blank,
        GrandAverage,
	    StandardCheck,
	    Spike
    }
}