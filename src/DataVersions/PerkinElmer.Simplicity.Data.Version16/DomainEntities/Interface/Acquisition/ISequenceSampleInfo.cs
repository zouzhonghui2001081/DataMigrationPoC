using System;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Acquisition
{
	public interface ISequenceSampleInfo
	{
		Guid Guid { get; set; }

		string SampleName { get; set; }
	    bool Selected { get; set; }
        string SampleId { get; set; }
        
		string UserComments { get; set; }

		SampleType SampleType { get; set; }

		int NumberOfRepeats { get; set; }

		int Level { get; set; }

        double SampleAmount { get; set; }

		double Multiplier { get; set; }

		double Divisor { get; set; } 

        double Addend { get; set; }

        double DilutionFactor { get; set; }

        double NormalizationFactor { get; set; }

        double UnknownAmountAdjustment { get; set; }

        double? InternalStandardAmountAdjustment { get; set; }

        double? StandardAmountAdjustment { get; set; }

        BaselineCorrection BaselineCorrection { get; set; }

		// For internal correction it should be SequenceSample guid,
		// for external correction it should be BatchRun guid,
		// if no correction used it should be empty guid
		Guid BaselineRunGuid { get; set; }

		// TODO: Driver specific properties has to be defined as expandable array of properties

		string RackCode { get; set; }

		int RackPosition { get; set; }

		string PlateCode { get; set; }
	    IntValueWithDeviceName PlateCodeAsInteger { get; set; }

		string PlatePosition { get; set; }

        IntValueWithDeviceName PlatePositionAsInteger { get; set; }

	    IntValueWithDeviceName DestinationVialAsInteger { get; set; }

	    string DestinationVial { get; set; }

        IntValueWithDeviceName VialPositionAsInteger { get; set; }

        string VialPosition { get; set; }

        DoubleValueWithDeviceName InjectionVolume { get; set; }

        IntValueWithDeviceName InjectionTypeAsInteger { get; set; }
        string InjectionType { get; set; }

	    IntValueWithDeviceName InjectionPortAsInteger { get; set; }
	    string InjectionPort { get; set; }

        bool IsSelected { get; set; } //???

		//TODO: do we need selector for PortA / PortB here?

		string AcquisitionMethodName { get; set; }

        int AcquisitionMethodVersionNumber { get; set; }
        short AcquisitionMethodState { get; set; }
        string ProcessingMethodName { get; set; }

        int ProcessingMethodVersionNumber { get; set; }

        short ProcessingMethodState { get; set; }

        //todo: mpcm delete this member
        string CalibrationMethodName { get; set; }

        int CalibrationBracket { get; set; }

        DateTime AcquiredDateTime { get; set; }

        string SampleReportTemplate { get; set; }
        string SummaryReportGroup { get; set; }

        SuitabilitySampleType SuitabilitySampleType { get; set; }
    }
}