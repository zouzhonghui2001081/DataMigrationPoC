using System.Collections.Generic;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Shared
{
    public interface ISpectrumMetaData
    {
        double TimeInSeconds { get; set; }
        bool UsePreviousXValues { get; set; }
        IList<double> XValues { get; set; }
    }
}