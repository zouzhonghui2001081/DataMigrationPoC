using BatchResultDeviceModuleDetails15 = PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography.BatchResultDeviceModuleDetails;
using BatchResultDeviceModuleDetails16 = PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography.BatchResultDeviceModuleDetails;

namespace PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography
{
    public class BatchResultDeviceModuleDetails
    {
        public static BatchResultDeviceModuleDetails16 Transform(
            BatchResultDeviceModuleDetails15 batchResultDeviceModuleDetails)
        {
            if (batchResultDeviceModuleDetails == null) return null;

            return new BatchResultDeviceModuleDetails16
            {
                BatchResultSetId = batchResultDeviceModuleDetails.BatchResultSetId,
                Id = batchResultDeviceModuleDetails.Id,
                Name = batchResultDeviceModuleDetails.Name,
                IsDisplayDriver = batchResultDeviceModuleDetails.IsDisplayDriver,
                DeviceType = batchResultDeviceModuleDetails.DeviceType,
                DeviceModuleId = batchResultDeviceModuleDetails.DeviceModuleId,
                InstrumentMasterId = batchResultDeviceModuleDetails.InstrumentMasterId,
                InstrumentId = batchResultDeviceModuleDetails.InstrumentId,
                DeviceDriverItemId = batchResultDeviceModuleDetails.DeviceDriverItemId,
                SettingsUserInterfaceSupported = batchResultDeviceModuleDetails.SettingsUserInterfaceSupported,
                Simulation = batchResultDeviceModuleDetails.Simulation,
                CommunicationTestedSuccessfully = batchResultDeviceModuleDetails.CommunicationTestedSuccessfully,
                FirmwareVersion = batchResultDeviceModuleDetails.FirmwareVersion,
                SerialNumber = batchResultDeviceModuleDetails.SerialNumber,
                ModelName = batchResultDeviceModuleDetails.ModelName,
                UniqueIdentifier = batchResultDeviceModuleDetails.UniqueIdentifier,
                InterfaceAddress = batchResultDeviceModuleDetails.InterfaceAddress
            };
        }
    }
}
