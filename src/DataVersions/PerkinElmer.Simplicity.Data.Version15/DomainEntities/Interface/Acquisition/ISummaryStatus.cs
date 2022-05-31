using System.Collections.Generic;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Acquisition
{
    public interface ISummaryStatus
    {
        IDictionary<IDeviceModule, IStatusParameterList> Status { get;}
    }
}