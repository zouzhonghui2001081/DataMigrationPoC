using Newtonsoft.Json.Linq;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.JsonConverter
{
	public static class JsonConverter
	{
		public static string ToJson<T>(T instance) where T : class
        {
            if (instance == null)
                return new NullJsonConvertor().ToJson(instance).ToString();

            var converter = JsonConverterRegistry.GetConverter<T>();
			return converter.ToJson(instance).ToString();
		}

		public static T FromJson<T>(string json) where T : class
        {
            var jObject = JObject.Parse(json);
            if (!jObject.HasValues)
                return (T)new NullJsonConvertor().FromJson(jObject);

            var converter = JsonConverterRegistry.GetConverter<T>();
			return converter.FromJson(jObject);
		}
	}
}