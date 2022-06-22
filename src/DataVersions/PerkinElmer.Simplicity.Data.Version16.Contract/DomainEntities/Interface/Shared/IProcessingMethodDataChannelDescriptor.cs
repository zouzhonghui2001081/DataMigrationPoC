namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Shared
{
    public interface IProcessingMethodDataChannelDescriptor
    {
        bool IsExtracted { get; }
        (string DataTypeDisplayName, string MetaDataDisplayName) GetDisplayName();
    }
}