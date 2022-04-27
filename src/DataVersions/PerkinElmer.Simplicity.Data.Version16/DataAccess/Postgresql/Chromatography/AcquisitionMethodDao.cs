using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using Dapper;
using PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography.AcquisitionMethod;
using PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography.ReviewApprove;

namespace PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.Chromatography
{
	internal class AcquisitionMethodDao : AcquisitionMethodBaseDao
	{
		public void Create(IDbConnection connection, string projectName, AcquisitionMethod acquisitionMethod)
		{
			try
			{
				var insertSql = InsertSql + $"RETURNING { IdColumn}";
				acquisitionMethod.Id = connection.ExecuteScalar<long>(insertSql, acquisitionMethod);

				long? projectId = ProjectDao.GetProjectId(connection, projectName);

				if (projectId.HasValue)
				{
					ProjectToAcquisitionMethodMap projectToAcquisitionMethodMap = new ProjectToAcquisitionMethodMap()
					{
						AcquisitionMethodId = acquisitionMethod.Id,
						ProjectId = projectId.Value
					};

					ProjectToAcquisitionMethodMapDao projectToAcquisitionMethodMapDao = new ProjectToAcquisitionMethodMapDao();
					projectToAcquisitionMethodMapDao.Create(connection, projectToAcquisitionMethodMap);

				    // Update Project Start Date in case of first acquisition method for the project
				    ProjectDao projectDao = new ProjectDao();
				    var project = projectDao.GetProjectByName(connection, projectName);

				    if (project.StartDate == null)
				    {
				        project.StartDate = DateTime.UtcNow;
				        projectDao.UpdateProjectStartDate(connection, project);
				    }
                }
			}
			catch (Exception ex)
			{
				Log.Error("Error in Create method", ex);
				throw;
			}
		}
		public void Create(IDbConnection connection, Guid projectGuid, AcquisitionMethod acquisitionMethod)
		{
			try
			{
				var insertSql = InsertSql + $"RETURNING { IdColumn}";
				acquisitionMethod.Id = connection.ExecuteScalar<long>(insertSql, acquisitionMethod);
				long? projectId = ProjectDao.GetProjectId(connection, projectGuid);
				if (projectId.HasValue)
				{
					ProjectToAcquisitionMethodMap projectToAcquisitionMethodMap = new ProjectToAcquisitionMethodMap()
					{
						AcquisitionMethodId = acquisitionMethod.Id,
						ProjectId = projectId.Value
					};
					ProjectToAcquisitionMethodMapDao projectToAcquisitionMethodMapDao = new ProjectToAcquisitionMethodMapDao();
					projectToAcquisitionMethodMapDao.Create(connection, projectToAcquisitionMethodMap);
            }
			}
			catch (Exception ex)
			{
				Log.Error("Error in Create method", ex);
				throw;
			}
		}
		public void Create(IDbConnection connection, long batchResultSetId, AcquisitionMethod acquisitionMethod)
		{
			try
			{
				var insertSql = InsertSql + $"RETURNING { IdColumn}";
				acquisitionMethod.Id = connection.ExecuteScalar<long>(insertSql, acquisitionMethod);

				BatchResultSetToAcquisitionMethodMapDao batchResultSetToAcquisitionMethodMapDao = new BatchResultSetToAcquisitionMethodMapDao();
				BatchResultSetToAcquisitionMethodMap batchResultSetToAcquisitionMethodMap = new BatchResultSetToAcquisitionMethodMap()
				{
					AcquisitionMethodId = acquisitionMethod.Id,
					BatchResultSetId = batchResultSetId
				};

				// Save BatchResultSet to AcquisitionMethod mapping
				batchResultSetToAcquisitionMethodMapDao.Create(connection, batchResultSetToAcquisitionMethodMap);
			}
			catch (Exception ex)
			{
				Log.Error("Error in CreateChildren method", ex);
				throw;
			}
		}
		public IList<AcquisitionMethod> GetAcquisitionMethodByIds(IDbConnection connection, long[] acquisitionMethodIds)
		{
			try
			{
				string acquisitionMethodIdsCsv = string.Concat("(", string.Join(",", acquisitionMethodIds), ")");

				var acquisitionMethods =
					connection.Query<AcquisitionMethod>(SelectSql + $"FROM {TableName} WHERE {IdColumn} IN {acquisitionMethodIdsCsv}");

				return acquisitionMethods.ToList();
			}
			catch (Exception ex)
			{
				Log.Error("Error in GetAcquisitionMethodByIds method", ex);
				throw;
			}
		}
        public AcquisitionMethod GetAcquisitionMethodByGuid(IDbConnection connection, string guid)
        {
            try
            {
                Guid guidValue = new Guid(guid);
                string queryString = SelectSql +
                    $"FROM {TableName} " +
                    $"WHERE {GuidColumn} ='{guidValue}'";
                return connection.Query<AcquisitionMethod>(
                        queryString).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Log.Error("Error in GetAcquisitionMethodByGuid method", ex);
                throw;
            }
        }
        public AcquisitionMethod Get(IDbConnection connection, string projectName, string methodName)
		{
			try
			{
				return GetAcquisitionMethod(connection, projectName, methodName);
			}
			catch (Exception ex)
			{
				Log.Error("Error in Select method", ex);
				throw;
			}
		}

        public AcquisitionMethod Get(IDbConnection connection, string projectName, string methodName, int versionNumber)
        {
            try
            {
                return GetAcquisitionMethod(connection, projectName, methodName, versionNumber);
            }
            catch (Exception ex)
            {
                Log.Error("Error in Select method", ex);
                throw;
            }
        }

        public AcquisitionMethod Get(IDbConnection connection, Guid projectGuid, Guid methodGuid)
		{
			try
			{
				return GetAcquisitionMethod(connection, projectGuid, methodGuid);
			}
			catch (Exception ex)
			{
				Log.Error("Error in Select method", ex);
				throw;
			}
		}

		public AcquisitionMethod[] GetAll(IDbConnection connection, string projectName)
		{
			try
			{
				return GetAcquisitionMethods(connection, projectName);
			}
			catch (Exception ex)
			{
				Log.Error("Error in GetAll method", ex);
				throw;
			}
		}
		public AcquisitionMethod[] GetAll(IDbConnection connection, Guid projectGuid)
		{
			try
			{
				return GetAcquisitionMethods(connection, projectGuid);
			}
			catch (Exception ex)
			{
				Log.Error("Error in SelectAll method", ex);
				throw;
			}
		}

		public bool IsExists(IDbConnection connection, string projectName, string methodName)
		{
			try
			{
				AcquisitionMethod acquisitionMethod = GetAcquisitionMethod(connection, projectName, methodName);
				return (acquisitionMethod != null);
			}
			catch (Exception ex)
			{
				Log.Error("Error in IsExists method", ex);
				throw;
			}
		}
		public bool IsExists(IDbConnection connection, Guid projectGuid, string methodName)
		{
			try
			{
				AcquisitionMethod acquisitionMethod = GetAcquisitionMethod(connection, projectGuid, methodName);
				return (acquisitionMethod != null);
			}
			catch (Exception ex)
			{
				Log.Error("Error in IsExists method", ex);
				throw;
			}
		}


		public long UpdateContent(IDbConnection connection,
			string projectName,
			string methodName, 
			bool reconciledRunTime,
			string userId,
			string userName)
		{
			try
			{
                AcquisitionMethod oldMethod = GetAcquisitionMethod(connection, projectName, methodName);
                if (oldMethod == null)
                {
                    return -1;
                }
                AcquisitionMethod newMethod = new AcquisitionMethod()
                {
                    MethodName = methodName,
                    VersionNumber = oldMethod.VersionNumber + 1,
                    ReconciledRunTime = oldMethod.ReconciledRunTime,
                    CreateDate = oldMethod.CreateDate,
                    ModifyDate = DateTime.UtcNow,
                    CreateUserId = oldMethod.CreateUserId,
                    ModifyUserId = userId,
                    Guid = Guid.NewGuid(),
                    ReviewApproveState = (short)ReviewApproveState.NeverSubmitted,
					ModifyUserName = userName,
					CreateUserName = oldMethod.CreateUserName
                };
				Create(connection, projectName, newMethod);
				long newAcquisitionMethodId = newMethod.Id;
                return newAcquisitionMethodId;
            }
            catch (Exception ex)
			{
				Log.Error("Error in UpdateContent method", ex);
				throw;
			}
		}

		public void UpdateMethodName(IDbConnection connection, string projectName, string methodName, string newMethodName)
		{
			try
			{
				long? acquisitionMethodId = GetAcquisitionMethodId(connection, projectName, methodName);

				if (acquisitionMethodId.HasValue)
				{
					connection.Execute(
						$"UPDATE {TableName} " +
						$"SET {MethodNameColumn} = @{MethodNameColumn}," +
						$"{ModifyUserIdColumn} = @{ModifyUserIdColumn}," +
						$"{ModifyDateColumn} = @{ModifyDateColumn} " +
						$"WHERE {IdColumn} = @{IdColumn}",
						new { MethodName = newMethodName,
							ModifyUserId = 1,
							ModifyDate = DateTime.UtcNow,
							Id = acquisitionMethodId.Value });
				}
			}
			catch (Exception ex)
			{
				Log.Error("Error in Select UpdateMethodName", ex);
				throw;
			}
		}

		public void Delete(IDbConnection connection, string projectName, string methodName)
		{
			try
			{
				long? acquisitionMethodId = GetAcquisitionMethodId(connection, projectName, methodName);

				if (acquisitionMethodId.HasValue)
				{
					ProjectToAcquisitionMethodMapDao projectToAcquisitionMethodMapDao = new ProjectToAcquisitionMethodMapDao();
					projectToAcquisitionMethodMapDao.Delete(connection, projectName, methodName);
					connection.Execute($"DELETE FROM {TableName} WHERE {IdColumn} = {acquisitionMethodId.Value}");
				}
			}
			catch (Exception ex)
			{
				Log.Error("Error in Delete method", ex);
				throw;
			}
		}

        public void Delete(IDbConnection connection, long acquisitionMethodId)
		{
			try
			{
				connection.Execute($"DELETE FROM {TableName} WHERE {IdColumn} = @{IdColumn}",
					new {Id = acquisitionMethodId});
			}
			catch (Exception ex)
			{
				Log.Error("Error in Delete method", ex);
				throw;
			}
		}

		public void DeleteAll(IDbConnection connection, string projectName)
		{
			try
			{
				List<long> acquisitionMethodIds = connection.Query<long>(
					$"SELECT {TableName}.{IdColumn} " +
					$"FROM {ProjectToAcquisitionMethodMapDao.TableName} " +
					$"INNER JOIN {ProjectDao.TableName} ON {ProjectDao.TableName}.{ProjectDao.IdColumn} = {ProjectToAcquisitionMethodMapDao.TableName}.{ProjectToAcquisitionMethodMapDao.ProjectIdColumn} " +
					$"INNER JOIN {TableName} ON {ProjectToAcquisitionMethodMapDao.TableName}.{ProjectToAcquisitionMethodMapDao.AcquisitionMethodIdColumn} = {TableName}.{IdColumn} " +
					$"WHERE {ProjectDao.TableName}.{ProjectDao.ProjectNameColumn} = '{projectName}'").ToList();

				ProjectToAcquisitionMethodMapDao projectToAcquisitionMethodMapDao = new ProjectToAcquisitionMethodMapDao();
				projectToAcquisitionMethodMapDao.DeleteAll(connection, projectName);

				foreach (var acquisitionMethodId in acquisitionMethodIds)
				{
					connection.Execute($"DELETE FROM {TableName} WHERE {IdColumn} = {acquisitionMethodId}");
				}
			}
			catch (Exception ex)
			{
				Log.Error("Error in DeleteAll method", ex);
				throw;
			}
		}
		public void DeleteAll(IDbConnection connection, Guid projectGuid)
		{
			try
			{
				var acquisitionMethodIds = connection.Query<long>(
					$"SELECT {TableName}.{IdColumn} " +
					$"FROM {ProjectToAcquisitionMethodMapDao.TableName} " +
					$"INNER JOIN {ProjectDao.TableName} ON {ProjectDao.TableName}.{ProjectDao.IdColumn} = {ProjectToAcquisitionMethodMapDao.TableName}.{ProjectToAcquisitionMethodMapDao.ProjectIdColumn} " +
					$"INNER JOIN {TableName} ON {ProjectToAcquisitionMethodMapDao.TableName}.{ProjectToAcquisitionMethodMapDao.AcquisitionMethodIdColumn} = {TableName}.{IdColumn} " +
					$"WHERE {ProjectDao.TableName}.{ProjectDao.GuidColumn} = @ProjectGuid",
					new { ProjectGuid = projectGuid}).ToList();

				var projectToAcquisitionMethodMapDao = new ProjectToAcquisitionMethodMapDao();
				projectToAcquisitionMethodMapDao.DeleteAll(connection, projectGuid);
				foreach (var acquisitionMethodId in acquisitionMethodIds)
				{
					connection.Execute($"DELETE FROM {TableName} WHERE {IdColumn} = {acquisitionMethodId}");
				}
			}
			catch (Exception ex)
			{
				Log.Error("Error in DeleteAll method", ex);
				throw;
			}
		}
		public void Delete(IDbConnection connection, Guid projectGuid, Guid acquisitionMethodGuid)
		{
			try
			{
				var acquisitionMethodId = connection.QueryFirstOrDefault<long>(
					$"SELECT {TableName}.{IdColumn} " +
					$"FROM {ProjectToAcquisitionMethodMapDao.TableName} " +
					$"INNER JOIN {ProjectDao.TableName} ON {ProjectDao.TableName}.{ProjectDao.IdColumn} = {ProjectToAcquisitionMethodMapDao.TableName}.{ProjectToAcquisitionMethodMapDao.ProjectIdColumn} " +
					$"INNER JOIN {TableName} ON {ProjectToAcquisitionMethodMapDao.TableName}.{ProjectToAcquisitionMethodMapDao.AcquisitionMethodIdColumn} = {TableName}.{IdColumn} " +
					$"WHERE {ProjectDao.TableName}.{ProjectDao.GuidColumn} = @ProjectGuid AND " +
					$"{TableName}.{GuidColumn} = @AcquisitionMethodGuid",
					new { ProjectGuid = projectGuid, AcquisitionMethodGuid = acquisitionMethodGuid });
				var projectToAcquisitionMethodMapDao = new ProjectToAcquisitionMethodMapDao();
				projectToAcquisitionMethodMapDao.Delete(connection, projectGuid, acquisitionMethodGuid);
				connection.Execute($"DELETE FROM {TableName} WHERE {IdColumn} = @AcquisitionMethodId",
					new { AcquisitionMethodId = acquisitionMethodId});
			}
			catch (Exception ex)
			{
				Log.Error("Error in Delete method", ex);
				throw;
			}
		}
		// BatchResultSet
		public IList<AcquisitionMethod> GetAcquisitionMethodBatchResultById(IDbConnection connection, long[] acquisitionMethodIds)
		{
			try
            {
                List<AcquisitionMethod> acquisitionMethods = new List<AcquisitionMethod>();
                if (!(acquisitionMethodIds.Length > 0))
                    return acquisitionMethods;

				string acquisitionMethodIdsCsv = string.Concat("(", string.Join(",", acquisitionMethodIds), ")");
                string query = SelectSql +
                               $"FROM {TableName} " +
                               $"WHERE {TableName}.{IdColumn} IN {acquisitionMethodIdsCsv}";
                acquisitionMethods.AddRange(connection.Query<AcquisitionMethod>(query));
				return acquisitionMethods;
			}
			catch (Exception ex)
			{
				Log.Error("Error in GetAcquisitionMethodBatchResultById method", ex);
				throw;
			}
		}

		public AcquisitionMethod GetAcquisitionMethodInfoForBatchResultSet(IDbConnection connection, long batchResultSetId, Guid acquisitionMethodGuid)
		{
			try
			{
				return connection.Query<AcquisitionMethod>(
					SelectSql +
					$"FROM {BatchResultSetToAcquisitionMethodMapDao.TableName} " +
					$"INNER JOIN {TableName} ON {BatchResultSetToAcquisitionMethodMapDao.TableName}.{BatchResultSetToAcquisitionMethodMapDao.AcquisitionMethodIdColumn} = {TableName}.{IdColumn} " +
					$"WHERE {BatchResultSetToAcquisitionMethodMapDao.TableName}.{BatchResultSetToAcquisitionMethodMapDao.BatchResultIdSetColumn} = {batchResultSetId} AND {TableName}.{GuidColumn} = '{acquisitionMethodGuid}'").FirstOrDefault();
			}
			catch (Exception ex)
			{
				Log.Error("Error in GetAcquisitionMethodInfoForBatchResultSet method", ex);
				throw;
			}
		}
        private AcquisitionMethod[] GetAcquisitionMethods(IDbConnection connection, string projectName)
        {
            try
            {
                string queryString = $"SELECT {ProjectDao.TableName}.{ProjectDao.GuidColumn} " +
                    $"FROM {ProjectDao.TableName} " +
                    $"WHERE {ProjectDao.TableName}.{ProjectDao.ProjectNameColumn} = @ProjectName ";
                var projectGuid = connection.Query<Guid>(
                    queryString,
                    new { ProjectName = projectName }).FirstOrDefault();
                return GetAcquisitionMethods(connection, projectGuid);
            }
            catch (Exception ex)
            {
                Log.Error("Error in GetAcquisitionMethods method", ex);
                throw;
            }
        }

        private AcquisitionMethod[] GetAcquisitionMethods(IDbConnection connection, Guid projectGuid)
		{
			try
			{
				return connection.Query<AcquisitionMethod>(
					$"{SelectSql} " +
					$"FROM {ProjectToAcquisitionMethodMapDao.TableName} " +
					$"INNER JOIN {ProjectDao.TableName} ON {ProjectDao.TableName}.{ProjectDao.IdColumn} = {ProjectToAcquisitionMethodMapDao.TableName}.{ProjectToAcquisitionMethodMapDao.ProjectIdColumn} " +
					$"INNER JOIN {TableName} ON {ProjectToAcquisitionMethodMapDao.TableName}.{ProjectToAcquisitionMethodMapDao.AcquisitionMethodIdColumn} = {TableName}.{IdColumn} " +
					$"WHERE {ProjectDao.TableName}.{ProjectDao.GuidColumn} = @ProjectGuid",
					new { ProjectGuid = projectGuid}).ToArray();
			}
			catch (Exception ex)
			{
				Log.Error("Error in GetAcquisitionMethods method", ex);
				throw;
			}
		}

		private AcquisitionMethod GetAcquisitionMethod(IDbConnection connection, string projectName, string methodName)
		{
			try
			{
                return GetFirstOrLatestVersion(connection, projectName, methodName, false);
			}
			catch (Exception ex)
			{
				Log.Error("Error in GetAcquisitionMethod method", ex);
				throw;
			}
		}
		private AcquisitionMethod GetAcquisitionMethod(IDbConnection connection, Guid projectGuid, Guid methodGuid)
		{
			try
			{
				return connection.Query<AcquisitionMethod>(
					SelectSql +
					$"FROM {ProjectToAcquisitionMethodMapDao.TableName} " +
					$"INNER JOIN {ProjectDao.TableName} ON {ProjectDao.TableName}.{ProjectDao.IdColumn} = {ProjectToAcquisitionMethodMapDao.TableName}.{ProjectToAcquisitionMethodMapDao.ProjectIdColumn} " +
					$"INNER JOIN {TableName} ON {ProjectToAcquisitionMethodMapDao.TableName}.{ProjectToAcquisitionMethodMapDao.AcquisitionMethodIdColumn} = {TableName}.{IdColumn} " +
					$"WHERE {ProjectDao.TableName}.{ProjectDao.GuidColumn} = @ProjectGuid AND {TableName}.{GuidColumn} = @MethodGuid",
					new { ProjectGuid = projectGuid, MethodGuid = methodGuid}).FirstOrDefault();

			}
			catch (Exception ex)
			{
				Log.Error("Error in GetAcquisitionMethod method", ex);
				throw;
			}
		}
		private AcquisitionMethod GetAcquisitionMethod(IDbConnection connection, Guid projectGuid, string methodName)
		{
			try
			{
				methodName = methodName?.ToLower();

				return connection.Query<AcquisitionMethod>(
					SelectSql +
					$"FROM {ProjectToAcquisitionMethodMapDao.TableName} " +
					$"INNER JOIN {ProjectDao.TableName} ON {ProjectDao.TableName}.{ProjectDao.IdColumn} = {ProjectToAcquisitionMethodMapDao.TableName}.{ProjectToAcquisitionMethodMapDao.ProjectIdColumn} " +
					$"INNER JOIN {TableName} ON {ProjectToAcquisitionMethodMapDao.TableName}.{ProjectToAcquisitionMethodMapDao.AcquisitionMethodIdColumn} = {TableName}.{IdColumn} " +
					$"WHERE {ProjectDao.TableName}.{ProjectDao.GuidColumn} = @ProjectGuid AND LOWER({TableName}.{MethodNameColumn}) = @MethodName",
					new { ProjectGuid = projectGuid, MethodName = methodName }).FirstOrDefault();

			}
			catch (Exception ex)
			{
				Log.Error("Error in GetAcquisitionMethod method", ex);
				throw;
			}
		}

		public long? GetAcquisitionMethodId(IDbConnection connection, string projectName, string methodName)
		{
			try
            {
                string queryString = $"SELECT {TableName}.{IdColumn} " +
                    $"FROM {TableName}, {ProjectDao.TableName}, {ProjectToAcquisitionMethodMapDao.TableName} " +
                    $"WHERE LOWER({TableName}.{MethodNameColumn}) = @MethodName " +
                    $"AND {ProjectDao.TableName}.{ProjectDao.ProjectNameColumn} = @ProjectName " +
                    $"AND {ProjectToAcquisitionMethodMapDao.TableName}.{ProjectToAcquisitionMethodMapDao.ProjectIdColumn} = {ProjectDao.TableName}.{ProjectDao.IdColumn} " +
                    $"AND {ProjectToAcquisitionMethodMapDao.TableName}.{ProjectToAcquisitionMethodMapDao.AcquisitionMethodIdColumn} = {TableName}.{IdColumn} " +
                    $"ORDER BY {TableName}.{VersionNumberColumn} DESC LIMIT 1";
                var id = connection.Query<long?>(
                    queryString,
                    new {ProjectName = projectName, MethodName = methodName.ToLower()}).FirstOrDefault();
                return id;
            }
			catch (Exception ex)
			{
				Log.Error("Error in GetAcquisitionMethodId method", ex);
				throw;
			}
		}

        public void UpdateReviewApproveState(IDbConnection connection, string projectName, string acquisitionMethodId, ReviewApproveState state, string modifiedUser)
        {
            try
            {
                long? projectId = ProjectDao.GetProjectId(connection, projectName);
                if (projectId.HasValue)
                {
                    var newState = (short)state;
                    string sql =
                        $"UPDATE {TableName} SET {ModifyDateColumn} = @ModifyDate, {ModifyUserIdColumn} = @ModifyUser, " +
                        $"{ReviewApproveStateColumn} = @ReviewApproveState " +
                        $"WHERE {GuidColumn} = @AcquisitionMethodId";
                    connection.Execute(sql, new { AcquisitionMethodId = Guid.TryParse(acquisitionMethodId, out Guid returnValue) ? returnValue : Guid.Empty,
						ModifyDate = DateTime.UtcNow,
						ModifyUser = modifiedUser,
						ReviewApproveState = newState
					});
                }
                else
                {
                    Log.Error("Unable to Update Acquisition Method as Project Name does not Exist");
                    throw new NullReferenceException();
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error in UpdateReviewApproveState method", ex);
                throw;
            }
        }

        public AcquisitionMethod GetAcquisitionMethodInfoByAcquisitionMethodId(IDbConnection connection, long acquisitionMethodId)
        {
	        try
	        {
		       return connection.QueryFirstOrDefault<AcquisitionMethod>(
			        SelectSql +
			        $"FROM {TableName} " +
			        $"WHERE {TableName}.{IdColumn} = @Id",
			        new {Id = acquisitionMethodId});
	        }
	        catch (Exception ex)
	        {
		        Log.Error("Error in GetAcquisitionMethod method", ex);
		        throw;
	        }
        }

        public AcquisitionMethod[] GetAcquisitionMethodAllVersion(IDbConnection connection, string projectName, string methodName)
        {
            try
            {
                string queryString = SelectSql +
                    $"FROM {TableName},{ProjectDao.TableName},{ProjectToAcquisitionMethodMapDao.TableName} " +
                    $"WHERE LOWER({TableName}.{MethodNameColumn}) = @MethodName " +
                    $"AND {ProjectDao.TableName}.{ProjectDao.ProjectNameColumn} = @ProjectName " +
                    $"AND {ProjectToAcquisitionMethodMapDao.TableName}.{ProjectToAcquisitionMethodMapDao.ProjectIdColumn} = {ProjectDao.TableName}.{ProjectDao.IdColumn} " +
                    $"AND {ProjectToAcquisitionMethodMapDao.TableName}.{ProjectToAcquisitionMethodMapDao.AcquisitionMethodIdColumn} = {TableName}.{IdColumn} " +
                    $"ORDER BY {TableName}.{VersionNumberColumn} DESC";
                return connection.Query<AcquisitionMethod>(
                        queryString,
                        new { ProjectName = projectName, MethodName = methodName?.ToLower() }).ToArray();

            }
            catch (Exception ex)
            {
                Log.Error("Error in GetAcquisitionMethodAllVersion method", ex);
                throw;
            }
        }

        private AcquisitionMethod GetFirstOrLatestVersion(IDbConnection connection, string projectName, string methodName, bool firstVersion)
        {
            try
            {
                string order;
                if (firstVersion)
                {
                    order = "ASC";
                }
                else
                {
                    order = "DESC";
                }
                string queryString = SelectSql +
                    $"FROM {TableName}, {ProjectDao.TableName}, {ProjectToAcquisitionMethodMapDao.TableName} " +
                    $"WHERE LOWER({TableName}.{MethodNameColumn}) = @MethodName " +
                    $"AND {ProjectDao.TableName}.{ProjectDao.ProjectNameColumn} = @ProjectName " +
                    $"AND {ProjectToAcquisitionMethodMapDao.TableName}.{ProjectToAcquisitionMethodMapDao.ProjectIdColumn} = {ProjectDao.TableName}.{ProjectDao.IdColumn} " +
                    $"AND {ProjectToAcquisitionMethodMapDao.TableName}.{ProjectToAcquisitionMethodMapDao.AcquisitionMethodIdColumn} = {TableName}.{IdColumn} " +
                    $"ORDER BY {TableName}.{VersionNumberColumn} {order} LIMIT 1";
                return connection.Query<AcquisitionMethod>(
                        queryString,
                        new { ProjectName = projectName, MethodName = methodName?.ToLower() }).FirstOrDefault();

            }
            catch (Exception ex)
            {
                Log.Error("Error in GetFirstOrLatestVersion method", ex);
                throw;
            }
        }

        private AcquisitionMethod GetAcquisitionMethod(IDbConnection connection, string projectName, string methodName, int versionNumber)
        {
            try
            {
                string queryString = SelectSql +
                    $"FROM {TableName}, {ProjectDao.TableName}, {ProjectToAcquisitionMethodMapDao.TableName} " +
                    $"WHERE LOWER({TableName}.{MethodNameColumn}) = @MethodName " +
                    $"AND {ProjectDao.TableName}.{ProjectDao.ProjectNameColumn} = @ProjectName " +
                    $"AND {ProjectToAcquisitionMethodMapDao.TableName}.{ProjectToAcquisitionMethodMapDao.AcquisitionMethodIdColumn} = {TableName}.{IdColumn} " +
                    $"AND {ProjectToAcquisitionMethodMapDao.TableName}.{ProjectToAcquisitionMethodMapDao.ProjectIdColumn} = {ProjectDao.TableName}.{ProjectDao.IdColumn} " +
                    $"AND {TableName}.{VersionNumberColumn} = @VersionNumber";
                return connection.Query<AcquisitionMethod>(
                        queryString,
                        new { ProjectName = projectName, MethodName = methodName?.ToLower(), VersionNumber = versionNumber }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Log.Error("Error in GetAcquisitionMethod method", ex);
                throw;
            }
        }

        public AcquisitionMethod GetLatestApprovedVersion(IDbConnection connection, string projectName, string methodName)
        {
            try
            {
                string queryString = SelectSql +
                    $"FROM {TableName}, {ProjectDao.TableName}, {ProjectToAcquisitionMethodMapDao.TableName} " +
                    $"WHERE LOWER({TableName}.{MethodNameColumn}) = @MethodName " +
                    $"AND {TableName}.{ReviewApproveStateColumn} = @ReviewApproveState " +
                    $"AND {ProjectDao.TableName}.{ProjectDao.ProjectNameColumn} = @ProjectName " +
                    $"AND {ProjectToAcquisitionMethodMapDao.TableName}.{ProjectToAcquisitionMethodMapDao.ProjectIdColumn} = {ProjectDao.TableName}.{ProjectDao.IdColumn} " +
                    $"AND {ProjectToAcquisitionMethodMapDao.TableName}.{ProjectToAcquisitionMethodMapDao.AcquisitionMethodIdColumn} = {TableName}.{IdColumn} " +
                    $"ORDER BY {TableName}.{VersionNumberColumn} DESC LIMIT 1";
                return connection.Query<AcquisitionMethod>(
                        queryString,
                        new { ProjectName = projectName, MethodName = methodName?.ToLower(), ReviewApproveState = (short)ReviewApproveState.Approved }).FirstOrDefault();

            }
            catch (Exception ex)
            {
                Log.Error("Error in GetLatestApprovedVersion method", ex);
                throw;
            }
        }

        public AcquisitionMethod[] GetLatestAcquisitionMethodVersionsOfProject(IDbConnection connection, string projectName)
        {
            try
            {
                var allMethods = GetAcquisitionMethods(connection, projectName);
                if (allMethods == null)
                {
                    return null;
                }
                return allMethods.GroupBy(am => am.MethodName.ToLower(CultureInfo.InvariantCulture)).Select(group => group.OrderByDescending(am => am.VersionNumber).First()).ToArray();
            }
            catch (Exception ex)
            {
                Log.Error("Error in GetLatestAcquisitionMethodVersionsOfProject method", ex);
                throw;
            }
        }

    }
}
