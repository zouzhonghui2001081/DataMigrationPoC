using System;
using System.Data;
using System.Reflection;
using Dapper;
using log4net;
using PerkinElmer.Simplicity.Data.Version16.DataEntities.AuditTrail;

namespace PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.AuditTrail
{
    internal class AuditTrailConfigurationTableDefinition
    {
        internal static string TableName { get; } = "AuditTrailConfiguration";

        internal static string KeyColumn { get; } = "Key";

        internal static string ValueColumn { get; } = "Value";
    }

    internal static class SqlAuditTrailConfiguration
    {
        internal static string InsertAuditTrailConfiguration =
            $"INSERT INTO {AuditTrailConfigurationTableDefinition.TableName} " +
            "(" +
            $"{AuditTrailConfigurationTableDefinition.KeyColumn}," +
            $"{AuditTrailConfigurationTableDefinition.ValueColumn}" +
            ") " +
            "VALUES(" +
            $"@{AuditTrailConfigurationTableDefinition.KeyColumn}," +
            $"@{AuditTrailConfigurationTableDefinition.ValueColumn}" +
            ")";

        internal static string SelectAuditTrailConfigurationFromTable =
            "SELECT " +
            $"{AuditTrailConfigurationTableDefinition.KeyColumn}," +
            $"{AuditTrailConfigurationTableDefinition.ValueColumn} " +
            $"FROM {AuditTrailConfigurationTableDefinition.TableName} ";

        internal static string SelectAuditTrailConfigurationFromTableByKey =
            SelectAuditTrailConfigurationFromTable +
            $"where lower({ AuditTrailConfigurationTableDefinition.KeyColumn}) = lower('{{0}}') ";

        internal static string ExistAuditTrailConfiguration =
            "SELECT 1 " +
            $"FROM {AuditTrailConfigurationTableDefinition.TableName} " +
            $"where lower({ AuditTrailConfigurationTableDefinition.KeyColumn}) = lower('{{0}}') ";

        internal static string UpdateAuditTrailConfiguration =
            $"UPDATE {AuditTrailConfigurationTableDefinition.TableName} " +
            $"SET {AuditTrailConfigurationTableDefinition.ValueColumn} = '{{0}}' " +
            $"WHERE {AuditTrailConfigurationTableDefinition.KeyColumn} = '{{1}}' ";
    }


    internal class AuditTrailConfigurationDao
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        internal void Create(IDbConnection connection, AuditTrailConfiguration auditTrailConfiguration)
        {
            Log.Info("Create() called");

            try
            {
                connection.Execute(SqlAuditTrailConfiguration.InsertAuditTrailConfiguration, auditTrailConfiguration);
            }
            catch (Exception ex)
            {
                Log.Error("Error in Create()", ex);
                throw;
            }
        }

        internal AuditTrailConfiguration Get(IDbConnection connection, string key)
        {
            Log.Error("Get() called");
            try
            {
                return connection.QueryFirstOrDefault<AuditTrailConfiguration>(
                    string.Format(SqlAuditTrailConfiguration.SelectAuditTrailConfigurationFromTableByKey, key));
            }
            catch (Exception ex)
            {
                Log.Error("Error in Get()", ex);
                throw;
            }
        }

        internal bool IsExist(IDbConnection connection, string key)
        {
            Log.Debug("IsExist() called");
            try
            {
                return connection.ExecuteScalar<bool>(
                    string.Format(SqlAuditTrailConfiguration.ExistAuditTrailConfiguration, key));
            }
            catch (Exception ex)
            {
                Log.Error("Error in IsExist()", ex);
                throw;
            }
        }

        internal void Update(IDbConnection connection, string key, string value)
        {
            Log.Debug("Update() called");
            try
            {
                connection.Execute(string.Format(SqlAuditTrailConfiguration.UpdateAuditTrailConfiguration, value, key));
            }
            catch (Exception ex)
            {
                Log.Error("Error in Update()", ex);
                throw;
            }
        }
    }
}
