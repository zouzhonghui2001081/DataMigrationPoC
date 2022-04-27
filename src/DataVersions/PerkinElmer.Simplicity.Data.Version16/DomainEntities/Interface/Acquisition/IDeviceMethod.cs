using System.Collections.Generic;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Acquisition
{
	public interface IDeviceMethod
	{
        IDeviceDriverItemDetails DeviceDriverItemDetails { get; set; }
        IList<IDeviceModuleDetails> DeviceModuleDetails { get; set; }
        IList<IDeviceChannelDescriptor> ExpectedDeviceChannelDescriptors { get; set; }
        string Content { get; set; }
	}
}