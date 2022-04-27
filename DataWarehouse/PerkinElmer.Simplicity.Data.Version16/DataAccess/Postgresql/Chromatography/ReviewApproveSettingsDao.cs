using System;
using System.Data;
using System.Reflection;
using Dapper;
using log4net;
using PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography.LabManagement;

namespace PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.Chromatography
{
	public class ReviewApproveSettingsDao
	{
		private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		public bool Save(IDbConnection connection, ConfigurationItem configurationItem)
		{
			if (configurationItem == null ||
				string.IsNullOrWhiteSpace(configurationItem.KeyName) ||
				string.IsNullOrWhiteSpace(configurationItem.Value))
			{
				throw new ArgumentException(nameof(configurationItem));
			}

			try
			{
				var result = connection.Execute(ReviewApproveSettingsSqls.Merge, configurationItem);
				return result == 1;
			}
			catch (Exception ex)
			{
				Log.Error("Error in Save Review Approve Settings method", ex);
				throw;
			}
		}

		public ConfigurationItem Get(IDbConnection connection, string keyName)
		{
			try
			{
				var select = ReviewApproveSettingsSqls.Select(keyName);
				return connection.QueryFirstOrDefault<ConfigurationItem>(select.sql, select.parameter);
			}
			catch (Exception ex)
			{
				Log.Error("Error in Get Review Approve Settings method", ex);
				throw;
			}
		}
	}

	internal static class ReviewApproveSettingsDaoTableDefinition
	{
		public static string TableName = "ReviewApproveSettings";

		public static string KeyNameColumn = "KeyName";

		public static string ValueColumn = "Value";
	}

	internal static class ReviewApproveSettingsSqls
	{
		public static string Merge =
			$"INSERT INTO {ReviewApproveSettingsDaoTableDefinition.TableName} " +
			$"({ReviewApproveSettingsDaoTableDefinition.KeyNameColumn} , {ReviewApproveSettingsDaoTableDefinition.ValueColumn}) " +
			$"VALUES (@{ReviewApproveSettingsDaoTableDefinition.KeyNameColumn} , @{ReviewApproveSettingsDaoTableDefinition.ValueColumn}) " +
			$"ON CONFLICT ({ReviewApproveSettingsDaoTableDefinition.KeyNameColumn}) DO UPDATE SET {ReviewApproveSettingsDaoTableDefinition.ValueColumn} = @{ReviewApproveSettingsDaoTableDefinition.ValueColumn} ";

		public static Func<string, (string sql, object parameter)> Select = (input) => (
			 $"select {ReviewApproveSettingsDaoTableDefinition.KeyNameColumn}, {ReviewApproveSettingsDaoTableDefinition.ValueColumn} " +
			$"from {ReviewApproveSettingsDaoTableDefinition.TableName} " +
			$"where lower({ReviewApproveSettingsDaoTableDefinition.KeyNameColumn}) = lower(@KeyName) ", new { KeyName = input });

	}
}
