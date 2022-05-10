using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Processing;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Implementation.Processing
{
    internal class PdaParameters : IPdaParameters
    {
        public IPdaWavelengthMaxParameters WavelengthParameters { get; set; }
        public IPdaPeakPurityParameters PeakPurityParameters { get; set; }
        public IPdaStandardConfirmationParameters StandardConfirmationParameters { get; set; }
        public IPdaAbsorbanceRatioParameters AbsorbanceRatioParameters { get; set; }
        public IPdaBaselineCorrectionParameters BaselineCorrectionParameters { get; set; }
        public IPdaLibrarySearchParameters PeakLibrarySearchParameters { get; set; }
        public IPdaLibraryConfirmationParameters LibraryConfirmationParameters { get; set; }

        public object Clone()
        {
            var pdaParameter = (PdaParameters)MemberwiseClone();
            pdaParameter.WavelengthParameters = (IPdaWavelengthMaxParameters)WavelengthParameters?.Clone();
            pdaParameter.PeakPurityParameters = (IPdaPeakPurityParameters)PeakPurityParameters?.Clone();
            pdaParameter.AbsorbanceRatioParameters =(IPdaAbsorbanceRatioParameters)AbsorbanceRatioParameters?.Clone();
            pdaParameter.BaselineCorrectionParameters =(IPdaBaselineCorrectionParameters)BaselineCorrectionParameters?.Clone();
            pdaParameter.StandardConfirmationParameters =(IPdaStandardConfirmationParameters)StandardConfirmationParameters?.Clone();
            pdaParameter.PeakLibrarySearchParameters = (IPdaLibrarySearchParameters)PeakLibrarySearchParameters?.Clone();
            pdaParameter.LibraryConfirmationParameters = (IPdaLibraryConfirmationParameters)LibraryConfirmationParameters?.Clone();

            return pdaParameter;
        }

        public bool Equals(IPdaParameters other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return WavelengthParameters.Equals(other.WavelengthParameters) 
                   && PeakPurityParameters.Equals(other.PeakPurityParameters) 
                   && StandardConfirmationParameters.Equals(other.StandardConfirmationParameters) 
                   && AbsorbanceRatioParameters.Equals(other.AbsorbanceRatioParameters) 
                   && BaselineCorrectionParameters.Equals(other.BaselineCorrectionParameters) 
                   && PeakLibrarySearchParameters.Equals(other.PeakLibrarySearchParameters) 
                   && LibraryConfirmationParameters.Equals(other.LibraryConfirmationParameters);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((PdaParameters) obj);
        }
    }
}
