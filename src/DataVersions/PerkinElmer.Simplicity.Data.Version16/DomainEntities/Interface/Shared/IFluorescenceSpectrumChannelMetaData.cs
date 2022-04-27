namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Shared
{
    public interface IFluorescenceSpectrumChannelMetaData : IChannelMetaData
    {
        string Name { get; set; }
        int EMWavelengthIntervalInNanometer { get; set; }
        int EXWaveLengthInNanometer { get; set; }
        int EMWaveLengthStartInNanometer { get; set; }
        int EMWaveLengthEndInNanometer { get; set; }
        double SignalNoiseRatio { get; set; }
        double PeakIntensityOfRamanScattering { get; set; }
        double BaselineIntensityOfRamanScattering { get; set; }
        int PeakWavelengthOfRamanScattering { get; set; }
        string FlowCellType { get; set; }
        double SignalToNoiseRatioSpecification { get; set; }
    }
}