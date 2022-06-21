namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Shared
{
    public interface IChannelMetaData 
    {
        string ResponseUnit { get; set; }
        double DefaultMinYScale { get; set; }
        double DefaultMaxYScale { get; set; }
        double MinValidYValue { get; set; }
        double MaxValidYValue { get; set; }
        double SamplingRateInMilliseconds { get; set; }
    }
}