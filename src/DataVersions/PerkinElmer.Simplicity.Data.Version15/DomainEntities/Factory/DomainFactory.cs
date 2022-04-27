using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using log4net;
using PerkinElmer.Acquisition.Devices;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Implementation.Acquisition;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Implementation.LabManagement;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Implementation.Processing;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Implementation.Processing.CompoundLibrary;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Implementation.Processing.SpectralAbsorbanceRatio;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Implementation.ReportBuilder;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Implementation.ReviewApprove;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Implementation.Sequence;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Implementation.Shared;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Acquisition;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.LabManagement;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Processing;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Processing.CompoundLibrary;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Processing.Spectral;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Reporting;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.ReviewApprove;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Sequence;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Shared;
using IDeviceMethod = PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Acquisition.IDeviceMethod;


namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.Factory
{
    public static class DomainFactory
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static readonly IDictionary<Type, Type> DomainTypesImplementation = new Dictionary<Type, Type>
        {
            {typeof(IBatchRunInfo), typeof(BatchRunInfo)},
            {typeof(IAcquisitionRunInfo), typeof(AcquisitionRunInfo)},
            {typeof(IUserInfo), typeof(UserInfo)},
            {typeof(IDeviceMethod), typeof(DeviceMethod)},
            {typeof(IAcquisitionMethod), typeof(AcquisitionMethod)},
            {typeof(IAcquisitionMethodInfo), typeof(AcquisitionMethodInfo)},
            {typeof(IAcquisitionMethodOverallInfo), typeof(AcquisitionMethodOverallInfo)},
            {typeof(IAcquisitionMethodVersionInfo), typeof(AcquisitionMethodInfoWithVersionState)},
            {typeof(IAnalysisResultSet), typeof(AnalysisResultSet)},
            {typeof(IVirtualBatchRun), typeof(VirtualBatchRun)},
            {typeof(IBatchRun), typeof(BatchRun)},
            {typeof(IProcessingMethod), typeof(ProcessingMethod)},
            {typeof(IChannelMethod), typeof(ChannelMethod)},
            {typeof(IIdentificationParameters), typeof(IdentificationParameters)},
            {typeof(IProcessingResults), typeof(ProcessingResults)},
            {typeof(ISequence), typeof(Sequence)},
            {typeof(IBatchResultSetInfo), typeof(BatchResultSetInfo)},
            {typeof(IBatchResultSet), typeof(BatchResultSet)},
            {typeof(ISequenceInfo), typeof(SequenceInfo)},
            {typeof(ISequenceSampleInfo), typeof(SequenceSampleInfo)},
            {typeof(IRunPeakResult), typeof(RunPeakResult)},
            {typeof(IProcessingMethodInfo), typeof(ProcessingMethodInfo)},
            {typeof(IIntegrationEvent), typeof(IntegrationEvent)},
            {typeof(ISmoothParameters), typeof(SmoothParameters)},
            {typeof(ISuitabilityResult), typeof(SuitabilityResult)},
            
            {typeof(ICompoundCalibrationResults), typeof(CompoundCalibrationResults)},
            {typeof(ICompoundError), typeof(CompoundError)},
            {typeof(IIntegrationEventError), typeof(IntegrationEventError)},
            {typeof(ICalibrationGlobalParameters), typeof(CalibrationGlobalParameters)},
            {typeof(ICalibrationParameters), typeof(CalibrationParameters)},
            {typeof(ICalibrationParameterError), typeof(CalibrationParameterError)},
            {typeof(ICalibrationRegressionEquation), typeof(CalibrationRegressionEquation)},
            {typeof(ICalibrationPointResponse), typeof(CalibrationPointResponse)},
            {typeof(IStreamDataInfo), typeof(StreamDataInfo)},
            {typeof(IStreamData), typeof(StreamData)},
            {typeof(ICompoundGroup), typeof(CompoundGroup)},
            {typeof(IChromatogramSetting), typeof(ChromatogramSetting)},
            {typeof(IBatchRunBase), typeof(BatchRunBase)},
            {typeof(IBatchRunWithRawData), typeof(BatchRunWithRawData)},
            {typeof(IReportTemplate), typeof(ReportTemplate)},
            {typeof(IModifiableProcessingMethod), typeof(ModifiableProcessingMethod)},
            {typeof(ICompound), typeof(Compound)},
            {typeof(IProjectInfo), typeof(ProjectInfo)},
            {typeof(IInstrumentDetailsModifiable), typeof(InstrumentDetailsModifiable)},
            {typeof(IInstrumentModifiable), typeof(InstrumentModifiable)},
            {typeof(IInstrumentMasterModifiable), typeof(InstrumentMasterModifiable)},
            {typeof(IDeviceModuleModifiable), typeof(DeviceModuleModifiable)},
            {typeof(IDeviceModuleDetailsModifiable), typeof(DeviceModuleDetailsModifiable)},
            {typeof(IDeviceDriverItemDetailsModifiable), typeof(DeviceDriverItemDetailsModifiable)},
            {typeof(ISpectrumMetaData), typeof(SpectrumMetaData)},
            {typeof(IDeviceIdentifier), typeof(DeviceIdentifier)},
            {typeof(IChannelIdentifier1), typeof(ChannelIdentifier)},
            {typeof(IChromatographicChannelDescriptor), typeof(ChromatographicChannelDescriptor)},
            {typeof(IDeviceChannelDescriptor), typeof(DeviceChannelDescriptor)},
            {typeof(IDeviceChannelIdentifier), typeof(DeviceChannelIdentifier)},
            {typeof(IChannelMappingItem), typeof(ChannelMappingItem)},
            {typeof(IPdaExtractedChannelMetaDataSimple), typeof(PdaExtractedChannelMetaDataSimple)},
            {typeof(IPdaExtractedChannelMetaDataProgrammed), typeof(PdaExtractedChannelMetaDataProgrammed)},
            {typeof(IPdaExtractedChannelMetaDataApexOptimized), typeof(PdaExtractedChannelMetaDataApexOptimized)},
            {typeof(IPdaExtractedChannelMetaDataMic), typeof(PdaExtractedChannelMetaDataMic)},
            {typeof(IPdaExtractionSegment), typeof(PdaExtractionSegment)},
            {typeof(IPostProcessingMetaData), typeof(PostProcessingMetaData)},
            {typeof(IGCChannelMetaData), typeof(GCChannelMetaData)},
            {typeof(IPdaChannelMetaData), typeof(PdaChannelMetaData)},
            {typeof(IUVChannelMetaData), typeof(UVChannelMetaData)},
            {typeof(IMultiUVChannelMetaData), typeof(MultiUVChannelMetaData)},
            {typeof(IFLChannelMetaData), typeof(FLChannelMetaData)},
            {typeof(IRIChannelMetaData), typeof(RIChannelMetaData)},
            {typeof(IAToDChannelMetaData), typeof(AToDChannelMetaData)},
            {typeof(ITemperatureChannelMetaData), typeof(TemperatureChannelMetaData)},
            {typeof(IPumpFlowChannelMetaData), typeof(PumpFlowChannelMetaData)},
            {typeof(ISolventProportionChannelMetaData), typeof(SolventProportionChannelMetaData)},
            {typeof(IPdaParameters),typeof(PdaParameters)},
            {typeof(IPdaWavelengthMaxParameters),typeof(PdaWavelengthMaxParameters)},
            {typeof(IPdaPeakPurityParameters),typeof(PdaPeakPurityParameters)},
            {typeof(IPdaLibrarySearchParameters),typeof(PdaLibrarySearchParameters)},
            {typeof(IManualOverrideMappingItem),typeof(ManualOverrideMappingItem)},
            {typeof(ISpectrumMethod),typeof(SpectrumMethod)},
            {typeof(ICompoundLibraryItem),typeof(CompoundLibraryItem)},
            {typeof(ICompoundLibraryItemContent),typeof(CompoundLibraryItemContent)},
            {typeof(IPdaAbsorbanceRatioParameters),typeof(PdaAbsorbanceRatioParameters)},
            {typeof(IPdaBaselineCorrectionParameters),typeof(PdaBaselineCorrectionParameters)},
            {typeof(IPdaStandardConfirmationParameters),typeof(PdaStandardConfirmationParameters)},
            {typeof(IPdaApexOptimizedParameters), typeof(PdaApexOptimizedParameters)},
            {typeof(ICompoundLibrary), typeof(CompoundLibrary)},
            {typeof(IErrorIndicatorDetails), typeof(ErrorIndicatorDetails)},
            {typeof(IESignaturePointInfo), typeof(ESignaturePointInfo)},
            {typeof(IPersistable), typeof(Persistable) },
            {typeof(ISequenceGroupSetting), typeof(SequenceGroupSetting) },
            {typeof(ICompoundId), typeof(CompoundId) },
            {typeof(IApprovalReviewItemInfo), typeof(ApproveReviewItemInfo)},
            {typeof(IAbsorbanceRatioResult), typeof(AbsorbanceRatioResult)},
            {typeof(IPdaLibraryConfirmationParameters),typeof(PdaLibraryConfirmationParameters)},
            {typeof(IPressureChannelMetaData), typeof(PressureChannelMetaData)},
            {typeof(IFluorescenceSpectrumChannelMetaData), typeof(FluorescenceSpectrumChannelMetaData)},
            {typeof(ICompoundSuitabilitySummaryResults), typeof(CompoundSuitabilitySummaryResults)},
//            {typeof(ISuitabilityParameters), typeof(SuitabilityParameters)},
            {typeof(ISuitabilityLimits), typeof(SuitabilityLimits)},
            {typeof(IReviewApprovableEntity), typeof(ReviewApprovableEntity)},
            {typeof(IReviewApprovableEntitySubItem), typeof(ReviewApprovableEntitySubItem)},
            {typeof(IReviewApproveSettings), typeof(ReviewApproveSettings)},
            {typeof(IAnalysisResultSetReviewApproveInfo), typeof(AnalysisResultSetReviewApproveInfo)},
            {typeof(IAnalysisResultSetReviewApproveSubItemInfo), typeof(AnalysisResultSetReviewApproveSubItemInfo)},
            {typeof(ICalibrationBatchRunInfo), typeof(CalibrationBatchRunInfo)},
            {typeof(IDeviceInformation), typeof(DeviceInformation)},
            {typeof(IAnalysisResultSetDescriptor), typeof(AnalysisResultSetDescriptor)},
            {typeof(IOriginalAnalysisResultSetDescriptor), typeof(OriginalAnalysisResultSetDescriptor)},
            {typeof(ISuitabilityMethod), typeof(SuitabilityMethod)},
            {typeof(ISuitabilityParameterCriteria), typeof(SuitabilityParameterCriteria)}
        };
            
        static DomainFactory()
        {
        }

        public static T Create<T>()
        {
            var type = typeof(T);

            if (!DomainTypesImplementation.TryGetValue(type, out var domainObjectType))
            {
                var message = string.Format(CultureInfo.InvariantCulture, "Type '{0}' can't be created!", type.Name);
                Log.Error(message);
                throw new Exception(message);
            }

            //Log.Debug(string.Format(CultureInfo.InvariantCulture, "Type '{0}' was created", type.Name));

            return (T)Activator.CreateInstance(domainObjectType);
        }

    }
}