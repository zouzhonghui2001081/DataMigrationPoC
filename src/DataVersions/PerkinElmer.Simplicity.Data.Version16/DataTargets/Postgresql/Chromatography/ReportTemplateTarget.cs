using System;
using System.Data;
using System.Reflection;
using log4net;
using Npgsql;
using PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.Chromatography;
using PerkinElmer.Simplicity.Data.Version16.DataTargets.Postgresql.AuditTrail;
using PerkinElmer.Simplicity.Data.Version16.Contract.Version.Chromatography;
using PerkinElmer.Simplicity.Data.Version16.Version.Context.TargetContext;

namespace PerkinElmer.Simplicity.Data.Version16.DataTargets.Postgresql.Chromatography
{
    internal class ReportTemplateTarget 
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        internal static void SaveReportTemplate(ReportTemplateData reportTemplateData, PostgresqlTargetContext postgresqlTargetContext)
        {
            using var connection = new NpgsqlConnection(postgresqlTargetContext.ChromatographyConnectionString);
            if (connection.State != ConnectionState.Open) connection.Open();
            var projectDao = new ProjectDao();
            var reportTemplateDao = new ReportTemplateDao();

            var project = projectDao.GetProject(connection, reportTemplateData.ProjectGuid);
            if (project == null) throw new ArgumentNullException(nameof(project));
            if (reportTemplateDao.IsExists(connection, project.Guid, reportTemplateData.ReportTemplate.Name))
                return;

            reportTemplateData.ReportTemplate.ProjectId = project.Id;
            reportTemplateDao.Insert(connection, reportTemplateData.ReportTemplate);
            if (reportTemplateData.ReviewApproveData != null)
                EntityAssociatedReviewApproveTarget.CreateReviewApproveEntity(connection, reportTemplateData.ReviewApproveData);
            EntityAssociatedAuditTrailTarget.CreateAuditTrailLogs(postgresqlTargetContext.AuditTrailConnectionString, reportTemplateData.AuditTrailLogs);
            connection.Close();
        }
    }
}
