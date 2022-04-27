using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Dapper;
using log4net;
using PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography.ReviewApprove;

namespace PerkinElmer.Simplicity.Data.Version15.DataAccess.Postgresql.Chromatography
{
	internal class EntityReviewApproveDao
	{
		private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		internal bool Create(IDbConnection connection, ReviewApprovableDataEntity reviewApproveEntity)
		{
			if (reviewApproveEntity == null ||
				string.IsNullOrWhiteSpace(reviewApproveEntity.EntityId) ||
				string.IsNullOrWhiteSpace(reviewApproveEntity.EntityName) ||
				string.IsNullOrWhiteSpace(reviewApproveEntity.EntityType))
			{
				throw new ArgumentException(nameof(reviewApproveEntity));
			}

			try
			{
				long id = connection.ExecuteScalar<long>(EntityReviewApproveSqls.Insert + $" returning {EntityReviewApproveTableDefinition.IdColumn}", reviewApproveEntity);
				reviewApproveEntity.Id = id;
                if(!string.IsNullOrEmpty(reviewApproveEntity.DataName))
                {
                    var dao = new EntityReviewApproveAssociatedDataMapDAO();
                    dao.Create(connection, reviewApproveEntity);
                }
                return true;
			}
			catch (Exception ex)
			{
				Log.Error("Error in Create review approve entity method", ex);
				throw;
			}
		}

		internal bool Delete(IDbConnection connection, long id)
		{
			try
			{
				var result = connection.Execute(EntityReviewApproveSqls.Delete, new { Id = id });
				return result == 1;
			}
			catch (Exception ex)
			{
				Log.Error("Error in Delete review approve entity method", ex);
				throw;
			}
		}

		internal IList<ReviewApprovableDataEntity> GetAll(IDbConnection connection, string projectName, bool includeApproved, short approvedState)
		{
			try
			{
				if (includeApproved)
				{
					return connection.Query<ReviewApprovableDataEntity>(EntityReviewApproveSqls.SelectAll, new { ProjectName = projectName }).ToList();
				}
				else
				{
					return connection.Query<ReviewApprovableDataEntity>(EntityReviewApproveSqls.SelectAllByState, new { ProjectName = projectName, EntityReviewApproveState = approvedState }).ToList();
				}
			}
			catch (Exception ex)
			{
				Log.Error("Error in GetAll review approve entities method", ex);
				throw;
			}
		}

		internal ReviewApprovableDataEntity Get(IDbConnection connection, string projectName, string entityId, string entityType)
		{
			try
			{
				return connection.QueryFirstOrDefault<ReviewApprovableDataEntity>(EntityReviewApproveSqls.SelectByEntityIdAndType, new { ProjectName = projectName, EntityId = entityId, EntityType = entityType });
			}
			catch (Exception ex)
			{
				Log.Error("Error in Update review approve entity method", ex);
				throw;
			}
		}
		internal bool Update(IDbConnection connection, ReviewApprovableDataEntity reviewApproveEntity)
		{
			try
			{
				var result = connection.Execute(EntityReviewApproveSqls.Update, reviewApproveEntity);
                if (!string.IsNullOrEmpty(reviewApproveEntity.DataName))
                {
                    var dao = new EntityReviewApproveAssociatedDataMapDAO();
                    dao.Create(connection, reviewApproveEntity);
                }
                return result == 1;
			}
			catch (Exception ex)
			{
				Log.Error("Error in Update review approve entity method", ex);
				throw;
			}
		}

		internal bool UpdateProjectName(IDbConnection connection, Guid projectGuid, string projectName)
		{
			try
			{
				var projectNames = connection.Query<string>(EntityReviewApproveSqls.SelectProjectName, new { projectId = projectGuid.ToString() }).ToList();
				bool isNamedChanged = false;
				int projectNameCount = projectNames.Count();
				if (projectNameCount > 1)
				{
					isNamedChanged = true;
				}
				else if(projectNameCount == 1 && projectNames[0].ToLower() != projectName.ToLower())
				{
					isNamedChanged = true;
				}

				if (isNamedChanged)
				{
					var result = connection.Execute(EntityReviewApproveSqls.UpdateProjectName, new { projectId = projectGuid.ToString(), ProjectName = projectName });
					return true;
				}
				else
				{
					return false;
				}
			}
			catch (Exception ex)
			{
				Log.Error("Error in UpdateProjectName method", ex);
				throw;
			}
		}

		internal bool UpdateEntityName(IDbConnection connection, Guid entityGuid, string entityName)
		{
			try
			{
				var result = connection.Execute(EntityReviewApproveSqls.UpdateEntityName, new { entityid = entityGuid.ToString(), entityname = entityName });
				return true;
			}
			catch (Exception ex)
			{
				Log.Error("Error in UpdateEntityName method", ex);
				throw;
			}
		}
	}

	internal static class EntityReviewApproveTableDefinition
	{
		internal static string TableName = "EntityReviewApprove";

		internal static string IdColumn { get; } = "Id";

		internal static string ProjectIdColumn { get; } = "ProjectId";

		internal static string ProjectNameColumn { get; } = "ProjectName";

		internal static string EntityIdColumn { get; } = "EntityId";

		internal static string EntityNameColumn { get; } = "EntityName";

		internal static string EntityTypeColumn { get; } = "EntityType";

		internal static string EntityReviewApproveStateColumn { get; } = "EntityReviewApproveState";

		internal static string LastActionTimestampColumn { get; } = "LastActionTimestamp";

		internal static string SubmitTimestampColumn { get; } = "SubmitTimestamp";

		internal static string InReviewByColumn { get; } = "InReviewBy";

		internal static string InApproveByColumn { get; } = "InApproveBy";

		internal static string ReviewedByColumn { get; } = "ReviewedBy";

		internal static string ApprovedByColumn { get; } = "ApprovedBy";

		internal static string RejectedByColumn { get; } = "RejectedBy";

		internal static string RecalledByColumn { get; } = "RecalledBy";

		internal static string PostponedByColumn { get; } = "PostponedBy";

		internal static string SubmittedByColumn { get; } = "SubmittedBy";

		internal static string InReviewByUserIdColumn { get; } = "InReviewByUserId";

		internal static string InApproveByUserIdColumn { get; } = "InApproveByUserId";

		internal static string ReviewedByUserIdColumn { get; } = "ReviewedByUserId";

		internal static string ApprovedByUserIdColumn { get; } = "ApprovedByUserId";

		internal static string RejectedByUserIdColumn { get; } = "RejectedByUserId";

		internal static string RecalledByUserIdColumn { get; } = "RecalledByUserId";

		internal static string PostponedByUserIdColumn { get; } = "PostponedByUserId";

		internal static string SubmittedByUserIdColumn { get; } = "SubmittedByUserId";

		internal static string LastModifiedByColumn { get; } = "LastModifiedBy";
	
		internal static string LastModifiedByUserIdColumn { get; } = "LastModifiedByUserId";

		internal static string ReviewedTimestampColumn { get; } = "ReviewedTimestamp";
		
		internal static string ApprovedTimestampColumn { get; } = "ApprovedTimestamp";
		
		internal static string ReviewedCountColumn { get; } = "ReviewedCount";

		internal static string ApprovedCountColumn { get; } = "ApprovedCount";
        internal static string VersionNumberColumn { get; } = "VersionNumber";
    }

	internal static class EntityReviewApproveSqls
	{
		internal static string Insert =
			$"insert into {EntityReviewApproveTableDefinition.TableName} " +
			$"({EntityReviewApproveTableDefinition.EntityIdColumn}, " +
			$"{EntityReviewApproveTableDefinition.EntityNameColumn}, " +
			$"{EntityReviewApproveTableDefinition.EntityTypeColumn}, " +
			$"{EntityReviewApproveTableDefinition.ProjectIdColumn}, " +
			$"{EntityReviewApproveTableDefinition.ProjectNameColumn}, " +
			$"{EntityReviewApproveTableDefinition.EntityReviewApproveStateColumn}, " +
			$"{EntityReviewApproveTableDefinition.SubmittedByColumn}, " +
			$"{EntityReviewApproveTableDefinition.SubmittedByUserIdColumn}, " +
			$"{EntityReviewApproveTableDefinition.LastModifiedByColumn}, " + 
			$"{EntityReviewApproveTableDefinition.LastModifiedByUserIdColumn}, " +
			$"{EntityReviewApproveTableDefinition.SubmitTimestampColumn}, " +
			$"{EntityReviewApproveTableDefinition.LastActionTimestampColumn}, " +
            $"{EntityReviewApproveTableDefinition.VersionNumberColumn}, " +
            $"{EntityReviewApproveTableDefinition.ReviewedCountColumn}, " +
			$"{EntityReviewApproveTableDefinition.ApprovedCountColumn} ) " +
			"values (" +
			$"@{EntityReviewApproveTableDefinition.EntityIdColumn}," +
			$"@{EntityReviewApproveTableDefinition.EntityNameColumn}, " +
			$"@{EntityReviewApproveTableDefinition.EntityTypeColumn}, " +
			$"@{EntityReviewApproveTableDefinition.ProjectIdColumn}, " +
			$"@{EntityReviewApproveTableDefinition.ProjectNameColumn}, " +
			$"@{EntityReviewApproveTableDefinition.EntityReviewApproveStateColumn}, " +
			$"@{EntityReviewApproveTableDefinition.SubmittedByColumn}, " +
			$"@{EntityReviewApproveTableDefinition.SubmittedByUserIdColumn}, " +
			$"@{EntityReviewApproveTableDefinition.SubmittedByColumn}, " +
			$"@{EntityReviewApproveTableDefinition.SubmittedByUserIdColumn}, " +
			$"@{EntityReviewApproveTableDefinition.SubmitTimestampColumn}, " +
			$"@{EntityReviewApproveTableDefinition.LastActionTimestampColumn}, " +
            $"@{EntityReviewApproveTableDefinition.VersionNumberColumn}, " +
            $"@{EntityReviewApproveTableDefinition.ReviewedCountColumn}, " +
			$"@{EntityReviewApproveTableDefinition.ApprovedCountColumn} ) ";

		internal static string InsertAll =
			$"insert into {EntityReviewApproveTableDefinition.TableName} " +
			$"({EntityReviewApproveTableDefinition.IdColumn}," +
			$"{EntityReviewApproveTableDefinition.ProjectIdColumn}," +
			$"{EntityReviewApproveTableDefinition.ProjectNameColumn}," +
			$"{EntityReviewApproveTableDefinition.EntityIdColumn}," +
			$"{EntityReviewApproveTableDefinition.EntityNameColumn}," +
			$"{EntityReviewApproveTableDefinition.EntityTypeColumn}," +
			$"{EntityReviewApproveTableDefinition.EntityReviewApproveStateColumn}, " +
			$"{EntityReviewApproveTableDefinition.LastActionTimestampColumn}," +
			$"{EntityReviewApproveTableDefinition.SubmitTimestampColumn}, " +
			$"{EntityReviewApproveTableDefinition.InReviewByColumn}," +
			$"{EntityReviewApproveTableDefinition.InApproveByColumn}," +
			$"{EntityReviewApproveTableDefinition.ReviewedByColumn}," +
			$"{EntityReviewApproveTableDefinition.ApprovedByColumn}," +
			$"{EntityReviewApproveTableDefinition.RejectedByColumn}," +
			$"{EntityReviewApproveTableDefinition.RecalledByColumn}," +
			$"{EntityReviewApproveTableDefinition.PostponedByColumn}," +
			$"{EntityReviewApproveTableDefinition.SubmittedByColumn}," +
			$"{EntityReviewApproveTableDefinition.SubmittedByColumn}," +
			$"{EntityReviewApproveTableDefinition.InReviewByUserIdColumn}," +
			$"{EntityReviewApproveTableDefinition.InApproveByUserIdColumn}," +
			$"{EntityReviewApproveTableDefinition.ReviewedByUserIdColumn}," +
			$"{EntityReviewApproveTableDefinition.ApprovedByUserIdColumn}," +
			$"{EntityReviewApproveTableDefinition.RejectedByUserIdColumn}," +
			$"{EntityReviewApproveTableDefinition.RecalledByUserIdColumn}," +
			$"{EntityReviewApproveTableDefinition.PostponedByUserIdColumn}," +
			$"{EntityReviewApproveTableDefinition.SubmittedByUserIdColumn}," +
			$"{EntityReviewApproveTableDefinition.LastModifiedByColumn}," +
			$"{EntityReviewApproveTableDefinition.LastModifiedByUserIdColumn}," +
			$"{EntityReviewApproveTableDefinition.ReviewedTimestampColumn}," +
			$"{EntityReviewApproveTableDefinition.ApprovedTimestampColumn}," +
			$"{EntityReviewApproveTableDefinition.ReviewedCountColumn}," +
			$"{EntityReviewApproveTableDefinition.ApprovedCountColumn}," +
			$"{EntityReviewApproveTableDefinition.VersionNumberColumn}) " +
			"values (" +
			$"@{EntityReviewApproveTableDefinition.IdColumn}," +
			$"@{EntityReviewApproveTableDefinition.ProjectIdColumn}," +
			$"@{EntityReviewApproveTableDefinition.ProjectNameColumn}," +
			$"@{EntityReviewApproveTableDefinition.EntityIdColumn}," +
			$"@{EntityReviewApproveTableDefinition.EntityNameColumn}," +
			$"@{EntityReviewApproveTableDefinition.EntityTypeColumn}," +
			$"@{EntityReviewApproveTableDefinition.EntityReviewApproveStateColumn}, " +
			$"@{EntityReviewApproveTableDefinition.LastActionTimestampColumn}," +
			$"@{EntityReviewApproveTableDefinition.SubmitTimestampColumn}, " +
			$"@{EntityReviewApproveTableDefinition.InReviewByColumn}," +
			$"@{EntityReviewApproveTableDefinition.InApproveByColumn}," +
			$"@{EntityReviewApproveTableDefinition.ReviewedByColumn}," +
			$"@{EntityReviewApproveTableDefinition.ApprovedByColumn}," +
			$"@{EntityReviewApproveTableDefinition.RejectedByColumn}," +
			$"@{EntityReviewApproveTableDefinition.RecalledByColumn}," +
			$"@{EntityReviewApproveTableDefinition.PostponedByColumn}," +
			$"@{EntityReviewApproveTableDefinition.SubmittedByColumn}," +
			$"@{EntityReviewApproveTableDefinition.SubmittedByColumn}," +
			$"@{EntityReviewApproveTableDefinition.InReviewByUserIdColumn}," +
			$"@{EntityReviewApproveTableDefinition.InApproveByUserIdColumn}," +
			$"@{EntityReviewApproveTableDefinition.ReviewedByUserIdColumn}," +
			$"@{EntityReviewApproveTableDefinition.ApprovedByUserIdColumn}," +
			$"@{EntityReviewApproveTableDefinition.RejectedByUserIdColumn}," +
			$"@{EntityReviewApproveTableDefinition.RecalledByUserIdColumn}," +
			$"@{EntityReviewApproveTableDefinition.PostponedByUserIdColumn}," +
			$"@{EntityReviewApproveTableDefinition.SubmittedByUserIdColumn}," +
			$"@{EntityReviewApproveTableDefinition.LastModifiedByColumn}," +
			$"@{EntityReviewApproveTableDefinition.LastModifiedByUserIdColumn}," +
			$"@{EntityReviewApproveTableDefinition.ReviewedTimestampColumn}," +
			$"@{EntityReviewApproveTableDefinition.ApprovedTimestampColumn}," +
			$"@{EntityReviewApproveTableDefinition.ReviewedCountColumn}," +
			$"@{EntityReviewApproveTableDefinition.ApprovedCountColumn}," +
			$"@{EntityReviewApproveTableDefinition.VersionNumberColumn}) ";

		internal static string Select =
			"select " +
			$"{EntityReviewApproveTableDefinition.IdColumn}," +
			$"{EntityReviewApproveTableDefinition.ProjectIdColumn}," +
			$"{EntityReviewApproveTableDefinition.ProjectNameColumn}," +
			$"{EntityReviewApproveTableDefinition.EntityIdColumn}," +
			$"{EntityReviewApproveTableDefinition.EntityNameColumn}," +
			$"{EntityReviewApproveTableDefinition.EntityTypeColumn}," +
			$"{EntityReviewApproveTableDefinition.EntityReviewApproveStateColumn}, " +
			$"{EntityReviewApproveTableDefinition.LastActionTimestampColumn}," +
			$"{EntityReviewApproveTableDefinition.SubmitTimestampColumn}, " +
			$"{EntityReviewApproveTableDefinition.InReviewByColumn}," +
			$"{EntityReviewApproveTableDefinition.InApproveByColumn}," +
			$"{EntityReviewApproveTableDefinition.ReviewedByColumn}," +
			$"{EntityReviewApproveTableDefinition.ApprovedByColumn}," +
			$"{EntityReviewApproveTableDefinition.RejectedByColumn}," +
			$"{EntityReviewApproveTableDefinition.RecalledByColumn}," +
			$"{EntityReviewApproveTableDefinition.PostponedByColumn}," +
			$"{EntityReviewApproveTableDefinition.SubmittedByColumn}," +
			$"{EntityReviewApproveTableDefinition.SubmittedByColumn}," +
			$"{EntityReviewApproveTableDefinition.InReviewByUserIdColumn}," +
			$"{EntityReviewApproveTableDefinition.InApproveByUserIdColumn}," +
			$"{EntityReviewApproveTableDefinition.ReviewedByUserIdColumn}," +
			$"{EntityReviewApproveTableDefinition.ApprovedByUserIdColumn}," +
			$"{EntityReviewApproveTableDefinition.RejectedByUserIdColumn}," +
			$"{EntityReviewApproveTableDefinition.RecalledByUserIdColumn}," +
			$"{EntityReviewApproveTableDefinition.PostponedByUserIdColumn}," +
			$"{EntityReviewApproveTableDefinition.SubmittedByUserIdColumn}," +
			$"{EntityReviewApproveTableDefinition.LastModifiedByColumn}," +
			$"{EntityReviewApproveTableDefinition.LastModifiedByUserIdColumn}," +
			$"{EntityReviewApproveTableDefinition.ReviewedTimestampColumn}," +
			$"{EntityReviewApproveTableDefinition.ApprovedTimestampColumn}," +
			$"{EntityReviewApproveTableDefinition.ReviewedCountColumn}," +
			$"{EntityReviewApproveTableDefinition.ApprovedCountColumn}," +
			$"{EntityReviewApproveTableDefinition.VersionNumberColumn} " +
			$"from {EntityReviewApproveTableDefinition.TableName} ";


		internal static string Update =
			$"update {EntityReviewApproveTableDefinition.TableName} " +
			"set " +
			$"{EntityReviewApproveTableDefinition.EntityNameColumn} = @{EntityReviewApproveTableDefinition.EntityNameColumn}, " +
			$"{EntityReviewApproveTableDefinition.EntityReviewApproveStateColumn} = @{EntityReviewApproveTableDefinition.EntityReviewApproveStateColumn}, " +
			$"{EntityReviewApproveTableDefinition.LastModifiedByColumn} = @{EntityReviewApproveTableDefinition.LastModifiedByColumn}, " +
			$"{EntityReviewApproveTableDefinition.LastModifiedByUserIdColumn} = @{EntityReviewApproveTableDefinition.LastModifiedByUserIdColumn}, " +
			$"{EntityReviewApproveTableDefinition.LastActionTimestampColumn} = @{EntityReviewApproveTableDefinition.LastActionTimestampColumn}, " +
			$"{EntityReviewApproveTableDefinition.InReviewByColumn} = @{EntityReviewApproveTableDefinition.InReviewByColumn}, " +
			$"{EntityReviewApproveTableDefinition.InApproveByColumn} = @{EntityReviewApproveTableDefinition.InApproveByColumn}, " +
			$"{EntityReviewApproveTableDefinition.ReviewedByColumn} = @{EntityReviewApproveTableDefinition.ReviewedByColumn}," +
			$"{EntityReviewApproveTableDefinition.ApprovedByColumn} = @{EntityReviewApproveTableDefinition.ApprovedByColumn}, " +
			$"{EntityReviewApproveTableDefinition.RejectedByColumn} = @{EntityReviewApproveTableDefinition.RejectedByColumn}, " +
			$"{EntityReviewApproveTableDefinition.RecalledByColumn} = @{EntityReviewApproveTableDefinition.RecalledByColumn}, " +
			$"{EntityReviewApproveTableDefinition.PostponedByColumn} = @{EntityReviewApproveTableDefinition.PostponedByColumn}, " +
			$"{EntityReviewApproveTableDefinition.SubmittedByColumn} = @{EntityReviewApproveTableDefinition.SubmittedByColumn}, " +
			$"{EntityReviewApproveTableDefinition.InReviewByUserIdColumn} = @{EntityReviewApproveTableDefinition.InReviewByUserIdColumn}, " +
			$"{EntityReviewApproveTableDefinition.InApproveByUserIdColumn} = @{EntityReviewApproveTableDefinition.InApproveByUserIdColumn}, " +
			$"{EntityReviewApproveTableDefinition.ReviewedByUserIdColumn} = @{EntityReviewApproveTableDefinition.ReviewedByUserIdColumn}," +
			$"{EntityReviewApproveTableDefinition.ApprovedByUserIdColumn} = @{EntityReviewApproveTableDefinition.ApprovedByUserIdColumn}, " +
			$"{EntityReviewApproveTableDefinition.RejectedByUserIdColumn} = @{EntityReviewApproveTableDefinition.RejectedByUserIdColumn}, " +
			$"{EntityReviewApproveTableDefinition.RecalledByUserIdColumn} = @{EntityReviewApproveTableDefinition.RecalledByUserIdColumn}, " +
			$"{EntityReviewApproveTableDefinition.PostponedByUserIdColumn} = @{EntityReviewApproveTableDefinition.PostponedByUserIdColumn}, " +
			$"{EntityReviewApproveTableDefinition.SubmittedByUserIdColumn} = @{EntityReviewApproveTableDefinition.SubmittedByUserIdColumn}, " +
			$"{EntityReviewApproveTableDefinition.ReviewedTimestampColumn} = @{EntityReviewApproveTableDefinition.ReviewedTimestampColumn}, " +
			$"{EntityReviewApproveTableDefinition.ApprovedTimestampColumn} = @{EntityReviewApproveTableDefinition.ApprovedTimestampColumn}, " +
			$"{EntityReviewApproveTableDefinition.ReviewedCountColumn} = @{EntityReviewApproveTableDefinition.ReviewedCountColumn}, " +
			$"{EntityReviewApproveTableDefinition.ApprovedCountColumn} = @{EntityReviewApproveTableDefinition.ApprovedCountColumn} " +
			"where " +
			$"{EntityReviewApproveTableDefinition.IdColumn} = @{EntityReviewApproveTableDefinition.IdColumn} ";

		internal static string SelectAll =
			$"select {EntityReviewApproveTableDefinition.TableName}.*, A.{AnalysisResultSetDao.NameColumn} as DataName  from {EntityReviewApproveTableDefinition.TableName} " +
            $"left JOIN  (select {AnalysisResultSetDao.TableName}.{AnalysisResultSetDao.NameColumn}, {EntityReviewApproveAssociatedDataMapTableDefinition.TableName}.{EntityReviewApproveAssociatedDataMapTableDefinition.ReviewApproveIdColumn} " +
                         $" from {EntityReviewApproveAssociatedDataMapTableDefinition.TableName}  " +
                         $" inner join {AnalysisResultSetDao.TableName} " +
                         $" on {EntityReviewApproveAssociatedDataMapTableDefinition.TableName}.{EntityReviewApproveAssociatedDataMapTableDefinition.DataIdColumn}={AnalysisResultSetDao.TableName}.{AnalysisResultSetDao.IdColumn}) as A " +
            $" on {EntityReviewApproveTableDefinition.TableName}.{EntityReviewApproveTableDefinition.EntityIdColumn}= A.{EntityReviewApproveAssociatedDataMapTableDefinition.ReviewApproveIdColumn} " +
            $"where {EntityReviewApproveTableDefinition.ProjectNameColumn} = @{EntityReviewApproveTableDefinition.ProjectNameColumn}";

		internal static string SelectAllByState =
            $"select {EntityReviewApproveTableDefinition.TableName}.*, A.{AnalysisResultSetDao.NameColumn} as DataName from " +
            $"{EntityReviewApproveTableDefinition.TableName} " +
            $"left JOIN  (select {AnalysisResultSetDao.TableName}.{AnalysisResultSetDao.NameColumn}, {EntityReviewApproveAssociatedDataMapTableDefinition.TableName}.{EntityReviewApproveAssociatedDataMapTableDefinition.ReviewApproveIdColumn} " +
                         $" from {EntityReviewApproveAssociatedDataMapTableDefinition.TableName}  " +
                         $" inner join {AnalysisResultSetDao.TableName} " +
                         $" on {EntityReviewApproveAssociatedDataMapTableDefinition.TableName}.{EntityReviewApproveAssociatedDataMapTableDefinition.DataIdColumn}={AnalysisResultSetDao.TableName}.{AnalysisResultSetDao.IdColumn}) as A " +
            $" on {EntityReviewApproveTableDefinition.TableName}.{EntityReviewApproveTableDefinition.EntityIdColumn}= A.{EntityReviewApproveAssociatedDataMapTableDefinition.ReviewApproveIdColumn} " +
            $"where {EntityReviewApproveTableDefinition.EntityReviewApproveStateColumn} != @{EntityReviewApproveTableDefinition.EntityReviewApproveStateColumn} " +
			$"{EntityReviewApproveTableDefinition.ProjectNameColumn} = @{EntityReviewApproveTableDefinition.ProjectNameColumn} ";

		internal static string SelectByEntityIdAndType =
            $"select {EntityReviewApproveTableDefinition.TableName}.*, A.{AnalysisResultSetDao.NameColumn} as DataName from " +
            $"{EntityReviewApproveTableDefinition.TableName} " +
            $"left JOIN  (select {AnalysisResultSetDao.TableName}.{AnalysisResultSetDao.NameColumn}, {EntityReviewApproveAssociatedDataMapTableDefinition.TableName}.{EntityReviewApproveAssociatedDataMapTableDefinition.ReviewApproveIdColumn} " +
                         $" from {EntityReviewApproveAssociatedDataMapTableDefinition.TableName}  " +
                         $" inner join {AnalysisResultSetDao.TableName} " +
                         $" on {EntityReviewApproveAssociatedDataMapTableDefinition.TableName}.{EntityReviewApproveAssociatedDataMapTableDefinition.DataIdColumn}={AnalysisResultSetDao.TableName}.{AnalysisResultSetDao.IdColumn}) as A " +
            $" on {EntityReviewApproveTableDefinition.TableName}.{EntityReviewApproveTableDefinition.EntityIdColumn}= A.{EntityReviewApproveAssociatedDataMapTableDefinition.ReviewApproveIdColumn} " +
            $"where {EntityReviewApproveTableDefinition.ProjectNameColumn} = @{EntityReviewApproveTableDefinition.ProjectNameColumn} and " +
			$"{EntityReviewApproveTableDefinition.EntityIdColumn} = @{EntityReviewApproveTableDefinition.EntityIdColumn} and " +
			$"{EntityReviewApproveTableDefinition.EntityTypeColumn} = @{EntityReviewApproveTableDefinition.EntityTypeColumn} ";

		internal static string Delete =
			$"delete from {EntityReviewApproveTableDefinition.TableName} " +
			"where " +
			$"{EntityReviewApproveTableDefinition.IdColumn} = @{EntityReviewApproveTableDefinition.IdColumn} ";

		internal static string DeleteAll = $"delete from {EntityReviewApproveTableDefinition.TableName} ";

		internal static string SelectProjectName =
			$"select distinct {EntityReviewApproveTableDefinition.ProjectNameColumn} from {EntityReviewApproveTableDefinition.TableName} " +
			$"where {EntityReviewApproveTableDefinition.ProjectIdColumn} = @{EntityReviewApproveTableDefinition.ProjectIdColumn}";

		internal static string UpdateProjectName =
			$"update {EntityReviewApproveTableDefinition.TableName} " +
			"set " +
			$"{EntityReviewApproveTableDefinition.ProjectNameColumn} = @{EntityReviewApproveTableDefinition.ProjectNameColumn} " +
			"where " +
			$"{EntityReviewApproveTableDefinition.ProjectIdColumn} = @{EntityReviewApproveTableDefinition.ProjectIdColumn} ";

		internal static string UpdateEntityName =
			$"update {EntityReviewApproveTableDefinition.TableName} " +
			"set " +
			$"{EntityReviewApproveTableDefinition.EntityNameColumn} = @{EntityReviewApproveTableDefinition.EntityNameColumn} " +
			"where " +
			$"{EntityReviewApproveTableDefinition.EntityIdColumn} = @{EntityReviewApproveTableDefinition.EntityIdColumn} ";
	}
}
