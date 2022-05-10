namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Processing
{
    public interface IPdaProcessingDeviceMetaData : IProcessingDeviceMetaData
    {
        bool AutoZero { get; set; }
    }
}