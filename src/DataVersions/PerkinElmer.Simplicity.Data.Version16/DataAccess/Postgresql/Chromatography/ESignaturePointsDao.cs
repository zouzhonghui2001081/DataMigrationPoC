using System;
using System.Data;
using System.Linq;
using System.Reflection;
using Dapper;
using log4net;
using PerkinElmer.Simplicity.Data.Version16.Contract.DataEntities.Chromatography.LabManagement;

namespace PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.Chromatography
{
    internal static class ESignaturePointsTableDefinition
    {
        internal static string TableName = "ESignaturePoints";

        internal static string IdColumn { get; } = "Id";

        internal static string GuidColumn { get; } = "Guid";

        internal static string NameColumn { get; } = "Name";

        internal static string ModuleNameColumn { get; } = "ModuleName";

        internal static string DisplayOrderColumn { get; } = "DisplayOrder";

        internal static string IsUseAuthColumn { get; } = "IsUseAuth";

        internal static string IsCustomReasonColumn { get; } = "IsCustomReason";

        internal static string IsPredefinedReasonColumn { get; } = "IsPredefinedReason";

        internal static string ReasonsColumn { get; } = "Reasons";

        internal static string CreatedDateColumn { get; } = "CreatedDate";

        internal static string CreatedUserIdColumn { get; } = "CreatedUserId";

        internal static string ModifiedDateColumn { get; } = "ModifiedDate";

        internal static string ModifiedUserIdColumn { get; } = "ModifiedUserId";
    }

    internal static class SqlESignaturePoints
    {
        internal static Func<(string module, string name),(string sql, object parameter)> Exist = (input) =>
            ($"select 1 from {ESignaturePointsTableDefinition.TableName} " +
            $"where lower({ESignaturePointsTableDefinition.ModuleNameColumn}) = lower(@Module) " +
            $"and lower({ESignaturePointsTableDefinition.NameColumn}) = lower(@Name)", new {Module = input.module, Name = input.name});

        internal static string Insert = 
            $"insert into {ESignaturePointsTableDefinition.TableName} " +
            $"({ESignaturePointsTableDefinition.GuidColumn}, " +
            $"{ESignaturePointsTableDefinition.NameColumn}, " +
            $"{ESignaturePointsTableDefinition.ModuleNameColumn}, " +
            $"{ESignaturePointsTableDefinition.DisplayOrderColumn}, " +
            $"{ESignaturePointsTableDefinition.IsUseAuthColumn}, " +
            $"{ESignaturePointsTableDefinition.IsCustomReasonColumn}, " +
            $"{ESignaturePointsTableDefinition.IsPredefinedReasonColumn}, " +
            $"{ESignaturePointsTableDefinition.ReasonsColumn}, " +
            $"{ESignaturePointsTableDefinition.CreatedDateColumn}, " +
            $"{ESignaturePointsTableDefinition.CreatedUserIdColumn}, " +
            $"{ESignaturePointsTableDefinition.ModifiedDateColumn}, " +
            $"{ESignaturePointsTableDefinition.ModifiedUserIdColumn} ) " +
            "values(" +
            $"@{ESignaturePointsTableDefinition.GuidColumn}," +
            $"@{ESignaturePointsTableDefinition.NameColumn}, " +
            $"@{ESignaturePointsTableDefinition.ModuleNameColumn}, " +
            $"@{ESignaturePointsTableDefinition.DisplayOrderColumn}, " +
            $"@{ESignaturePointsTableDefinition.IsUseAuthColumn}, " +
            $"@{ESignaturePointsTableDefinition.IsCustomReasonColumn}, " +
            $"@{ESignaturePointsTableDefinition.IsPredefinedReasonColumn}, " +
            $"@{ESignaturePointsTableDefinition.ReasonsColumn}, " +
            $"@{ESignaturePointsTableDefinition.CreatedDateColumn}, " +
            $"@{ESignaturePointsTableDefinition.CreatedUserIdColumn}, " +
            $"@{ESignaturePointsTableDefinition.ModifiedDateColumn}, " +
            $"@{ESignaturePointsTableDefinition.ModifiedUserIdColumn} ) ";

        internal static string Update =
            $"update {ESignaturePointsTableDefinition.TableName} " +
            "set " +
            $"{ESignaturePointsTableDefinition.DisplayOrderColumn} = @{ESignaturePointsTableDefinition.DisplayOrderColumn}, " +
            $"{ESignaturePointsTableDefinition.IsUseAuthColumn} = @{ESignaturePointsTableDefinition.IsUseAuthColumn}, " +
            $"{ESignaturePointsTableDefinition.IsCustomReasonColumn} = @{ESignaturePointsTableDefinition.IsCustomReasonColumn}, " +
            $"{ESignaturePointsTableDefinition.IsPredefinedReasonColumn} = @{ESignaturePointsTableDefinition.IsPredefinedReasonColumn}, " +
            $"{ESignaturePointsTableDefinition.ReasonsColumn} = @{ESignaturePointsTableDefinition.ReasonsColumn}," +
            $"{ESignaturePointsTableDefinition.ModifiedDateColumn} = @{ESignaturePointsTableDefinition.ModifiedDateColumn}, " +
            $"{ESignaturePointsTableDefinition.ModifiedUserIdColumn} = @{ESignaturePointsTableDefinition.ModifiedUserIdColumn} " +
            "where " +
            $"lower({ESignaturePointsTableDefinition.ModuleNameColumn}) = lower(@{ESignaturePointsTableDefinition.ModuleNameColumn}) " +
            $"and lower({ESignaturePointsTableDefinition.NameColumn}) = lower(@{ESignaturePointsTableDefinition.NameColumn}) ";


        internal static string SelectById = 
            "select " +
            $"{ESignaturePointsTableDefinition.IdColumn}, " +
            $"{ESignaturePointsTableDefinition.GuidColumn}," +
            $"{ESignaturePointsTableDefinition.NameColumn}, " +
            $"{ESignaturePointsTableDefinition.ModuleNameColumn}, " +
            $"{ESignaturePointsTableDefinition.DisplayOrderColumn}, " +
            $"{ESignaturePointsTableDefinition.IsUseAuthColumn}, " +
            $"{ESignaturePointsTableDefinition.IsCustomReasonColumn}, " +
            $"{ESignaturePointsTableDefinition.IsPredefinedReasonColumn}, " +
            $"{ESignaturePointsTableDefinition.ReasonsColumn}, " +
            $"{ESignaturePointsTableDefinition.CreatedDateColumn}, " +
            $"{ESignaturePointsTableDefinition.CreatedUserIdColumn}, " +
            $"{ESignaturePointsTableDefinition.ModifiedDateColumn}, " +
            $"{ESignaturePointsTableDefinition.ModifiedUserIdColumn} " +
            $"from {ESignaturePointsTableDefinition.TableName} " +
            $"where {ESignaturePointsTableDefinition.GuidColumn} = '{{0}}' ";

        internal static Func<(string module, string name), (string sql, object parameter)> SelectByName = (input) =>(
            "select " +
            $"{ESignaturePointsTableDefinition.IdColumn}, " +
            $"{ESignaturePointsTableDefinition.GuidColumn}," +
            $"{ESignaturePointsTableDefinition.NameColumn}, " +
            $"{ESignaturePointsTableDefinition.ModuleNameColumn}, " +
            $"{ESignaturePointsTableDefinition.DisplayOrderColumn}, " +
            $"{ESignaturePointsTableDefinition.IsUseAuthColumn}, " +
            $"{ESignaturePointsTableDefinition.IsCustomReasonColumn}, " +
            $"{ESignaturePointsTableDefinition.IsPredefinedReasonColumn}, " +
            $"{ESignaturePointsTableDefinition.ReasonsColumn}, " +
            $"{ESignaturePointsTableDefinition.CreatedDateColumn}, " +
            $"{ESignaturePointsTableDefinition.CreatedUserIdColumn}, " +
            $"{ESignaturePointsTableDefinition.ModifiedDateColumn}, " +
            $"{ESignaturePointsTableDefinition.ModifiedUserIdColumn} " +
            $"from {ESignaturePointsTableDefinition.TableName} " +
            $"where lower({ESignaturePointsTableDefinition.ModuleNameColumn}) = lower(@Module) " +
            $"and lower({ESignaturePointsTableDefinition.NameColumn}) = lower(@Name)", new { Module = input.module, Name = input.name });


        internal static string SelectAll =
            "select " +
            $"{ESignaturePointsTableDefinition.IdColumn}, " +
            $"{ESignaturePointsTableDefinition.GuidColumn}," +
            $"{ESignaturePointsTableDefinition.NameColumn}, " +
            $"{ESignaturePointsTableDefinition.ModuleNameColumn}, " +
            $"{ESignaturePointsTableDefinition.DisplayOrderColumn}, " +
            $"{ESignaturePointsTableDefinition.IsUseAuthColumn}, " +
            $"{ESignaturePointsTableDefinition.IsCustomReasonColumn}, " +
            $"{ESignaturePointsTableDefinition.IsPredefinedReasonColumn}, " +
            $"{ESignaturePointsTableDefinition.ReasonsColumn}, " +
            $"{ESignaturePointsTableDefinition.CreatedDateColumn}, " +
            $"{ESignaturePointsTableDefinition.CreatedUserIdColumn}, " +
            $"{ESignaturePointsTableDefinition.ModifiedDateColumn}, " +
            $"{ESignaturePointsTableDefinition.ModifiedUserIdColumn} " +
            $"from {ESignaturePointsTableDefinition.TableName} " +
            $"order by {ESignaturePointsTableDefinition.DisplayOrderColumn} ";

        internal static string DeleteAll = 
            $"delete from {ESignaturePointsTableDefinition.TableName} ";

        internal static Func<(string module, string name), (string sql, object parameter)> Delete = (input) => (
            $"delete from {ESignaturePointsTableDefinition.TableName} " +
            $"where lower({ESignaturePointsTableDefinition.ModuleNameColumn}) = lower(@Module) " +
            $"and lower({ESignaturePointsTableDefinition.NameColumn}) = lower(@Name) ", new { Module = input.module, Name = input.name });


    }

    internal class ESignaturePointsDao
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public bool InsertESignaturePoint(IDbConnection connection, ESignaturePoint signaturePoint)
        {
            if (signaturePoint == null ||
                string.IsNullOrWhiteSpace(signaturePoint.ModuleName) ||
                string.IsNullOrWhiteSpace(signaturePoint.Name) ||
                IsExists(connection, signaturePoint.ModuleName, signaturePoint.Name))
                throw new ArgumentException(nameof(signaturePoint));

            try
            {
                var result = connection.Execute(SqlESignaturePoints.Insert, signaturePoint);
                return result == 1;
            }
            catch (Exception ex)
            {
                Log.Error("Error in InsertESignaturePoint method", ex);
                throw;
            }
        }

        public bool UpdateESignaturePoint(IDbConnection connection, ESignaturePoint signaturePoint)
        {
            if (signaturePoint == null ||
                string.IsNullOrWhiteSpace(signaturePoint.ModuleName) ||
                string.IsNullOrWhiteSpace(signaturePoint.Name) ||
                !IsExists(connection, signaturePoint.ModuleName, signaturePoint.Name))
                throw new ArgumentException(nameof(signaturePoint));

            try
            {
                var result = connection.Execute(SqlESignaturePoints.Update, signaturePoint);
                return result == 1;
            }
            catch (Exception ex)
            {
                Log.Error("Error in UpdateESignaturePoint method", ex);
                throw;
            }
        }

        public bool IsExists(IDbConnection connection, string moduleName, string pointName)
        {
            if (string.IsNullOrWhiteSpace(pointName))
                throw new ArgumentException(nameof(pointName));

            try
            {
                var exist = SqlESignaturePoints.Exist((moduleName, pointName));
                var result = connection.ExecuteScalar(exist.sql, exist.parameter);
                return result != null && (int)result > 0;
            }
            catch (Exception ex)
            {
                Log.Error("Error in IsExists method", ex);
                throw;
            }
        }

        public ESignaturePoint GetESignaturePointById(IDbConnection connection, Guid pointGuid)
        {
            try
            {
                return connection.QueryFirstOrDefault<ESignaturePoint>(string.Format(SqlESignaturePoints.SelectById, pointGuid));
            }
            catch (Exception ex)
            {
                Log.Error("Error in GetESignaturePointById method", ex);
                throw;
            }
        }

        public ESignaturePoint GetESignaturePointByName(IDbConnection connection, string moduleName, string pointName)
        {
            if(string.IsNullOrWhiteSpace(moduleName))
                throw new ArgumentException(nameof(moduleName));
            if (string.IsNullOrWhiteSpace(pointName))
                throw new ArgumentException(nameof(pointName));

            try
            {
                var selectByName = SqlESignaturePoints.SelectByName((moduleName, pointName));
                return connection.QueryFirstOrDefault<ESignaturePoint>(selectByName.sql, selectByName.parameter);
            }
            catch (Exception ex)
            {
                Log.Error("Error in GetESignaturePointById method", ex);
                throw;
            }
        }

        public ESignaturePoint[] GetAllESignaturePoints(IDbConnection connection)
        {
            try
            {
                return connection.Query<ESignaturePoint>(SqlESignaturePoints.SelectAll).ToArray();
            }
            catch (Exception ex)
            {
                Log.Error("Error in GetAllESignaturePoints method", ex);
                throw;
            }
        }
    }
}
