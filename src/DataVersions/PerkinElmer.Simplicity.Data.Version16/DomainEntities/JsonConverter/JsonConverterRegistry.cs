using System;
using System.Collections.Generic;
using PerkinElmer.Acquisition.Devices;
using PerkinElmer.Domain.Contracts.Acquisition;
using PerkinElmer.Domain.Contracts.Processing;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Implementation.Shared;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Acquisition;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.LabManagement;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Processing;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Processing.CompoundLibrary;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Reporting;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Shared;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.JsonConverter.Acquisition;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.JsonConverter.LabManagement;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.JsonConverter.Processing;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.JsonConverter.Processing.CompoundLibrary;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.JsonConverter.Reporting;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.JsonConverter.Shared;
using IDeviceMethod = PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Acquisition.IDeviceMethod;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.JsonConverter
{
    internal static class JsonConverterRegistry
	{
		private static readonly IDictionary<Type, object> JsonConverters = new Dictionary<Type, object>
		{
			{typeof(IAcquisitionMethod), new AcquisitionMethodJsonConverter()},
            {typeof(IAcquisitionMethodInfo), new AcquisitionMethodInfoJsonConverter()},
            {typeof(IDeviceMethod), new DeviceMethodJsonConverter()},
            {typeof(IUserInfo), new UserInfoJsonConverter()},
            {typeof(IBatchResultSet), new BatchResultSetJsonConverter()},
            {typeof(IBatchResultSetInfo), new BatchResultSetInfoJsonConverter()},
            {typeof(IBatchRunWithRawData), new BatchRunWithRawDataJsonConverter()},
            {typeof(IStreamData), new StreamDataJsonConverter()},
            {typeof(IStreamDataInfo), new StreamDataInfoJsonConverter()},
            {typeof(IProcessingMethod), new ProcessingMethodJsonConverter()},
            {typeof(IProcessingMethodInfo), new ProcessingMethodInfoJsonConverter()},
            {typeof(IProcessingDeviceMethod), new ProcessingDeviceMethodJsonConverter()},
            {typeof(IProcessingDeviceMetaData), new ProcessingDeviceMetaDataJsonConverter()},
            {typeof(ISpectrumMethod), new SpectrumMethodJsonConverter()},
            {typeof(IBatchRunInfo), new BatchRunInfoJsonConverter()},
            {typeof(IDeviceIdentifier), new DeviceIdentifierJsonConverter()},
            {typeof(IChannelIdentifier1), new ChannelIdentifier1JsonConverter()},
            {typeof(IDeviceChannelIdentifier), new DeviceChannelIdentifierJsonConverter()},
            {typeof(IDeviceChannelDescriptor), new DeviceChannelDescriptorJsonConverter()},
            {typeof(IFLChannelMetaData), new FlChannelMetaDataJsonConverter()},
            {typeof(IFluorescenceSpectrumChannelMetaData), new FluorescenceSpectrumChannelMetaDataJsonConverter()},
            {typeof(ITemperatureChannelMetaData), new TemperatureChannelMetaDataJsonConverter()},
            {typeof(IPumpFlowChannelMetaData), new PumpFlowChannelMetaDataJsonConverter()},
            {typeof(ISolventProportionChannelMetaData), new SolventProportionChannelMetaDataJsonConverter()},
            {typeof(IPressureChannelMetaData), new PressureInBarChannelMetaDataJsonConverter()},
            {typeof(IGCChannelMetaData), new GcChannelMetaDataJsonConverter()},
            {typeof(IUVChannelMetaData), new UvChannelMetaDataJsonConverter()},
            {typeof(IMultiUVChannelMetaData), new MultiUvChannelMetaDataJsonConverter()},
            {typeof(IPdaChannelMetaData), new PdaChannelMetaDataJsonConverter()},
            {typeof(IRIChannelMetaData), new RiChannelMetaDataJsonConverter()},
            {typeof(IAToDChannelMetaData), new AToDChannelMetaDataJSonConverter()},
            {typeof(IChromatographicChannelDescriptor), new ChromatographicChannelDescriptorJsonConverter()},

            {typeof(MultiUvProcessingMethodDataChannelMetaData), new MultiUvProcessingMethodDataChannelMetaDataJsonConverter()},
            {typeof(FLProcessingMethodDataChannelMetaData), new FLProcessingMethodDataChannelMetaDataJSonConverter()},
            {typeof(UVProcessingMethodDataChannelMetaData), new UVProcessingMethodDataChannelMetaDataJsonConverter()},
			{typeof(PdaExtractedProcessingMethodDataChannelMetaData), new PdaExtractedProcessingMethodDataChannelMetaDataJsonConverter()},
			{typeof(PdaApexOptimizedProcessingMethodDataChannelMetaData), new PdaApexOptimizedProcessingMethodDataChannelMetaDataJsonConverter()},
			{typeof(RiProcessingMethodDataChannelMetaData), new RiProcessingMethodDataChannelMetaDataJsonConverter()},
			{typeof(GCProcessingMethodDataChannelMetaData), new GCProcessingMethodDataChannelMetaDataJsonConverter()},
            {typeof(AToDProcessingMethodDataChannelMetaData), new AToDProcessingMethodDataChannelMetaDataJsonConverter()},
            {typeof(PdaMicProcessingMethodDataChannelMetaData), new PdaMicProcessingMethodDataChannelMetaDataJsonConverter()},
			{typeof(ProcessingMethodDataChannelDescriptor), new ProcessingMethodDataChannelDescriptorJsonConverter()},
			{typeof(IProcessingMethodChannelIdentifier), new ProcessingMethodChannelIdentifierJsonConverter()},

			{typeof(IPdaExtractedChannelMetaDataSimple), new PdaExtractedChannelMetaDataSimpleJsonConvertor()},
            {typeof(IPdaExtractedChannelMetaDataApexOptimized), new PdaExtractedChannelMetaDataApexOptimizedJsonConvertor()},
            {typeof(IPdaExtractedChannelMetaDataMic), new PdaExtractedChannelMetaDataMicJsonConvertor()},
            {typeof(IPdaExtractedChannelMetaDataProgrammed), new PdaExtractedChannelMetaDataProgrammedJsonConvertor()},
            {typeof(IPdaExtractionSegment), new PdaExtractionSegmentJsonConvertor()},
            {typeof(IPostProcessingMetaData), new PostProcessingMetaDataJsonConvertor()},
            {typeof(ICompoundLibraryItemContent), new CompoundLibraryItemContentJsonConverter()},
            {typeof(ICompoundLibraryItem), new CompoundLibraryItemJsonConverter()},
            {typeof(ICalibrationGlobalParameters), new CalibrationGlobalParametersJsonConverter()},
            {typeof(ISuitabilityMethod), new SuitabilityMethodParametersJsonConverter()},
            {typeof(ICalibrationParameterError), new CalibrationParameterErrorJsonConverter()},
            {typeof(ICalibrationParameters), new CalibrationParametersJsonConverter()},
            {typeof(ICalibrationPointResponse), new CalibrationPointResponseJsonConverter()},
            {typeof(ICalibrationRegressionEquation), new CalibrationRegressionEquationJsonConverter()},
            {typeof(IChannelMappingItem), new ChannelMappingItemJsonConverter()},
            {typeof(IChannelMethod), new ChannelMethodJsonConverter()},
            {typeof(IChromatogramSetting), new ChromatogramSettingJsonConverter()},
            {typeof(ICompoundCalibrationResults), new CompoundCalibrationResultsJsonConverter()},
            {typeof(ICompoundError), new CompoundErrorJsonConverter()},
            {typeof(ICompoundGroup), new CompoundGroupJsonConverter()},
            {typeof(ICompoundId), new CompoundIdJsonConverter()},
            {typeof(ICompound), new CompoundJsonConverter()},
            {typeof(ISequenceInfo), new SequenceInfoJsonConverter()},
            {typeof(ICompoundLibrary), new CompoundLibraryJsonConverter()},
            {typeof(IAnalysisResultSetDescriptor), new AnalysisResultSetDescriptorJsonConvertor()},
            {typeof(IAnalysisResultSet), new AnalysisResultSetJsonConverter()},
            {typeof(IIdentificationParameters), new IdentificationParametersJsonConverter()},
            {typeof(IIntegrationEventError), new IntegrationEventErrorJsonConverter()},
            {typeof(IIntegrationEvent), new IntegrationEventJsonConverter()},
            {typeof(IManualOverrideMappingItem), new ManualOverrideMappingItemJsonConverter()},
            {typeof(IModifiableProcessingMethod), new ModifiableProcessingMethodJsonConverter()},
            {typeof(IPdaAbsorbanceRatioParameters), new PdaAbsorbanceRatioParametersJsonConverter()},
            {typeof(IPdaApexOptimizedParameters), new PdaApexOptimizedParametersJsonConverter()},
            {typeof(IPdaBaselineCorrectionParameters), new PdaBaselineCorrectionParametersJsonConverter()},
            {typeof(IPdaParameters), new PdaParametersJsonConverter()},
            {typeof(IPdaWavelengthMaxParameters), new PdaWavelengthMaxParametersJsonConverter()},
            {typeof(IPdaPeakPurityParameters), new PdaPeakPurityParametersJsonConverter()},
            {typeof(IPdaLibrarySearchParameters), new PdaLibrarySearchParametersJsonConverter()},
            {typeof(IPdaLibraryConfirmationParameters), new PdaLibraryConfirmationParametersJsonConverter()},
            {typeof(IPdaProcessingDeviceMetaData), new PdaProcessingDeviceMetaDataJsonConverter()},
            {typeof(IPdaStandardConfirmationParameters), new PdaStandardConfirmationParametersJsonConverter()},
            {typeof(IProcessingResults), new ProcessingResultsJsonConverter()},
            {typeof(IRunPeakResult), new RunPeakResultJsonConverter()},
            {typeof(ISmoothParameters), new SmoothParametersJsonConverter()},
            {typeof(ISuitabilityResult), new SuitabilityResultJsonConverter()},
            {typeof(IVirtualBatchRun), new VirtualBatchRunJsonConverter()},
            {typeof(ISpectrumMetaData), new SpectrumMetaDataJsonConverter() },
            {typeof(IPersistable), new PersistableJsonConverter() },
            {typeof(IErrorIndicatorDetails), new ErrorIndicatorDetailsJsonConverter() },
            {typeof(IESignaturePointInfo), new ESignaturePointInfoJsonConverter() },
            {typeof(IProjectInfo), new ProjectInfoJsonConverter() },
            {typeof(IReportTemplate), new ReportTemplateJsonConverter() },
            {typeof(ISequenceSampleInfo),new SequenceSampleInfoJsonConverter() },
            {typeof(IntValueWithDeviceName), new IntValueWithDeviceNameJsonConverter() },
            {typeof(DoubleValueWithDeviceName), new DoubleValueWithDeviceNameJsonConverter() },
            
            {typeof(IDeviceDriverItemDetails), new DeviceDriverItemDetailsJsonConverter() },
            {typeof(IBatchRun), new BatchRunJsonConverter() },
			{typeof(ISequence), new SequenceJsonConverter() },
            {typeof(Id), new IdJsonConverter() },
            {typeof(IDeviceModule), new DeviceModuleJsonConverter() },
		    {typeof(IDeviceModuleDetails), new DeviceModuleDetailsJsonConverter() },
		    {typeof(IDeviceInformation), new DeviceInformationJsonConverter() },
            {typeof(DeviceModuleCompleteId), new DeviceModuleCompleteIdJsonConverter() },
            {typeof(IInstrumentDetails), new InstrumentDetailsJsonConverter() },
            {typeof(IDictionary<Guid ,IDictionary<PharmacopeiaType, IDictionary<SuitabilityParameter, ISuitabilityParameterCriteria>>> ), new CompoundPharmacopeiaDefinitionsJsonConverter()},
		};

		static JsonConverterRegistry()
		{
		}

		public static IJsonConverter<T> GetConverter<T>()
		{
			return (IJsonConverter<T>) JsonConverters[typeof(T)];
		}
	}
}