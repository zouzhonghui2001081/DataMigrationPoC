namespace PerkinElmer.Simplicity.Data.Version15.DataAccess.Postgresql.Chromatography
{
    internal abstract class SequenceSampleDaoBase
    {
        internal static string IdColumn { get; } = "Id";
        internal static string GuidColumn { get; } = "Guid";
        internal static string SampleNameColumn { get; } = "SampleName";
        internal static string SelectedColumn { get; } = "Selected";
        internal static string SampleIdColumn { get; } = "SampleId";
        internal static string UserCommentsColumn { get; } = "UserComments";
        internal static string SampleTypeColumn { get; } = "SampleType";
        internal static string NumberOfRepeatsColumn { get; } = "NumberOfRepeats";
        internal static string LevelColumn { get; } = "Level";
        internal static string MultiplierColumn { get; } = "Multiplier";
        internal static string DivisorColumn { get; } = "Divisor";
        internal static string UnknownAmountAdjustmentColumn { get; } = "UnknownAmountAdjustment";
        internal static string InternalStandardAmountAdjustmentColumn { get; } = "InternalStandardAmountAdjustment";
        internal static string BaselineCorrectionColumn { get; } = "BaselineCorrection";
        internal static string BaselineRunIdColumn { get; } = "BaselineRunId"; // Is this still used?
        internal static string BaselineRunGuidColumn { get; } = "BaselineRunGuid";
        internal static string RackCodeColumn { get; } = "RackCode";
        internal static string RackPositionColumn { get; } = "RackPosition";
        internal static string PlateCodeColumn { get; } = "PlateCode";
        internal static string PlateCodeAsIntegerColumn { get; } = "PlateCodeAsInteger";
        internal static string PlateCodeAsIntegerDeviceNameColumn { get; } = "PlateCodeAsIntegerDeviceName";
        internal static string PlatePositionColumn { get; } = "PlatePosition";
        internal static string PlatePositionAsIntegerColumn { get; } = "PlatePositionAsInteger";
        internal static string PlatePositionAsIntegerDeviceNameColumn { get; } = "PlatePositionAsIntegerDeviceName";
        internal static string VialPositionColumn { get; } = "VialPosition";
        internal static string VialPositionAsIntegerColumn { get; } = "VialPositionAsInteger";
        internal static string VialPositionAsIntegerDeviceNameColumn { get; } = "VialPositionAsIntegerDeviceName";
        internal static string DestinationVialColumn { get; } = "DestinationVial";
        internal static string DestinationVialAsIntegerColumn { get; } = "DestinationVialAsInteger";
        internal static string DestinationVialAsIntegerDeviceNameColumn { get; } = "DestinationVialAsIntegerDeviceName";
        internal static string InjectionVolumeColumn { get; } = "InjectionVolume";
        internal static string InjectionVolumeDeviceNameColumn { get; } = "InjectionVolumeDeviceName";
        internal static string InjectionTypeColumn { get; } = "InjectionType";
	    internal static string InjectionPortAsIntegerColumn { get; } = "InjectionPortAsInteger";
	    internal static string InjectionPortAsIntegerDeviceNameColumn { get; } = "InjectionPortAsIntegerDeviceName";
	    internal static string InjectionPortColumn { get; } = "InjectionPort";
		internal static string AcquisitionMethodNameColumn { get; } = "AcquisitionMethodName";
        internal static string AcquisitionMethodVersionNumberColumn { get; } = "AcquisitionMethodVersionNumber";
        internal static string ProcessingMethodNameColumn { get; } = "ProcessingMethodName";
        internal static string ProcessingMethodVersionNumberColumn { get; } = "ProcessingMethodVersionNumber";
        internal static string CalibrationCurveNameColumn { get; } = "CalibrationCurveName";
	    internal static string InjectionTypeAsIntegerColumn { get; } = "InjectionTypeAsInteger";
	    internal static string InjectionTypeAsIntegerDeviceNameColumn { get; } = "InjectionTypeAsIntegerDeviceName";
	    internal static string SampleAmountColumn { get; } = "SampleAmount";
	    internal static string DilutionFactorColumn { get; } = "DilutionFactor";
	    internal static string AddendColumn { get; } = "Addend";
	    internal static string NormalizationFactorColumn { get; } = "NormalizationFactor";
        internal static string SampleReportTemplateColumn { get; } = "SampleReportTemplate";
        internal static string SummaryReportGroupColumn { get; } = "SummaryReportGroup";
	    internal static string StandardAmountAdjustmentColumn { get; } = "StandardAmountAdjustment";
	    internal static string SuitabilitySampleTypeColumn { get; } = "SuitabilitySampleType";
	}
}