using System;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Processing.CompoundLibrary;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.JsonConverter.Processing.CompoundLibrary
{
    internal class CompoundLibraryJsonConverter : IJsonConverter<ICompoundLibrary>
    {
        const int CurrentVersion = 1;
        private const string VersionKeyName = "Version";
        private const string NameKeyName = "Name";
        private const string GuidKeyName = "Guid";
        private const string DescriptionKeyName = "Description";
        private const string CreatedTimeKeyName = "CreatedTime";
        private const string ModifiedTimeKeyName = "ModifiedTime";

        public JObject ToJson(ICompoundLibrary instance)
        {
            return instance == null ? null : new JObject
            {
                {VersionKeyName, new JValue(CurrentVersion)},
                {NameKeyName, new JValue(instance.Name)},
                {GuidKeyName, new JValue(instance.Guid)},
                {DescriptionKeyName, new JValue(instance.Description)},
                {CreatedTimeKeyName, new JValue(instance.CreatedTime)},
                {ModifiedTimeKeyName, new JValue(instance.ModifiedTime)}
            };
        }

        public ICompoundLibrary FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;

            var version = (int)jObject[VersionKeyName];
            if (version > CurrentVersion)
                throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);

            var compoundLibrary = DomainFactory.Create<ICompoundLibrary>();
            compoundLibrary.Name = (string)jObject[NameKeyName];
            compoundLibrary.Guid = (Guid)jObject[GuidKeyName];
            compoundLibrary.Description = (string)jObject[DescriptionKeyName];
            compoundLibrary.CreatedTime = (string)jObject[CreatedTimeKeyName];
            compoundLibrary.ModifiedTime = (string)jObject[ModifiedTimeKeyName];
            return compoundLibrary;
        }
    }
}
