using System;
using PerkinElmer.Domain.Contracts.Acquisition;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Acquisition;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Implementation.Acquisition
{
    internal class SequenceSampleInfo : ISequenceSampleInfo
    {
        public SequenceSampleInfo()
        {
            Guid = Guid.NewGuid();
        }        

        public Guid Guid { get; set; }

        public string SampleName { get; set; }

        public bool Selected { get; set; }

        public string SampleId { get; set; }
        
        public uint Number { get; set; }

        public string UserComments { get; set; }

        public SampleType SampleType { get; set; }

        public int NumberOfRepeats { get; set; }

        public int Level { get; set; }

        public double SampleAmount { get; set; }

        public double Multiplier { get; set; }

        public double Divisor { get; set; }

        public double Addend { get; set; }

        public double DilutionFactor { get; set; }

        public double NormalizationFactor { get; set; }

        public double UnknownAmountAdjustment { get; set; }

        public double? InternalStandardAmountAdjustment { get; set; }

        public BaselineCorrection BaselineCorrection { get; set; }

        public Guid BaselineRunGuid { get; set; }

        public string RackCode { get; set; }

        public int RackPosition { get; set; }

        public string PlateCode { get; set; }

        public string PlatePosition { get; set; }

        public string VialPosition { get; set; }

        public string DestinationVial { get; set; }

        public IntValueWithDeviceName PlatePositionAsInteger { get; set; }
        public IntValueWithDeviceName PlateCodeAsInteger { get; set; }
        public IntValueWithDeviceName VialPositionAsInteger { get; set; }
        public IntValueWithDeviceName DestinationVialAsInteger { get; set; }
        public DoubleValueWithDeviceName InjectionVolume { get; set; }
        public string InjectionType { get; set; }
        public IntValueWithDeviceName InjectionTypeAsInteger { get; set; }

        public bool IsSelected { get; set; }

        //TODO: these two properties needs to be persisted 
        public IntValueWithDeviceName InjectionPortAsInteger { get; set; }
        public string InjectionPort { get; set; }

	    public string AcquisitionMethodName { get; set; }

        public int AcquisitionMethodVersionNumber { get; set; }
        public short AcquisitionMethodState { get; set; }
        public string ProcessingMethodName { get; set; }

        public int ProcessingMethodVersionNumber { get; set; }
        public short ProcessingMethodState { get; set; }
        //todo: mpcm delete this member
        public string CalibrationMethodName { get; set; }

        public int CalibrationBracket { get; set; }

        public double? StandardAmountAdjustment { get ; set; }

        public DateTime AcquiredDateTime { get ; set ; }
        public string SampleReportTemplate { get; set; }
        public string SummaryReportGroup { get; set; }

        public SuitabilitySampleType SuitabilitySampleType { get; set; }
    }
}
