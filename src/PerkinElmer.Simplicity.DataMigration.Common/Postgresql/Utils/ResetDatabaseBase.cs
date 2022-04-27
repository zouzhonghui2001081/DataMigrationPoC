using System;
using System.Configuration;
using System.IO;
using System.Net.Sockets;
using System.Reflection;
using Dapper;
using log4net;
using Npgsql;

namespace PerkinElmer.Simplicity.DataMigration.Common.Postgresql.Utils
{
    public class ResetDatabaseBase
    {
        private const int SocketErrorCode = 10061;

        protected NpgsqlConnectionStringBuilder DefaultDbConnectionStrBuilder;

        protected readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public ResetDatabaseBase()
        {
            var connectionStringName = ConfigurationManager.AppSettings["ConnectionStringDefaultDb"];
            var defaultConnectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
            DefaultDbConnectionStrBuilder = new NpgsqlConnectionStringBuilder(defaultConnectionString) { Pooling = false };
        }

        protected (bool, ErrorCode) IsDatabaseExist(string databaseName)
        {
            Log.Info("DatabaseExist() called");
            try
            {
                bool applicationDbExists;
                using (var dbConnection = new NpgsqlConnection(DefaultDbConnectionStrBuilder.ConnectionString))
                {
                    dbConnection.Open();
                    applicationDbExists = dbConnection.ExecuteScalar<bool>(
                        $"SELECT EXISTS(SELECT 1 FROM pg_catalog.pg_database where datname = '{databaseName}');");
                    dbConnection.Close();
                }

                return applicationDbExists ? (true, ErrorCode.NoError) : (false, ErrorCode.ChromatographyDbDoesNotExists);
            }
            catch (SocketException ex)
            {
                Log.Error("Could not establish connection to database in DatabaseExist() method", ex);
                if (ex.ErrorCode.Equals(SocketErrorCode))
                {
                    return (false, ErrorCode.NpgSqlConnectionFailure);
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error occurred DatabaseExist()", ex);
                return (false, ErrorCode.NpgSqlConnectionFailure);
            }
            return (true, ErrorCode.NoError);
        }

        protected string GetSqlScript(ReleaseVersions releaseVersion, string resourceName)
        {
            Log.Info("GetSqlScript() called");

            var assembly = typeof(ResetDatabaseBase).Assembly;

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (var reader = new StreamReader(stream ?? throw new InvalidOperationException(
                                                         $"Failed to load resource {resourceName}")))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        protected void InitializeDatabase(string databaseName)
        {
            try
            {
                string createDatabase =
                $"CREATE DATABASE  \"{databaseName}\" " +
                "WITH OWNER = postgres " +
                "ENCODING = 'UTF8' " +
                "TABLESPACE = pg_default " +
                "LC_COLLATE = 'English_United States.1252' " +
                "LC_CTYPE = 'English_United States.1252' " +
                "CONNECTION LIMIT = -1";

                using (var defaultConnection = new NpgsqlConnection(DefaultDbConnectionStrBuilder.ConnectionString))
                {
                    defaultConnection.Open();
                    defaultConnection.Execute(createDatabase);
                    defaultConnection.Close();
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error occurred during InitializeDatabase", ex);
                throw;
            }
        }

        protected void DropDatabase(string databaseName)
        {
            try
            {
                using (var defaultConnection =
                    new NpgsqlConnection(DefaultDbConnectionStrBuilder.ConnectionString))
                {
                    defaultConnection.Open();
                    // Below query will delete any backend database connections from connection pool
                    defaultConnection.Execute(
                        $"SELECT pg_terminate_backend(pg_stat_activity.pid) " +
                        $"FROM pg_stat_activity " +
                        $"WHERE pg_stat_activity.datname = '{databaseName}';");

                    defaultConnection.Execute($"DROP DATABASE IF EXISTS \"{databaseName}\";");
                    defaultConnection.Close();
                    Log.Info($"Dropped database {databaseName}");
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error occurred during ResetDatabase", ex);
                throw;
            }
        }

        protected bool DatabaseSchemaExists(string connection, string schemaVersionTable)
        {
            bool databaseSchemaExists = false;
            try
            {
                using (var dbConnection = new NpgsqlConnection(connection))
                {
                    dbConnection.Open();
                    databaseSchemaExists = dbConnection.ExecuteScalar<bool>(
                        $"SELECT EXISTS(SELECT 1 FROM information_schema.tables " +
                        $"WHERE table_schema = 'public' AND table_name = lower('{schemaVersionTable}'));");
                    dbConnection.Close();

                }

                return databaseSchemaExists;
            }
            catch (Exception ex)
            {
                Log.Error("Error in DatabaseSchemaExists method", ex);
                return databaseSchemaExists;
            }
        }
    }
}
