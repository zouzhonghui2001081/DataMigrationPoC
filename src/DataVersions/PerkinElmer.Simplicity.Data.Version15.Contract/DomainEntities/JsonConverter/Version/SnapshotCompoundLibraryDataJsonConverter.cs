using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Processing.CompoundLibrary;
using PerkinElmer.Simplicity.Data.Version15.Contract.Version.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.JsonConverter.Version
{
    internal class SnapshotCompoundLibraryDataJsonConverter : IJsonConverter<SnapshotCompoundLibraryData>
    {
        private const string SnapshotCompoundLibraryKeyName = "SnapshotCompoundLibrary";
        private const string CompoundLibraryItemsKeyName = "CompoundLibraryItems";

        public JObject ToJson(SnapshotCompoundLibraryData instance)
        {
            if (instance == null) return null;

            var compoundLibraryItemsDomain = new List<ICompoundLibraryItem>();
            if (instance.CompoundLibraryItems != null)
            {
                foreach (var compoundLibraryItem in instance.CompoundLibraryItems)
                {
                    var compoundLibraryItemDomain = DomainFactory.Create<ICompoundLibraryItem>();
                    var compoundItemContentDomain = DomainFactory.Create<ICompoundLibraryItemContent>();
                    DomainContractAdaptor.PopulateCompoundLibraryItem(compoundLibraryItem, compoundLibraryItemDomain);
                    DomainContractAdaptor.PopulateCompoundLibraryItemContent(compoundLibraryItem,
                        compoundItemContentDomain);
                    compoundLibraryItemDomain.ItemContent = compoundItemContentDomain;
                    compoundLibraryItemsDomain.Add(compoundLibraryItemDomain);
                }
            }

            var jObject = new JObject
            {
                { SnapshotCompoundLibraryKeyName, JsonConverterRegistry.GetConverter<SnapshotCompoundLibrary>().ToJson(instance.SnapshotCompoundLibrary)}
            };
            JsonConverterHelper.SetCollectionPropertyToJObject<ICompoundLibraryItem>(jObject, compoundLibraryItemsDomain, CompoundLibraryItemsKeyName);
            return jObject;
        }

        public SnapshotCompoundLibraryData FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;

            var snapshotCompoundLibraryData = new SnapshotCompoundLibraryData
            {
                SnapshotCompoundLibrary = jObject[SnapshotCompoundLibraryKeyName].Type == JTokenType.Null
                    ? null : JsonConverterRegistry.GetConverter<SnapshotCompoundLibrary>().FromJson((JObject) jObject[SnapshotCompoundLibraryKeyName])
            };
            var compoundLibraryItemsDomain = JsonConverterHelper.GetListPropertyFromJson<ICompoundLibraryItem>(jObject, CompoundLibraryItemsKeyName);

            var compoundLibraryItems = new List<CompoundLibraryItem>();
            var compoundLibraryItemContents = new List<ICompoundLibraryItemContent>();
            if (compoundLibraryItemsDomain != null)
            {
                foreach (var compoundLibraryItemDomain in compoundLibraryItemsDomain)
                    compoundLibraryItemContents.Add(compoundLibraryItemDomain.ItemContent);

                DomainContractAdaptor.PopulateCompoundLibraryItemEntity(compoundLibraryItemsDomain, compoundLibraryItems);
                DomainContractAdaptor.PopulateCompoundLibraryItemEntity(compoundLibraryItemContents, compoundLibraryItems);
            }
            
            snapshotCompoundLibraryData.CompoundLibraryItems = compoundLibraryItems;
            return snapshotCompoundLibraryData;
        }
    }
}
