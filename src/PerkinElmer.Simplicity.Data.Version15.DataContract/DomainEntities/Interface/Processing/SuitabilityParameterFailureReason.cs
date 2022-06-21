namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Processing
{
    public enum SuitabilityParameterFailureReason
    {
        None = 0,
        CalculationDisabled = 1, //User specifically requested to not calculate a suitability parameter
        CompoundNotFound = 2, //Compound is marked as Suitability Compound, and Injection is marked as SST but the peak of Compound is not found in this injection
        NotApplicableForSelectedPharma = 3, // Compound not applicable for selected Pharma

        SigToNoiseWindowTooNarrow = 101,    //User specified noise window contains less than 3 X-Y points
        SigToNoiseCannotSelectNoiseWindowAutomatically = 102, //to display in both columns (S/N and Noise), this error can be combined with SigToNoiseWindowTooNarrow
        SigToNoiseNoiseIsZeroCannotCalculateNoiseRatio = 103, //to be displayed in S/N column
        SigToNoiseUserSpecifiedNoiseWindowIsBeyondTheInjectionEndTime = 104, // User specified Noise Window is completely outside the retention time range of the injection.
        
        TheorPlatesBadPeakAsymmetry = 201, //Peak Apex is in the same point as PeakStart => would need to divide by zero to calculate peak asymmetry 
        
        RelativeRetentionMissingReferencePeak = 301,    //Peak of interest is the first in chromatogram, cannot find previous peak
        RelativeRetentionReferencePeakCoincidesWithVoidTime = 302,     //Previous peak apex coincides with Void Time => would need to divide by zero
        RelativeRetentionZeroVoidTime = 303, 
        // This error is specifically requested to be returned by chemists, to draw attention that r coincides with RRT
        // which makes questionable the idea of calculating both these SST Parameters, so only one will be reported without error in this case. 
        
        RelativeRetentionReferencePeakWithZeroRetentionTime = 304,

        ResolutionNoAdjacentPeak = 401,    //Our peak is the first, cannot find previous peak

        TailingFactorZeroLeadingEdgeWidth = 501, //Peak "with tail of zero-width" => would need to divide by zero

        KPrimeZeroVoidTime = 601, //VoidTime==0 => would need to divide by zero

        SummaryInvalid = 701, //Summary Values (Average, SD, RSD) is invalid because at least one of elements being summed is invalid
        SummaryAverageIsZero = 702,   //Cannot calculate RSD
        SummaryNeedAtLeast2SamplesToCalculateStDev = 703,
    }
}