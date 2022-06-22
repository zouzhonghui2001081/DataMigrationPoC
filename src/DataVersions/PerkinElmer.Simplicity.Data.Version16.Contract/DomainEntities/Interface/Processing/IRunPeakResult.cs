using System;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Processing.Spectral;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Processing
{
    public interface IRunPeakResult : ICloneable
	{
		Guid PeakGuid { get; set; }				// to identify this peak; used by suitability object; not persistent in DB

		[Obsolete] Guid BatchRunGuid { get; set; }         // from OriginalBatchRun in IVirtualBatchRun

		Guid BatchRunChannelGuid { get; set; } // this is the Chromatogram Guid, replaces the pair of obsolete (BatchRunGuid, ChannelIndex)
        
		Guid CompoundGuid { get; set; }         // from ICompound in processing method

		CompoundType CompoundType { get; set; }

	    CompoundAssignmentType CompoundAssignmentType { get; set; }

	    int PeakNumber { get; set; }				// from array

		string PeakName { get; set; }               //from PeaksToBeNamed and INovaPeakInfo.Name

		double Area { get; set; }               //INovaPeakInfo.Area

		double Height { get; set; }                           //INovaPeakInfo.Height

		double? InternalStandardAreaRatio { get; set; }		//Quant

		double? InternalStandardHeightRatio { get; set; }        //Quant

	    double? InternalStandardAmount { get; set; } // from compound calibration parameters

	    double? InternalStandardAmountRatio { get; set; }    //Quant

        double AreaPercent { get; set; }        //Quant

		double RetentionTime { get; set; }      //INovaPeakInfo.ApexXValue

		double StartPeakTime { get; set; }			//StartXValue

		double EndPeakTime { get; set; }			//EndXValue

#region ForGraphics Drawing only
		bool Overlapped { get; set; }//INovaPeakInfo.Overlapped

		bool IsBaselineExpo { get; set; }//INovaPeakInfo.

		double BaselineSlope { get; set; }//INovaPeakInfo.

		double BaselineIntercept { get; set; }//INovaPeakInfo.

		double ExpoA { get; set; }//INovaPeakInfo.ExpoA

		double ExpoB { get; set; }//INovaPeakInfo.ExpoB

		double ExpoCorrection { get; set; }//INovaPeakInfo.ExpoCorr

		double ExpoDecay { get; set; }//INovaPeakInfo.ExpoDecay

		double ExpoHeight { get; set; } // INovaPeakInfo.ExpoHeight
#endregion ForGraphics Drawing only

		double SignalToNoiseRatio { get; set; }//INovaPeakInfo.SignalNoiseRatio

		double? Amount { get; set; }        //from Quant-apply calibration

		AmountResultError AmountError { get; set; }

	    Guid ReferenceInternalStandardPeakGuid { get; set; }//from procmethod

        Guid ReferenceInternalStandardCompoundGuid { get; set; } 

        double? AreaToAmountRatio { get; set; }    //Quant

		double? AreaToHeightRatio { get; set; }    //INovaPeakInfo.AreaHeightRatio

        BaselineCode BaselineCode { get; set; }//INovaPeakInfo.BaselineCode

        string LibraryCompound { get; set; }
        string SearchLibraryCompound { get; set; }
        string HitQualityValue { get; set; }
        string SearchMatch { get; set; }
        string LibraryName { get; set; }

        string SearchLibrary { get; set; }
		Guid LibraryGuid { get; set; }
        bool LibraryConfirmation { get; set; }
		HittingCalibrationRange? CalibrationInRange { get; set; }//INovaPeakInfo.CalibrationInRange

		double? NormalizedAmount { get; set; }     //Quant

		double? RelativeRetentionTime { get; set; }    //Quant

		double? RawAmount { get; set; }    //INovaPeakInfo.RawAmount

		Guid RetTimeReferenceGuid { get; set; }//from procmethod
		Guid RrtReferenceGuid { get; set; }     //from procmethod

		//ISuitabilityResult SuitabilityResult { get; set; }

#region Suitability inputs
		int MidIndex { get; set; }//INovaPeakInfo.MidIndex
		int StartIndex { get; set; }//INovaPeakInfo.StartIndex
		int StopIndex { get; set; }//INovaPeakInfo.StopIndex
#endregion Suitability inputs

        double? WavelengthMax { get; set; }
	    double? AbsorbanceAtWavelengthMax { get; set; }
	    WavelengthMaxError WavelengthMaxError { get; set; }

	    double? PeakPurity { get; set; }
	    bool PeakPurityPassed { get; set; }
        PeakPurityError PeakPurityError { get; set; }

        double? StandardConfirmationIndex { get; set; }
        bool StandardConfirmationPassed { get; set; }
        StandardConfirmationError StandardConfirmationError { get; set; }

        double? AbsorbanceRatio { get; set; }
		AbsorbanceRatioError AbsorbanceRatioError { get; set; }

	    bool ModifiedByManualEvent { get; set; }

		string DataSourceType { get; set; }
	}
}