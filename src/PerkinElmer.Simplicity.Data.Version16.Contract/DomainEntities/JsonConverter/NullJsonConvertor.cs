using Newtonsoft.Json.Linq;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.JsonConverter
{
    internal class NullJsonConvertor : IJsonConverter<object>
    {
        public JObject ToJson(object instance)
        {
            // Empty JObject
            return new JObject();
        }

        public object FromJson(JObject jObject)
        {
            return null;
        }
    }
}