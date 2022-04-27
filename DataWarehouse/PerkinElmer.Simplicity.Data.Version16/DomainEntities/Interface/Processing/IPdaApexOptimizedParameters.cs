using System;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Processing
{
    public interface IPdaApexOptimizedParameters: IEquatable<IPdaApexOptimizedParameters>,ICloneable
    {
        double MinWavelength { get; set; }
        double MaxWavelength { get; set; }
        double WavelengthBandwidth { get; set; }
        bool UseReference { get; set; }
        double ReferenceWavelength { get; set; }
        double ReferenceWavelengthBandwidth { get; set; }
        bool ApplyBaselineCorrection { get; set; }
        bool UseAutoAbsorbanceThreshold { get; set; }
        double ManualAbsorbanceThreshold { get; set; }
        bool IsEqual(IPdaApexOptimizedParameters other);
    }
}
