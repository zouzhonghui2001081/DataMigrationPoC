using System.Collections.Generic;
using DeviceMethod15 = PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography.AcquisitionMethod.DeviceMethod;
using DeviceMethod16 = PerkinElmer.Simplicity.Data.Version16.Contract.DataEntities.Chromatography.AcquisitionMethod.DeviceMethod;

namespace PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography.AcquisitionMethod
{
    public class DeviceMethod
    {
        public static DeviceMethod16 Transform(DeviceMethod15 deviceMethod)
        {
            var deviceMethod16 = new DeviceMethod16
            {
                Id = deviceMethod.Id,
                AcquisitionMethodId = deviceMethod.AcquisitionMethodId,
                Name = deviceMethod.Name,
                Content = deviceMethod.Content,
                Configuration = deviceMethod.Configuration,
                DeviceType = deviceMethod.DeviceType,
                InstrumentMasterId = deviceMethod.InstrumentMasterId,
                InstrumentId = deviceMethod.InstrumentId,
                DeviceDriverItemId = deviceMethod.DeviceDriverItemId,
            };
            var deviceModules = new List<Data.Version16.Contract.DataEntities.Chromatography.AcquisitionMethod.DeviceModuleDetails>();
            foreach (var deviceModule in deviceMethod.DeviceModules)
                deviceModules.Add(DeviceModuleDetails.Transform(deviceModule));
            deviceMethod16.DeviceModules = deviceModules.ToArray();

            var expectedDeviceChannelDescriptors = new List<Data.Version16.Contract.DataEntities.Chromatography.AcquisitionMethod.ExpectedDeviceChannelDescriptor>();
            foreach (var expectedDeviceChannelDescriptor in deviceMethod.ExpectedDeviceChannelDescriptors)
                expectedDeviceChannelDescriptors.Add(ExpectedDeviceChannelDescriptor.Transform(expectedDeviceChannelDescriptor));

            deviceMethod16.ExpectedDeviceChannelDescriptors = expectedDeviceChannelDescriptors.ToArray();
            return deviceMethod16;
        }
    }
}
