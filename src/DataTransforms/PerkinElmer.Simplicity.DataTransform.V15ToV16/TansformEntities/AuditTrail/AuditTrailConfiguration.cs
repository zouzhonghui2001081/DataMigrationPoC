using AuditTrailConfiguration15 = PerkinElmer.Simplicity.Data.Version15.DataEntities.AuditTrail.AuditTrailConfiguration;
using AuditTrailConfiguration16 = PerkinElmer.Simplicity.Data.Version16.DataEntities.AuditTrail.AuditTrailConfiguration;

namespace PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.AuditTrail
{
    internal class AuditTrailConfiguration
    {
        public static AuditTrailConfiguration16 Transform(AuditTrailConfiguration15 auditTrailConfiguration)
        {
            if (auditTrailConfiguration == null) return null;
            return new AuditTrailConfiguration16
            {
                Key = auditTrailConfiguration.Key,
                Value = auditTrailConfiguration.Value
            };
        }
    }
}
