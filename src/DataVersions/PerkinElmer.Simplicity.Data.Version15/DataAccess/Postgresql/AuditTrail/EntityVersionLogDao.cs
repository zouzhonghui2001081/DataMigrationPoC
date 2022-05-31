using System;
using System.Data;
using System.Linq;
using System.Reflection;
using Dapper;
using log4net;
using PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.AuditTrail;

namespace PerkinElmer.Simplicity.Data.Version15.DataAccess.Postgresql.AuditTrail
{
    internal static class EntityVersionLogTableDefinition
    {
        internal static string TableName = "EntityVersionLog";

        internal static string IdColumn { get; } = "Id";

        internal static string EntityIdColumn { get; } = "EntityId";

        internal static string EntityTypeColumn { get; } = "EntityType";

        internal static string AfterChangeVersionNumberColumn { get; } = "AfterChangeVersionNumber";

        internal static string VersionDataColumn { get; } = "VersionData";

        internal static string CreationTimestampColumn { get; } = "CreationTimestamp";

        internal static string DescriptionColumn { get; } = "Description";

        internal static string BeforeChangeVersionNumberColumn { get; } = "BeforeChangeVersionNumber";

        internal static string BeforeChangeEntityIdColumn { get; } = "BeforeChangeEntityId";

    }

    internal static class EntityVersionLogSqls
    {
        internal static string ExistByVersionNumber =
            $"select 1 from {EntityVersionLogTableDefinition.TableName} " +
            $"where lower({EntityVersionLogTableDefinition.EntityIdColumn}) = lower('{{0}}') " +
            $"and lower({EntityVersionLogTableDefinition.EntityTypeColumn}) = lower('{{1}}') " +
            $"and {EntityVersionLogTableDefinition.AfterChangeVersionNumberColumn} = '{{2}}' ";


        internal static string ExistVersion =
            $"select 1 from {EntityVersionLogTableDefinition.TableName} " +
            $"where lower({EntityVersionLogTableDefinition.EntityIdColumn}) = lower('{{0}}') " +
            $"and lower({EntityVersionLogTableDefinition.EntityTypeColumn}) = lower('{{1}}') ";

        internal static string InsertVersion =
            $"insert into {EntityVersionLogTableDefinition.TableName} " +
            "(" +
            $"{EntityVersionLogTableDefinition.EntityIdColumn}, " +
            $"{EntityVersionLogTableDefinition.EntityTypeColumn}, " +
            $"{EntityVersionLogTableDefinition.AfterChangeVersionNumberColumn}, " +
            $"{EntityVersionLogTableDefinition.BeforeChangeVersionNumberColumn}, " +
            $"{EntityVersionLogTableDefinition.BeforeChangeEntityIdColumn}, " +
            $"{EntityVersionLogTableDefinition.VersionDataColumn}, " +
            $"{EntityVersionLogTableDefinition.CreationTimestampColumn}, " +
            $"{EntityVersionLogTableDefinition.DescriptionColumn}" +
            ") " +
            "values(" +
            $"@{EntityVersionLogTableDefinition.EntityIdColumn}, " +
            $"@{EntityVersionLogTableDefinition.EntityTypeColumn}, " +
            $"@{EntityVersionLogTableDefinition.AfterChangeVersionNumberColumn}, " +
            $"@{EntityVersionLogTableDefinition.BeforeChangeVersionNumberColumn}, " +
            $"@{EntityVersionLogTableDefinition.BeforeChangeEntityIdColumn}, " +
            $"@{EntityVersionLogTableDefinition.VersionDataColumn}, " +
            $"@{EntityVersionLogTableDefinition.CreationTimestampColumn}, " +
            $"@{EntityVersionLogTableDefinition.DescriptionColumn} " +
            ") ";

        internal static string InsertVersionAndReturnId =
            InsertVersion + $"RETURNING {EntityVersionLogTableDefinition.IdColumn}";

        internal static string UpdateVersionByVersionNumber =
            $"update {EntityVersionLogTableDefinition.TableName} " +
            "set " +
            $"{EntityVersionLogTableDefinition.BeforeChangeVersionNumberColumn} = @{EntityVersionLogTableDefinition.BeforeChangeVersionNumberColumn}, " +
            $"{EntityVersionLogTableDefinition.BeforeChangeEntityIdColumn} = @{EntityVersionLogTableDefinition.BeforeChangeEntityIdColumn}, " +
            $"{EntityVersionLogTableDefinition.VersionDataColumn} = @{EntityVersionLogTableDefinition.VersionDataColumn}, " +
            $"{EntityVersionLogTableDefinition.CreationTimestampColumn} = @{EntityVersionLogTableDefinition.CreationTimestampColumn}, " +
            $"{EntityVersionLogTableDefinition.DescriptionColumn} = @{EntityVersionLogTableDefinition.DescriptionColumn} " +
            $"where lower({EntityVersionLogTableDefinition.EntityIdColumn}) = lower('{{0}}') " +
            $"and lower({EntityVersionLogTableDefinition.EntityTypeColumn}) = lower('{{1}}') " +
            $"and {EntityVersionLogTableDefinition.AfterChangeVersionNumberColumn} = '{{2}}' ";


        internal static string SelectVersionByVersionNumber =
            "select " +
            $"{EntityVersionLogTableDefinition.IdColumn}, " +
            $"{EntityVersionLogTableDefinition.EntityIdColumn}," +
            $"{EntityVersionLogTableDefinition.EntityTypeColumn}, " +
            $"{EntityVersionLogTableDefinition.AfterChangeVersionNumberColumn}, " +
            $"{EntityVersionLogTableDefinition.BeforeChangeVersionNumberColumn}, " +
            $"{EntityVersionLogTableDefinition.BeforeChangeEntityIdColumn}, " +
            $"{EntityVersionLogTableDefinition.VersionDataColumn}, " +
            $"{EntityVersionLogTableDefinition.CreationTimestampColumn}, " +
            $"{EntityVersionLogTableDefinition.DescriptionColumn} " +
            $"from {EntityVersionLogTableDefinition.TableName} " +
            $"where lower({EntityVersionLogTableDefinition.EntityIdColumn}) = lower('{{0}}') " +
            $"and lower({EntityVersionLogTableDefinition.EntityTypeColumn}) = lower('{{1}}') " +
            $"and {EntityVersionLogTableDefinition.AfterChangeVersionNumberColumn} = '{{2}}' ";


        internal static string SelectAllVersionsForEntity =
            "select " +
            $"{EntityVersionLogTableDefinition.IdColumn}, " +
            $"{EntityVersionLogTableDefinition.EntityIdColumn}," +
            $"{EntityVersionLogTableDefinition.EntityTypeColumn}, " +
            $"{EntityVersionLogTableDefinition.AfterChangeVersionNumberColumn}, " +
            $"{EntityVersionLogTableDefinition.BeforeChangeVersionNumberColumn}, " +
            $"{EntityVersionLogTableDefinition.BeforeChangeEntityIdColumn}, " +
            $"{EntityVersionLogTableDefinition.VersionDataColumn}, " +
            $"{EntityVersionLogTableDefinition.CreationTimestampColumn}, " +
            $"{EntityVersionLogTableDefinition.DescriptionColumn} " +
            $"from {EntityVersionLogTableDefinition.TableName} " +
            $"where lower({ EntityVersionLogTableDefinition.EntityIdColumn}) = lower('{{0}}') " +
            $"and lower({EntityVersionLogTableDefinition.EntityTypeColumn}) = lower('{{1}}')";

        internal static string SelectVersionByVersionId =
            "select " +
            $"{EntityVersionLogTableDefinition.IdColumn}, " +
            $"{EntityVersionLogTableDefinition.EntityIdColumn}," +
            $"{EntityVersionLogTableDefinition.EntityTypeColumn}, " +
            $"{EntityVersionLogTableDefinition.AfterChangeVersionNumberColumn}, " +
            $"{EntityVersionLogTableDefinition.BeforeChangeVersionNumberColumn}, " +
            $"{EntityVersionLogTableDefinition.BeforeChangeEntityIdColumn}, " +
            $"{EntityVersionLogTableDefinition.VersionDataColumn}, " +
            $"{EntityVersionLogTableDefinition.CreationTimestampColumn}, " +
            $"{EntityVersionLogTableDefinition.DescriptionColumn} " +
            $"from {EntityVersionLogTableDefinition.TableName} " +
            $"where { EntityVersionLogTableDefinition.IdColumn} = '{{0}}' ";

        internal static string SelectMaxVersionNumberForEntity =
            $"select max({EntityVersionLogTableDefinition.AfterChangeVersionNumberColumn}) " +
            $"from {EntityVersionLogTableDefinition.TableName} " +
            $"where lower({ EntityVersionLogTableDefinition.EntityIdColumn}) = lower('{{0}}') " +
            $"and lower({EntityVersionLogTableDefinition.EntityTypeColumn}) = lower('{{1}}')";

        internal static string DeleteVersionByVersionNumber =
            $"delete from {EntityVersionLogTableDefinition.TableName} " +
            $"where lower({EntityVersionLogTableDefinition.EntityIdColumn}) = lower('{{0}}') " +
            $"and lower({EntityVersionLogTableDefinition.EntityTypeColumn}) = lower('{{1}}') " +
            $"and {EntityVersionLogTableDefinition.AfterChangeVersionNumberColumn} = '{{2}}' ";


        internal static string DeleteAllVersionsForEntity =
            $"delete from {EntityVersionLogTableDefinition.TableName} " +
            $"where lower({ EntityVersionLogTableDefinition.EntityIdColumn}) = lower('{{0}}') " +
            $"and lower({EntityVersionLogTableDefinition.EntityTypeColumn}) = lower('{{1}}')";
    }

    public class EntityVersionLogDao
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        internal bool IsExist(IDbConnection connection, string entityId, string entityType, long entityVersionNumber)
        {
            if (string.IsNullOrWhiteSpace(entityId))
                throw new ArgumentException(nameof(entityId));
            if (string.IsNullOrWhiteSpace(entityType))
                throw new ArgumentException(nameof(entityType));

            try
            {
                var result = connection.ExecuteScalar(string.Format(EntityVersionLogSqls.ExistByVersionNumber, entityId, entityType, entityVersionNumber));
                return result != null && (int)result > 0;
            }
            catch (Exception ex)
            {
                Log.Error("Error in IsExistByVersionNumber method", ex);
                throw;
            }
        }

        internal bool IsExist(IDbConnection connection, string entityId, string entityType)
        {
            if (string.IsNullOrWhiteSpace(entityId))
                throw new ArgumentException(nameof(entityId));
            if (string.IsNullOrWhiteSpace(entityType))
                throw new ArgumentException(nameof(entityType));

            try
            {
                var result = connection.ExecuteScalar(string.Format(EntityVersionLogSqls.ExistVersion, entityId, entityType));
                return result != null && (int)result > 0;
            }
            catch (Exception ex)
            {
                Log.Error("Error in IsExistVersionForEntity method", ex);
                throw;
            }
        }

        internal long Insert(IDbConnection connection, EntityVersionLogEntry entityVersion)
        {
            if (entityVersion == null ||
                string.IsNullOrWhiteSpace(entityVersion.EntityId) ||
                string.IsNullOrWhiteSpace(entityVersion.EntityType) ||
                string.IsNullOrWhiteSpace(entityVersion.VersionData) ||
                IsExist(connection, entityVersion.EntityId, entityVersion.EntityType, entityVersion.AfterChangeVersionNumber))
                throw new ArgumentException(nameof(entityVersion));

            try
            {
                entityVersion.Id = connection.ExecuteScalar<long>(EntityVersionLogSqls.InsertVersionAndReturnId, entityVersion);
                return entityVersion.Id;
            }
            catch (Exception ex)
            {
                Log.Error("Error in InsertEntityVersion method", ex);
                throw;
            }
        }

        internal bool Update(IDbConnection connection, EntityVersionLogEntry entityVersion)
        {
            if (entityVersion == null ||
                string.IsNullOrWhiteSpace(entityVersion.EntityId) ||
                string.IsNullOrWhiteSpace(entityVersion.EntityType) ||
                string.IsNullOrWhiteSpace(entityVersion.VersionData) ||
                !IsExist(connection, entityVersion.EntityId, entityVersion.EntityType, entityVersion.AfterChangeVersionNumber))
                throw new ArgumentException(nameof(entityVersion));

            try
            {
                var result =
                    connection.Execute(string.Format(EntityVersionLogSqls.UpdateVersionByVersionNumber, entityVersion.EntityId, entityVersion.EntityType, entityVersion.AfterChangeVersionNumber), entityVersion);
                return result == 1;
            }
            catch (Exception ex)
            {
                Log.Error("Error in UpdateEntityVersion method", ex);
                throw;
            }
        }

        internal bool Delete(IDbConnection connection, string entityId, string entityType, long entityVersionNumber)
        {
            if (string.IsNullOrWhiteSpace(entityId))
                throw new ArgumentException(nameof(entityId));
            if (string.IsNullOrWhiteSpace(entityType))
                throw new ArgumentException(nameof(entityType));

            try
            {
                connection.Execute(string.Format(EntityVersionLogSqls.DeleteVersionByVersionNumber, entityId, entityType, entityVersionNumber));
            }
            catch (Exception ex)
            {
                Log.Error("Error in DeleteEntityVersionByVersionNumber method", ex);
                throw;
            }
            return true;
        }

        internal bool Delete(IDbConnection connection, string entityId, string entityType)
        {
            if (string.IsNullOrWhiteSpace(entityId))
                throw new ArgumentException(nameof(entityId));
            if (string.IsNullOrWhiteSpace(entityType))
                throw new ArgumentException(nameof(entityType));

            try
            {
                connection.Execute(string.Format(EntityVersionLogSqls.DeleteAllVersionsForEntity, entityId, entityType));
            }
            catch (Exception ex)
            {
                Log.Error("Error in DeleteEntityVersions method", ex);
                throw;
            }
            return true;
        }

        internal EntityVersionLogEntry Get(IDbConnection connection, string entityId, string entityType, long entityVersionNumber)
        {
            if (string.IsNullOrWhiteSpace(entityId))
                throw new ArgumentException(nameof(entityId));
            if (string.IsNullOrWhiteSpace(entityType))
                throw new ArgumentException(nameof(entityType));

            try
            {
                return connection.QueryFirstOrDefault<EntityVersionLogEntry>(string.Format(EntityVersionLogSqls.SelectVersionByVersionNumber, entityId, entityType, entityVersionNumber));
            }
            catch (Exception ex)
            {
                Log.Error("Error in GetESignaturePointById method", ex);
                throw;
            }
        }

        internal EntityVersionLogEntry Get(IDbConnection connection, long versionId)
        {
            if (versionId < 0)
                throw new ArgumentException(nameof(versionId));

            try
            {
                return connection.QueryFirstOrDefault<EntityVersionLogEntry>(string.Format(EntityVersionLogSqls.SelectVersionByVersionId, versionId));
            }
            catch (Exception ex)
            {
                Log.Error("Error in GetESignaturePointById method", ex);
                throw;
            }
        }

        internal EntityVersionLogEntry[] Get(IDbConnection connection, string entityId, string entityType)
        {
            if (string.IsNullOrWhiteSpace(entityId))
                throw new ArgumentException(nameof(entityId));
            if (string.IsNullOrWhiteSpace(entityType))
                throw new ArgumentException(nameof(entityType));

            try
            {
                return connection.Query<EntityVersionLogEntry>(string.Format(EntityVersionLogSqls.SelectAllVersionsForEntity, entityId, entityType)).ToArray();
            }
            catch (Exception ex)
            {
                Log.Error("Error in GetAllESignaturePoints method", ex);
                throw;
            }
        }

        internal long GetMaxEntityVersionNumber(IDbConnection connection, string entityId, string entityType)
        {
            if (string.IsNullOrWhiteSpace(entityId))
                throw new ArgumentException(nameof(entityId));
            if (string.IsNullOrWhiteSpace(entityType))
                throw new ArgumentException(nameof(entityType));

            try
            {
                return connection.ExecuteScalar<long>(string.Format(EntityVersionLogSqls.SelectMaxVersionNumberForEntity, entityId, entityType));
            }
            catch (Exception ex)
            {
                Log.Error("Error in GetMaxVersionNumberForEntity method", ex);
                throw;
            }
        }
    }
}
