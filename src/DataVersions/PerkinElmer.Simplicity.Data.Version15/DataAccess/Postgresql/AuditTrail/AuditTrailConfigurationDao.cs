using System;
using System.Data;
using System.Reflection;
using Dapper;
using log4net;
using PerkinElmer.Simplicity.Data.Version15.DataEntities.AuditTrail;

namespace PerkinElmer.Simplicity.Data.Version15.DataAccess.Postgresql.AuditTrail
{
    internal class AuditTrailConfigurationDao
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        internal static string TableName { get; } = "AuditTrailConfiguration";
        internal static string KeyColumn { get; } = "Key";
        internal static string ValueColumn { get; } = "Value";

        internal void Create(IDbConnection connection, AuditTrailConfiguration auditTrailConfiguration)
        {
            Log.Info("Create() called");

            try
            {
                connection.Execute(
                    $"INSERT INTO {TableName} " +
                    $"({KeyColumn}," +
                    $"{ValueColumn}) " +
                    $"VALUES(@{KeyColumn}," +
                    $"@{ValueColumn})", auditTrailConfiguration);
            }
            catch (Exception ex)
            {
                Log.Error($"Error in Create()", ex);
                throw;
            }
        }

        internal AuditTrailConfiguration Get(IDbConnection connection, string key)
        {
            Log.Error($"GetActionDescription() called");
            try
            {
                return connection.QueryFirstOrDefault<AuditTrailConfiguration>(
                    $"SELECT {KeyColumn}," +
                    $"{ValueColumn} " +
                    $"FROM {TableName} " +
                    $"WHERE {KeyColumn} = '{key}'");
            }
            catch (Exception ex)
            {
                Log.Error($"Error in Get()", ex);
                throw;
            }
        }

        internal bool IsExist(IDbConnection connection, string key)
        {
            Log.Debug($"IsExist() called");
            try
            {
                return connection.ExecuteScalar<bool>(
                    $"SELECT 1 " +
                    $"FROM {TableName} " +
                    $"WHERE {KeyColumn} = '{key}'");
            }
            catch (Exception ex)
            {
                Log.Error($"Error in IsExist()", ex);
                throw;
            }
        }

        internal void Update(IDbConnection connection, string key, string value)
        {
            Log.Debug($"Update() called");
            try
            {
                connection.Execute($"UPDATE {TableName} " +
                                          $"SET {ValueColumn} = '{value}' " +
                                          $"WHERE {KeyColumn} = '{key}'");
            }
            catch (Exception ex)
            {
                Log.Error($"Error in Update()", ex);
                throw;
            }
        }
    }
}
