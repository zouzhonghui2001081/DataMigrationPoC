using System;
using System.Data;
using System.Reflection;
using log4net;
using Npgsql;
using PerkinElmer.Simplicity.Data.Version15.DataAccess.Postgresql.Chromatography;
using PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography.ProcessingMethod;
using PerkinElmer.Simplicity.Data.Version15.DataTargets.Postgresql.AuditTrail;
using PerkinElmer.Simplicity.Data.Version15.Version;
using PerkinElmer.Simplicity.Data.Version15.Version.Data.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version15.DataTargets.Postgresql.Chromatography
{
    internal class ProcessingMethodTarget 
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        internal static void SaveProcessingMethod(ProcessingMethodData processingMethodData)
        {
            using (var connection = new NpgsqlConnection(Version15Host.ChromatographyConnection))
            {
                if (connection.State != ConnectionState.Open) connection.Open();
                CreateProcessingMethod(connection, processingMethodData.ProjectGuid, processingMethodData.ProcessingMethod);
                EntityAssociatedReviewApproveTarget.CreateReviewApproveEntity(connection, processingMethodData.ReviewApproveData);
                EntityAssociatedAuditTrailTarget.CreateAuditTrailLogs(processingMethodData.AuditTrailLogs);
                connection.Close();
            }
        }

        internal static long CreateProcessingMethod(IDbConnection connection, Guid projectId, ProcessingMethod processingMethod)
        {
            var projectDao = new ProjectDao();
            var processingMethodProjDao = new ProcessingMethodProjDao();
            var projectToProcessingMethodMapDao = new ProjectToProcessingMethodMapDao();
            
            var project = projectDao.GetProject(connection, projectId);
            if (project == null) throw new ArgumentNullException(nameof(project));
            if (processingMethodProjDao.IsExistsProcessingMethod(connection, project.Name, processingMethod.Name,
                processingMethod.Guid))
                return -1;

            processingMethodProjDao.Create(connection, processingMethod);

            var map = new ProjectToProcessingMethodMap()
            {
                ProcessingMethodId = processingMethod.Id,
                ProjectId = project.Id
            };
            projectToProcessingMethodMapDao.Create(connection, map);

            return processingMethod.Id;
        }

        public static long CreateProcessingMethod(IDbConnection connection, long batchResultSetId, ProcessingMethod processingMethod)
        {
            var processingMethodBatchResultDao = new ProcessingMethodBatchResultDao();
            processingMethodBatchResultDao.Create(connection, batchResultSetId, processingMethod);
            return processingMethod.Id;
        }

        public static long CreateModifiableProcessingMethod(IDbConnection connection, long analyisiResultSetId, ProcessingMethod modifiableProcessingMethod)
        {
            var processingMethodModifiableDao = new ProcessingMethodModifiableDao();
            processingMethodModifiableDao.Create(connection, analyisiResultSetId, modifiableProcessingMethod);
            return modifiableProcessingMethod.Id;
        }
    }
}
