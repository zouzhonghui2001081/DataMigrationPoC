using System.Collections.Generic;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Acquisition;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.Implementation.Acquisition
{
	internal class DeviceMethod : IDeviceMethod
	{
        public IDeviceDriverItemDetails DeviceDriverItemDetails { get; set; }
        public IList<IDeviceModuleDetails> DeviceModuleDetails { get; set; }
        public IList<IDeviceChannelDescriptor> ExpectedDeviceChannelDescriptors { get; set; }
        public string Content { get; set; }
	}
}