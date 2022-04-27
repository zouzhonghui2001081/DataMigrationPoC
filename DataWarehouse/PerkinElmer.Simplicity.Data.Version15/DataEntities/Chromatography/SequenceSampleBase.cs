using System;

namespace PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography
{
    public class SequenceSampleBase
	{
		public long Id { get; set; }

		public Guid Guid { get; set; }
        public bool Selected { get; set; }
		public string SampleName { get; set; }

		public string SampleId { get; set; }

		public string UserComments { get; set; }

		public int SampleType { get; set; }

		public int NumberOfRepeats { get; set; }

		public int Level { get; set; }

		public double Multiplier { get; set; }

        public double Divisor { get; set; }

		public double UnknownAmountAdjustment { get; set; }

		public double? InternalStandardAmountAdjustment { get; set; }

		public int BaselineCorrection { get; set; }

		public Guid BaselineRunGuid { get; set; }

		public long BaselineRunId { get; set; }

		public string RackCode { get; set; }

		public int RackPosition { get; set; }

		public string PlateCode { get; set; }

        public int PlateCodeAsInteger { get; set; }

        public string PlateCodeAsIntegerDeviceName { get; set; }

        public string PlatePosition { get; set; }

        public int PlatePositionAsInteger { get; set; }

        public string PlatePositionAsIntegerDeviceName { get; set; }

		public string VialPosition { get; set; }

		public int VialPositionAsInteger { get; set; }

        public string VialPositionAsIntegerDeviceName { get; set; }

	    public string DestinationVial { get; set; }

	    public int DestinationVialAsInteger { get; set; }

	    public string DestinationVialAsIntegerDeviceName { get; set; }

        public double InjectionVolume { get; set; }

		public string InjectionVolumeDeviceName { get; set; }

		public string InjectionType { get; set; }

		public string AcquisitionMethodName { get; set; }

        public int AcquisitionMethodVersionNumber { get; set; }

        public string ProcessingMethodName { get; set; }

        public int ProcessingMethodVersionNumber { get; set; }

        public string CalibrationCurveName { get; set; }

		public int InjectionPortAsInteger { get; set; }

		public string InjectionPortAsIntegerDeviceName { get; set; }

		public string InjectionPort { get; set; }

		public int InjectionTypeAsInteger { get; set; }

		public string InjectionTypeAsIntegerDeviceName { get; set; }

		public double SampleAmount { get; set; }

		public double DilutionFactor { get; set; }

		public double Addend { get; set; }

		public double NormalizationFactor { get; set; }

		public double? StandardAmountAdjustment { get; set; }
        public string SampleReportTemplate { get; set; }
        public string SummaryReportGroup { get; set; }
        public short SuitabilitySampleType { get; set; }
	}
}
