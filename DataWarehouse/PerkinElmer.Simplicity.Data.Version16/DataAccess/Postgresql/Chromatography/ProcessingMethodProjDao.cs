using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography.ProcessingMethod;
using PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography.ReviewApprove;

namespace PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.Chromatography
{
    internal class ProcessingMethodProjDao : ProcessingMethodBaseDao
	{
		public ProcessingMethod GetProcessingMethod(IDbConnection connection, string projectName, string processingMethodName)
		{
			try
			{
				var processingMethod = GetProcessingMethodInfo(connection, projectName, processingMethodName);
				GetProcessingMethodChildren(connection, processingMethod);

				return processingMethod;
			}
			catch (Exception ex)
			{
				Log.Error("Error in GetProcessingMethod method", ex);
				throw;
			}
		}
		public ProcessingMethod GetProcessingMethod(IDbConnection connection, Guid projectGuid,
			Guid processingMethodGuid)
		{
			try
			{
				var processingMethod = GetProcessingMethodInfo(connection, projectGuid, processingMethodGuid);
				GetProcessingMethodChildren(connection, processingMethod);

				return processingMethod;
			}
			catch (Exception ex)
			{
				Log.Error("Error in GetProcessingMethod method", ex);
				throw;
			}
		}
		public IList<ProcessingMethod> GetAll(IDbConnection connection, Guid projectGuid)
		{
			try
			{
				var processingMethods = GetAllProcessingMethods(connection, projectGuid);

				foreach (var processingMethod in processingMethods)
				{
					GetProcessingMethodChildren(connection, processingMethod);
				}

				return processingMethods;
			}
			catch (Exception ex)
			{
				Log.Error("Error in GetAll method", ex);
				throw;
			}
		}
		public ProcessingMethod GetProcessingMethodInfo(IDbConnection connection, string projectName,
			string processingMethodName)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(projectName))
				{
					throw new ArgumentException(nameof(projectName));
				}

				if (string.IsNullOrWhiteSpace(processingMethodName))
				{
					throw new ArgumentException(nameof(processingMethodName));
				}

				projectName = projectName.ToLower();
				processingMethodName = processingMethodName.ToLower();

				var processingMethod = connection.QueryFirstOrDefault<ProcessingMethod>(
					selectSql +
					$"FROM {ProjectDao.TableName} " +
					$"INNER JOIN {ProjectToProcessingMethodMapDao.TableName} ON {ProjectDao.TableName}.{ProjectDao.IdColumn} = {ProjectToProcessingMethodMapDao.TableName}.{ProjectToProcessingMethodMapDao.ProjectIdColumn} " +
					$"INNER JOIN {TableName} ON {ProjectToProcessingMethodMapDao.TableName}.{ProjectToProcessingMethodMapDao.ProcessingMethodIdColumn} = {TableName}.{IdColumn} " +
					$"WHERE LOWER({ProjectDao.TableName}.{ProjectDao.ProjectNameColumn}) = @ProjectName AND LOWER({TableName}.{NameColumn}) = @ProcessingMethodName " +
                    $"ORDER BY {TableName}.{VersionNumberColumn} DESC LIMIT 1",
					new { ProjectName = projectName, ProcessingMethodName = processingMethodName});

				return processingMethod;

			}
			catch (Exception ex)
			{
				Log.Error("Error in GetProcessingMethodInfo method", ex);
				throw;
			}
		}
		public ProcessingMethod GetProcessingMethodInfo(IDbConnection connection, Guid projectGuid,
			Guid processingMethodGuid)
		{
			try
			{
				var processingMethod = connection.QueryFirstOrDefault<ProcessingMethod>(
					selectSql +
					$"FROM {ProjectDao.TableName} " +
					$"INNER JOIN {ProjectToProcessingMethodMapDao.TableName} ON {ProjectDao.TableName}.{ProjectDao.IdColumn} = {ProjectToProcessingMethodMapDao.TableName}.{ProjectToProcessingMethodMapDao.ProjectIdColumn} " +
					$"INNER JOIN {TableName} ON {ProjectToProcessingMethodMapDao.TableName}.{ProjectToProcessingMethodMapDao.ProcessingMethodIdColumn} = {TableName}.{IdColumn} " +
					$"WHERE {ProjectDao.TableName}.{ProjectDao.GuidColumn} = @ProjectGuid AND {TableName}.{GuidColumn} = @ProcessingMethodGuid",
					new { ProjectGuid = projectGuid, ProcessingMethodGuid = processingMethodGuid});

				return processingMethod;

			}
			catch (Exception ex)
			{
				Log.Error("Error in GetProcessingMethodInfo method", ex);
				throw;
			}
		}
		public IList<ProcessingMethod> GetAllProcessingMethods(IDbConnection connection, Guid projectGuid)
		{
			try
			{
				var processingMethods = connection.Query<ProcessingMethod>(
					selectSql +
					$"FROM {ProjectDao.TableName} " +
					$"INNER JOIN {ProjectToProcessingMethodMapDao.TableName} ON {ProjectDao.TableName}.{ProjectDao.IdColumn} = {ProjectToProcessingMethodMapDao.TableName}.{ProjectToProcessingMethodMapDao.ProjectIdColumn} " +
					$"INNER JOIN {TableName} ON {ProjectToProcessingMethodMapDao.TableName}.{ProjectToProcessingMethodMapDao.ProcessingMethodIdColumn} = {TableName}.{IdColumn} " +
					$"WHERE {ProjectDao.TableName}.{ProjectDao.GuidColumn} = @ProjectGuid",
					new { ProjectGuid = projectGuid}).ToList();

				foreach (var processingMethod in processingMethods)
				{
					GetProcessingMethodChildren(connection, processingMethod);
				}

				return processingMethods;

			}
			catch (Exception ex)
			{
				Log.Error("Error in GetProcessingMethods method", ex);
				throw;
			}
		}
		public IList<ProcessingMethod> GetAllProcessingMethodInfos(IDbConnection connection, Guid projectGuid)
		{
			try
			{
				var processingMethods = connection.Query<ProcessingMethod>(
					selectSql +
					$"FROM {ProjectDao.TableName} " +
					$"INNER JOIN {ProjectToProcessingMethodMapDao.TableName} ON {ProjectDao.TableName}.{ProjectDao.IdColumn} = {ProjectToProcessingMethodMapDao.TableName}.{ProjectToProcessingMethodMapDao.ProjectIdColumn} " +
					$"INNER JOIN {TableName} ON {ProjectToProcessingMethodMapDao.TableName}.{ProjectToProcessingMethodMapDao.ProcessingMethodIdColumn} = {TableName}.{IdColumn} " +
					$"WHERE {ProjectDao.TableName}.{ProjectDao.GuidColumn} = @ProjectGuid",
					new { ProjectGuid = projectGuid }).ToList();

				return processingMethods;

			}
			catch (Exception ex)
			{
				Log.Error("Error in GetProcessingMethodInfos method", ex);
				throw;
			}
		}

		public long Create(IDbConnection connection, string projectName, ProcessingMethod processingMethod)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(projectName))
				{
					throw new ArgumentException(nameof(projectName));
				}

				if (string.IsNullOrWhiteSpace(processingMethod.Name))
				{
					throw new ArgumentException(nameof(processingMethod.Name));
				}

				long? projectId = ProjectDao.GetProjectId(connection, projectName);

				if (projectId.HasValue)
				{
					var flag = CheckForSingleQuote(processingMethod, out var splitCount);
					if (flag && splitCount.Length >= 3)
					{
						processingMethod.Name = processingMethod.Name.Replace("'", "''");
					}

					projectName = projectName.ToLower();

					long processingMethodId = connection.QueryFirstOrDefault<long>($"SELECT {TableName}.{IdColumn} " +
								 $"FROM {ProjectDao.TableName} " +
								 $"INNER JOIN {ProjectToProcessingMethodMapDao.TableName} ON {ProjectDao.TableName}.{ProjectDao.IdColumn} = {ProjectToProcessingMethodMapDao.TableName}.{ProjectToProcessingMethodMapDao.ProjectIdColumn} " +
								 $"INNER JOIN {TableName} ON {ProjectToProcessingMethodMapDao.TableName}.{ProjectToProcessingMethodMapDao.ProcessingMethodIdColumn} = {TableName}.{IdColumn} " +
								 $"WHERE LOWER({ProjectDao.TableName}.{ProjectDao.ProjectNameColumn}) = @ProjectName AND {TableName}.{GuidColumn} =  @ProcessingMethodGuid",
						new { ProjectName = projectName, ProcessingMethodGuid = processingMethod.Guid});

					if (flag == false || ((flag && splitCount.Length >= 3)))
					{
						processingMethod.Name = processingMethod.Name.Replace("''", "'");
					}

					if (processingMethodId <= 0)
					{
						Create(connection, processingMethod);
						var map = new ProjectToProcessingMethodMap()
						{
							ProcessingMethodId = processingMethod.Id,
							ProjectId = projectId.Value
						};

						var mapDao = new ProjectToProcessingMethodMapDao();
						mapDao.Create(connection, map);
					}
					else
					{
						processingMethod.Id = processingMethodId;
						UpdateProcessingMethod(connection, projectName, processingMethod);
					}

					// Update Project Start Date in case of first processing method for the project
					ProjectDao projectDao = new ProjectDao();
					var project = projectDao.GetProjectByName(connection, projectName);

					if (project.StartDate == null)
					{
						project.StartDate = DateTime.UtcNow;
						projectDao.UpdateProjectStartDate(connection, project);
					}
				}
				else
				{
					throw new NullReferenceException();
				}
			}
			catch (Exception ex)
			{
				Log.Error("Error in Create method", ex);
				throw;
			}

			return processingMethod.Id;
		}

		public bool Create(IDbConnection connection, Guid projectGuid, ProcessingMethod processingMethod)
		{
			long? projectId = ProjectDao.GetProjectId(connection, projectGuid);

			if (projectId.HasValue)
			{
				Create(connection, processingMethod);
				var map = new ProjectToProcessingMethodMap()
				{
					ProcessingMethodId = processingMethod.Id,
					ProjectId = projectId.Value
				};

				var mapDao = new ProjectToProcessingMethodMapDao();
				mapDao.Create(connection, map);
				return true;
			}

			return false;
		}
		private static bool CheckForSingleQuote(ProcessingMethod processingMethod, out string[] splitCount)
		{
			var flag = false;
			splitCount = new string[processingMethod.Name.Length];
			if (processingMethod.Name.Contains('\''))
			{
				splitCount = processingMethod.Name.Split('\'');

				if (splitCount.Length <= 3)
				{
					processingMethod.Name = processingMethod.Name.Replace("'", "''");
				}
				else
				{
					for (int i = 0; i < splitCount.Length; i++)
					{
						if (splitCount[i].Length > 0)
						{
							flag = true;
							break;
						}
					}

					if (flag == false)
						processingMethod.Name = processingMethod.Name.Replace("'", "''");
				}
			}

			return flag;
		}

		public long CreateDefaultProcessingMethod(IDbConnection connection, string projectName,
			ProcessingMethod processingMethod)
		{
			try
			{
				long? projectId = ProjectDao.GetProjectId(connection, projectName);

				if (projectId.HasValue)
				{
					Create(connection, processingMethod);

					var map = new ProjectToProcessingMethodMap()
					{
						ProcessingMethodId = processingMethod.Id,
						ProjectId = projectId.Value
					};

					var mapDao = new ProjectToProcessingMethodMapDao();
					mapDao.Create(connection, map);
				}
			}
			catch (Exception ex)
			{
				Log.Error("Error in Create method", ex);
				throw;
			}

			return processingMethod.Id;
		}

		public bool IsExists(IDbConnection connection, string projectName, string processingMethodName)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(projectName))
				{
					throw new ArgumentException(nameof(projectName));
				}

				if (string.IsNullOrWhiteSpace(processingMethodName))
				{
					throw new ArgumentException(nameof(processingMethodName));
				}

				projectName = projectName.ToLower();
				processingMethodName = processingMethodName.ToLower();

				string sql = selectSql +
							 $"FROM {ProjectDao.TableName} " +
							 $"INNER JOIN {ProjectToProcessingMethodMapDao.TableName} ON {ProjectDao.TableName}.{ProjectDao.IdColumn} = {ProjectToProcessingMethodMapDao.TableName}.{ProjectToProcessingMethodMapDao.ProjectIdColumn} " +
							 $"INNER JOIN {TableName} ON {ProjectToProcessingMethodMapDao.TableName}.{ProjectToProcessingMethodMapDao.ProcessingMethodIdColumn} = {TableName}.{IdColumn} " +
							 $"WHERE LOWER({ProjectDao.TableName}.{ProjectDao.ProjectNameColumn}) = @ProjectName AND LOWER({TableName}.{NameColumn}) = @ProcessingMethodName";

				var processingMethod = connection.QueryFirstOrDefault<ProcessingMethod>(sql, new { ProjectName = projectName, ProcessingMethodName = processingMethodName});

				return (processingMethod != null);
			}
			catch (Exception ex)
			{
				Log.Error("Error in IsExists method", ex);
				throw;
			}
		}
		public bool IsProjectProcessingMethodExists(IDbConnection connection, Guid projectGuid, string processingMethodName)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(processingMethodName))
				{
					throw new ArgumentException(nameof(processingMethodName));
				}

				processingMethodName = processingMethodName.ToLower();

				string sql = selectSql +
				             $"FROM {ProjectDao.TableName} " +
				             $"INNER JOIN {ProjectToProcessingMethodMapDao.TableName} ON {ProjectDao.TableName}.{ProjectDao.IdColumn} = {ProjectToProcessingMethodMapDao.TableName}.{ProjectToProcessingMethodMapDao.ProjectIdColumn} " +
				             $"INNER JOIN {TableName} ON {ProjectToProcessingMethodMapDao.TableName}.{ProjectToProcessingMethodMapDao.ProcessingMethodIdColumn} = {TableName}.{IdColumn} " +
				             $"WHERE {ProjectDao.TableName}.{ProjectDao.GuidColumn} = @ProjectGuid AND LOWER({TableName}.{NameColumn}) = @ProcessingMethodName";

				var processingMethod = connection.QueryFirstOrDefault<ProcessingMethod>(sql, new { ProjectGuid = projectGuid, ProcessingMethodName = processingMethodName });

				return (processingMethod != null);
			}
			catch (Exception ex)
			{
				Log.Error("Error in IsProjectProcessingMethodExists method", ex);
				throw;
			}
		}

		public bool IsExistsProcessingMethod(IDbConnection connection, string projectName, string processingMethodName, Guid processingMethodGuid)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(projectName))
				{
					throw new ArgumentException(nameof(projectName));
				}

				if (string.IsNullOrWhiteSpace(processingMethodName))
				{
					throw new ArgumentException(nameof(processingMethodName));
				}

				projectName = projectName.ToLower();

				string sqlString = selectSql +
								   $"FROM {ProjectDao.TableName} " +
								   $"INNER JOIN {ProjectToProcessingMethodMapDao.TableName} ON {ProjectDao.TableName}.{ProjectDao.IdColumn} = {ProjectToProcessingMethodMapDao.TableName}.{ProjectToProcessingMethodMapDao.ProjectIdColumn} " +
								   $"INNER JOIN {TableName} ON {ProjectToProcessingMethodMapDao.TableName}.{ProjectToProcessingMethodMapDao.ProcessingMethodIdColumn} = {TableName}.{IdColumn} " +
								   $"WHERE LOWER({ProjectDao.TableName}.{ProjectDao.ProjectNameColumn}) = @ProjectName AND {TableName}.{GuidColumn} = @ProcessingMethodGuid";
				var processingMethod = connection.QueryFirstOrDefault<ProcessingMethod>(sqlString, new { ProjectName = projectName, ProcessingMethodGuid = processingMethodGuid});

				return (processingMethod != null);
			}
			catch (Exception ex)
			{
				Log.Error("Error in IsExistsProcessingMethod method", ex);
				throw;
			}
		}

		public List<ProcessingMethod> GetExternalProcessingMethods(IDbConnection connection, string projectName)
		{
			try
			{
				projectName = projectName.ToLower();

				var processingMethodList = connection.Query<ProcessingMethod>(
					selectSql +
					$"FROM {ProjectDao.TableName} " +
					$"INNER JOIN {ProjectToProcessingMethodMapDao.TableName} ON {ProjectDao.TableName}.{ProjectDao.IdColumn} = {ProjectToProcessingMethodMapDao.TableName}.{ProjectToProcessingMethodMapDao.ProjectIdColumn} " +
					$"INNER JOIN {TableName} ON {ProjectToProcessingMethodMapDao.TableName}.{ProjectToProcessingMethodMapDao.ProcessingMethodIdColumn} = {TableName}.{IdColumn} " +
					$"WHERE LOWER({ProjectDao.TableName}.{ProjectDao.ProjectNameColumn}) = @ProjectName",
					new { ProjectName = projectName});

				return processingMethodList.ToList();
			}
			catch (Exception ex)
			{
				Log.Error("Error in GetExternalProcessingMethods method", ex);
				throw;
			}
		}

		public ProcessingMethod GetExternalProcessingMethod(IDbConnection connection, string projectName, string processMethodName)
		{
			try
			{
				projectName = projectName.ToLower();
				processMethodName = processMethodName.ToLower();

				var processingMethod = connection.QueryFirstOrDefault<ProcessingMethod>(
					selectSql +
					$"FROM {ProjectDao.TableName} " +
					$"INNER JOIN {ProjectToProcessingMethodMapDao.TableName} ON {ProjectDao.TableName}.{ProjectDao.IdColumn} = {ProjectToProcessingMethodMapDao.TableName}.{ProjectToProcessingMethodMapDao.ProjectIdColumn} " +
					$"INNER JOIN {TableName} ON {ProjectToProcessingMethodMapDao.TableName}.{ProjectToProcessingMethodMapDao.ProcessingMethodIdColumn} = {TableName}.{IdColumn} " +
					$"WHERE LOWER({ProjectDao.TableName}.{ProjectDao.ProjectNameColumn}) = @ProjectName" +
					$" AND LOWER({TableName}.{NameColumn}) = @ProcessMethodName",
					new { ProjectName = projectName, ProcessMethodName = processMethodName});

				return processingMethod;
			}
			catch (Exception ex)
			{
				Log.Error("Error in GetExternalProcessingMethod method", ex);
				throw;
			}
		}

		public long Update(IDbConnection connection, string projectName, ProcessingMethod processingMethod)
		{
			if (string.IsNullOrWhiteSpace(projectName))
			{
				throw new ArgumentException(nameof(projectName));
			}

			projectName = projectName.ToLower();
			string processingMethodName = processingMethod.Name.ToLower();

			var processingMethodId = connection.QueryFirstOrDefault<long>(
				$"SELECT {TableName}.{IdColumn} " +
				$"FROM {ProjectDao.TableName} " +
				$"INNER JOIN {ProjectToProcessingMethodMapDao.TableName} ON {ProjectDao.TableName}.{ProjectDao.IdColumn} = {ProjectToProcessingMethodMapDao.TableName}.{ProjectToProcessingMethodMapDao.ProjectIdColumn} " +
				$"INNER JOIN {TableName} ON {ProjectToProcessingMethodMapDao.TableName}.{ProjectToProcessingMethodMapDao.ProcessingMethodIdColumn} = {TableName}.{IdColumn} " +
				$"WHERE LOWER({ProjectDao.TableName}.{ProjectDao.ProjectNameColumn}) = @ProjectName AND LOWER({TableName}.{NameColumn}) = @ProcessingMethodName",
				new { ProjectName = projectName, ProcessingMethodName = processingMethodName });

			if (processingMethodId > 0)
			{
				string sql =
					updateSql +
					$"WHERE {IdColumn} = {processingMethodId} RETURNING Id";
				connection.ExecuteScalar<long>(sql, processingMethod);

				UpdateChildren(connection, processingMethod);
			}

			return processingMethodId;
		}

		public long UpdateProcessingMethod(IDbConnection connection, string projectName, ProcessingMethod processingMethod)
		{
			if (string.IsNullOrWhiteSpace(projectName))
			{
				throw new ArgumentException(nameof(projectName));
			}

			string sql =
				$"UPDATE {TableName} SET " +
				$"{NameColumn} = @{NameColumn}," +
				$"{GuidColumn} = @{GuidColumn}," +
				$"{ModifiedDateColumn} = @{ModifiedDateColumn}," +
				$"{ModifiedUserIdColumn} = @{ModifiedUserIdColumn}," +
				$"{ModifiedFromOriginalColumn} = @{ModifiedFromOriginalColumn}," +
				$"{OriginalReadOnlyMethodGuidColumn} = @{OriginalReadOnlyMethodGuidColumn}," +
				$"{NumberOfLevelsColumn} = @{NumberOfLevelsColumn}," +
				$"{AmountUnitsColumn} = @{AmountUnitsColumn}," +
				$"{UnidentifiedPeakCalibrationTypeColumn} = @{UnidentifiedPeakCalibrationTypeColumn}," +
				$"{UnidentifiedPeakCalibrationFactorColumn} = @{UnidentifiedPeakCalibrationFactorColumn}," +
				$"{DescriptionColumn} = @{DescriptionColumn}," +
				$"{ReviewApproveStateColumn} = @{ReviewApproveStateColumn}," +
                $"{UnidentifiedPeakReferenceCompoundGuidColumn} = @{UnidentifiedPeakReferenceCompoundGuidColumn} " +
				$"WHERE {IdColumn} = {processingMethod.Id} RETURNING Id";
			connection.ExecuteScalar<long>(sql, processingMethod);

			UpdateChildren(connection, processingMethod);


			return processingMethod.Id;
		}

		public bool Delete(IDbConnection connection, long processingMethodProjId)
		{
			try
			{
				var map = new ProjectToProcessingMethodMapDao();
				map.Delete(connection, processingMethodProjId);

				connection.Execute($"Delete from {TableName} where {IdColumn}={processingMethodProjId}");
				return true;
			}
			catch (Exception ex)
			{
				Log.Error(
					$"Error occured in Delete() method of class{GetType().Name} - {ex.Message}");
				throw;
			}
		}

		public void DeleteAll(IDbConnection connection, Guid projectGuid)
		{
			try
			{
				var processingMethodIds = connection.Query<long>(
					$"SELECT {TableName}.{IdColumn} " +
					$"FROM {ProjectToProcessingMethodMapDao.TableName} " +
					$"INNER JOIN {ProjectDao.TableName} ON {ProjectDao.TableName}.{ProjectDao.IdColumn} = {ProjectToProcessingMethodMapDao.TableName}.{ProjectToProcessingMethodMapDao.ProjectIdColumn} " +
					$"INNER JOIN {TableName} ON {ProjectToProcessingMethodMapDao.TableName}.{ProjectToProcessingMethodMapDao.ProcessingMethodIdColumn} = {TableName}.{IdColumn} " +
					$"WHERE {ProjectDao.TableName}.{ProjectDao.GuidColumn} = @ProjectGuid",
					new { ProjectGuid = projectGuid }).ToList();

				foreach (var processingMethodId in processingMethodIds)
				{
					Delete(connection, processingMethodId);
				}
			}
			catch (Exception ex)
			{
				Log.Error("Error in DeleteAll method", ex);
				throw;
			}
		}
		public void Delete(IDbConnection connection, Guid projectGuid, Guid processingMethodGuid)
		{
			try
			{
				var processingMethodId = connection.QueryFirstOrDefault<long>(
					$"SELECT {TableName}.{IdColumn} " +
					$"FROM {ProjectToProcessingMethodMapDao.TableName} " +
					$"INNER JOIN {ProjectDao.TableName} ON {ProjectDao.TableName}.{ProjectDao.IdColumn} = {ProjectToProcessingMethodMapDao.TableName}.{ProjectToProcessingMethodMapDao.ProjectIdColumn} " +
					$"INNER JOIN {TableName} ON {ProjectToProcessingMethodMapDao.TableName}.{ProjectToProcessingMethodMapDao.ProcessingMethodIdColumn} = {TableName}.{IdColumn} " +
					$"WHERE {ProjectDao.TableName}.{ProjectDao.GuidColumn} = @ProjectGuid AND " +
					$"{TableName}.{GuidColumn} = @ProcessingMethodGuid",
					new { ProjectGuid = projectGuid, ProcessingMethodGuid = processingMethodGuid });

				Delete(connection, processingMethodId);
			}
			catch (Exception ex)
			{
				Log.Error("Error in DeleteAll method", ex);
				throw;
			}
		}

		public bool RenameProcessingMethodName(IDbConnection connection, string projectName,
			string originalProcessingMethodName, string newProcessingMethodName)
		{
			if (string.IsNullOrWhiteSpace(projectName))
			{
				throw new ArgumentException(nameof(projectName));
			}

			if (string.IsNullOrEmpty(originalProcessingMethodName))
			{
				throw new ArgumentException(nameof(originalProcessingMethodName));
			}

			if (string.IsNullOrEmpty(newProcessingMethodName))
			{
				throw new ArgumentException(nameof(newProcessingMethodName));
			}

			ProcessingMethod procMethod =
				GetProcessingMethodInfo(connection, projectName, originalProcessingMethodName);

			if (procMethod != null)
			{
				string sql = $"UPDATE {TableName} " +
				             $"SET {NameColumn}='{newProcessingMethodName}' " +
				             $"WHERE {IdColumn}={procMethod.Id}";
				connection.Execute(sql);
				return true;
			}
			else
			{
				return false;
			}
		}

        public void UpdateReviewApproveState(IDbConnection connection, string projectName, string processingMethodId, ReviewApproveState state, string modifiedUser)
        {
            try
            {
                long? projectId = ProjectDao.GetProjectId(connection, projectName);
                if (projectId.HasValue)
                {
                    var newState = (short)state;
                    string sql =
                        $"UPDATE {TableName} SET {ModifiedDateColumn} = @ModifyDate, {ModifiedUserIdColumn} = @ModifyUser, " +
                        $"{ReviewApproveStateColumn} = @ReviewApproveState " +
                        $"WHERE {GuidColumn} = @ProcessingMethodId";
                    connection.Execute(sql, new { ProcessingMethodId = Guid.TryParse(processingMethodId, out Guid returnValue) ? returnValue : Guid.Empty,
						ModifyDate = DateTime.UtcNow,
						ModifyUser = modifiedUser,
						ReviewApproveState = newState
					});
                }
                else
                {
                    Log.Error("Unable to Update Processing Method as Project Name does not Exist");
                    throw new NullReferenceException();
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error in Processing Method UpdateReviewApproveState method", ex);
                throw;
            }
        }

        public ProcessingMethod GetProcessingMethodByProcessingMethodId(IDbConnection connection, long processingMethodId)
        {
	        try
	        {
		        var processingMethod = connection.Query<ProcessingMethod>(
			        selectSql +
			        $"FROM {TableName} WHERE {IdColumn} ={processingMethodId}").FirstOrDefault();

		        if (processingMethod != null)
		        {
			        GetProcessingMethodChildren(connection, processingMethod);
			        return processingMethod;
		        }

				return null;
	        }
	        catch (Exception ex)
	        {
		        Log.Error("Error in GetProcessingMethodByProcessingMethodId method", ex);
		        throw;
	        }
		}

        public ProcessingMethod GetProcessingMethodByGuid(IDbConnection connection, string processingMethodGuid)
        {
            try
            {
                var processingMethod = connection.Query<ProcessingMethod>(selectSql +
                                                       $"FROM {TableName} WHERE {GuidColumn} =@ProcessingMethodGuid"
													   , new { ProcessingMethodGuid = Guid.TryParse(processingMethodGuid, out Guid returnValue) ? returnValue : Guid.Empty }
													   ).FirstOrDefault();
                GetProcessingMethodChildren(connection, processingMethod);
                return processingMethod;
            }
            catch (Exception ex)
            {
                Log.Error("Error in GetProcessingMethodByGuid method", ex);
                throw;
            }
        }

        public ProcessingMethod GetProcessingMethodByGuid(IDbConnection connection, string projectName, string processingMethodGuid)
        {
            try
            {
                var sql = selectSql +
                          $"FROM {ProjectDao.TableName} " +
                          $"INNER JOIN {ProjectToProcessingMethodMapDao.TableName} ON {ProjectDao.TableName}.{ProjectDao.IdColumn} = {ProjectToProcessingMethodMapDao.TableName}.{ProjectToProcessingMethodMapDao.ProjectIdColumn} " +
                          $"INNER JOIN {TableName} ON {ProjectToProcessingMethodMapDao.TableName}.{ProjectToProcessingMethodMapDao.ProcessingMethodIdColumn} = {TableName}.{IdColumn} " +
                          $"WHERE LOWER({ProjectDao.TableName}.{ProjectDao.ProjectNameColumn}) = LOWER(@ProjectName) AND {TableName}.{GuidColumn} = @ProcessingMethodGuid ";

                var processingMethod = connection.Query<ProcessingMethod>(sql, new { ProjectName = projectName, ProcessingMethodGuid = Guid.TryParse(processingMethodGuid, out Guid returnValue) ? returnValue : Guid.Empty }).FirstOrDefault();
                GetProcessingMethodChildren(connection, processingMethod);
                return processingMethod;
            }
            catch (Exception ex)
            {
                Log.Error("Error in GetProcessingMethodByGuid method", ex);
                throw;
            }
        }

        public long GetProcessingMethodByGuid(IDbConnection connection, Guid processingMethodGuid)
		{
			try
			{
				return connection.Query<long>($"SELECT {TableName}.{IdColumn} " +
														  $"FROM {TableName} WHERE {GuidColumn} ='{processingMethodGuid}' ").FirstOrDefault();

			}
			catch (Exception ex)
			{
				Log.Error("Error in GetProcessingMethodByGuid method", ex);
				throw;
			}
		}
    }
}
