using ConfigurationItem15 = PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography.LabManagement.ConfigurationItem;
using ConfigurationItem16 = PerkinElmer.Simplicity.Data.Version16.Contract.DataEntities.Chromatography.LabManagement.ConfigurationItem;

namespace PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography.LabManagement
{
    public static class ConfigurationItemTransform
    {
        public static ConfigurationItem16 Transform(ConfigurationItem15 configurationItem)
        {
            if (configurationItem == null) return null;
            return new ConfigurationItem16
            {
                KeyName = configurationItem.KeyName,
                Value = configurationItem.Value
            };
        }
    }
}
