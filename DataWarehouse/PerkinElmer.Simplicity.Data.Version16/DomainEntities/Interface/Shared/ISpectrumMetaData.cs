using System.Collections.Generic;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Shared
{
    public interface ISpectrumMetaData
    {
        double TimeInSeconds { get; set; }
        bool UsePreviousXValues { get; set; }
        IList<double> XValues { get; set; }
    }
}