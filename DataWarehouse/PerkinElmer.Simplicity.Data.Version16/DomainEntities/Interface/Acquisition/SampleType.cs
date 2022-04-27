﻿namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Acquisition
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