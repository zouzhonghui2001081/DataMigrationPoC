using System.ComponentModel;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Processing.MaintenanceTesting
{
    public enum MaintenanceReportType
    {
        [Description("ASTM N&D Test")]
        NoiseAndDrift,
        [Description("Raman Sensitivity Test")]
        RamanScattering
    }
}
