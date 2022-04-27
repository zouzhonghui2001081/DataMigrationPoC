using System;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Processing
{
    public interface ISpectrumMethod : ICloneable, IEquatable<ISpectrumMethod>
    {
        Guid Guid { get; set; }
        double StartRetentionTime { get; set; }
        // EndRetentionTime==0.0 when StartRetTime is not zero means EndRetTime is the same as StartRetTime.
        double EndRetentionTime { get; set; }

        BaselineCorrectionType BaselineCorrectionType { get; set; }
        double BaselineStartRetentionTime { get; set; }
        double BaselineEndRetentionTime { get; set; }
        bool IsEqual(ISpectrumMethod other);
    }
}
