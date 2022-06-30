using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.JsonConverter
{
	public static class JsonConverterHelper
	{
		public static List<T> GetListPropertyFromJson<T>(JObject jObject, string propertyName)
		{
			JToken token = jObject[propertyName];
			if (token != null && token.Type != JTokenType.Null)
			{
				var list = new List<T>();
				var array = JArray.FromObject(jObject[propertyName]);
                var converter = JsonConverterRegistry.GetConverter<T>();
				if (typeof(T).IsInterface)
				{
					foreach (var jToken in array)
					{
						var jObj = (JObject)jToken;
						var item = converter.FromJson(jObj);
						list.Add(item);
					}

					return list;
				}
                if (converter != null)
                {
                    foreach (var jToken in array)
                    {
                        var jObj = (JObject)jToken;
                        var item = converter.FromJson(jObj);
                        list.Add(item);
                    }
                    return list;
				}
                return array.ToObject<List<T>>();
            }

            return null;
        }

		public static T[] GetArrayPropertyFromJson<T>(JObject jObject, string propertyName)
		{
			JToken token = jObject[propertyName];
			if (token != null && token.Type != JTokenType.Null)
			{
				var list = new List<T>();
				var array = JArray.FromObject(jObject[propertyName]);
				if (typeof(T).IsInterface)
				{
					foreach (var jToken in array)
					{
						var jObj = (JObject)jToken;
						var item = JsonConverterRegistry.GetConverter<T>().FromJson(jObj);
						list.Add(item);
					}

					return list.ToArray();
				}
				else
				{
					return array.ToObject<T[]>();
				}
			}
			else
			{
				return null;
			}
		}

		public static Dictionary<TKey, TValue> GetDictionaryPropertyFromJson<TKey, TValue>(JObject jObject, string propertyName, Func<string, TKey> converter)
		{
			JToken token = jObject[propertyName];
			if (token != null && token.Type != JTokenType.Null)
			{
				Dictionary<TKey, TValue> dic = new Dictionary<TKey, TValue>();
				var array = token.ToArray();
				if (typeof(TValue).IsInterface)
				{
					foreach (var jToken in array)
					{
						var jProperty = (JProperty)jToken;
						TKey key = converter(jProperty.Name);
						dic[key] = JsonConverterRegistry.GetConverter<TValue>().FromJson((JObject)jProperty.Value);
					}

					return dic;
				}
				else
				{
					throw new NotImplementedException();
				}
			}
			else
			{
				return null;
			}
		}

		public static Dictionary<TKey, TValueList> GetDictionaryPropertyFromJson<TKey, TValueList, TValue>(JObject jObject, string propertyName, Func<string, TKey> converter) where TValueList : ICollection<TValue>, new()
		{
			JToken token = jObject[propertyName];
			if (token != null && token.Type != JTokenType.Null)
			{
				Dictionary<TKey, TValueList> dic = new Dictionary<TKey, TValueList>();
				var array = token.ToArray();
				if (typeof(TValue).IsInterface)
				{
					foreach (var jToken in array)
					{
						var jProperty = (JProperty)jToken;
						TKey key = converter(jProperty.Name);
						if (jProperty.Value.Type == JTokenType.Array)
						{
							TValueList valueList = new TValueList();
							var propArray = jProperty.Value.ToArray();
							foreach (var item in propArray)
							{
								valueList.Add(JsonConverterRegistry.GetConverter<TValue>().FromJson((JObject) item));
							}
							dic[key] = valueList;
						}
					}
					return dic;
				}
				else
				{
					throw new NotImplementedException();
				}
			}
			else
			{
				return null;
			}
		}

		public static void SetCollectionPropertyToJObject<T>(JObject jObject, ICollection<T> collection, string propertyName)
		{
			if (typeof(T).IsInterface)
			{
				var jArray = new JArray();
				if (collection != null)
				{
					foreach (var item in collection)
					{
						jArray.Add(JsonConverterRegistry.GetConverter<T>().ToJson(item));
					}

					jObject.Add(propertyName, jArray);
				}
				else
				{
					jObject.Add(propertyName, new JValue(collection));
				}
			}
			else
			{
				if (collection == null)
				{
					jObject.Add(propertyName, new JValue(collection));
				}
				else
				{
                    var jArray = new JArray();
                    foreach (var item in collection)
                    {
                        jArray.Add(JsonConverterRegistry.GetConverter<T>().ToJson(item));
                    }
					jObject.Add(propertyName, jArray);
				}
			}
		}
		
		public static void SetListPropertyToJObject<T>(JObject jObject, IReadOnlyList<T> list, string propertyName)
		{
			if (typeof(T).IsInterface)
			{
				var jArray = new JArray();
				if (list != null)
				{
					foreach (var item in list)
					{
						jArray.Add(JsonConverterRegistry.GetConverter<T>().ToJson(item));
					}

					jObject.Add(propertyName, jArray);
				}
				else
				{
					jObject.Add(propertyName, new JValue(list));
				}
			}
			else
			{
				if (list == null)
				{
					jObject.Add(propertyName, new JValue(list));
				}
				else
				{
					jObject.Add(propertyName, new JArray(list));
				}
			}
		}
	}
}