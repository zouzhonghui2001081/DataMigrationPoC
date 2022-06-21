using System;
using System.Collections.Generic;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Processing
{
	public interface IPdaParameters: ICloneable, IEquatable<IPdaParameters>
    {
		IPdaWavelengthMaxParameters	WavelengthParameters { get; set; }
		IPdaPeakPurityParameters	PeakPurityParameters { get; set; }
        IPdaStandardConfirmationParameters StandardConfirmationParameters { get; set; }
        IPdaAbsorbanceRatioParameters AbsorbanceRatioParameters { get; set; }
        IPdaBaselineCorrectionParameters BaselineCorrectionParameters { get; set; }
        IPdaLibrarySearchParameters PeakLibrarySearchParameters { get; set; }
        IPdaLibraryConfirmationParameters LibraryConfirmationParameters { get; set; }
    }


	public interface IPdaLibrarySearchParameters : IEquatable<IPdaLibrarySearchParameters>, ICloneable
	{
		double MinimumWavelength { get; set; }
		double MaximumWavelength { get; set; }
		double MatchRetentionTimeWindow { get; set; }
		bool IsMatchRetentionTimeWindowEnabled { get; set; }
		bool IsBaselineCorrectionEnabled { get; set; }
		double HitDistanceThreshold { get; set; }
		bool IsPeakLibrarySearch { get; set; }
		IList<string> SelectedLibraries { get; set; }
        bool UseWavelengthLimits { get; set; }
		int MaxNumberOfResults { get; set; }
	}

	public interface IPdaWavelengthMaxParameters: IEquatable<IPdaWavelengthMaxParameters>,ICloneable
    {
		double MinWavelength { get; set; }
		double MaxWavelength { get; set; }
		bool ApplyBaselineCorrection { get; set; }
		bool UseAutoAbsorbanceThreshold { get; set; }
		double ManualAbsorbanceThreshold { get; set; }
	}

    public interface IPdaPeakPurityParameters: IEquatable<IPdaPeakPurityParameters>,ICloneable
    {
		double MinWavelength { get; set; }
		double MaxWavelength { get; set; }
		int MinimumDataPoints { get; set; }
		bool ApplyBaselineCorrection { get; set; }
		double PurityLimit { get; set; }
		double PercentOfPeakHeightForSpectra { get; set; }
		bool UseAutoAbsorbanceThreshold { get; set; }
		double ManualAbsorbanceThreshold { get; set; }
	}

    public interface IPdaLibraryConfirmationParameters : IEquatable<IPdaLibraryConfirmationParameters>, ICloneable
    {
        double MinimumWavelength { get; set; }
        double MaximumWavelength { get; set; }
        bool IsBaselineCorrectionEnabled { get; set; }
        double HitDistanceThreshold { get; set; }
	    IList<string> SelectedLibraries { get; set; }
    }

}