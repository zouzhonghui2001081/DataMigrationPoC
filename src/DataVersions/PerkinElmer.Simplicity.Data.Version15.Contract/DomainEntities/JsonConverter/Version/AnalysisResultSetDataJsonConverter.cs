using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Processing;
using PerkinElmer.Simplicity.Data.Version15.Contract.Version.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.JsonConverter.Version
{
    internal class AnalysisResultSetDataJsonConverter : IJsonConverter<AnalysisResultSetData>
    {
        private const string ProjectGuidKeyName = "ProjectGuid";
        private const string AnalysisResultSetKeyName = "AnalysisResultSet";
        private const string BatchResultSetDataKeyName = "BatchResultSetData";
        private const string BatchRunAnalysisResultsKeyName = "BatchRunAnalysisResults";
        private const string BatchRunChannelMapsKeyName = "BatchRunChannelMaps";
        private const string ManualOverrideMapsKeyName = "ManualOverrideMaps";
        private const string BrChannelsWithExceededNumberOfPeaksKeyName = "BrChannelsWithExceededNumberOfPeaks";
        private const string CompoundSuitabilitySummaryResultsKeyName = "CompoundSuitabilitySummaryResults";
        private const string CompoundLibraryDataKeyName = "CompoundLibraryData";


        public JObject ToJson(AnalysisResultSetData instance)
        {
            if (instance == null) return null;

            var jObject = new JObject
            {
                {ProjectGuidKeyName, instance.ProjectGuid},
                {AnalysisResultSetKeyName, JsonConverterRegistry.GetConverter<AnalysisResultSet>().ToJson(instance.AnalysisResultSet)}
            };
            JsonConverterHelper.SetCollectionPropertyToJObject<BatchResultSetData>(jObject, instance.BatchResultSetData, BatchResultSetDataKeyName);
            JsonConverterHelper.SetCollectionPropertyToJObject<BatchRunAnalysisResultData>(jObject, instance.BatchRunAnalysisResults, BatchRunAnalysisResultsKeyName);
            JsonConverterHelper.SetCollectionPropertyToJObject<SnapshotCompoundLibraryData>(jObject, instance.CompoundLibraryData, CompoundLibraryDataKeyName);
            JsonConverterHelper.SetCollectionPropertyToJObject<BatchRunChannelMap>(jObject, instance.BatchRunChannelMaps, BatchRunChannelMapsKeyName);

            var manualOverrideMappingItems = new List<IManualOverrideMappingItem>();
            if (instance.ManualOverrideMaps != null)
            {
                foreach (var manualOverrideMapping in instance.ManualOverrideMaps)
                {
                    var manualOverrideMappingDomain = DomainFactory.Create<IManualOverrideMappingItem>();
                    DomainContractAdaptor.PopulateManualOverrideMap(manualOverrideMapping, manualOverrideMappingDomain);

                    if (manualOverrideMapping.ManualOverrideIntegrationEvents != null)
                    {
                        manualOverrideMappingDomain.TimedIntegrationParameters = new List<IIntegrationEvent>();
                        foreach (var timedIntegrationParameter in manualOverrideMapping.ManualOverrideIntegrationEvents)
                        {
                            var integrationEvent = DomainFactory.Create<IIntegrationEvent>();
                            DomainContractAdaptor.PopulateIntegrationEvent(timedIntegrationParameter, integrationEvent);
                            manualOverrideMappingDomain.TimedIntegrationParameters.Add(integrationEvent);
                        }
                    }
                    manualOverrideMappingItems.Add(manualOverrideMappingDomain);
                }
            }
            JsonConverterHelper.SetCollectionPropertyToJObject<IManualOverrideMappingItem>(jObject, manualOverrideMappingItems, ManualOverrideMapsKeyName);

            var brChannelsWithExceededNumberOfPeaks = new List<Guid>();
            if (instance.BrChannelsWithExceededNumberOfPeaks != null)
            {
                foreach (var brChannel in instance.BrChannelsWithExceededNumberOfPeaks)
                    brChannelsWithExceededNumberOfPeaks.Add(brChannel.BatchRunChannelGuid);
            }
            JsonConverterHelper.SetCollectionPropertyToJObject<string>(jObject, brChannelsWithExceededNumberOfPeaks.Select(g => g.ToString()).ToArray(), BrChannelsWithExceededNumberOfPeaksKeyName);

            var compoundSuitabilitySummaryResults = new List<ICompoundSuitabilitySummaryResults>();
            if (instance.CompoundSuitabilitySummaryResults != null)
            {
                foreach (var compoundSuitabilitySummaryResult in instance.CompoundSuitabilitySummaryResults)
                {
                    var compoundSuitabilitySummaryResultDomain = DomainFactory.Create<ICompoundSuitabilitySummaryResults>();
                    DomainContractAdaptor.PopulateCompoundSuitabilitySummaryResults(compoundSuitabilitySummaryResult, compoundSuitabilitySummaryResultDomain);
                }
            }
            JsonConverterHelper.SetCollectionPropertyToJObject<ICompoundSuitabilitySummaryResults>(jObject, compoundSuitabilitySummaryResults, CompoundSuitabilitySummaryResultsKeyName);

            return jObject;
        }

        public AnalysisResultSetData FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;

            var analysisResultSetData = new AnalysisResultSetData
            {
                ProjectGuid = (Guid) jObject[ProjectGuidKeyName],
                AnalysisResultSet = jObject[AnalysisResultSetKeyName].Type == JTokenType.Null
                    ? null : JsonConverterRegistry.GetConverter<AnalysisResultSet>().FromJson((JObject) jObject[AnalysisResultSetKeyName]),
                BatchResultSetData = JsonConverterHelper.GetListPropertyFromJson<BatchResultSetData>(jObject, BatchResultSetDataKeyName),
                BatchRunAnalysisResults = JsonConverterHelper.GetListPropertyFromJson<BatchRunAnalysisResultData>(jObject, BatchRunAnalysisResultsKeyName),
                BatchRunChannelMaps = JsonConverterHelper.GetListPropertyFromJson<BatchRunChannelMap>(jObject, BatchRunChannelMapsKeyName),
                CompoundLibraryData = JsonConverterHelper.GetListPropertyFromJson<SnapshotCompoundLibraryData>(jObject, CompoundLibraryDataKeyName),
            };

            var manualOverrideMappingItems = JsonConverterHelper.GetListPropertyFromJson<IManualOverrideMappingItem>(jObject, ManualOverrideMapsKeyName);
            var manualOverrideMapEntities = new List<ManualOverrideMap>();
            if (manualOverrideMappingItems != null)
            {
                foreach (var manualOverrideMappingItem in manualOverrideMappingItems)
                {
                    var manualOverrideMapEntity = new ManualOverrideMap();
                    if (analysisResultSetData.AnalysisResultSet != null)
                        manualOverrideMapEntity.AnalysisResultSetId = analysisResultSetData.AnalysisResultSet.Id;
                    DomainContractAdaptor.PopulateManualOverrideMapEntity(manualOverrideMappingItem, manualOverrideMapEntity);
                    if (manualOverrideMappingItem.TimedIntegrationParameters != null)
                    {
                        var manualOverrideIntegrationEvents = new List<ManualOverrideIntegrationEvent>();
                        foreach (var timedIntegrationParameter in manualOverrideMappingItem.TimedIntegrationParameters)
                        {
                            var manualOverrideIntegrationEvent = new ManualOverrideIntegrationEvent();
                            DomainContractAdaptor.PopulateIntegrationEventEntity(timedIntegrationParameter, manualOverrideIntegrationEvent);
                            manualOverrideIntegrationEvents.Add(manualOverrideIntegrationEvent);
                        }
                        manualOverrideMapEntity.ManualOverrideIntegrationEvents = manualOverrideIntegrationEvents;
                    }

                    manualOverrideMapEntities.Add(manualOverrideMapEntity);
                }
            }
            analysisResultSetData.ManualOverrideMaps = manualOverrideMapEntities;

            var brChannels = JsonConverterHelper.GetListPropertyFromJson<string>(jObject, BrChannelsWithExceededNumberOfPeaksKeyName);
            var brChannelsWithExceededNumberOfPeaks = new List<BrChannelsWithExceededNumberOfPeaks>();
            if (brChannels != null)
            {
                foreach (var brChannel in brChannels)
                {
                    var brChannelWithExceededNumberOfPeaks = new BrChannelsWithExceededNumberOfPeaks();
                    if (analysisResultSetData.AnalysisResultSet != null)
                        brChannelWithExceededNumberOfPeaks.AnalysisResultSetId = analysisResultSetData.AnalysisResultSet.Id;
                    brChannelWithExceededNumberOfPeaks.BatchRunChannelGuid = Guid.Parse(brChannel);
                    brChannelsWithExceededNumberOfPeaks.Add(brChannelWithExceededNumberOfPeaks);
                }
            }
            analysisResultSetData.BrChannelsWithExceededNumberOfPeaks = brChannelsWithExceededNumberOfPeaks;


            var compoundSuitabilitySummaryResults = JsonConverterHelper.GetListPropertyFromJson<ICompoundSuitabilitySummaryResults>(jObject, CompoundSuitabilitySummaryResultsKeyName);
            var compoundSuitabilitySummaryResultsEntities = new List<CompoundSuitabilitySummaryResults>();
            if (compoundSuitabilitySummaryResults != null)
            {
                foreach (var compoundSuitabilitySummaryResultsDomain in compoundSuitabilitySummaryResults)
                {
                    var compoundGuid = compoundSuitabilitySummaryResultsDomain.CompoundGuid;
                    var compoundSuitabilitySummaryResultsEntity = new CompoundSuitabilitySummaryResults();
                    DomainContractAdaptor.PopulateCompoundSuitabilitySummaryResultEntity(compoundGuid, compoundSuitabilitySummaryResultsDomain, compoundSuitabilitySummaryResultsEntity);
                    compoundSuitabilitySummaryResultsEntities.Add(compoundSuitabilitySummaryResultsEntity);
                }
            }

            analysisResultSetData.CompoundSuitabilitySummaryResults = compoundSuitabilitySummaryResultsEntities;

            return analysisResultSetData;
        }
    }
}
