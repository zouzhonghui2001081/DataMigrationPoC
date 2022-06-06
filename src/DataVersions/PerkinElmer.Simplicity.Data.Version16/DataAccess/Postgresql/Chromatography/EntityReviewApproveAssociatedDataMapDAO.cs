using System;
using System.Data;
using System.Reflection;
using Dapper;
using log4net;
using PerkinElmer.Simplicity.Data.Version16.Contract.DataEntities.Chromatography.ReviewApprove;

namespace PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.Chromatography
{
    internal class EntityReviewApproveAssociatedDataMapDAO
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        internal bool Create(IDbConnection connection, ReviewApprovableDataEntity reviewApproveEntity)
        {
            if (reviewApproveEntity == null ||
               string.IsNullOrEmpty(reviewApproveEntity.DataName) ||
               string.IsNullOrWhiteSpace(reviewApproveEntity.EntityId))
            {
                throw new ArgumentException(nameof(reviewApproveEntity));
            }

            try
            {
                var analysisResultSetDao = new AnalysisResultSetDao();
                var id = analysisResultSetDao.GetAnalysisResultSetId(connection, reviewApproveEntity.ProjectName, reviewApproveEntity.DataName);
                if (IsExists(connection, reviewApproveEntity.EntityId))
                {
                    var result = connection.Execute(EntityReviewApproveAssociatedDataMapSqls.Update, new { reviewapproveid = reviewApproveEntity.EntityId, dataid = id });
                }
                else
                {
                    var result = connection.Execute(EntityReviewApproveAssociatedDataMapSqls.Insert, new { reviewapproveid = reviewApproveEntity.EntityId, dataid = id });

                }
                return true;
            }
            catch (Exception ex)
            {
                Log.Error("Error in Create entity review approve associated data map method", ex);
                throw;
            }
        }

        internal (string reviewApproveId, long dataId) Get(IDbConnection connection, string reviewApproveId)
        {
	        try
	        {
		        var queryResult = connection.QueryFirstOrDefault(
			        EntityReviewApproveAssociatedDataMapSqls.GetByReviewApproveId, new {reviewapproveid = reviewApproveId});

		        if (queryResult != null)
		        {
					return (queryResult.reviewapproveid,queryResult.dataid);
		        }

		        return (string.Empty, 0);
	        }
			catch (Exception ex)
	        {
		        Log.Error("Error in Get method", ex);
		        throw;
	        }
        }

        internal bool Delete(IDbConnection connection, string reviewApproveId)
        {
	        var rowsAffected = connection.Execute(EntityReviewApproveAssociatedDataMapSqls.Delete,
		        new {reviewapproveid = reviewApproveId});
	        return rowsAffected != 0;
        }
        internal bool IsExists(IDbConnection connection, string entityID)
        {           
            try
            {
                var result = connection.ExecuteScalar(EntityReviewApproveAssociatedDataMapSqls.GetByReviewApproveId, new { reviewapproveid = entityID });
                return result != null && (int)result > 0;
            }
            catch (Exception ex)
            {
                Log.Error("Error in IsExists method", ex);
                throw;
            }
        }

    }
    internal static class EntityReviewApproveAssociatedDataMapTableDefinition
    {
        internal static string TableName = "EntityReviewApproveAssociatedDataMap";

        internal static string GuidColumn { get; } = "Guid";

        internal static string ReviewApproveIdColumn { get; } = "reviewapproveid";
        internal static string DataIdColumn { get; } = "dataid";
    }
    internal static class EntityReviewApproveAssociatedDataMapSqls
    {
        internal static string Insert =
            $"insert into {EntityReviewApproveAssociatedDataMapTableDefinition.TableName} " +
            $"({EntityReviewApproveAssociatedDataMapTableDefinition.ReviewApproveIdColumn}, " +
            $"{EntityReviewApproveAssociatedDataMapTableDefinition.DataIdColumn}) " +            
            "values (" +
            $"@{EntityReviewApproveAssociatedDataMapTableDefinition.ReviewApproveIdColumn}, " +
            $"@{EntityReviewApproveAssociatedDataMapTableDefinition.DataIdColumn})";

        internal static string Update =
            $"update {EntityReviewApproveAssociatedDataMapTableDefinition.TableName} " +
            "set " +
            $"{EntityReviewApproveAssociatedDataMapTableDefinition.DataIdColumn} = @{EntityReviewApproveAssociatedDataMapTableDefinition.DataIdColumn} " +
            "where " +
            $"{EntityReviewApproveAssociatedDataMapTableDefinition.ReviewApproveIdColumn} = @{EntityReviewApproveAssociatedDataMapTableDefinition.ReviewApproveIdColumn} ";        

        internal static string Delete =
            $"delete from {EntityReviewApproveAssociatedDataMapTableDefinition.TableName} " +
            "where " +
            $"{EntityReviewApproveAssociatedDataMapTableDefinition.ReviewApproveIdColumn} = @{EntityReviewApproveAssociatedDataMapTableDefinition.ReviewApproveIdColumn} ";
        internal static string GetByReviewApproveId =
            $"select {EntityReviewApproveAssociatedDataMapTableDefinition.ReviewApproveIdColumn}," +
            $"{EntityReviewApproveAssociatedDataMapTableDefinition.DataIdColumn}" +
            $" from {EntityReviewApproveAssociatedDataMapTableDefinition.TableName} " +
            "where " +
            $"{EntityReviewApproveAssociatedDataMapTableDefinition.ReviewApproveIdColumn} = @{EntityReviewApproveAssociatedDataMapTableDefinition.ReviewApproveIdColumn} ";

        internal static string DeleteAll = $"delete from {EntityReviewApproveAssociatedDataMapTableDefinition.TableName} ";
    }
}
