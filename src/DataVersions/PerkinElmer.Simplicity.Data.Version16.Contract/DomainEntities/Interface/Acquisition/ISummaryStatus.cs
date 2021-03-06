using System.Collections.Generic;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Acquisition
{
    public interface ISummaryStatus
    {
        IDictionary<IDeviceModule, IStatusParameterList> Status { get;}
    }
}