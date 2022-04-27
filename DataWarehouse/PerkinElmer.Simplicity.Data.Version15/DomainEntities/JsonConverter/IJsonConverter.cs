using Newtonsoft.Json.Linq;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.JsonConverter
{
	internal interface IJsonConverter<T>
	{
		JObject ToJson(T instance);

		T FromJson(JObject jObject);
	}
}