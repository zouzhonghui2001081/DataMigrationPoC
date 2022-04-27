using DeviceDriverItemDetails15 = PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography.DeviceDriverItemDetails;
using DeviceDriverItemDetails16 = PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography.DeviceDriverItemDetails;

namespace PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography
{
    public class DeviceDriverItemDetails
    {
        public static DeviceDriverItemDetails16 Transform(
            DeviceDriverItemDetails15 deviceDriverItemDetails)
        {
            return new DeviceDriverItemDetails16
            {
                Id = deviceDriverItemDetails.Id,
                BatchResultSetId = deviceDriverItemDetails.BatchResultSetId,
                Configuration = deviceDriverItemDetails.Configuration,
                DeviceType = deviceDriverItemDetails.DeviceType,
                Name = deviceDriverItemDetails.Name,
                IsDisplayDriver = deviceDriverItemDetails.IsDisplayDriver,
                InstrumentMasterId = deviceDriverItemDetails.InstrumentMasterId,
                InstrumentId = deviceDriverItemDetails.InstrumentId,
                DeviceDriverItemId = deviceDriverItemDetails.DeviceDriverItemId
            };
        }
    }
}
