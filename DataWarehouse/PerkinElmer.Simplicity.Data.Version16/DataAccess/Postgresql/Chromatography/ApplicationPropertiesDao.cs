using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Globalization;
using System.IO;
using System.Reflection;
using Dapper;
using log4net;

namespace PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.Chromatography
{
    internal static class ApplicationPropertiesTableDefinition
    {
        public static string TableName = "ApplicationProperties";

        public static string ColumnKeyName = "KeyName";

        public static string ColumnValue = "Value";
    }

    internal static class ApplicationPropertiesSql
    {
        public static string Save = $"INSERT INTO {ApplicationPropertiesTableDefinition.TableName} ({ApplicationPropertiesTableDefinition.ColumnKeyName}, {ApplicationPropertiesTableDefinition.ColumnValue}) VALUES " +
            $"(@{ApplicationPropertiesTableDefinition.ColumnKeyName} , @{ApplicationPropertiesTableDefinition.ColumnValue}) " +
            $"ON CONFLICT ({ApplicationPropertiesTableDefinition.ColumnKeyName}) DO UPDATE SET {ApplicationPropertiesTableDefinition.ColumnValue} = @{ApplicationPropertiesTableDefinition.ColumnValue}";

        public static Func<string, (string sql, object parameter)> Delete = keyName => (
			$"delete from {ApplicationPropertiesTableDefinition.TableName} " +
            $"where lower({ApplicationPropertiesTableDefinition.ColumnKeyName}) = lower(@KeyName) ", new { KeyName = keyName });

		public static Func<string, (string sql, object parameter)> Select = keyName => (
			$"select {ApplicationPropertiesTableDefinition.ColumnValue} " +
            $"from {ApplicationPropertiesTableDefinition.TableName} " +
            $"where lower({ApplicationPropertiesTableDefinition.ColumnKeyName}) = lower(@KeyName) ", new { KeyName = keyName });
	}

    public class ApplicationPropertiesDao
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public bool Save(IDbConnection connection, string keyName, string value)
        {
            if (string.IsNullOrWhiteSpace(keyName))
                throw new ArgumentException(nameof(keyName));

            try
            {
                var result = connection.Execute(ApplicationPropertiesSql.Save, new { keyname=keyName, value});
                return result == 1;
            }
            catch (Exception ex)
            {
                Log.Error("Error in Save method", ex);
                throw;
            }
        }

        public bool Delete(IDbConnection connection, string keyName)
        {
            if (string.IsNullOrWhiteSpace(keyName))
                throw new ArgumentException(nameof(keyName));

            try
            {
				var delete = ApplicationPropertiesSql.Delete(keyName);
				var result = connection.Execute(delete.sql, delete.parameter);
                return result == 1;
            }
            catch (Exception ex)
            {
                Log.Error($"Error in Delete method", ex);
                throw;
            }
        }

        public string Get(IDbConnection connection, string keyName)
        {
            try
            {
				var select = ApplicationPropertiesSql.Select(keyName);
				return connection.QueryFirstOrDefault<string>(select.sql, select.parameter);
            }
            catch (Exception ex)
            {
                Log.Error("Error in Get method", ex);
                throw;
            }
        }
    }

	internal class ApplicationPropertiesConnection : IDisposable
	{
		private static string SystemConfigurationFolder => Path.Combine(
			Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
			"PerkinElmer\\SimplicityChrom");
		private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		private const string Password = "710puswefraWe2TuxekemEKu";
		private const string Schema =
			@"CREATE TABLE properties(
			key VARCHAR(128) NOT NULL,
			value VARCHAR(128) NOT NULL,
			UNIQUE(key));";
		private bool _disposed;
		internal static readonly string CurrentSchemaVersion = "1.0";
		private const string VersionKey = "version";

		public ApplicationPropertiesConnection()
		{
			string database = Path.Combine(SystemConfigurationFolder, $"SimplicityChrom.crash");
			Connection = OpenDatabaseConnection(database);
		}

		public SQLiteConnection Connection { get; }
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		private SQLiteConnection OpenDatabaseConnection(string dataFile)
		{
			var directoryName = Path.GetDirectoryName(dataFile);

			if (Directory.Exists(directoryName) == false)
			{
				Directory.CreateDirectory(directoryName ?? throw new ArgumentException(nameof(dataFile)));
			}

			bool createSchema = File.Exists(dataFile) == false;

			if (createSchema == false)
			{
				UpdateSchemaIfChanged(dataFile);
			}

			string databaseConnectionString = GetDatabaseConnectionString(dataFile);

			var connection = new SQLiteConnection(databaseConnectionString);
			try
			{
				connection.Open();

				if (!IntegrityValid(connection))
					throw new Exception("Data file is corrupted!");

				ExecuteNonQuery(connection, "PRAGMA foreign_keys=ON");

				if (createSchema)
					CreateSchema(connection);

				return connection;
			}
			catch (SQLiteException exception)
			{
				Log.Error("Error in OpenDatabaseConnection()", exception);
				throw new Exception("Data file can't be opened!", exception);
			}
		}

		private void UpdateSchemaIfChanged(string dataFile)
		{
			string databaseConnectionString = GetDatabaseConnectionString(dataFile);

			using (var connection = new SQLiteConnection(databaseConnectionString))
			{
				connection.Open();

				if (IsSchemaVersionChanged(connection))
				{
					// Drop database for now - Eventually will support updating of database.
					if (connection.State == ConnectionState.Open)
					{
						connection.Close();
					}

					if (File.Exists(dataFile))
					{
						File.Delete(dataFile);
					}

					connection.Open();
					CreateSchema(connection);
				}
			}
		}
		private bool IsSchemaVersionChanged(SQLiteConnection connection)
		{
			bool isSchemaVersionChanged = false;

			using (var command = new SQLiteCommand(connection))
			{
				command.CommandText = "SELECT value " +
									  "FROM properties " +
									  "WHERE key = @key";
				command.Parameters.Add("@key", DbType.String).Value = VersionKey;

				using (var reader = command.ExecuteReader())
				{
					if (reader.HasRows && reader.Read())
					{
						var version = reader.GetString(reader.GetOrdinal("value"));
						isSchemaVersionChanged = version != CurrentSchemaVersion;
					}

					reader.Close();
				}
			}

			return isSchemaVersionChanged;
		}
		private static string GetDatabaseConnectionString(string fullFilePath)
		{
			return string.Format(CultureInfo.InvariantCulture, $"Data Source={fullFilePath};Version=3;Password='{Password}'");
		}

		private bool IntegrityValid(SQLiteConnection connection)
		{
			using (var command = new SQLiteCommand("PRAGMA integrity_check", connection))
			{
				using (SQLiteDataReader reader = command.ExecuteReader())
				{
					var response = new List<string>();
					while (reader.Read())
						response.Add(reader.GetString(0));

					if (response.Count == 1)
						return response[0] == "ok";

					return false;
				}
			}
		}
		private void CreateSchema(SQLiteConnection connection)
		{
			ExecuteNonQuery(connection, Schema);

			using (var command = new SQLiteCommand(connection))
			{
				command.CommandText =
					"INSERT INTO properties (key,value) VALUES (@key,@value)";
				command.Parameters.Add("@key", DbType.String).Value = VersionKey;
				command.Parameters.Add("@value", DbType.String).Value = CurrentSchemaVersion;
				command.ExecuteNonQuery();
			}
		}

		private void ExecuteNonQuery(SQLiteConnection connection, string text)
		{
			using (SQLiteCommand command = connection.CreateCommand())
			{
				command.CommandText = text;
				command.ExecuteNonQuery();
			}
		}
		protected virtual void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
					Connection.Dispose();

				_disposed = true;
			}
		}
	}
}
