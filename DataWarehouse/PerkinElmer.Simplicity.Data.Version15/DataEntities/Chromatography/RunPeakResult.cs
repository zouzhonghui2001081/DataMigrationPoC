using System;

namespace PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography
{
    public class RunPeakResult
    {
        public long Id { get; set; }

        public long CalculatedChannelDataId { get; set; }

        //public long CompoundId { get; set; }

        public Guid BatchRunChannelGuid { get; set; }

        public Guid CompoundGuid { get; set; }

        public Guid ChannelGuid { get; set; }

        public int PeakNumber { get; set; }

        public double Area { get; set; }

        public double Height { get; set; }

        public double? InternalStandardAreaRatio { get; set; }

        public double? InternalStandardHeightRatio { get; set; }

        public double AreaPercent { get; set; }

        public double RetentionTime { get; set; }

        public double StartPeakTime { get; set; }

        public double EndPeakTime { get; set; }

        public double BaselineSlope { get; set; }

        public double BaselineIntercept { get; set; }

        public double SignalToNoiseRatio { get; set; }

        public double? Amount { get; set; }

        public double? InternalStandardAmountRatio { get; set; }

        public double? AreaToAmountRatio { get; set; }

        public double? AreaToHeightRatio { get; set; }

        public int BaselineCode { get; set; }

        public int? CalibrationInRange { get; set; }

        public Guid PeakGroupGuid { get; set; }

        public string PeakName { get; set; }

        public string PeakGroup { get; set; }

        public bool Overlapped { get; set; }

        public bool IsBaselineExpo { get; set; }

        public double ExpoA { get; set; }

        public double ExpoB { get; set; }

        public double ExpoCorrection { get; set; }

        public double ExpoDecay { get; set; }

        public Guid RetTimeReferenceGuid { get; set; }

        public Guid RrtReferenceGuid { get; set; }

        public Guid InternalStandardGuid { get; set; }

        public double KPrime { get; set; }

        public double? NormalizedAmount { get; set; }

        public double? RelativeRetentionTime { get; set; }

        public double? RawAmount { get; set; }

        public double TailingFactor { get; set; }

        public double Resolution { get; set; }

        public double PeakWidth { get; set; }

        public double PeakWidthAtHalfHeight { get; set; }
        public double PlatesDorseyFoley { get; set; }
        public double PlatesTangential { get; set; }
        public double PeakWidthAt5PercentHeight { get; set; }
        public double PeakWidthAt10PercentHeight { get; set; }
        public double RelativeRetTimeSuit { get; set; }
        public double Signal { get; set; }
        public double ExpoHeight { get; set; }
        public Guid PeakGuid { get; set; }
        public double? InternalStandardAmount { get; set; }
        public Guid ReferenceInternalStandardCompoundGuid { get; set; }
        public int AmountError { get; set; }
        public short CompoundType { get; set; }
        public double? AbsorbanceRatio { get; set; }
        public double? StandardConfirmationIndex { get; set; }
        public bool StandardConfirmationPassed { get; set; }
        public short StandardConfirmationError { get; set; }
        public double? WavelengthMax { get; set; }
        public double? AbsorbanceAtWavelengthMax { get; set; }
        public int WavelengthMaxError { get; set; }
        public double? PeakPurity { get; set; }
        public bool PeakPurityPassed { get; set; }
        public int PeakPurityError { get; set; }
	    public string DataSourceType { get; set; }
	    public int AbsorbanceRatioError { get; set; }
        public bool ManuallyOverriden { get; set; }
	    public int MidIndex { get; set; }
	    public int StartIndex { get; set; }
	    public int StopIndex { get; set; }
	    public string LibraryCompound { get; set; }
        public string SearchLibraryCompound { get; set; }

        public string HitQualityValue { get; set; }
        public string SearchMatch { get; set; }
	    public string LibraryName { get; set; }
        public string SearchLibrary { get; set; }
	    public Guid LibraryGuid { get; set; }
	    public bool LibraryConfirmation { get; set; }
	    public short CompoundAssignmentType { get; set; }
    }
}
