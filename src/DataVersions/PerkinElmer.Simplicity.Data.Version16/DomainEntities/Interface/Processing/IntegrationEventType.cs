namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Processing
{
	public enum IntegrationEventType
	{

        //Timed Events
	    BunchingFactorEvent = 1, //BF
	    NoiseThresholdEvent = 2, //NT
	    AreaThresholdEvent = 3, //AT

	    DisablePeakDetection = 4, //-P, +P
	    InhibitEndOfPeakDetection = 6, //+I

	    EnableNegativePeakDetection = 5, //+N, -N
	    ForceStartOfNewPeak = 9, //S
	    ForceEndOfPeak = 8, //E
	    ForceBaseline = 10, //BL
	    ForceCommonBaseline = 7, //+CB, -CB
	    ForceHorizontalForwardBaseline = 11, //+HF, -HF
	    ForceHorizontalBackwardBaseline = 12, //HR
	    EnableValleyToValleyBaseline = 13, //+V, -V
	    ForceExponentialSkim = 14, //+X
	    PreventExponentialSkim = -14, //-X
	    EnableTangentialSkim = 15, //T

	   // EnableNonForcedCommonBaseline = 7,  // +CB

	    LocateRetentionTimeatMaximum = 19, //LM
	    ForceRetentionTime = 18, //RT
	    EnableSmoothPeakEnds = 17, //+SM, -SM
		EnableManualIntegration = 16, //+M, -M
		UserForced = 20, //+UF, -UF

	    // Parameters
	    BunchingFactorParameter = 200,
	    NoiseThresholdParameter = 201,
	    AreaThresholdParameter = 202,
	    VoidTime = 203,
	    UnidentifiedPeakQuantCompound = 204,
	    TimeAdjustment = 205,
	    UnretainedPeakTime = 206,
	    RrtReferenceCompound = 207,
	    WidthRatio = 208,
	    ValleyToPeakRatio = 209,
	    PeakHeightRatio = 210,
	    AdjustedHeightRatio = 213,
	    ValleyHeightRatio = 214,
	    SmoothFunction = 215,
	    SmoothWidth = 216,     // number of points in smoothing function
	    SmoothPasses = 217,    // iterations
	    SmoothOrder = 218,     // polynomial degree for Savitzky-Golay
	    SmoothCycles = 219    // number of sinusoid periods in a wavelet

    }
}