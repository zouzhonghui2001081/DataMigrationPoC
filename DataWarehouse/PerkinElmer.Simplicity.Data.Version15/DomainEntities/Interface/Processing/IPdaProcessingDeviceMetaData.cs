namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Processing
{
    public interface IPdaProcessingDeviceMetaData : IProcessingDeviceMetaData
    {
        bool AutoZero { get; set; }
    }
}