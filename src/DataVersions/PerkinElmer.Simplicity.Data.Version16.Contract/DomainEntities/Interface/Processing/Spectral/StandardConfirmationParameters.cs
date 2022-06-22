namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Processing.Spectral
{
    public class StandardConfirmationParameters
    {
        public bool UseWavelengthLimits { get; set; }
        public double MinWavelength { get; set; }
        public double MaxWavelength { get; set; }

        public bool BaselineCorrected { get; set; }
        public double PassThreshold { get; set; }
        
        public bool AutoThreshold { get; set; }
        public bool AutoThresholdForStandard { get; set; }
        public double ManualThreshold { get; set; }
        public double ManualThresholdForStandard { get; set; }
        public int MinimumNumberOfDataPoints { get; set; }

    }
}