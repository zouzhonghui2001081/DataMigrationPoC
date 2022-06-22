namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Processing
{
	// Constraint: The int values must coincide with AlgoSuitabilityParameter int values.	
	public enum SuitabilityParameter
	{
		Area = 0,
		Height = 1,
		TheoreticalPlatesN = 2,
		TheoreticalPlatesNTangential = 3,
		TailingFactor = 4,
		RelativeRetention = 5,
		CapacityFactor = 6, //aka KPrime
		Resolution = 7,
		UspResolution = 8,
        SignalToNoise = 9,
        // Peak Widths are here in case in future they will have Limits. Currently not used.
        PeakWidthAtBase = 10, 
        PeakWidthAt5Pct = 11,
        PeakWidthAt10Pct = 12,
        PeakWidthAt50Pct = 13,
        
        RelativeRetentionTime = 14,
        RetentionTime = 15,
    }
}