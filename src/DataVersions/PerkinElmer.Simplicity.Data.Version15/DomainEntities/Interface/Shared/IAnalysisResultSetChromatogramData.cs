using System;
using System.Collections.Generic;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Processing;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Shared
{
    public interface IAnalysisResultSetChromatogramData
    {
        IList<(Guid BatchRunChannelGuid, Guid BatchRunGuid, IChromatographicChannelDescriptor ChannelDescriptor)> GetAllChannelDescriptors();

        IList<(Guid BatchRunChannelGuid, IChromatographicChannelDescriptor ChannelDescriptor)> GetChannelDescriptors(Guid batchRunGuid);

        (IList<double> TimeInSeconds, IList<double> Response) GetXyData(Guid batchRunChannelGuid);

        (IList<double> TimeInSeconds, IList<double> Response) GetXyData(Guid batchRunGuid, IChromatographicChannelDescriptor channelDescriptor);

        IList<Guid> GetBatchRunChannelGuids(Guid procMethodChannelGuid);

        IList<Guid> GetBatchRunChannelGuidsOfProcMethod(Guid procMethodGuid);
        
        IList<Guid> GetBatchRunChannelGuidsOfBatchRun(Guid batchRunGuid);

        IList<Guid> GetBatchRunChannelGuidsOfBatchRuns(IEnumerable<Guid> batchRunGuids, Guid pmChannelGuid);

        IList<Guid> GetBatchRunGuidsParentsOfBatchRunChannels(IList<Guid> batchRunChannelGuids);

        Guid GetBatchRunChannelGuid(Guid batchRunGuid, Guid procMethodChannelGuid);

        Guid GetBatchRunChannelGuid(IVirtualBatchRun batchRun, Guid procMethodChannelGuid);

        Guid GetBatchRunGuid(Guid batchRunChannelGuid);

        Guid GetOrigBatchRunGuid(Guid batchRunChannelGuid);

        Guid GetOrigBatchRunGuidOfBatchRun(Guid batchRunGuid);

        Guid GetProcessingMethodChannelGuid(Guid batchRunChannelGuid);

        (Guid batchRunGuid, Guid processingMethodChannelGuid) GetBatchRunAndProcessingMethodChannelGuid(
            Guid batchRunChannelGuid);

        Guid GetBatchRunChannelGuidOfChannelDescriptor(Guid batchRunGuid, IChromatographicChannelDescriptor channelDescriptor);

        IChromatographicChannelDescriptor GetChromatographicChannelDescriptor(Guid batchRunChannelGuid);

        IChromatographicChannelDescriptor GetChromatographicChannelDescriptorForChannelGuid(Guid batchRunGuid, Guid processingMethodChannelGuid);

        Guid GetProcessingMethodChannelGuid(Guid procMethodGuid, IChromatographicChannelDescriptor channelDescriptor);

        IChromatographicChannelDescriptor GetChromatographicChannelDescriptorOfProcessingMethodChannel(Guid procMethodGuid, Guid procMethodChannelGuid);

        Guid GetProcessingMethodChannelGuidForBatchRun(Guid batchRunGuid, IChromatographicChannelDescriptor channelDescriptor);

        (Guid batchRunGuid, Guid ProcessingMethodGuid, Guid ProcessingMethodChannelGuid) GetProcessingMethodBatchRunGuidForBatchRunChannelGuid(Guid batchRunChannelGuid);

        List<Guid> GetBatchRunChannelGuidOfProcessingGuid(Guid procMethodGuid, Guid procMethodChannelGuid);

        IList<Guid> GetBatchRunChannelGuidsOfChromatographicChannelDescriptor(IChromatographicChannelDescriptor channelDescriptor);

        void SubtractBlankForAllChannels(Guid batchRunGuid, Guid blankOrigBatchRunGuid);

        IList<(Guid BatchRunChannelGuid, Guid ProcessingMethodChannelGuid, IChromatographicChannelDescriptor chromatographicChannelDescriptor)> GetBatchRunProcessingMethodChannelGuidForBatchRun(Guid batchRunGuid, Guid processingMethodGuid);

        IDeviceChannelDescriptor GetDeviceChannelDescriptorForPda(Guid batchRunGuid);

        IList<(Guid BatchRunGuid, Guid BatchRunChannelGuid, Guid ProcessingmethodGuid, IChromatographicChannelDescriptor ChannelDescriptor)> GetChannelDescriptors();

        IChromatographicChannelDescriptor GetChromatographicChannelDescriptor(Guid pmGuid, IProcessingMethodChannelIdentifier pmIdentifier);
        IProcessingMethodChannelIdentifier GetProcessingMethodChannelIdentifier(Guid batchRunChannelGuid);
        IList<IDeviceChannelDescriptor> GetDeviceChannelDescriptors(Guid originalBatchRunGuid);
        
        HashSet<Guid> GetBatchRunsContainingProcessingMethodChannel(Guid pmChannelGuid);
    }
}
