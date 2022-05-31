using ExpectedDeviceChannelDescriptor15 = PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography.AcquisitionMethod.ExpectedDeviceChannelDescriptor;
using ExpectedDeviceChannelDescriptor16 = PerkinElmer.Simplicity.Data.Version16.Contract.DataEntities.Chromatography.AcquisitionMethod.ExpectedDeviceChannelDescriptor;

namespace PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography.AcquisitionMethod
{
    public class ExpectedDeviceChannelDescriptor
    {
        public static ExpectedDeviceChannelDescriptor16 Transform(
            ExpectedDeviceChannelDescriptor15 expectedDeviceChannelDescriptor)
        {
            var expectedDeviceChannelDescriptor16 = new ExpectedDeviceChannelDescriptor16
            {
                Id = expectedDeviceChannelDescriptor.Id,
                DeviceMethodId = expectedDeviceChannelDescriptor.DeviceMethodId,
                DeviceChannelDescriptor = expectedDeviceChannelDescriptor.DeviceChannelDescriptor
            };
            return expectedDeviceChannelDescriptor16;
        }
    }
}
