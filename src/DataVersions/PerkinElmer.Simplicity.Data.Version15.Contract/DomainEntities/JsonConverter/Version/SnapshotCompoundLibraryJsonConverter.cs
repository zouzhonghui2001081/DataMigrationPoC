using System;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.JsonConverter.Version
{
    internal class SnapshotCompoundLibraryJsonConverter : IJsonConverter<SnapshotCompoundLibrary>
    {
        private const string IdKeyName = "Id";
        private const string AnalysisResultSetIdKeyName = "AnalysisResultSetId";
        private const string LibraryNameKeyName = "LibraryName";
        private const string LibraryGuidKeyName = "LibraryGuid";
        private const string CreatedDateKeyName = "CreatedDate";
        private const string ModifiedDateKeyName = "ModifiedDate";

        public JObject ToJson(SnapshotCompoundLibrary instance)
        {
            if (instance == null) return null;
            return new JObject
            {
                {IdKeyName, instance.Id},
                {AnalysisResultSetIdKeyName, instance.AnalysisResultSetId},
                {LibraryNameKeyName, instance.LibraryName},
                {LibraryGuidKeyName, instance.LibraryGuid},
                {CreatedDateKeyName, instance.CreatedDate},
                {ModifiedDateKeyName, instance.ModifiedDate},
            };
        }

        public SnapshotCompoundLibrary FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;
            return new SnapshotCompoundLibrary
            {
                Id = (long)jObject[IdKeyName],
                AnalysisResultSetId = (long)jObject[AnalysisResultSetIdKeyName],
                LibraryName = (string)jObject[LibraryNameKeyName],
                LibraryGuid = (Guid)jObject[LibraryGuidKeyName],
                CreatedDate = (DateTime)jObject[CreatedDateKeyName],
                ModifiedDate = (DateTime)jObject[ModifiedDateKeyName]
            };
        }
    }
}
