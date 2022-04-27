using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography.ProcessingMethod;
using PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography.ReviewApprove;

namespace PerkinElmer.Simplicity.Data.Version15.DataAccess.Postgresql.Chromatography
{
    internal class ProcessingMethodWithVersionProjDao : ProcessingMethodBaseDao
    {
        private const int InitialVersionNumber = 1;

        public bool IsVersionExist(IDbConnection connection, string projectName,
            string methodName, int versionNumber)
        {
            if (string.IsNullOrWhiteSpace(projectName))
                throw new ArgumentException(nameof(projectName));
            if (string.IsNullOrWhiteSpace(methodName))
                throw new ArgumentException(nameof(methodName));
            if (versionNumber < InitialVersionNumber)
                throw new ArgumentException(nameof(versionNumber));
            try
            {
                var sqlString = selectSql +
                                $"FROM {ProjectDaoBase.TableName} " +
                                $"INNER JOIN {ProjectToProcessingMethodMapDao.TableName} ON {ProjectDaoBase.TableName}.{ProjectDaoBase.IdColumn} = {ProjectToProcessingMethodMapDao.TableName}.{ProjectToProcessingMethodMapDao.ProjectIdColumn} " +
                                $"INNER JOIN {TableName} ON {ProjectToProcessingMethodMapDao.TableName}.{ProjectToProcessingMethodMapDao.ProcessingMethodIdColumn} = {TableName}.{IdColumn} " +
                                $"WHERE LOWER({ProjectDaoBase.TableName}.{ProjectDaoBase.ProjectNameColumn}) = @ProjectName " +
                                $"AND LOWER({TableName}.{NameColumn}) = @ProcessingMethodName " +
                                $"AND {TableName}.{VersionNumberColumn} = @VersionNumber";

                projectName = projectName.ToLower();
                methodName = methodName.ToLower();
                var processingMethod = connection.QueryFirstOrDefault<ProcessingMethod>(
	                sqlString,
	                new { ProjectName = projectName, ProcessingMethodName = methodName, VersionNumber  = versionNumber});

                return processingMethod != null;
            }
            catch (Exception ex)
            {
                Log.Error("Error in IsExistsProcessingMethod method", ex);
                throw;
            }
        }

        public int GetNextVersionNumber(IDbConnection connection, string projectName,
            string methodName)
        {
            if (string.IsNullOrWhiteSpace(projectName))
                throw new ArgumentException(nameof(projectName));
            if (string.IsNullOrWhiteSpace(methodName))
                throw new ArgumentException(nameof(methodName));

            var projectProcessingMethodDao = new ProcessingMethodProjDao();
            if (!projectProcessingMethodDao.IsExists(connection, projectName, methodName))
                return InitialVersionNumber;

            try
            {
	            projectName = projectName.ToLower();
	            methodName = methodName.ToLower();

	            var sqlString = $"SELECT MAX({TableName}.{VersionNumberColumn}) " +
	                            $"FROM {ProjectDaoBase.TableName} " +
	                            $"INNER JOIN {ProjectToProcessingMethodMapDao.TableName} ON {ProjectDaoBase.TableName}.{ProjectDaoBase.IdColumn} = {ProjectToProcessingMethodMapDao.TableName}.{ProjectToProcessingMethodMapDao.ProjectIdColumn} " +
	                            $"INNER JOIN {TableName} ON {ProjectToProcessingMethodMapDao.TableName}.{ProjectToProcessingMethodMapDao.ProcessingMethodIdColumn} = {TableName}.{IdColumn} " +
	                            $"WHERE LOWER({ProjectDaoBase.TableName}.{ProjectDaoBase.ProjectNameColumn}) = @ProjectName " +
	                            $"AND LOWER({TableName}.{NameColumn}) = @MethodName";

                var currentMaxVersionNumber = connection.ExecuteScalar<int>(
	                sqlString, new { ProjectName = projectName, MethodName = methodName});
                return currentMaxVersionNumber < InitialVersionNumber
                    ? InitialVersionNumber
                    : currentMaxVersionNumber + 1;
            }
            catch (Exception ex)
            {
                Log.Error("Error in GetNextVersionNumberForProcessingMethod method", ex);
                throw;
            }
        }

        public ProcessingMethod GetMethodByVersion(IDbConnection connection, string projectName, string methodName, int version)
        {
            if (string.IsNullOrWhiteSpace(projectName))
                throw new ArgumentException(nameof(projectName));
            if (string.IsNullOrWhiteSpace(methodName))
                throw new ArgumentException(nameof(methodName));

            try
            {
	            projectName = projectName.ToLower();
	            methodName = methodName.ToLower();

                var processingMethod = connection.QueryFirstOrDefault<ProcessingMethod>(
                    selectSql +
                    $"FROM {ProjectDaoBase.TableName} " +
                    $"INNER JOIN {ProjectToProcessingMethodMapDao.TableName} ON {ProjectDaoBase.TableName}.{ProjectDaoBase.IdColumn} = {ProjectToProcessingMethodMapDao.TableName}.{ProjectToProcessingMethodMapDao.ProjectIdColumn} " +
                    $"INNER JOIN {TableName} ON {ProjectToProcessingMethodMapDao.TableName}.{ProjectToProcessingMethodMapDao.ProcessingMethodIdColumn} = {TableName}.{IdColumn} " +
                    $"WHERE LOWER({ProjectDaoBase.TableName}.{ProjectDaoBase.ProjectNameColumn}) = @ProjectName " +
                    $"AND LOWER({TableName}.{NameColumn}) = @MethodName " +
                    $"AND {TableName}.{VersionNumberColumn} = @VersionNumber ",
                    new { ProjectName = projectName, MethodName = methodName, VersionNumber = version });

                GetProcessingMethodChildren(connection, processingMethod);
                return processingMethod;
            }
            catch (Exception ex)
            {
                Log.Error("Error in GetProcessingMethodByVersion method", ex);
                throw;
            }
        }

        public ProcessingMethod GetLatestApprovedVersionByName(IDbConnection connection, string projectName,
            string methodName)
        {
            if (string.IsNullOrWhiteSpace(projectName))
                throw new ArgumentException(nameof(projectName));
            if (string.IsNullOrWhiteSpace(methodName))
                throw new ArgumentException(nameof(methodName));
            try
            {
	            projectName = projectName.ToLower();
	            methodName = methodName.ToLower();

                var approvedProcessingMethods = connection.Query<ProcessingMethod>(
                    selectSql +
                    $"FROM {ProjectDaoBase.TableName} " +
                    $"INNER JOIN {ProjectToProcessingMethodMapDao.TableName} ON {ProjectDaoBase.TableName}.{ProjectDaoBase.IdColumn} = {ProjectToProcessingMethodMapDao.TableName}.{ProjectToProcessingMethodMapDao.ProjectIdColumn} " +
                    $"INNER JOIN {TableName} ON {ProjectToProcessingMethodMapDao.TableName}.{ProjectToProcessingMethodMapDao.ProcessingMethodIdColumn} = {TableName}.{IdColumn} " +
                    $"WHERE LOWER({ProjectDaoBase.TableName}.{ProjectDaoBase.ProjectNameColumn}) = @ProjectName " +
                    $"AND LOWER({TableName}.{NameColumn}) = @MethodName " +
                    $"AND {ReviewApproveStateColumn} = @ReviewApproveState",
                    new { ProjectName = projectName, MethodName = methodName, ReviewApproveState = ReviewApproveState.Approved });

                var latestApprovedVersion = approvedProcessingMethods.OrderByDescending(method => method.VersionNumber).FirstOrDefault();
                if(latestApprovedVersion != null)
                    GetProcessingMethodChildren(connection, latestApprovedVersion);
                return latestApprovedVersion;
            }
            catch (Exception ex)
            {
                Log.Error("Error in GetProcessingMethodByVersion method", ex);
                throw;
            }
        }

        public ProcessingMethod GetLatestVersionByName(IDbConnection connection, string projectName,
            string methodName)
        {
            if (string.IsNullOrWhiteSpace(projectName))
                throw new ArgumentException(nameof(projectName));
            if (string.IsNullOrWhiteSpace(methodName))
                throw new ArgumentException(nameof(methodName));
            try
            {
                projectName = projectName.ToLower();
                methodName = methodName.ToLower();

                var approvedProcessingMethods = connection.Query<ProcessingMethod>(
                    selectSql +
                    $"FROM {ProjectDaoBase.TableName} " +
                    $"INNER JOIN {ProjectToProcessingMethodMapDao.TableName} ON {ProjectDaoBase.TableName}.{ProjectDaoBase.IdColumn} = {ProjectToProcessingMethodMapDao.TableName}.{ProjectToProcessingMethodMapDao.ProjectIdColumn} " +
                    $"INNER JOIN {TableName} ON {ProjectToProcessingMethodMapDao.TableName}.{ProjectToProcessingMethodMapDao.ProcessingMethodIdColumn} = {TableName}.{IdColumn} " +
                    $"WHERE LOWER({ProjectDaoBase.TableName}.{ProjectDaoBase.ProjectNameColumn}) = @ProjectName " +
                    $"AND LOWER({TableName}.{NameColumn}) = @MethodName ",
                    new { ProjectName = projectName, MethodName = methodName});

                var latestVersion = approvedProcessingMethods.OrderByDescending(method => method.VersionNumber).FirstOrDefault();
                if (latestVersion != null)
                    GetProcessingMethodChildren(connection, latestVersion);
                return latestVersion;
            }
            catch (Exception ex)
            {
                Log.Error("Error in GetProcessingMethodByVersion method", ex);
                throw;
            }
        }

        public IList<ProcessingMethod> GetVersionsInfoByName(IDbConnection connection, string projectName,
            string methodName)
        {
            if (string.IsNullOrWhiteSpace(projectName))
                throw new ArgumentException(nameof(projectName));
            if (string.IsNullOrWhiteSpace(methodName))
                throw new ArgumentException(nameof(methodName));
            try
            {
	            projectName = projectName.ToLower();
	            methodName = methodName.ToLower();

                var processingMethodsVersion = connection.Query<ProcessingMethod>(
                    selectSql +
                    $"FROM {ProjectDaoBase.TableName} " +
                    $"INNER JOIN {ProjectToProcessingMethodMapDao.TableName} ON {ProjectDaoBase.TableName}.{ProjectDaoBase.IdColumn} = {ProjectToProcessingMethodMapDao.TableName}.{ProjectToProcessingMethodMapDao.ProjectIdColumn} " +
                    $"INNER JOIN {TableName} ON {ProjectToProcessingMethodMapDao.TableName}.{ProjectToProcessingMethodMapDao.ProcessingMethodIdColumn} = {TableName}.{IdColumn} " +
                    $"WHERE LOWER({ProjectDaoBase.TableName}.{ProjectDaoBase.ProjectNameColumn}) = @ProjectName " +
                    $"AND LOWER({TableName}.{NameColumn}) = @MethodName",
                    new { ProjectName = projectName, MethodName = methodName}).ToList();

                return processingMethodsVersion;
            }
            catch (Exception ex)
            {
                Log.Error("Error in GetVersionsInfoByName method", ex);
                throw;
            }
        }

        public long CreateVersion(IDbConnection connection, string projectName, ProcessingMethod processingMethod)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(projectName))
                    throw new ArgumentException(nameof(projectName));
                if (string.IsNullOrWhiteSpace(processingMethod.Name))
                    throw new ArgumentException(nameof(processingMethod.Name));
                if (processingMethod.VersionNumber == null || processingMethod.VersionNumber < InitialVersionNumber)
                    throw new ArgumentException(nameof(processingMethod.VersionNumber));

                if (processingMethod.Name.Contains('\''))
                    processingMethod.Name = processingMethod.Name.Replace("'", "''");

                var processingMethodProjDao = new ProcessingMethodProjDao();
                if (processingMethodProjDao.IsExistsProcessingMethod(connection, projectName, processingMethod.Name, processingMethod.Guid))
                    throw new ArgumentException($"The processing method {processingMethod.Name} with Guid {processingMethod.Guid} already exist");
                if (IsVersionExist(connection, projectName, processingMethod.Name, processingMethod.VersionNumber.Value))
                    throw new ArgumentException($"The processing method {processingMethod.Name} with version {processingMethod.VersionNumber} already exist");
                var projectId = ProjectDao.GetProjectId(connection, projectName);
                if (!projectId.HasValue)
                    throw new ArgumentException($"The project {projectName} not exist");

                Create(connection, processingMethod);
                var projectToProcessingMethodMap = new ProjectToProcessingMethodMap
                {
                    ProcessingMethodId = processingMethod.Id,
                    ProjectId = projectId.Value
                };

                var projectToProcessingMethodMapDao = new ProjectToProcessingMethodMapDao();
                projectToProcessingMethodMapDao.Create(connection, projectToProcessingMethodMap);

            }
            catch (Exception ex)
            {
                Log.Error("Error in CreateVersion method", ex);
                throw;
            }

            return processingMethod.Id;
        }
    }
}
