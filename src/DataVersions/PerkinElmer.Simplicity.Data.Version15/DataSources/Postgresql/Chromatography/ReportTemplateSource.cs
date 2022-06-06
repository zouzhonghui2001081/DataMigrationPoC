using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using log4net;
using Npgsql;
using PerkinElmer.Simplicity.Data.Version15.DataAccess.Postgresql.Chromatography;
using PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.AuditTrail;
using PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography.ReviewApprove;
using PerkinElmer.Simplicity.Data.Version15.DataSources.Postgresql.AuditTrail;
using PerkinElmer.Simplicity.Data.Version15.Version;
using PerkinElmer.Simplicity.Data.Version15.Contract.Version;
using PerkinElmer.Simplicity.Data.Version15.Contract.Version.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version15.DataSources.Postgresql.Chromatography
{
    internal class ReportTemplateSource 
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static IList<Version15DataBase> GetReportTemplates(Guid projectGuid, bool isIncludeAuditTrail)
        {
            var migrationEntities = new List<Version15DataBase>();
            var reportTemplateDao = new ReportTemplateDao();
            using (var connection = new NpgsqlConnection(Version15Host.ChromatographyConnection))
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
                        reportTemplateData.AuditTrailLogs = EntityAssociatedAuditTrailSource.GetAuditTrail(reportTemplate.Id.ToString(), EntityTypeConstants.ReportTemplate);

                    migrationEntities.Add(reportTemplateData);
                }
                connection.Close();
            }

            return migrationEntities;
        }

        public static IList<Version15DataBase> GetReportTemplates(Guid projectGuid, IList<Guid> reportTemplateGuids, bool isIncludeAuditTrail)
        {
            var migrationEntities = new List<Version15DataBase>();
            var reportTemplateDao = new ReportTemplateDao();
            using (var connection = new NpgsqlConnection(Version15Host.ChromatographyConnection))
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
