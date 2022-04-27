using DeviceModuleDetails15 = PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography.AcquisitionMethod.DeviceModuleDetails;
using DeviceModuleDetails16 = PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography.AcquisitionMethod.DeviceModuleDetails;

namespace PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography.AcquisitionMethod
{
    public class DeviceModuleDetails
    {
        public static DeviceModuleDetails16 Transform(DeviceModuleDetails15 deviceModuleDetails)
        {
            var deviceModuleDetails16 = new DeviceModuleDetails16
            {
                DeviceMethodId = deviceModuleDetails.DeviceMethodId,
                Id = deviceModuleDetails.Id,
                Name = deviceModuleDetails.Name,
                IsDisplayDriver = deviceModuleDetails.IsDisplayDriver,
                DeviceType = deviceModuleDetails.DeviceType,
                DeviceModuleId = deviceModuleDetails.DeviceModuleId,
                InstrumentMasterId = deviceModuleDetails.InstrumentMasterId,
                InstrumentId = deviceModuleDetails.InstrumentId,
                DeviceDriverItemId = deviceModuleDetails.DeviceDriverItemId,
                SettingsUserInterfaceSupported = deviceModuleDetails.SettingsUserInterfaceSupported,
                Simulation = deviceModuleDetails.Simulation,
                CommunicationTestedSuccessfully = deviceModuleDetails.CommunicationTestedSuccessfully,
                FirmwareVersion = deviceModuleDetails.FirmwareVersion,
                SerialNumber = deviceModuleDetails.SerialNumber,
                ModelName = deviceModuleDetails.ModelName,
                UniqueIdentifier = deviceModuleDetails.UniqueIdentifier,
                InterfaceAddress = deviceModuleDetails.InterfaceAddress
            };
            return deviceModuleDetails16;
        }
    }
}
