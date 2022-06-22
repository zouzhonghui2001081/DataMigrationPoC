using System.Collections.Generic;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Acquisition
{
    public interface IStatusParameterList
    {
        string SubDeviceId { get; }
        IList<IStatusParameter> StatusParameters { get; }
    }
}
