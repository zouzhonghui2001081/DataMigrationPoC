namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Processing
{
    public interface IPdaProcessingDeviceMetaData : IProcessingDeviceMetaData
    {
        bool AutoZero { get; set; }
    }
}