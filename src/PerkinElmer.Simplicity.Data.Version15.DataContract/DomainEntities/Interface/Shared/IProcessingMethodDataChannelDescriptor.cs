namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Shared
{
    public interface IProcessingMethodDataChannelDescriptor
    {
        bool IsExtracted { get; }
        (string DataTypeDisplayName, string MetaDataDisplayName) GetDisplayName();
    }
}