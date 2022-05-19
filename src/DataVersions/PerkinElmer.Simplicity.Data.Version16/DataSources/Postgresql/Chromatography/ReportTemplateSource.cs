using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using log4net;
using Npgsql;
using PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.Chromatography;
using PerkinElmer.Simplicity.Data.Version16.DataEntities.AuditTrail;
using PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography.ReviewApprove;
using PerkinElmer.Simplicity.Data.Version16.DataSources.Postgresql.AuditTrail;
using PerkinElmer.Simplicity.Data.Version16.Version;
using PerkinElmer.Simplicity.Data.Version16.Version.Data;
using PerkinElmer.Simplicity.Data.Version16.Version.Data.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version16.DataSources.Postgresql.Chromatography
{
    internal class ReportTemplateSource 
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static IList<Version16DataBase> GetReportTemplates(Guid projectGuid,
            bool isIncludeAuditTrail)
        {
            var migrationEntities = new List<Version16DataBase>();
            var reportTemplateDao = new ReportTemplateDao();
            using (var connection = new NpgsqlConnection(Version16Host.ChromatographyConnection))
            {
                if (connection.State != ConnectionState.Open) connection.Open();
                var reportTemplates = reportTemplateDao.GetList(connection, projectGuid, true);

                foreach (var reportTemplate in reportTemplates)
                {
                    var reviewApproveData = EntityAssociatedReviewApproveSource.GetReviewApproveData(connection, projectGuid,
                        reportTemplate.Id.ToString(), ReviewApproveEntityType.ReportTemplate);
                    var reportTemplateData = new ReportTemplateData
                    {
                        ProjectGuid = projectGuid,
                        ReportTemplate = reportTemplate,
                        ReviewApproveData = reviewApproveData
                    };

                    if (isIncludeAuditTrail)
                        reportTemplateData.AuditTrailLogs = EntityAssociatedAuditTrailSource.GetAuditTrail( reportTemplate.Id.ToString(), EntityTypeConstants.ReportTemplate);

                    migrationEntities.Add(reportTemplateData);
                }
                connection.Close();
            }

            return migrationEntities;
        }

        public static IList<Version16DataBase> GetReportTemplates(Guid projectGuid, IList<Guid> reportTemplateGuids,
            bool isIncludeAuditTrail)
        {
            var migrationEntities = new List<Version16DataBase>();
            var reportTemplateDao = new ReportTemplateDao();
            using (var connection = new NpgsqlConnection(Version16Host.ChromatographyConnection))
            {
                if (connection.State != ConnectionState.Open) connection.Open();
                foreach (var reportTemplateGuid in reportTemplateGuids)
                {
                    var reportTemplate = reportTemplateDao.Get(connection, projectGuid, reportTemplateGuid);
                    var reviewApproveData = EntityAssociatedReviewApproveSource.GetReviewApproveData(connection, projectGuid,
                        reportTemplateGuid.ToString(), ReviewApproveEntityType.ReportTemplate);

                    var reportTemplateData = new ReportTemplateData
                    {
                        ProjectGuid = projectGuid,
                        ReportTemplate = reportTemplate,
                        ReviewApproveData = reviewApproveData
                    };
                    if (isIncludeAuditTrail)
                        reportTemplateData.AuditTrailLogs = EntityAssociatedAuditTrailSource.GetAuditTrail(reportTemplate.Id.ToString(), EntityTypeConstants.ReportTemplate);

                    migrationEntities.Add(reportTemplateData);
                }
                connection.Close();
            }

            return migrationEntities;
        }
    }
}
