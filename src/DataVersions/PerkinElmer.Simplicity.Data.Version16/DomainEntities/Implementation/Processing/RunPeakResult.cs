using System;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Processing;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Processing.Spectral;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Implementation.Processing
{
	internal class RunPeakResult : IRunPeakResult
	{
		public Guid PeakGuid { get; set; }
		public Guid BatchRunGuid { get; set; }
		public Guid BatchRunChannelGuid { get; set; }
		public Guid ProcessingMethodChannelGuid { get; set; }
		public Guid CompoundGuid { get; set; }
		public CompoundType CompoundType { get; set; }
	    public CompoundAssignmentType CompoundAssignmentType { get; set; }
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
        public string LibraryCompound { get; set; }
        public string SearchLibraryCompound { get; set; }
        public string HitQualityValue { get; set; }
        public string SearchMatch { get; set; }
        public string LibraryName { get; set; }
        public string SearchLibrary { get; set; }
        public Guid LibraryGuid { get; set; }
        public bool LibraryConfirmation { get; set; }
		public double SignalToNoiseRatio { get; set; }
		public double? Amount { get; set; }
		public AmountResultError AmountError { get; set; }
		public double? InternalStandardAmount { get; set; }
        public double? InternalStandardAmountRatio { get; set; }
	    public Guid ReferenceInternalStandardCompoundGuid { get; set; }
	    public double? AreaToAmountRatio { get; set; }
		public double? AreaToHeightRatio { get; set; }
		public BaselineCode BaselineCode { get; set; }
		public HittingCalibrationRange? CalibrationInRange { get; set; }
		public double? NormalizedAmount { get; set; }
		public double? RelativeRetentionTime { get; set; }
		public double? RawAmount { get; set; }
		public Guid PeakGroupGuid { get; set; }
		public string PeakName { get; set; }
		public bool Overlapped { get; set; }
		public bool IsBaselineExpo { get; set; }
		public double ExpoA { get; set; }
		public double ExpoB { get; set; }
		public double ExpoCorrection { get; set; }
		public double ExpoDecay { get; set; }
		public double ExpoHeight { get; set; }
		public Guid RetTimeReferenceGuid { get; set; }
		public Guid RrtReferenceGuid { get; set; }
		public Guid ReferenceInternalStandardPeakGuid { get; set; }
        public ISuitabilityResult SuitabilityResult { get; set; }
        public int MidIndex { get; set; }
		public int StartIndex { get; set; }
		public int StopIndex { get; set; }
	    public double? WavelengthMax { get; set; }
	    public double? AbsorbanceAtWavelengthMax { get; set; }
	    public WavelengthMaxError WavelengthMaxError { get; set; }
	    public double? PeakPurity { get; set; }
	    public bool PeakPurityPassed { get; set; }
	    public PeakPurityError PeakPurityError { get; set; }
        public double? StandardConfirmationIndex { get; set; }
        public bool StandardConfirmationPassed { get; set; }
        public StandardConfirmationError StandardConfirmationError { get; set; }
        public double? AbsorbanceRatio { get; set; }
	    public bool ModifiedByManualEvent { get; set; }
	    public string DataSourceType { get; set; }
        public AbsorbanceRatioError AbsorbanceRatioError { get; set; }

        public object Clone()
        {
            var runResult = new RunPeakResult
            {

                PeakGuid = PeakGuid,
                BatchRunGuid = BatchRunGuid,
                BatchRunChannelGuid = BatchRunChannelGuid,
                ProcessingMethodChannelGuid = ProcessingMethodChannelGuid,
                CompoundAssignmentType = CompoundAssignmentType,
                PeakName = PeakName,
                CompoundGuid = CompoundGuid,
                CompoundType = CompoundType,
                PeakNumber = PeakNumber,
                Area = Area,
                Height = Height,
                InternalStandardAreaRatio = InternalStandardAreaRatio,
                InternalStandardHeightRatio = InternalStandardHeightRatio,
                AreaPercent = AreaPercent,
                RetentionTime = RetentionTime,
                InternalStandardAmount = InternalStandardAmount,
                StartPeakTime = StartPeakTime,
                EndPeakTime = EndPeakTime,
                BaselineSlope = BaselineSlope,
                BaselineIntercept = BaselineIntercept,
                SignalToNoiseRatio = SignalToNoiseRatio,
                Amount = Amount,
                AmountError = AmountError,
                InternalStandardAmountRatio = InternalStandardAmountRatio,
                ReferenceInternalStandardCompoundGuid = ReferenceInternalStandardCompoundGuid,
                AreaToAmountRatio = AreaToAmountRatio,
                AreaToHeightRatio = AreaToHeightRatio,
                BaselineCode = BaselineCode,
                CalibrationInRange = CalibrationInRange,
                NormalizedAmount = NormalizedAmount,
                RelativeRetentionTime = RelativeRetentionTime,
                RawAmount = RawAmount,
                Overlapped = Overlapped,
                IsBaselineExpo = IsBaselineExpo,
                ExpoA = ExpoA,
                ExpoB = ExpoB,
                ExpoCorrection = ExpoCorrection,
                ExpoDecay = ExpoDecay,
                ExpoHeight = ExpoHeight,
                RetTimeReferenceGuid = RetTimeReferenceGuid,
                ReferenceInternalStandardPeakGuid = ReferenceInternalStandardPeakGuid,
                SuitabilityResult = (ISuitabilityResult) SuitabilityResult?.Clone(),
                MidIndex = MidIndex,
                StartIndex = StartIndex,
                StopIndex = StopIndex,
                WavelengthMax = WavelengthMax,
                AbsorbanceAtWavelengthMax = AbsorbanceAtWavelengthMax,
                WavelengthMaxError = WavelengthMaxError,
                PeakPurity = PeakPurity,
                PeakPurityPassed = PeakPurityPassed,
                PeakPurityError = PeakPurityError,
                AbsorbanceRatio = AbsorbanceRatio,
                DataSourceType = DataSourceType,
                AbsorbanceRatioError = AbsorbanceRatioError,
                LibraryConfirmation = LibraryConfirmation,
                HitQualityValue = HitQualityValue,
                LibraryCompound = LibraryCompound,
                SearchLibraryCompound =SearchLibraryCompound,
                SearchMatch = SearchMatch,
                LibraryName = LibraryName,
                SearchLibrary = SearchLibrary,
                LibraryGuid = LibraryGuid,
                PeakGroupGuid = PeakGroupGuid,
                RrtReferenceGuid = RrtReferenceGuid,
                StandardConfirmationIndex = StandardConfirmationIndex,
                StandardConfirmationPassed = StandardConfirmationPassed,
                StandardConfirmationError = StandardConfirmationError,
                ModifiedByManualEvent = ModifiedByManualEvent,
            };
            
            return runResult;
        }
    }
}
