using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Implementation.Processing;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Processing;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.JsonConverter.Shared
{
	internal class CompoundPharmacopeiaDefinitionsJsonConverter : IJsonConverter<IDictionary<Guid, IDictionary<PharmacopeiaType, IDictionary<SuitabilityParameter, ISuitabilityParameterCriteria>>>>
	{
		private const int CurrentVersion = 2;
		private const string VersionKeyName = "Version";
		private const string CompoundPharmacopeiaDefinitionsKeyName = "CompoundPharmacopeiaDefinitions";
		private const string SuitabilityCriteriaEnabledKeyName = "Enabled";
		private const string SuitabilityCriteriaLowerLimitKeyName = "LowerLimit";
		private const string SuitabilityCriteriaUpperLimitKeyName = "UpperLimit";
		private const string SuitabilityCriteriaRsdLimitKeyName = "RsdLimit";


		public JObject ToJson(IDictionary<Guid, IDictionary<PharmacopeiaType, IDictionary<SuitabilityParameter, ISuitabilityParameterCriteria>>> instance)
		{
			if (instance == null) return null;
			var jObject = new JObject
			{
				{VersionKeyName, new JValue(CurrentVersion)},
				{CompoundPharmacopeiaDefinitionsKeyName, JsonConvert.SerializeObject(instance)},
			};

			return jObject;
		}

		public IDictionary<Guid, IDictionary<PharmacopeiaType, IDictionary<SuitabilityParameter, ISuitabilityParameterCriteria>>> FromJson(JObject jObject)
		{
			if (jObject == null || jObject.Type == JTokenType.Null) return null;

			var version = (int)jObject[VersionKeyName];
			if (version > CurrentVersion)
				throw new Exception("Unsupported serialized object version!");

			var suitabilityMethod = new SuitabilityMethod();
			return suitabilityMethod.CompoundPharmacopeiaDefinitions = DeserializeCompoundPharmacopieaDefinitions(Deserialize(jObject[CompoundPharmacopeiaDefinitionsKeyName].ToString()));


		}

		private static IDictionary<Guid, IDictionary<PharmacopeiaType, IDictionary<SuitabilityParameter, ISuitabilityParameterCriteria>>> DeserializeCompoundPharmacopieaDefinitions(object obj)
		{
			IDictionary objDictionary = (IDictionary)obj;
			IDictionary<Guid, IDictionary<PharmacopeiaType, IDictionary<SuitabilityParameter, ISuitabilityParameterCriteria>>> compoundPharmacopieaDefinitions = new Dictionary<Guid, IDictionary<PharmacopeiaType, IDictionary<SuitabilityParameter, ISuitabilityParameterCriteria>>>();
			foreach (var guid in objDictionary.Keys)
			{
				IDictionary<PharmacopeiaType, IDictionary<SuitabilityParameter, ISuitabilityParameterCriteria>> pharmacopieaSuitabilityParameterCriteriaDictionary = new Dictionary<PharmacopeiaType, IDictionary<SuitabilityParameter, ISuitabilityParameterCriteria>>();
				var pharmacopieaDictionary = (IDictionary)objDictionary[guid];

				foreach (var pharmacopieaDictionaryKey in pharmacopieaDictionary.Keys)
				{
					Enum.TryParse(pharmacopieaDictionaryKey.ToString(), out PharmacopeiaType pharmacopeiaType);
					IDictionary<SuitabilityParameter, ISuitabilityParameterCriteria> suitabilityParameterCriteriaDictionary = new Dictionary<SuitabilityParameter, ISuitabilityParameterCriteria>();
					var suitabilityParameterDictionary = (IDictionary)pharmacopieaDictionary[pharmacopieaDictionaryKey];

					foreach (var suitabilityParameterDictionaryKey in suitabilityParameterDictionary.Keys)
					{
						Enum.TryParse(suitabilityParameterDictionaryKey.ToString(), out SuitabilityParameter suitabilityParameter);
						var suitabilityParameterDictionaryValuesDictionary = (IDictionary)suitabilityParameterDictionary[suitabilityParameterDictionaryKey];

						var suitabilityParameterCriteria = SuitabilityParameterCriteria(suitabilityParameterDictionaryValuesDictionary);

						suitabilityParameterCriteriaDictionary.Add(suitabilityParameter, suitabilityParameterCriteria);
					}
					pharmacopieaSuitabilityParameterCriteriaDictionary.Add(pharmacopeiaType, suitabilityParameterCriteriaDictionary);
				}
				compoundPharmacopieaDefinitions.Add(Guid.Parse(guid.ToString()), pharmacopieaSuitabilityParameterCriteriaDictionary);
			}
			return compoundPharmacopieaDefinitions;
		}

		private static ISuitabilityParameterCriteria SuitabilityParameterCriteria(IDictionary suitabilityParameterDictionaryValuesDictionary)
		{
			ISuitabilityParameterCriteria suitabilityParameterCriteria = new SuitabilityParameterCriteria();

			foreach (var suitabilityParameterDictionaryValuesDictionaryKey in suitabilityParameterDictionaryValuesDictionary.Keys)
			{
				switch (suitabilityParameterDictionaryValuesDictionaryKey.ToString())
				{
					case SuitabilityCriteriaEnabledKeyName:
						suitabilityParameterCriteria.Enabled = (bool)suitabilityParameterDictionaryValuesDictionary[SuitabilityCriteriaEnabledKeyName];
						break;
					case SuitabilityCriteriaLowerLimitKeyName:
						suitabilityParameterCriteria.LowerLimit = (double)suitabilityParameterDictionaryValuesDictionary[SuitabilityCriteriaLowerLimitKeyName];
						break;
					case SuitabilityCriteriaUpperLimitKeyName:
						suitabilityParameterCriteria.UpperLimit = (double)suitabilityParameterDictionaryValuesDictionary[SuitabilityCriteriaUpperLimitKeyName];
						break;
					case SuitabilityCriteriaRsdLimitKeyName:
						suitabilityParameterCriteria.RsdLimit = (double)suitabilityParameterDictionaryValuesDictionary[SuitabilityCriteriaRsdLimitKeyName];
						break;
				}
			}

			return suitabilityParameterCriteria;
		}


		public static object Deserialize(string json)
		{
			return ToObject(JToken.Parse(json));
		}

		private static object ToObject(JToken token)
		{
			switch (token.Type)
			{
				case JTokenType.Object:
					var dic = token.Children<JProperty>()
						.ToDictionary(prop => prop.Name,
							prop => ToObject(prop.Value));
					return dic;


				case JTokenType.Array:
					return token.Select(ToObject).ToList();

				default:
					return ((JValue)token).Value;
			}
		}

	}
}