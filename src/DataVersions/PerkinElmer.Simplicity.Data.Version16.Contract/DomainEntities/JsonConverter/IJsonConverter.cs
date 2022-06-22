using Newtonsoft.Json.Linq;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.JsonConverter
{
	internal interface IJsonConverter<T>
	{
		JObject ToJson(T instance);

		T FromJson(JObject jObject);
	}
}