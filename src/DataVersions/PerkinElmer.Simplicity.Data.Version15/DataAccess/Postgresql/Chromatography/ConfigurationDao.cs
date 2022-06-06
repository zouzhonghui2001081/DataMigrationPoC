using System;
using System.Data;
using System.Linq;
using System.Reflection;
using Dapper;
using log4net;
using PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography.LabManagement;

namespace PerkinElmer.Simplicity.Data.Version15.DataAccess.Postgresql.Chromatography
{
    internal static class ConfigurationTableDefinition
    {
        public static string TableName = "MiscParam";

        public static string ColumnKeyName = "KeyName";

        public static string ColumnValue = "Value";
    }

    internal static class SqlConfiguration
    {
        public static Func<string, (string sql, object parameter)> Exist = keyName => (
            $"select 1 from {ConfigurationTableDefinition.TableName} " +
            $"where lower({ConfigurationTableDefinition.ColumnKeyName}) = lower(@KeyName) ", new { KeyName = keyName });


        public static string Insert = 
            $"insert into {ConfigurationTableDefinition.TableName} " +
            $"({ConfigurationTableDefinition.ColumnKeyName} , {ConfigurationTableDefinition.ColumnValue}) " +
            $"values(@{ConfigurationTableDefinition.ColumnKeyName} , @{ConfigurationTableDefinition.ColumnValue}) ";

        public static Func<string, (string sql, object parameter)> Delete = keyName => (
            $"delete from {ConfigurationTableDefinition.TableName} " +
            $"where lower({ConfigurationTableDefinition.ColumnKeyName}) = lower(@KeyName) ", new { KeyName = keyName });

        public static string DeleteAll = 
            $"delete from {ConfigurationTableDefinition.TableName} ";

        public static string Update =
            $"update {ConfigurationTableDefinition.TableName} " +
            $"set {ConfigurationTableDefinition.ColumnValue} = @{ConfigurationTableDefinition.ColumnValue} " +
            $"where lower({ConfigurationTableDefinition.ColumnKeyName}) = lower(@{ConfigurationTableDefinition.ColumnKeyName}) ";

        public static Func<string, (string sql, object parameter)> Select = keyName => (
            $"select {ConfigurationTableDefinition.ColumnKeyName}, {ConfigurationTableDefinition.ColumnValue} " +
            $"from {ConfigurationTableDefinition.TableName} " +
            $"where lower({ConfigurationTableDefinition.ColumnKeyName}) = lower(@KeyName) ", new { KeyName = keyName });

        public static string SelectAll = 
            $"select {ConfigurationTableDefinition.ColumnKeyName}, {ConfigurationTableDefinition.ColumnValue} " +
            $"from {ConfigurationTableDefinition.TableName} ";

    }

    public class ConfigurationDao
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public bool IsExists(IDbConnection connection, string keyName)
        {
            if (string.IsNullOrWhiteSpace(keyName))
                throw new ArgumentException(nameof(keyName));

            try
            {
                var exist = SqlConfiguration.Exist(keyName);
                var result = connection.ExecuteScalar(exist.sql, exist.parameter);
                return result != null && (int) result > 0;
            }
            catch (Exception ex)
            {
                Log.Error("Error in IsExists method", ex);
                throw;
            }
        }

        public bool CreateConfigurationItem(IDbConnection connection, ConfigurationItem configurationItem)
        {
            if (configurationItem == null ||
                string.IsNullOrWhiteSpace(configurationItem.KeyName) ||
                string.IsNullOrWhiteSpace(configurationItem.Value) ||
                IsExists(connection, configurationItem.KeyName) )
                throw new ArgumentException(nameof(configurationItem));

            try
            {
                var result = connection.Execute(SqlConfiguration.Insert, configurationItem);
                return result == 1;
            }
            catch (Exception ex)
            {
                Log.Error("Error in CreateConfigurationItem method", ex);
                throw;
            }
        }

        public bool DeleteConfigurationItem(IDbConnection connection, string keyName)
        {
            if (string.IsNullOrWhiteSpace(keyName))
                throw new ArgumentException(nameof(keyName));
            if (!IsExists(connection, keyName)) return true;

            try
            {
                var delete = SqlConfiguration.Delete(keyName);
                var result = connection.Execute(delete.sql, delete.parameter);
                return result == 1;
            }
            catch (Exception ex)
            {
                Log.Error($"Error in DeleteConfigurationItem method", ex);
                throw;
            }
        }

        public bool UpdateConfigurationItem(IDbConnection connection, ConfigurationItem configurationItem)
        {
            if (configurationItem == null ||
                string.IsNullOrWhiteSpace(configurationItem.KeyName) ||
                string.IsNullOrWhiteSpace(configurationItem.Value) ||
                !IsExists(connection, configurationItem.KeyName))
                throw new ArgumentException(nameof(configurationItem));

            try
            {
                var result = connection.Execute(SqlConfiguration.Update, configurationItem);
                return result == 1;
            }
            catch (Exception ex)
            {
                Log.Error("Error in UpdateConfigurationItem method", ex);
                throw;
            }
        }

        public ConfigurationItem GetConfigurationItem(IDbConnection connection, string keyName)
        {
            try
            {
                var select = SqlConfiguration.Select(keyName);
                return connection.QueryFirstOrDefault<ConfigurationItem>(select.sql, select.parameter);
            }
            catch (Exception ex)
            {
                Log.Error("Error in GetConfigurationItem method", ex);
                throw;
            }
        }

        public ConfigurationItem[] GetAllConfigurationItem(IDbConnection connection)
        {
            try
            {
                return connection.Query<ConfigurationItem>(SqlConfiguration.SelectAll).ToArray();
            }
            catch (Exception ex)
            {
                Log.Error("Error in GetConfigurationItem method", ex);
                throw;
            }
        }
    }
}
