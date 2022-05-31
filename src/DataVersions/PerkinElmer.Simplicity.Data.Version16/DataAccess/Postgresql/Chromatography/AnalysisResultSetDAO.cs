using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Dapper;
using log4net;
using PerkinElmer.Simplicity.Data.Version16.Contract.DataEntities.Chromatography;
using PerkinElmer.Simplicity.Data.Version16.Contract.DataEntities.Chromatography.ReviewApprove;

namespace PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.Chromatography
{
    public class AnalysisResultSetDao
	{
		protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		internal static string TableName { get; } = "AnalysisResultSet";
		internal static string IdColumn { get; } = "Id";
		internal static string GuidColumn { get; } = "Guid";
		internal static string ProjectIdColumn { get; } = "ProjectId";
		internal static string NameColumn { get; } = "Name";
		internal static string CreatedDateColumn { get; } = "CreatedDate";
		internal static string CreatedUserIdColumn { get; } = "CreatedUserId";
		internal static string CreatedUserNameColumn { get; } = "CreatedUserName";
		internal static string ModifiedDateColumn { get; } = "ModifiedDate";
		internal static string ModifiedUserIdColumn { get; } = "ModifiedUserId";
		internal static string ModifiedUserNameColumn { get; } = "ModifiedUserName";
		internal static string TypeColumn { get; } = "Type";
		internal static string ReviewApproveStateColumn { get; } = "ReviewApproveState";
		internal static string BatchResultSetGuidColumn { get; } = "BatchResultSetGuid";
		internal static string BatchResultNameColumn { get; } = "BatchResultSetName";
		internal static string BatchResultSetCreatedDateColumn { get; } = "BatchResultSetCreatedDate";
		internal static string BatchResultSetCreatedUserIdColumn { get; } = "BatchResultSetCreatedUserId";
		internal static string BatchResultSetModifiedDateColumn { get; } = "BatchResultSetModifiedDate";
		internal static string BatchResultSetModifiedUserIdColumn { get; } = "BatchResultSetModifiedUserId";
		internal static string ImportedColumn { get; } = "Imported";
		internal static string AutoProcessedColumn { get; } = "AutoProcessed";
		internal static string PartialColumn { get; } = "Partial";
		internal static string OnlyOriginalExistsColumn { get; } = "OnlyOriginalExists";
		internal static string SourceTypeColumn { get; } = "SourceType";
		internal static string OriginalAnalysisResultSetGuidColumn { get; } = "OriginalAnalysisResultSetGuid";
		internal static string IsCopyColumn { get; } = "IsCopy";

		protected readonly string SelectSql =
			"SELECT " +
			$"{TableName}.{IdColumn}," +
			$"{TableName}.{GuidColumn}," +
			$"{TableName}.{ProjectIdColumn}," +
			$"{TableName}.{NameColumn}," +
			$"{TableName}.{CreatedDateColumn}," +
			$"{TableName}.{CreatedUserIdColumn}," +
			$"{TableName}.{CreatedUserNameColumn}," +
			$"{TableName}.{ModifiedDateColumn}," +
			$"{TableName}.{ModifiedUserIdColumn}," +
			$"{TableName}.{ModifiedUserNameColumn}," +
			$"{TableName}.{TypeColumn}," +
			$"{TableName}.{ReviewApproveStateColumn}," +
			$"{TableName}.{BatchResultSetGuidColumn}," +
			$"{TableName}.{BatchResultNameColumn}," +
			$"{TableName}.{BatchResultSetCreatedDateColumn}," +
			$"{TableName}.{BatchResultSetCreatedUserIdColumn}," +
			$"{TableName}.{BatchResultSetModifiedDateColumn}," +
			$"{TableName}.{BatchResultSetModifiedUserIdColumn}," +
			$"{TableName}.{ImportedColumn}," +
			$"{TableName}.{AutoProcessedColumn}," +
			$"{TableName}.{PartialColumn}," +
			$"{TableName}.{OnlyOriginalExistsColumn}," +
			$"{TableName}.{OriginalAnalysisResultSetGuidColumn}, " +
			$"{TableName}.{IsCopyColumn}, " +
            $"{EntityReviewApproveTableDefinition.TableName}.{EntityReviewApproveTableDefinition.ReviewedByColumn}, " +
            $"{EntityReviewApproveTableDefinition.TableName}.{EntityReviewApproveTableDefinition.ReviewedTimestampColumn}, " +
            $"{EntityReviewApproveTableDefinition.TableName}.{EntityReviewApproveTableDefinition.ApprovedByColumn}, " +
            $"{EntityReviewApproveTableDefinition.TableName}.{EntityReviewApproveTableDefinition.ApprovedTimestampColumn} " +
            $"FROM {TableName} ";

		protected readonly string TableSelectSql =
			"SELECT " +
			$"{TableName}.{IdColumn}," +
			$"{TableName}.{GuidColumn}," +
			$"{TableName}.{ProjectIdColumn}," +
			$"{TableName}.{NameColumn}," +
			$"{TableName}.{CreatedDateColumn}," +
			$"{TableName}.{CreatedUserIdColumn}," +
			$"{TableName}.{CreatedUserNameColumn}," +
			$"{TableName}.{ModifiedDateColumn}," +
			$"{TableName}.{ModifiedUserIdColumn}," +
			$"{TableName}.{ModifiedUserNameColumn}," +
			$"{TableName}.{TypeColumn}," +
			$"{TableName}.{ReviewApproveStateColumn}," +
			$"{TableName}.{BatchResultSetGuidColumn}," +
			$"{TableName}.{BatchResultNameColumn}," +
			$"{TableName}.{BatchResultSetCreatedDateColumn}," +
			$"{TableName}.{BatchResultSetCreatedUserIdColumn}," +
			$"{TableName}.{BatchResultSetModifiedDateColumn}," +
			$"{TableName}.{BatchResultSetModifiedUserIdColumn}," +
			$"{TableName}.{ImportedColumn}," +
			$"{TableName}.{AutoProcessedColumn}," +
			$"{TableName}.{PartialColumn}," +
			$"{TableName}.{OnlyOriginalExistsColumn}," +
			$"{TableName}.{OriginalAnalysisResultSetGuidColumn}," +
			$"{TableName}.{IsCopyColumn} " +
			$"FROM {TableName} ";


		protected readonly string InsertSql =
			$"INSERT INTO {TableName} " +
			$"({GuidColumn}," +
			$"{ProjectIdColumn}," +
			$"{NameColumn}," +
			$"{CreatedDateColumn}," +
			$"{CreatedUserIdColumn}," +
			$"{CreatedUserNameColumn}," +
			$"{ModifiedDateColumn}," +
			$"{ModifiedUserIdColumn}," +
			$"{ModifiedUserNameColumn}," +
			$"{TypeColumn}," +
			$"{ReviewApproveStateColumn}," +
			$"{BatchResultSetGuidColumn}," +
			$"{BatchResultNameColumn}," +
			$"{BatchResultSetCreatedDateColumn}," +
			$"{BatchResultSetCreatedUserIdColumn}," +
			$"{BatchResultSetModifiedDateColumn}," +
			$"{BatchResultSetModifiedUserIdColumn}," +
			$"{ImportedColumn}," +
			$"{AutoProcessedColumn}," +
			$"{PartialColumn}," +
			$"{OnlyOriginalExistsColumn}," +
			$"{OriginalAnalysisResultSetGuidColumn}," +
			$"{IsCopyColumn}) " +
			"VALUES " +
			$"(@{GuidColumn}," +
			$"@{ProjectIdColumn}," +
			$"@{NameColumn}," +
			$"@{CreatedDateColumn}," +
			$"@{CreatedUserIdColumn}," +
			$"@{CreatedUserNameColumn}," +
			$"@{ModifiedDateColumn}," +
			$"@{ModifiedUserIdColumn}," +
			$"@{ModifiedUserNameColumn}," +
			$"@{TypeColumn}," +
			$"@{ReviewApproveStateColumn}," +
			$"@{BatchResultSetGuidColumn}," +
			$"@{BatchResultNameColumn}," +
			$"@{BatchResultSetCreatedDateColumn}," +
			$"@{BatchResultSetCreatedUserIdColumn}," +
			$"@{BatchResultSetModifiedDateColumn}," +
			$"@{BatchResultSetModifiedUserIdColumn}," +
			$"@{ImportedColumn}," +
			$"@{AutoProcessedColumn}," +
			$"@{PartialColumn}," +
			$"@{OnlyOriginalExistsColumn}," +
			$"@{OriginalAnalysisResultSetGuidColumn}," +
			$"@{IsCopyColumn}) ";
        private readonly object analysisResultSetGuid;

        public bool Create(IDbConnection connection, AnalysisResultSet analysisResultSet)
		{
			try
			{
				analysisResultSet.Id = connection.ExecuteScalar<long>(InsertSql + " RETURNING Id", analysisResultSet);
				return analysisResultSet.Id != 0;
			}
			catch (Exception ex)
			{
				Log.Error($"Error in Create method", ex);
				throw;
			}
		}

		public bool Update(IDbConnection connection,
			Guid projectGuid,
			Guid analysisResultSetGuid,
			string analysisResultSetName,
			DateTime modifiedDateTimeUtc,
			string modifiedByUserId,
			string modifiedByUserName,
			bool partial,
			bool onlyOriginalExists)
		{
			try
			{
				var projectDao = new ProjectDao();
				var project = projectDao.GetProject(connection, projectGuid);

				string sql =
					$"UPDATE {TableName} SET " +
					$"{NameColumn} = @AnalysisResultSetName, " +
					$"{ModifiedDateColumn} = @ModifiedDate, " +
					$"{ModifiedUserIdColumn} = @ModifiedUserId," +
					$"{ModifiedUserNameColumn} = @ModifiedUserName," +
					$"{PartialColumn} = @Partial," +
					$"{OnlyOriginalExistsColumn} = @OnlyOriginalExists " +
					$"WHERE {ProjectIdColumn} = @ProjectId AND {GuidColumn} = @AnalysisResultSetGuid";

				var numberOfRowsUpdated = connection.Execute(sql,
					new
					{
						ModifiedDate = modifiedDateTimeUtc,
						ModifiedUserId = modifiedByUserId,
						ModifiedUserName = modifiedByUserName,
						AnalysisResultSetName = analysisResultSetName,
						ProjectId = project.Id,
						AnalysisResultSetGuid = analysisResultSetGuid,
						Partial = partial,
						OnlyOriginalExists = onlyOriginalExists
					});

				return numberOfRowsUpdated != 0;
			}
			catch (Exception ex)
			{
				Log.Error($"Error in Update method", ex);
				throw;
			}
		}
		public void EndBatchResultSetAutoProcessing(IDbConnection connection, Guid projectGuid, Guid analysisResultSetGuid)
		{
			try
			{
				var projectDao = new ProjectDao();
				var project = projectDao.GetProject(connection, projectGuid);

				string sql =
					$"UPDATE {TableName} SET {ModifiedDateColumn} = @ModifiedDate," +
					$" {AutoProcessedColumn} = @AutoProcessed," +
					$"{PartialColumn} = @Partial " +
					$"WHERE {ProjectIdColumn} = @ProjectId AND {GuidColumn} = @AnalysisResultSetGuid";

				connection.Execute(sql, 
					new { ModifiedDate= DateTime.UtcNow,
						AutoProcessed = false,
						Partial = false,
						ProjectId = project.Id,
						AnalysisResultSetGuid = analysisResultSetGuid});
			}
			catch (Exception ex)
			{
				Log.Error($"Error in EndBatchResultSetAutoProcessing method", ex);
				throw;
			}
		}
		public void UpdateModifiedDate(IDbConnection connection, Guid projectGuid, Guid analysisResultSetGuid)
		{
			try
			{
				var projectDao = new ProjectDao();
				var project = projectDao.GetProject(connection, projectGuid);

				string sql =
					$"UPDATE {TableName} SET {ModifiedDateColumn} = @ModifiedDate " +
					$"WHERE {ProjectIdColumn} = @ProjectId AND {GuidColumn} = @AnalysisResultSetGuid";

				connection.Execute(sql,
					new
					{
						ModifiedDate = DateTime.UtcNow,
						ProjectId = project.Id,
						AnalysisResultSetGuid = analysisResultSetGuid
					});
			}
			catch (Exception ex)
			{
				Log.Error($"Error in UpdateModifiedDate method", ex);
				throw;
			}
		}
		public List<AnalysisResultSet> GetAll(IDbConnection connection, Guid projectGuid)
		{
			try
			{
				return connection.Query<AnalysisResultSet>(
					SelectSql +
					$"INNER JOIN {ProjectDao.TableName} ON {ProjectDao.TableName}.{ProjectDao.IdColumn} = {TableName}.{ProjectIdColumn} " +
                    $"LEFT JOIN {EntityReviewApproveTableDefinition.TableName} ON {TableName}.{GuidColumn} ::varchar = {EntityReviewApproveTableDefinition.TableName}.{EntityReviewApproveTableDefinition.EntityIdColumn} " +
                    $"WHERE {ProjectDao.TableName}.{ProjectDao.GuidColumn} = @ProjectGuid " +
					$"ORDER BY {IdColumn}",
					new {ProjectGuid = projectGuid}).ToList();
			}
			catch (Exception ex)
			{
				Log.Error($"Error in GetAll method", ex);
				throw;
			}
		}

		public AnalysisResultSet Get(IDbConnection connection, Guid projectGuid, Guid analysisResultSetGuid)
		{
			return connection.QueryFirstOrDefault<AnalysisResultSet>(
				SelectSql +
				$"INNER JOIN {ProjectDao.TableName} ON {ProjectDao.TableName}.{ProjectDao.IdColumn} = {TableName}.{ProjectIdColumn} " +
                $"LEFT JOIN {EntityReviewApproveTableDefinition.TableName} ON {TableName}.{GuidColumn} ::varchar = {EntityReviewApproveTableDefinition.TableName}.{EntityReviewApproveTableDefinition.EntityIdColumn} " +
                $"WHERE {ProjectDao.TableName}.{ProjectDao.GuidColumn} = @ProjectGuid AND " +
				$"	{TableName}.{GuidColumn} = @AnalysisResultSetGuid",
				new { ProjectGuid = projectGuid, AnalysisResultSetGuid = analysisResultSetGuid });
		}

		public AnalysisResultSet Get(IDbConnection connection, long analysisResultSetId)
		{
			return connection.QueryFirstOrDefault<AnalysisResultSet>(
				SelectSql +
				$"WHERE {ProjectDao.TableName}.{ProjectDao.IdColumn} = @AnalysisResultSetId" +
				new { AnalysisResultSetId = analysisResultSetId });
		}

		internal AnalysisResultSet GetAnalysisResultSet(IDbConnection connection, string projectName,
			string analysisResultSetName)
		{
			try
			{
                projectName = projectName.ToLower();
                analysisResultSetName = analysisResultSetName.ToLower();
                return connection.QueryFirstOrDefault<AnalysisResultSet>(
						$"SELECT " +
						$"{TableName}.{IdColumn}," +
						$"{TableName}.{GuidColumn}," +
						$"{TableName}.{ProjectIdColumn}," +
						$"{TableName}.{NameColumn}," +
						$"{TableName}.{CreatedDateColumn}," +
						$"{TableName}.{CreatedUserIdColumn}," +
						$"{TableName}.{CreatedUserNameColumn}," +
						$"{TableName}.{ModifiedDateColumn}," +
						$"{TableName}.{ModifiedUserIdColumn}," +
						$"{TableName}.{ModifiedUserNameColumn}," +
						$"{TableName}.{TypeColumn}," +
						$"{TableName}.{ReviewApproveStateColumn}, " +
						$"{TableName}.{OnlyOriginalExistsColumn}, " +
						$"{ProjectDao.TableName}.{ProjectDao.ProjectNameColumn} AS ProjectName " +
                        $"FROM {TableName} INNER JOIN {ProjectDao.TableName} " +
						$"ON {TableName}.{ProjectIdColumn} = {ProjectDao.TableName}.{ProjectDao.IdColumn} " +
                        $"WHERE LOWER({ProjectDao.TableName}.{ProjectDao.ProjectNameColumn}) = @ProjectName AND LOWER({TableName}.{NameColumn}) = @AnalysisResultSetName",
				        new { ProjectName = projectName, AnalysisResultSetName = analysisResultSetName });
            }
			catch (Exception ex)
			{
				Log.Error($"Error in GetAnalysisResultSet method", ex);
				throw;
			}
		}
		internal IList<AnalysisResultSet> GetAnalysisResultSets(IDbConnection connection, string projectName,
			string analysisResultSetName)
		{
			try
			{
				return connection.Query<AnalysisResultSet>(
					TableSelectSql +
					$"INNER JOIN {ProjectDao.TableName} " +
					$"ON {TableName}.{ProjectIdColumn} = {ProjectDao.TableName}.{ProjectDao.IdColumn} " +
					$"WHERE {ProjectDao.TableName}.{ProjectDao.ProjectNameColumn} = @ProjectName AND " +
					$"Lower({TableName}.{NameColumn}) = @AnalysisResultSetName",
					new { ProjectName = projectName, AnalysisResultSetName = analysisResultSetName.ToLower()}).ToList();
			}
			catch (Exception ex)
			{
				Log.Error($"Error in GetAnalysisResultSets method", ex);
				throw;
			}
		}
		internal IList<AnalysisResultSet> GetAnalysisResultSets(IDbConnection connection, Guid projectGuid,
			string analysisResultSetName)
		{
			try
			{
				return connection.Query<AnalysisResultSet>(
					TableSelectSql +
					$"INNER JOIN {ProjectDao.TableName} ON {TableName}.{ProjectIdColumn} = {ProjectDao.TableName}.{ProjectDao.IdColumn} " +
					$"WHERE {ProjectDao.TableName}.{ProjectDao.GuidColumn} = @ProjectGuid AND " +
					$"LOWER({TableName}.{NameColumn}) = @AnalysisResultSetName",
					new { ProjectGuid = projectGuid, AnalysisResultSetName = analysisResultSetName.ToLower() }).ToList();
			}
			catch (Exception ex)
			{
				Log.Error($"Error in GetAnalysisResultSets method", ex);
				throw;
			}
		}
		internal IList<AnalysisResultSet> GetAnalysisResultSetsByOriginalAnalysisResultSetGuid(IDbConnection connection, Guid projectGuid,
			Guid originalAnalysisResultSetGuid)
		{
			try
			{
				return connection.Query<AnalysisResultSet>(
					"SELECT " +
					$"{TableName}.{IdColumn}," +
					$"{TableName}.{GuidColumn}," +
					$"{TableName}.{ProjectIdColumn}," +
					$"{TableName}.{NameColumn}," +
					$"{TableName}.{CreatedDateColumn}," +
					$"{TableName}.{CreatedUserIdColumn}," +
					$"{TableName}.{CreatedUserNameColumn}," +
					$"{TableName}.{ModifiedDateColumn}," +
					$"{TableName}.{ModifiedUserIdColumn}," +
					$"{TableName}.{ModifiedUserNameColumn}," +
					$"{TableName}.{TypeColumn}," +
					$"{TableName}.{ReviewApproveStateColumn}," +
					$"{TableName}.{BatchResultSetGuidColumn}," +
					$"{TableName}.{BatchResultNameColumn}," +
					$"{TableName}.{BatchResultSetCreatedDateColumn}," +
					$"{TableName}.{BatchResultSetCreatedUserIdColumn}," +
					$"{TableName}.{BatchResultSetModifiedDateColumn}," +
					$"{TableName}.{BatchResultSetModifiedUserIdColumn}," +
					$"{TableName}.{ImportedColumn}," +
					$"{TableName}.{AutoProcessedColumn}," +
					$"{TableName}.{PartialColumn}," +
					$"{TableName}.{OnlyOriginalExistsColumn}," +
					$"{TableName}.{OriginalAnalysisResultSetGuidColumn}," +
					$"{TableName}.{IsCopyColumn} " +
					$"FROM {TableName} INNER JOIN {ProjectDao.TableName} " +
					$"ON {TableName}.{ProjectIdColumn} = {ProjectDao.TableName}.{ProjectDao.IdColumn} " +
					$"WHERE {ProjectDao.TableName}.{ProjectDao.GuidColumn} = @ProjectGuid AND " +
					$"{TableName}.{OriginalAnalysisResultSetGuidColumn} = @OriginalAnalysisResultSetGuid",
					new { ProjectGuid = projectGuid, OriginalAnalysisResultSetGuid = originalAnalysisResultSetGuid}).ToList();
			}
			catch (Exception ex)
			{
				Log.Error($"Error in GetAnalysisResultSets method", ex);
				throw;
			}
		}

		internal AnalysisResultSet GetAnalysisResultSet(IDbConnection connection, Guid projectGuid,
			Guid analysisResultSetGuid)
		{
			try
			{
				return connection.QueryFirstOrDefault<AnalysisResultSet>(
					TableSelectSql +
					$"INNER JOIN {ProjectDao.TableName} ON {TableName}.{ProjectIdColumn} = {ProjectDao.TableName}.{ProjectDao.IdColumn} " +
                    $"WHERE {ProjectDao.TableName}.{ProjectDao.GuidColumn} = @ProjectGuid AND " +
					$"{TableName}.{GuidColumn} = @AnalysisResultSetGuid",
					new {ProjectGuid = projectGuid, AnalysisResultSetGuid = analysisResultSetGuid});
			}
			catch (Exception ex)
			{
				Log.Error($"Error in GetAnalysisResultSet method", ex);
				throw;
			}
		}
		internal IEnumerable<AnalysisResultSet> GetAnalysisResultSetsByProjectName(IDbConnection connection,
			string projectName)
		{
			try
			{
				IEnumerable<AnalysisResultSet> analysisResultSet = connection.Query<AnalysisResultSet>(
				$"SELECT {TableName}.{IdColumn}," +
				$"{TableName}.{GuidColumn}," +
				$"{TableName}.{ProjectIdColumn}," +
				$"{TableName}.{NameColumn}," +
				$"{TableName}.{CreatedDateColumn}," +
				$"{TableName}.{CreatedUserIdColumn}," +
				$"{TableName}.{CreatedUserNameColumn}," +
				$"{TableName}.{ModifiedDateColumn}," +
				$"{TableName}.{ModifiedUserIdColumn}," +
				$"{TableName}.{ModifiedUserNameColumn}," +
				$"{TableName}.{TypeColumn}," +
				$"{TableName}.{ReviewApproveStateColumn}," +
                $"{ProjectDao.TableName}.{ProjectDao.ProjectNameColumn} AS ProjectName " +
                $"FROM {TableName} INNER JOIN {ProjectDao.TableName} " +
                        $"ON {TableName}.{ProjectIdColumn} = {ProjectDao.TableName}.{ProjectDao.IdColumn} " +
						$"WHERE {ProjectDao.TableName}.{ProjectDao.ProjectNameColumn} = '{projectName.Replace("'", "''")}'");

				return analysisResultSet;
			}
			catch (Exception ex)
			{
				Log.Error("Error in GetAnalysisResultSetByProjectId method", ex);
				throw;
			}
		}

		public bool IsExists(IDbConnection connection, string projectName, string analysisResultSetName)
		{
			if (string.IsNullOrWhiteSpace(projectName))
			{
				throw new ArgumentException(nameof(projectName));
			}

			if (string.IsNullOrEmpty(analysisResultSetName))
			{
				throw new ArgumentException(nameof(analysisResultSetName));
			}

			AnalysisResultSet analysisResultSet = GetAnalysisResultSet(connection, projectName, analysisResultSetName);

			if (analysisResultSet != null)
			{
				return true;
			}

			return false;
		}
		public bool IsExists(IDbConnection connection, Guid projectGuid, string analysisResultSetName)
		{
			if (string.IsNullOrEmpty(analysisResultSetName))
			{
				throw new ArgumentException(nameof(analysisResultSetName));
			}

			var analysisResultSet = connection.QueryFirstOrDefault<AnalysisResultSet>(
				TableSelectSql +
				$"INNER JOIN {ProjectDao.TableName} ON {TableName}.{ProjectIdColumn} = {ProjectDao.TableName}.{ProjectDao.IdColumn} " +
				$"WHERE {ProjectDao.TableName}.{ProjectDao.GuidColumn} = @ProjectGuid AND " +
				$"LOWER({TableName}.{NameColumn}) = @AnalysisResultSetName",
				new { ProjectGuid = projectGuid, AnalysisResultSetName = analysisResultSetName.ToLower() });

			return (analysisResultSet != null);
		}

		public void DeleteAnalysisResultSet(IDbConnection connection, string projectName, string analysisResultSetName)
		{
			try
			{
				var analysisResultSetId = GetAnalysisResultSetId(connection, projectName, analysisResultSetName);

				if (analysisResultSetId.HasValue)
				{
					connection.Execute($"DELETE FROM {TableName} " +
									   $"WHERE {IdColumn}={analysisResultSetId.Value}");
				}

			}
			catch (Exception ex)
			{
				Log.Error("Error in DeleteAnalysisResultSet method", ex);
				throw;
			}
		}
		public void DeleteAnalysisResultSet(IDbConnection connection, long analysisResultSetId)
		{
			try
			{
				connection.Execute($"DELETE FROM {TableName} " +
				                   $"WHERE {IdColumn} = @Id",
					new {Id = analysisResultSetId});
			}
			catch (Exception ex)
			{
				Log.Error("Error in DeleteAnalysisResultSet method", ex);
				throw;
			}
		}

		public long? GetAnalysisResultSetId(IDbConnection connection, string projectName, string analysisResultSetName)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(projectName))
				{
					throw new ArgumentException(nameof(projectName));
				}

				if (string.IsNullOrWhiteSpace(analysisResultSetName))
				{
					throw new ArgumentException(nameof(analysisResultSetName));
				}

				string sql = $"SELECT {TableName}.{IdColumn} " +
							 $"FROM {TableName} INNER JOIN {ProjectDao.TableName} ON {TableName}.{ProjectIdColumn} = {ProjectDao.TableName}.{IdColumn}" +
							 $" WHERE Lower({TableName}.{NameColumn}) = Lower('{analysisResultSetName.Replace("'", "''")}') AND Lower({ProjectDao.TableName}.{NameColumn}) = Lower('{projectName}')";

				var analysisResultSetId = connection.QueryFirstOrDefault<long?>(sql);

				return analysisResultSetId;
			}
			catch (Exception ex)
			{
				Log.Error("Error in GetAnalysisResultSetId method", ex);
				throw;
			}
		}

		public bool RenameAnalysisRestSetName(IDbConnection connection, string projectName, string originalAnalysisResultSetName, string newAnalysisResultSetName)
		{
			long? analysisResultSetId = 0;
			if (string.IsNullOrWhiteSpace(projectName))
			{
				throw new ArgumentException(nameof(projectName));
			}

			if (string.IsNullOrEmpty(originalAnalysisResultSetName))
			{
				throw new ArgumentException(nameof(originalAnalysisResultSetName));
			}
			if (string.IsNullOrEmpty(newAnalysisResultSetName))
			{
				throw new ArgumentException(nameof(newAnalysisResultSetName));
			}
			long? projectId = ProjectDao.GetProjectId(connection, projectName);
			if (projectId.HasValue)
			{
				analysisResultSetId = GetAnalysisResultSetId(connection, projectName, originalAnalysisResultSetName);
				string sql = $"Update {TableName} Set {NameColumn} = '{newAnalysisResultSetName}' WHERE " +
							 $" {IdColumn} ={analysisResultSetId} and {ProjectIdColumn} = {projectId} RETURNING Id";
				analysisResultSetId =connection.Execute(sql);
				
			}
			return analysisResultSetId > 0; ;
		}

        public void UpdateReviewApproveState(IDbConnection connection, string projectName, string analysisResultSetId, ReviewApproveState state, string modifiedUser)
        {
            try
            {
                long? projectId = ProjectDao.GetProjectId(connection, projectName);
                if (projectId.HasValue)
                {
					var newState = (short)state;
					string sql =
						$"UPDATE {TableName} SET {ModifiedDateColumn} = '{DateTime.UtcNow}', {ModifiedUserIdColumn} = '{modifiedUser}', " +
						$"{ReviewApproveStateColumn} = {newState} " +
						$"WHERE {GuidColumn} = '{analysisResultSetId}'";
					connection.Execute(sql);
				}
                else
                {
                    Log.Error("Unable to Update Analysis Result Set as Project Name does not Exist");
                    throw new NullReferenceException();
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error in UpdateReviewApproveState method", ex);
                throw;
            }
        }

        public Guid GetAnalysisResultSetGuid(IDbConnection connection, long analysisResultSetId)
        {
            string sql =
                $"SELECT {TableName}.{GuidColumn} " +
                $"FROM {TableName} "+
                $"WHERE {TableName}.{IdColumn} = @AnalysisResultSetId";

            var analysisResultSetGuid= connection.QueryFirstOrDefault<Guid>(sql, new { AnalysisResultSetId = analysisResultSetId });
           
            return analysisResultSetGuid;

        }

        public  Guid GetProjectGuid(IDbConnection connection, long analysisResultSetId)
        {
            string sql =
                $"SELECT {ProjectDao.TableName}.{GuidColumn} " +
                $"FROM {TableName} " +
                $"INNER JOIN {ProjectDao.TableName} ON {ProjectDao.TableName}.{ProjectDao.IdColumn} = {TableName}.{ProjectIdColumn} " +
                $"WHERE {TableName}.{IdColumn} = @AnalysisResultSetId";

            var projectGuid = connection.QueryFirstOrDefault<Guid>(sql, new { AnalysisResultSetId = analysisResultSetId });

            return projectGuid;
        }

    }
}
