namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Implementation.Shared
{
    public enum ProcessingMethodDataChannelType
    {
        GC = 0, // DetectorType
        UV = 1, // wavelength , programmed flag
        MultiUV = 2,
        FL = 3, // EX, EM, programmed flag
        RI = 4, // -
        AToD = 5, // -
        PdaExtracted = 6, // wavelength , programmed flag
        PdaApexOptimized = 7, // - wavelength
        PdaMic = 8 // - 
    }
}