using System;
using System.Data;
using System.Linq;
using System.Reflection;
using Dapper;
using log4net;
using PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography.LabManagement;

namespace PerkinElmer.Simplicity.Data.Version15.DataAccess.Postgresql.Chromatography
{
    internal static class ApprovalReviewItemsTableDefinition
    {
        internal static string TableName = "ApprovalReviewItems";

        internal static string IdColumn { get; } = "Id";

        internal static string GuidColumn { get; } = "Guid";

        internal static string NameColumn { get; } = "Name";

        internal static string DisplayOrderColumn { get; } = "DisplayOrder";

        internal static string IsApprovalReviewOnColumn { get; } = "IsApprovalReviewOn";

        internal static string IsSubmitReviewApproveColumn { get; } = "IsSubmitReviewApprove";

        internal static string IsSubmitApproveColumn { get; } = "IsSubmitApprove";

        internal static string CreatedDateColumn { get; } = "CreatedDate";

        internal static string CreatedUserIdColumn { get; } = "CreatedUserId";

        internal static string ModifiedDateColumn { get; } = "ModifiedDate";

        internal static string ModifiedUserIdColumn { get; } = "ModifiedUserId";
    }

    internal static class SqlApprovalReviewItems
    {
        internal static string IsExist = 
            $"select 1 from {ApprovalReviewItemsTableDefinition.TableName} " +
            $"where lower({ESignaturePointsTableDefinition.NameColumn}) = lower('{{0}}')";

        internal static string Insert =  
            $"insert into {ApprovalReviewItemsTableDefinition.TableName} " +
            $"({ApprovalReviewItemsTableDefinition.GuidColumn}, " +
            $"{ApprovalReviewItemsTableDefinition.NameColumn}, " +
            $"{ApprovalReviewItemsTableDefinition.DisplayOrderColumn}, " +
            $"{ApprovalReviewItemsTableDefinition.IsApprovalReviewOnColumn}, " +
            $"{ApprovalReviewItemsTableDefinition.IsSubmitReviewApproveColumn}, " +
            $"{ApprovalReviewItemsTableDefinition.IsSubmitApproveColumn}, " +
            $"{ApprovalReviewItemsTableDefinition.CreatedDateColumn}, " +
            $"{ApprovalReviewItemsTableDefinition.CreatedUserIdColumn}, " +
            $"{ApprovalReviewItemsTableDefinition.ModifiedDateColumn}, " +
            $"{ApprovalReviewItemsTableDefinition.ModifiedUserIdColumn} ) " +
            "values(" +
            $"@{ApprovalReviewItemsTableDefinition.GuidColumn}," +
            $"@{ApprovalReviewItemsTableDefinition.NameColumn}, " +
            $"@{ApprovalReviewItemsTableDefinition.DisplayOrderColumn}, " +
            $"@{ApprovalReviewItemsTableDefinition.IsApprovalReviewOnColumn}, " +
            $"@{ApprovalReviewItemsTableDefinition.IsSubmitReviewApproveColumn}, " +
            $"@{ApprovalReviewItemsTableDefinition.IsSubmitApproveColumn}, " +
            $"@{ApprovalReviewItemsTableDefinition.CreatedDateColumn}, " +
            $"@{ApprovalReviewItemsTableDefinition.CreatedUserIdColumn}, " +
            $"@{ApprovalReviewItemsTableDefinition.ModifiedDateColumn}, " +
            $"@{ApprovalReviewItemsTableDefinition.ModifiedUserIdColumn} ) ";

        internal static string Update =
            $"update {ApprovalReviewItemsTableDefinition.TableName} " +
            "set " +
            $"{ApprovalReviewItemsTableDefinition.DisplayOrderColumn} = @{ApprovalReviewItemsTableDefinition.DisplayOrderColumn}, " +
            $"{ApprovalReviewItemsTableDefinition.IsApprovalReviewOnColumn} = @{ApprovalReviewItemsTableDefinition.IsApprovalReviewOnColumn}, " +
            $"{ApprovalReviewItemsTableDefinition.IsSubmitReviewApproveColumn} = @{ApprovalReviewItemsTableDefinition.IsSubmitReviewApproveColumn}, " +
            $"{ApprovalReviewItemsTableDefinition.IsSubmitApproveColumn} = @{ApprovalReviewItemsTableDefinition.IsSubmitApproveColumn}, " +
            $"{ApprovalReviewItemsTableDefinition.ModifiedDateColumn} = @{ApprovalReviewItemsTableDefinition.ModifiedDateColumn}, " +
            $"{ApprovalReviewItemsTableDefinition.ModifiedUserIdColumn} = @{ApprovalReviewItemsTableDefinition.ModifiedUserIdColumn} " +
            "where " +
            $"lower({ApprovalReviewItemsTableDefinition.NameColumn}) = lower(@{ApprovalReviewItemsTableDefinition.NameColumn}) ";

        internal static Func<Guid, (string sql, object parameter)> SelectById = keyName => (
             "select " +
            $"{ApprovalReviewItemsTableDefinition.IdColumn}, " +
            $"{ApprovalReviewItemsTableDefinition.GuidColumn}," +
            $"{ApprovalReviewItemsTableDefinition.NameColumn}, " +
            $"{ApprovalReviewItemsTableDefinition.DisplayOrderColumn}, " +
            $"{ApprovalReviewItemsTableDefinition.IsApprovalReviewOnColumn}, " +
            $"{ApprovalReviewItemsTableDefinition.IsSubmitReviewApproveColumn}, " +
            $"{ApprovalReviewItemsTableDefinition.IsSubmitApproveColumn}, " +
            $"{ApprovalReviewItemsTableDefinition.CreatedDateColumn}, " +
            $"{ApprovalReviewItemsTableDefinition.CreatedUserIdColumn}, " +
            $"{ApprovalReviewItemsTableDefinition.ModifiedDateColumn}, " +
            $"{ApprovalReviewItemsTableDefinition.ModifiedUserIdColumn} " +
            $"from {ApprovalReviewItemsTableDefinition.TableName} " +
            $"where {ApprovalReviewItemsTableDefinition.GuidColumn} = @Guid ", new { Guid = keyName });

        internal static Func<string, (string sql, object parameter)> SelectByName = keyName => (
             "select " +
            $"{ApprovalReviewItemsTableDefinition.IdColumn}, " +
            $"{ApprovalReviewItemsTableDefinition.GuidColumn}," +
            $"{ApprovalReviewItemsTableDefinition.NameColumn}, " +
            $"{ApprovalReviewItemsTableDefinition.DisplayOrderColumn}, " +
            $"{ApprovalReviewItemsTableDefinition.IsApprovalReviewOnColumn}, " +
            $"{ApprovalReviewItemsTableDefinition.IsSubmitReviewApproveColumn}, " +
            $"{ApprovalReviewItemsTableDefinition.IsSubmitApproveColumn}, " +
            $"{ApprovalReviewItemsTableDefinition.CreatedDateColumn}, " +
            $"{ApprovalReviewItemsTableDefinition.CreatedUserIdColumn}, " +
            $"{ApprovalReviewItemsTableDefinition.ModifiedDateColumn}, " +
            $"{ApprovalReviewItemsTableDefinition.ModifiedUserIdColumn} " +
            $"from {ApprovalReviewItemsTableDefinition.TableName} " +
            $"where lower({ESignaturePointsTableDefinition.NameColumn}) = lower(@Name) ", new { Name = keyName });

        internal static string SelectAll =
            "select " +
            $"{ApprovalReviewItemsTableDefinition.IdColumn}, " +
            $"{ApprovalReviewItemsTableDefinition.GuidColumn}," +
            $"{ApprovalReviewItemsTableDefinition.NameColumn}, " +
            $"{ApprovalReviewItemsTableDefinition.DisplayOrderColumn}, " +
            $"{ApprovalReviewItemsTableDefinition.IsApprovalReviewOnColumn}, " +
            $"{ApprovalReviewItemsTableDefinition.IsSubmitReviewApproveColumn}, " +
            $"{ApprovalReviewItemsTableDefinition.IsSubmitApproveColumn}, " +
            $"{ApprovalReviewItemsTableDefinition.CreatedDateColumn}, " +
            $"{ApprovalReviewItemsTableDefinition.CreatedUserIdColumn}, " +
            $"{ApprovalReviewItemsTableDefinition.ModifiedDateColumn}, " +
            $"{ApprovalReviewItemsTableDefinition.ModifiedUserIdColumn} " +
            $"from {ApprovalReviewItemsTableDefinition.TableName} " +
            $"order by {ApprovalReviewItemsTableDefinition.DisplayOrderColumn} ";

        internal static string DeleteAll =
            $"delete from {ApprovalReviewItemsTableDefinition.TableName} ";

        internal static Func<string, (string sql, object parameter)> Delete = keyName => (
             $"delete from {ApprovalReviewItemsTableDefinition.TableName} " +
            $"where lower({ApprovalReviewItemsTableDefinition.NameColumn}) = lower(@Name) ", new { Name = keyName });
    }

    internal class ApprovalReviewItemsDao
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public bool InsertApprovalReviewItem(IDbConnection connection, ApprovalReviewItem approvalReviewItem)
        {
            if (approvalReviewItem == null ||
                string.IsNullOrWhiteSpace(approvalReviewItem.Name) ||
                IsExists(connection, approvalReviewItem.Name))
                throw new ArgumentException(nameof(approvalReviewItem));

            try
            {
                var result = connection.Execute(SqlApprovalReviewItems.Insert, approvalReviewItem);
                return result == 1;
            }
            catch (Exception ex)
            {
                Log.Error("Error in InsertApprovalReviewItem method", ex);
                throw;
            }
        }

        public bool UpdateApprovalReviewItem(IDbConnection connection, ApprovalReviewItem approvalReviewItem)
        {
            if (approvalReviewItem == null ||
                string.IsNullOrWhiteSpace(approvalReviewItem.Name) ||
                !IsExists(connection, approvalReviewItem.Name))
                throw new ArgumentException(nameof(approvalReviewItem));

            try
            {
                var result = connection.Execute(string.Format(SqlApprovalReviewItems.Update, approvalReviewItem.Name), approvalReviewItem);
                return result == 1;
            }
            catch (Exception ex)
            {
                Log.Error("Error in UpdateApprovalReviewItem method", ex);
                throw;
            }
        }

        public bool IsExists(IDbConnection connection, string approvalReviewItemName)
        {
            if (string.IsNullOrWhiteSpace(approvalReviewItemName))
                throw new ArgumentException(nameof(approvalReviewItemName));

            try
            {
                var result = connection.ExecuteScalar(string.Format(SqlApprovalReviewItems.IsExist, approvalReviewItemName));
                return result != null && (int)result > 0;
            }
            catch (Exception ex)
            {
                Log.Error("Error in IsExists method", ex);
                throw;
            }
        }

        public ApprovalReviewItem GetApprovalReviewItemById(IDbConnection connection, Guid approvalReviewItemGuid)
        {
            try
            {
                var selectById = SqlApprovalReviewItems.SelectById(approvalReviewItemGuid);
                return connection.QueryFirstOrDefault<ApprovalReviewItem>(selectById.sql, selectById.parameter);
            }
            catch (Exception ex)
            {
                Log.Error("Error in GetApprovalReviewItemById method", ex);
                throw;
            }
        }

        public ApprovalReviewItem GetApprovalReviewItemByName(IDbConnection connection, string approvalReviewItemName)
        {
            if (string.IsNullOrWhiteSpace(approvalReviewItemName))
                throw new ArgumentException(nameof(approvalReviewItemName));

            try
            {
                var selectByName = SqlApprovalReviewItems.SelectByName(approvalReviewItemName);
                return connection.QueryFirstOrDefault<ApprovalReviewItem>(selectByName.sql, selectByName.parameter);
            }
            catch (Exception ex)
            {
                Log.Error("Error in GetApprovalReviewItemByName method", ex);
                throw;
            }
        }

        public ApprovalReviewItem[] GetAllApprovalReviewItems(IDbConnection connection)
        {
            try
            {
                return connection.Query<ApprovalReviewItem>(SqlApprovalReviewItems.SelectAll).ToArray();
            }
            catch (Exception ex)
            {
                Log.Error("Error in GetAllApprovalReviewItems method", ex);
                throw;
            }
        }


    }
}
