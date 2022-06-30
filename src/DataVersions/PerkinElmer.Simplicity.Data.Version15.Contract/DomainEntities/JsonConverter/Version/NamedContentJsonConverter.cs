using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.JsonConverter.Version
{
    internal class NamedContentJsonConverter : IJsonConverter<NamedContent>
    {
        private const string IdKeyName = "Id";
        private const string BatchRunIdKeyName = "BatchRunId";
        private const string KeyKeyName = "Key";
        private const string ValueDateKeyName = "Value";

        public JObject ToJson(NamedContent instance)
        {
            if (instance == null) return null;

            return new JObject
            {
                {IdKeyName, instance.Id},
                {BatchRunIdKeyName, instance.BatchRunId},
                {KeyKeyName, instance.Key},
                {ValueDateKeyName, instance.Value},
            };
        }

        public NamedContent FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;

            return new NamedContent
            {
                Id = (long) jObject[IdKeyName],
                BatchRunId = (long) jObject[BatchRunIdKeyName],
                Key = (string) jObject[KeyKeyName],
                Value = (string) jObject[ValueDateKeyName],
            };
        }
    }
}
