namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Shared
{
    public interface IProcessingMethodDataChannelDescriptor
    {
        bool IsExtracted { get; }
        (string DataTypeDisplayName, string MetaDataDisplayName) GetDisplayName();
    }
}