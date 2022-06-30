using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Acquisition;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.JsonConverter.Version
{
    internal class BatchResultSetJsonConverter : IJsonConverter<BatchResultSet>
    {
        private const string IdKeyName = "Id";
        private const string ProjectIdKeyName = "ProjectId";
        private const string GuidKeyName = "Guid";
        private const string CreatedDateKeyName = "CreatedDate";
        private const string CreatedUserIdKeyName = "CreatedUserId";
        private const string ModifiedDateKeyName = "ModifiedDate";
        private const string ModifiedUserIdKeyName = "ModifiedUserId";
        private const string IsCompletedKeyName = "IsCompleted";
        private const string NameKeyName = "Name";
        private const string DataSourceTypeKeyName = "DataSourceType";
        private const string InstrumentMasterIdKeyName = "InstrumentMasterId";
        private const string InstrumentIdKeyName = "InstrumentId";
        private const string InstrumentNameKeyName = "InstrumentName";
        private const string RegulatedKeyName = "Regulated";
        private const string SequenceSampleInfosKeyName = "SequenceSampleInfos";

        public JObject ToJson(BatchResultSet instance)
        {
            if (instance == null) return null;
            var jObject = new JObject
            {
                {IdKeyName, instance.Id},
                {ProjectIdKeyName, instance.ProjectId},
                {GuidKeyName, instance.Guid},
                {CreatedDateKeyName, instance.CreatedDate},
                {CreatedUserIdKeyName, instance.CreatedUserId},
                {ModifiedDateKeyName, instance.ModifiedDate},
                {ModifiedUserIdKeyName, instance.ModifiedUserId},
                {IsCompletedKeyName, instance.IsCompleted},
                {NameKeyName, instance.Name},
                {DataSourceTypeKeyName, instance.DataSourceType},
                {InstrumentMasterIdKeyName, instance.InstrumentMasterId},
                {InstrumentIdKeyName, instance.InstrumentId},
                {InstrumentNameKeyName, instance.InstrumentName},
                {RegulatedKeyName, instance.Regulated}
            };
            var sequenceSamples = new List<ISequenceSampleInfo>();
            if (instance.SequenceSampleInfos != null)
            {
                foreach (var sequenceSampleEntity in instance.SequenceSampleInfos)
                {
                    var sequenceSampleDomain = DomainFactory.Create<ISequenceSampleInfo>();
                    DomainContractAdaptor.PopulateSequenceSample(sequenceSampleEntity, sequenceSampleDomain);
                    sequenceSamples.Add(sequenceSampleDomain);
                }
            }
            JsonConverterHelper.SetCollectionPropertyToJObject<ISequenceSampleInfo>(jObject, sequenceSamples, SequenceSampleInfosKeyName);
            return jObject;
        }

        public BatchResultSet FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;

            var batchResultSet = new BatchResultSet
            {
                Id = (long) jObject[IdKeyName],
                ProjectId = (long) jObject[ProjectIdKeyName],
                Guid = (Guid) jObject[GuidKeyName],
                CreatedDate = (DateTime) jObject[CreatedDateKeyName],
                CreatedUserId = (string) jObject[CreatedUserIdKeyName],
                ModifiedDate = (DateTime) jObject[ModifiedDateKeyName],
                ModifiedUserId = (string) jObject[ModifiedUserIdKeyName],
                IsCompleted = (bool) jObject[IsCompletedKeyName],
                Name = (string) jObject[NameKeyName],
                DataSourceType = (short) jObject[DataSourceTypeKeyName],
                InstrumentMasterId = (string) jObject[InstrumentMasterIdKeyName],
                InstrumentId = (string) jObject[InstrumentIdKeyName],
                InstrumentName = (string) jObject[InstrumentNameKeyName],
                Regulated = (bool) jObject[RegulatedKeyName]
            };

            var sequenceSampleInfos = JsonConverterHelper.GetListPropertyFromJson<ISequenceSampleInfo>(jObject, SequenceSampleInfosKeyName);
            if (sequenceSampleInfos != null)
            {
                var sequenceSampleEntities = new List<SequenceSampleInfoBatchResult>();
                foreach (var sequenceSampleInfo in sequenceSampleInfos)
                {
                    var sequenceSampleInfoBatchResult = new SequenceSampleInfoBatchResult();
                    DomainContractAdaptor.PopulateSequenceSampleInfoEntity(sequenceSampleInfo, sequenceSampleInfoBatchResult);
                    sequenceSampleEntities.Add(sequenceSampleInfoBatchResult);
                }

                batchResultSet.SequenceSampleInfos = sequenceSampleEntities;
            }

            return batchResultSet;
        }
    }
}
