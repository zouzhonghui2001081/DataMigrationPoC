namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Processing
{
	public class SpectrumBaselineCorrectionParameters
    {
        private const int ConvertToSeconds = 60;
        public SpectrumBaselineCorrectionParameters()
        {
            BaselineCorrection = BaselineCorrectionType.PeakStart;
        }

	    public SpectrumBaselineCorrectionParameters(IPdaBaselineCorrectionParameters parameters) : this()
	    {
	        if (parameters != null)
	        {
	            BaselineCorrection = parameters.CorrectionType;

	            switch (parameters.CorrectionType)
	            {
	                case BaselineCorrectionType.SelectedSpectrum:
	                    StartTimeOfBaselineSpectrum = parameters.SelectedSpectrumTimeInSeconds * ConvertToSeconds ?? 0;
	                    break;
	                case BaselineCorrectionType.AverageOfRange:
	                    StartTimeOfBaselineSpectrum = parameters.RangeStartInSeconds *ConvertToSeconds ?? 0;
	                    EndTimeOfBaselineSpectrum = parameters.RangeEndInSeconds* ConvertToSeconds ?? 0;
	                    break;
	            }
	        }
	    }

        public BaselineCorrectionType BaselineCorrection { get; set; }
		//peak start - time (double) - use StartTimeOfBaselineSpectrum
		//selectedSpectrum - time (double) - use StartTimeOfBaselineSpectrum
		//average of range - time start, end (double) - use StartTimeOfBaselineSpectrum, EndTimeOfBaselineSpectrum
		//interpolate peak start and end - use StartTimeOfBaselineSpectrum, EndTimeOfBaselineSpectrum
		//interpolate between selected points - use StartTimeOfBaselineSpectrum, EndTimeOfBaselineSpectrum
		public double StartTimeOfBaselineSpectrum { get; set; }
		public double EndTimeOfBaselineSpectrum { get; set; }
	}
}