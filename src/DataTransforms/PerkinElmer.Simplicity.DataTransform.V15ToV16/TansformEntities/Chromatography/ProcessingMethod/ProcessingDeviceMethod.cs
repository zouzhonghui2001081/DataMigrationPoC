using ProcessingDeviceMethod15 = PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography.ProcessingMethod.ProcessingDeviceMethod;
using ProcessingDeviceMethod16 = PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography.ProcessingMethod.ProcessingDeviceMethod;

namespace PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography.ProcessingMethod
{
    public class ProcessingDeviceMethod
    {
        public static ProcessingDeviceMethod16 Transform(
            ProcessingDeviceMethod15 processingDeviceMethod)
        {
            if (processingDeviceMethod == null) return null;
            return new ProcessingDeviceMethod16
            {
                Id = processingDeviceMethod.Id,
                ProcessingMethodId = processingDeviceMethod.ProcessingMethodId,
                DeviceClass = processingDeviceMethod.DeviceClass,
                DeviceIndex = processingDeviceMethod.DeviceIndex,
                MetaData = processingDeviceMethod.MetaData
            };
        }
    }
}
