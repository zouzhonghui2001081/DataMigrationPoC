using System;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Processing.CompoundLibrary;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.JsonConverter.Processing.CompoundLibrary
{
    internal class CompoundLibraryItemJsonConverter : IJsonConverter<ICompoundLibraryItem>
    {
        private const int CurrentVersion = 1;
        private const string VersionKeyName = "Version";
        private const string CompoundNameKeyName = "CompoundName";
        private const string CompoundGuidKeyName = "CompoundGuid";
        private const string CreatedDateKeyName = "CreatedDate";
        private const string ItemContentKeyName = "ItemContent";

        public JObject ToJson(ICompoundLibraryItem instance)
        {
            return instance == null ? null : new JObject
            {
                {VersionKeyName, new JValue(CurrentVersion)},
                {CompoundNameKeyName, new JValue(instance.CompoundName)},
                {CompoundGuidKeyName, new JValue(instance.CompoundGuid)},
                {CreatedDateKeyName, new JValue(instance.CreatedDate)},
                {ItemContentKeyName, JsonConverterRegistry.GetConverter<ICompoundLibraryItemContent>().ToJson(instance.ItemContent)}
            };
        }

        public ICompoundLibraryItem FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;

            var version = (int)jObject[VersionKeyName];
            if (version > CurrentVersion)
                throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);

            var compoundLibraryItem = DomainFactory.Create<ICompoundLibraryItem>();
            compoundLibraryItem.CompoundName = (string)jObject[CompoundNameKeyName];
            compoundLibraryItem.CompoundGuid = (Guid)jObject[CompoundGuidKeyName];
            compoundLibraryItem.CreatedDate = (DateTime) jObject[CreatedDateKeyName];
            compoundLibraryItem.ItemContent = jObject[ItemContentKeyName].Type == JTokenType.Null ? 
                null : JsonConverterRegistry.GetConverter<ICompoundLibraryItemContent>().FromJson((JObject)jObject[ItemContentKeyName]);
            return compoundLibraryItem;
        }
    }
}
