using System.Collections.Generic;
using System.Linq;
using PerkinElmer.Acquisition.Devices;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Acquisition
{
    public static class DeviceModuleDetailsExtension
    {
        public static Dictionary<DeviceType, int> DeviceTypeMap = new Dictionary<DeviceType, int>()
        {
            {
                DeviceType.Pump, 0
            },
            {
                DeviceType.Autosampler, 1
            },
            {
                DeviceType.Oven, 2
            },
            {
                DeviceType.UvDetector, 3
            },
            {
                DeviceType.PhotodiodeArrayDetector, 4
            },
            {
                DeviceType.FluorescenceDetector, 5
            },
            {
                DeviceType.RefractiveIndexDetector, 6
            },
            {
                DeviceType.LcStack, 7
            },
            {
                DeviceType.Valve, 8
            },
            {
                DeviceType.MassSpec, 9
            }
        };

        public static IReadOnlyList<T> OrderByDeviceType<T>(this IReadOnlyList<T> deviceModuleDetails) where T:IDeviceModule
        {
            try
            {
                return (IReadOnlyList<T>)deviceModuleDetails.OrderBy(d => DeviceTypeMap[d.DeviceType]).ToList();
            }
            catch (System.Exception)
            {
                return deviceModuleDetails;
            }
        }
      
    }
}
