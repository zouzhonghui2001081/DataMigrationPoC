namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Processing.Spectral
{
    public class SpectrumMatchParameters
    {
        public bool UseWavelengthLimits { get; set; }
        public double MinWavelength { get; set; }
        public double MaxWavelength { get; set; }

        public bool UseRetentionTime { get; set; }
        public double RetentionTimeTolerance { get; set; }

        public bool BaselineCorrected { get; set; } //Affects both library spectrum and our spectrum under test

        public double HitDistanceThreshold { get; set; } // Hits that are farther than this distance will not be returned
        public int MaxNumberOfResults { get; set; }      // Only this number of closest hits will be returned
    }
}