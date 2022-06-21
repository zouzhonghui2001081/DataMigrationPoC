using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using log4net;
using Npgsql;
using PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.Chromatography;
using PerkinElmer.Simplicity.Data.Version16.Contract.DataEntities.AuditTrail;
using PerkinElmer.Simplicity.Data.Version16.Contract.DataEntities.Chromatography.ReviewApprove;
using PerkinElmer.Simplicity.Data.Version16.DataSources.Postgresql.AuditTrail;
using PerkinElmer.Simplicity.Data.Version16.Contract.Version;
using PerkinElmer.Simplicity.Data.Version16.Contract.Version.Chromatography;
using PerkinElmer.Simplicity.Data.Version16.Version.Context.SourceContext;

namespace PerkinElmer.Simplicity.Data.Version16.DataSources.Postgresql.Chromatography
{
    internal class ProcessingMethodSource 
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static IList<Version16DataBase> GetProcessingMethods(Guid projectGuid,
            PostgresqlSourceContext postgresqlSourceContext)
        {
            var migrationEntities = new List<Version16DataBase>();
            var processingMethodProjDao = new ProcessingMethodProjDao();

            using (var connection = new NpgsqlConnection(postgresqlSourceContext.ChromatographyConnectionString))
            {
                if (connection.State != ConnectionState.Open) connection.Open();
                var processingMethods = processingMethodProjDao.GetAllProcessingMethods(connection, projectGuid);
                foreach (var processingMethod in processingMethods)
                {
                    var reviewApproveData = EntityAssociatedReviewApproveSource.GetReviewApproveData(connection, projectGuid,
                        processingMethod.Guid.ToString(), ReviewApproveEntityType.ProcessingMethod);
                    var processingMethodData = new ProcessingMethodData
                    {
                        ProjectGuid = projectGuid,
                        ProcessingMethod = processingMethod,
                        ReviewApproveData = reviewApproveData
                    };
                    if (postgresqlSourceContext.IsIncludeAuditTrail)
                        processingMethodData.AuditTrailLogs = EntityAssociatedAuditTrailSource.GetAuditTrail(postgresqlSourceContext.AuditTrailConnectionString,processingMethod.Guid.ToString(), EntityTypeConstants.ProcessingMethod);

                    migrationEntities.Add(processingMethodData);
                }
                connection.Close();
            }

            return migrationEntities;
        }

        public static IList<Version16DataBase> GetProcessingMethod(Guid projectGuid,
            IList<Guid> processingMethodGuids, PostgresqlSourceContext postgresqlSourceContext)
        {
            var migrationEntities = new List<Version16DataBase>();
            var processingMethodProjDao = new ProcessingMethodProjDao();

            using (var connection = new NpgsqlConnection(postgresqlSourceContext.ChromatographyConnectionString))
            {
                if (connection.State != ConnectionState.Open) connection.Open();
                foreach (var processingMethodGuid in processingMethodGuids)
                {
                    var processingMethod = processingMethodProjDao.GetProcessingMethod(connection, projectGuid, processingMethodGuid);
                    var reviewApproveData = EntityAssociatedReviewApproveSource.GetReviewApproveData(connection, projectGuid,
                        processingMethod.Guid.ToString(), ReviewApproveEntityType.ProcessingMethod);
                    var processingMethodData = new ProcessingMethodData
                    {
                        ProjectGuid = projectGuid,
                        ProcessingMethod = processingMethod,
                        ReviewApproveData = reviewApproveData
                    };
                    if (postgresqlSourceContext.IsIncludeAuditTrail)
                        processingMethodData.AuditTrailLogs = EntityAssociatedAuditTrailSource.GetAuditTrail(postgresqlSourceContext.AuditTrailConnectionString, processingMethod.Guid.ToString(), EntityTypeConstants.ProcessingMethod);

                    migrationEntities.Add(processingMethodData);
                }
                connection.Close();
            }

            return migrationEntities;
        }
    }
}
