using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Dapper;
using log4net;
using PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography.ReviewApprove;

namespace PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.Chromatography
{
	internal class EntitySubItemReviewApproveDao
	{
		private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		internal bool Create(IDbConnection connection, IList<ReviewApprovableDataEntitySubItem> entitySubItems)
		{
			try
			{
				var result = connection.Execute(EntitySubItemReviewApproveSqls.Insert, entitySubItems);
				return result >= 1;
			}
			catch (Exception ex)
			{
				Log.Error("Error in Create review approve entity sub items method", ex);
				throw;
			}
		}

		internal bool DeleteAll(IDbConnection connection, long reviewApproveEntityId)
		{
			try
			{
				var result = connection.Execute(EntitySubItemReviewApproveSqls.Delete, new { EntityReviewApproveId  = reviewApproveEntityId });
				return result >= 1;
			}
			catch (Exception ex)
			{
				Log.Error("Error in DeleteAll review approve entity sub items method", ex);
				throw;
			}
		}

		internal IList<ReviewApprovableDataEntitySubItem> GetAll(IDbConnection connection, long reviewApproveEntityId)
		{
			try
			{
				return connection.Query<ReviewApprovableDataEntitySubItem>(EntitySubItemReviewApproveSqls.SelectAll, new { EntityReviewApproveId = reviewApproveEntityId }).ToList();
			}
			catch (Exception ex)
			{
				Log.Error("Error in GetAll review approve entity sub items method", ex);
				throw;
			}
		}

		internal bool Update(IDbConnection connection, ReviewApprovableDataEntitySubItem reviewApproveEntitySubItem)
		{
			try
			{
				var result = connection.Execute(EntitySubItemReviewApproveSqls.Update, reviewApproveEntitySubItem);
				return result == 1;
			}
			catch (Exception ex)
			{
				Log.Error("Error in Update review approve entity sub item method", ex);
				throw;
			}
		}

		internal bool Update(IDbConnection connection, IList<ReviewApprovableDataEntitySubItem> entitySubItems)
		{
			try
			{
				var result = connection.Execute(EntitySubItemReviewApproveSqls.Update, entitySubItems);
				return result >= 1;
			}
			catch (Exception ex)
			{
				Log.Error("Error in Update review approve entity sub items method", ex);
				throw;
			}
		}

		internal bool UpdateProjectName(IDbConnection connection, Guid projectGuid, string projectName)
		{
			try
			{
				var result = connection.Execute(EntitySubItemReviewApproveSqls.UpdateProjectName, new { projectId = projectGuid.ToString(), ProjectName = projectName });
				return true;
			}
			catch(Exception ex)
			{
				Log.Error("Error in UpdateProjectName method", ex);
				throw;
			}
		}

		internal bool UpdateEntitySubItemName(IDbConnection connection, Guid entitySubItemGuid, string entitySubItemName)
		{
			try
			{
				var result = connection.Execute(EntitySubItemReviewApproveSqls.UpdateEntitySubItemName, new { entitysubitemid = entitySubItemGuid.ToString(), entitysubitemname = entitySubItemName });
				return true;
			}
			catch (Exception ex)
			{
				Log.Error("Error in UpdateEntityName method", ex);
				throw;
			}
		}
	}

	internal static class EntitySubItemReviewApproveTableDefinition
	{
		internal static string TableName = "EntitySubItemReviewApprove";

		internal static string IdColumn { get; } = "Id";

		internal static string ProjectIdColumn { get; } = "ProjectId";

		internal static string ProjectNameColumn { get; } = "ProjectName";

		internal static string EntityReviewApproveIdColumn { get; } = "EntityReviewApproveId";

		internal static string EntitySubItemIdColumn { get; } = "EntitySubItemId";

		internal static string EntitySubItemNameColumn { get; } = "EntitySubItemName";

		internal static string EntitySubItemTypeColumn { get; } = "EntitySubItemType";

		internal static string EntitySubItemReviewApproveStateColumn { get; } = "EntitySubItemReviewApproveState";
		internal static string EntitySubItemSampleReportTemplate { get; } = "EntitySubItemSampleReportTemplate";
		internal static string EntitySubItemSummaryReportGroup { get; } = "EntitySubItemSummaryReportGroup";
		internal static string ReviewApproveCommentColumn { get; } = "ReviewApproveComment";
	}

	internal static class EntitySubItemReviewApproveSqls
	{
		internal static string Insert =
			$"insert into {EntitySubItemReviewApproveTableDefinition.TableName} " +
			$"({EntitySubItemReviewApproveTableDefinition.EntityReviewApproveIdColumn}, " +
			$"{EntitySubItemReviewApproveTableDefinition.EntitySubItemIdColumn}, " +
			$"{EntitySubItemReviewApproveTableDefinition.EntitySubItemNameColumn}, " +
			$"{EntitySubItemReviewApproveTableDefinition.EntitySubItemTypeColumn}, " +
			$"{EntitySubItemReviewApproveTableDefinition.ProjectIdColumn}, " +
			$"{EntitySubItemReviewApproveTableDefinition.ProjectNameColumn}, " +
			$"{EntitySubItemReviewApproveTableDefinition.EntitySubItemSampleReportTemplate}, " +
			$"{EntitySubItemReviewApproveTableDefinition.EntitySubItemSummaryReportGroup}, " +
			$"{EntitySubItemReviewApproveTableDefinition.EntitySubItemReviewApproveStateColumn} ) " +
			"values(" +
			$"@{EntitySubItemReviewApproveTableDefinition.EntityReviewApproveIdColumn}," +
			$"@{EntitySubItemReviewApproveTableDefinition.EntitySubItemIdColumn}, " +
			$"@{EntitySubItemReviewApproveTableDefinition.EntitySubItemNameColumn}, " +
			$"@{EntitySubItemReviewApproveTableDefinition.EntitySubItemTypeColumn}, " +
			$"@{EntitySubItemReviewApproveTableDefinition.ProjectIdColumn}, " +
			$"@{EntitySubItemReviewApproveTableDefinition.ProjectNameColumn}, " +
			$"@{EntitySubItemReviewApproveTableDefinition.EntitySubItemSampleReportTemplate}, " +
			$"@{EntitySubItemReviewApproveTableDefinition.EntitySubItemSummaryReportGroup}, " +
			$"@{EntitySubItemReviewApproveTableDefinition.EntitySubItemReviewApproveStateColumn} )";

		internal static string Update =
			$"update {EntitySubItemReviewApproveTableDefinition.TableName} " +
			"set " +
			$"{EntitySubItemReviewApproveTableDefinition.EntitySubItemReviewApproveStateColumn} = @{EntitySubItemReviewApproveTableDefinition.EntitySubItemReviewApproveStateColumn}, " +
			$"{EntitySubItemReviewApproveTableDefinition.ReviewApproveCommentColumn} = @{EntitySubItemReviewApproveTableDefinition.ReviewApproveCommentColumn} " +
			"where " +
			$"{EntitySubItemReviewApproveTableDefinition.EntityReviewApproveIdColumn} = @{EntitySubItemReviewApproveTableDefinition.EntityReviewApproveIdColumn} and " +
			$"{EntitySubItemReviewApproveTableDefinition.EntitySubItemIdColumn} = @{EntitySubItemReviewApproveTableDefinition.EntitySubItemIdColumn}";

		internal static string SelectAll =
			"select * from " +
			$"{EntitySubItemReviewApproveTableDefinition.TableName} " +
			$"where {EntitySubItemReviewApproveTableDefinition.EntityReviewApproveIdColumn} = @{EntitySubItemReviewApproveTableDefinition.EntityReviewApproveIdColumn}";

		internal static string Delete =
			$"delete from {EntitySubItemReviewApproveTableDefinition.TableName} " +
			"where " +
			$"{EntitySubItemReviewApproveTableDefinition.EntityReviewApproveIdColumn} = @{EntitySubItemReviewApproveTableDefinition.EntityReviewApproveIdColumn} ";

		internal static string UpdateProjectName =
			$"update {EntitySubItemReviewApproveTableDefinition.TableName} " +
			"set " +
			$"{EntitySubItemReviewApproveTableDefinition.ProjectNameColumn} = @{EntitySubItemReviewApproveTableDefinition.ProjectNameColumn} " +
			"where " +
			$"{EntitySubItemReviewApproveTableDefinition.ProjectIdColumn} = @{EntitySubItemReviewApproveTableDefinition.ProjectIdColumn} ";

		internal static string UpdateEntitySubItemName =
			$"update {EntitySubItemReviewApproveTableDefinition.TableName} " +
			"set " +
			$"{EntitySubItemReviewApproveTableDefinition.EntitySubItemNameColumn} = @{EntitySubItemReviewApproveTableDefinition.EntitySubItemNameColumn} " +
			"where " +
			$"{EntitySubItemReviewApproveTableDefinition.EntitySubItemIdColumn} = @{EntitySubItemReviewApproveTableDefinition.EntitySubItemIdColumn} ";


	}
}
