namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Shared
{
    public enum DeviceChannelType
    {
        GC = 0, //IGCChannelMetaData
        UV = 1, //IUVChannelMetaData
        PDA = 2, //IPdaChannelMetaData
        FL = 3, //IFLChannelMetaData
        FLSpectrum = 4, //IFluorescenceSpectrumChannelMetaData
        RI = 5, // IRIChannelMetaData
        AToD = 6, // IAToDChannelMetaData
        Temperature = 7, //ITemperatureChannelMetaData 
        Pressure = 8, // IPressureChannelMetaData
        PumpFlow = 9, // IPumpFlowChannelMetaData
        SolventProportion = 10, // ISolventProportionChannelMetaData
        MultiUV = 11, //IMultiUVChannelMetaData
        AuxChannel = 12 // IAuxChannelMetaData;
    }
}