using System;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text.Json;
using Dapper;
using log4net;
using Npgsql;
using ConnectionStrings = PerkinElmer.Simplicity.Data.Version15.DataAccess.Postgresql.ConnectionStrings;

namespace PerkinElmer.Simplicity.Data.Version15.Version
{
    internal class Version15Host
    {
        private const string AuditTrailDbSchema = "PerkinElmer.Simplicity.Data.Version15.DataAccess.Postgresql.SQL.AuditTrailDBSchema.sql";

        private const string SecurityDbSchema = "PerkinElmer.Simplicity.Data.Version15.DataAccess.Postgresql.SQL.SecurityDbSchema.sql";

        private const string SecurityData = "PerkinElmer.Simplicity.Data.Version15.DataAccess.Postgresql.SQL.SecurityDbData.sql";

        private const string ChromatographyDbSchema = "PerkinElmer.Simplicity.Data.Version15.DataAccess.Postgresql.SQL.ChromatographyDBSchema.sql";

        private const string ChromatographyNotificationFunctionTriggers = "PerkinElmer.Simplicity.Data.Version15.DataAccess.Postgresql.SQL.Version15.NotificationFunctionTriggers.sql";

        private const string ChromatographyDummyData = "PerkinElmer.Simplicity.Data.Version15.DataAccess.Postgresql.SQL.DummyRuns.sql";

        private const string ConnectionStringResourceName = "PerkinElmer.Simplicity.Data.Version15.DataAccess.Postgresql.ConnectionStrings.json";

        public const string SchemaTableName = "SchemaVersion";

        public static readonly System.Version AuditTrailSchemaVersion = new System.Version(0, 5);

        public static readonly System.Version SecuritySchemaVersion = new System.Version(1, 8);

        public static readonly System.Version ChromatographySchemaVersion = new System.Version(1, 7);

        public const int ChromatographyMajorDataVersion = -1;

        public const int ChromatographyMinorDataVersion = 29;

        protected static ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #region Postgresql

        private static ConnectionStrings _connectionStrings;
        private static ConnectionStrings ConnectionStrings => _connectionStrings ?? (_connectionStrings = GetConnectionStrings());

        private static NpgsqlConnectionStringBuilder _systemDbConnectionStringBuilder;
        private static NpgsqlConnectionStringBuilder SystemDbConnectionStrBuilder =>
            _systemDbConnectionStringBuilder ?? (_systemDbConnectionStringBuilder =
                new NpgsqlConnectionStringBuilder(ConnectionStrings.System) {Pooling = false});

        internal static string ChromatographyConnection => ConnectionStrings.Chromatography;

        internal static string AuditTrailConnection => ConnectionStrings.AuditTrail;

        internal static string SecurityConnection => ConnectionStrings.Security;

        public static void PreparePostgresqlHost()
        {
            ResetChromatographyDatabase(new NpgsqlConnectionStringBuilder(ChromatographyConnection));
            ResetAuditTrailDatabase(new NpgsqlConnectionStringBuilder(AuditTrailConnection));
            ResetSecurityDatabase(new NpgsqlConnectionStringBuilder(SecurityConnection));
        }

        private static void ResetChromatographyDatabase(NpgsqlConnectionStringBuilder chromatographyConnBuilder)
        {
            try
            {
                DropDatabase(chromatographyConnBuilder.Database);
                CreateChromatographyDatabase(chromatographyConnBuilder);
            }
            catch (Exception ex)
            {
                Log.Error("Error in ResetChromatographyDatabase", ex);
            }
        }

        private static void ResetAuditTrailDatabase(NpgsqlConnectionStringBuilder auditTrailConnBuilder)
        {
            try
            {
                DropDatabase(auditTrailConnBuilder.Database);
                CreateAuditTrailDatabase(auditTrailConnBuilder);
            }
            catch (Exception ex)
            {
                Log.Error("Error in ResetChromatographyDatabase", ex);
            }
        }

        private static void ResetSecurityDatabase(NpgsqlConnectionStringBuilder securityConnBuilder)
        {
            try
            {
                DropDatabase(securityConnBuilder.Database);
                CreateSecurityDatabase(securityConnBuilder);
            }
            catch (Exception ex)
            {
                Log.Error("Error in ResetChromatographyDatabase", ex);
            }
        }

        private static void CreateChromatographyDatabase(NpgsqlConnectionStringBuilder chromatographyConnBuilder)
        {
            InitializeDatabase(chromatographyConnBuilder.Database);
            using (var dbConnection = new NpgsqlConnection(chromatographyConnBuilder.ConnectionString))
            {
                dbConnection.Open();
                // Create schema
                dbConnection.Execute(GetSqlScript(ChromatographyDbSchema));
                InsertSchemaVersion(dbConnection, SchemaTableName, ChromatographySchemaVersion);
                InsertDataVersion(dbConnection, SchemaTableName, ChromatographyMajorDataVersion, ChromatographyMinorDataVersion);
                // Insert dummy data
                dbConnection.Execute(GetSqlScript(ChromatographyDummyData));
                dbConnection.Execute(GetSqlScript(ChromatographyNotificationFunctionTriggers));
                dbConnection.Close();
            }
            Log.Info($"Created database {chromatographyConnBuilder.Database}");
        }

        private static void CreateAuditTrailDatabase(NpgsqlConnectionStringBuilder auditTrailConnBuilder)
        {
            // Create database
            InitializeDatabase(auditTrailConnBuilder.Database);

            using (var dbConnection = new NpgsqlConnection(auditTrailConnBuilder.ConnectionString))
            {
                dbConnection.Open();
                // Create schema
                dbConnection.Execute(GetSqlScript(AuditTrailDbSchema));
                InsertSchemaVersion(dbConnection, SchemaTableName, AuditTrailSchemaVersion);
                dbConnection.Close();
            }
            Log.Info($"Created database {auditTrailConnBuilder.Database}");
        }

        private static void CreateSecurityDatabase(NpgsqlConnectionStringBuilder securityConnBuilder)
        {
            // Create database
            InitializeDatabase(securityConnBuilder.Database);

            using (IDbConnection dbConnection = new NpgsqlConnection(securityConnBuilder.ConnectionString))
            {
                dbConnection.Open();
                // Create schema
                dbConnection.Execute(GetSqlScript(SecurityDbSchema));
                InsertSchemaVersion(dbConnection, SchemaTableName, SecuritySchemaVersion);
                // Insert data
                dbConnection.Execute(GetSqlScript(SecurityData));
                dbConnection.Close();
            }
            Log.Info($"Created database {securityConnBuilder.Database}");
        }

        private static void InsertSchemaVersion(IDbConnection dbConnection, string tableName, System.Version schemaVersion)
        {
            Log.Info("InsertCurrentSchemaVersion() called");

            try
            {
                dbConnection.Execute(
                    $"INSERT INTO {tableName}(MajorVersion, MinorVersion) Values({schemaVersion.Major}, {schemaVersion.Minor});");
            }
            catch (Exception ex)
            {
                Log.Error($"Error in schema version update", ex);
                throw;
            }
        }

        private static void InsertDataVersion(IDbConnection dbConnection, string schemaTableName, int majorVersion, int minorVersion)
        {
            Log.Info("InsertCurrentDataVersion() called");

            try
            {
                dbConnection.Execute($"INSERT INTO {schemaTableName}(MajorVersion, MinorVersion) Values({majorVersion}, {minorVersion});");
            }
            catch (Exception ex)
            {
                Log.Error($"Error in InsertCurrentDataVersion()", ex);
                throw;
            }
        }

        private static void InitializeDatabase(string databaseName)
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

                using (var defaultConnection = new NpgsqlConnection(SystemDbConnectionStrBuilder.ConnectionString))
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

        private static void DropDatabase(string databaseName)
        {
            try
            {
                using (var defaultConnection =
                    new NpgsqlConnection(SystemDbConnectionStrBuilder.ConnectionString))
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

        private static ConnectionStrings GetConnectionStrings()
        {
            var assembly = typeof(Version15Host).Assembly;

            using (var stream = assembly.GetManifestResourceStream(ConnectionStringResourceName))
            {
                using (var reader = new StreamReader(stream ?? throw new InvalidOperationException(
                                                         $"Failed to load resource {ConnectionStringResourceName}")))
                {
                    var strings = reader.ReadToEnd();
                    return JsonSerializer.Deserialize<ConnectionStrings>(strings);
                }
            }
        }

        private static string GetSqlScript(string resourceName)
        {
            Log.Info("GetSqlScript() called");

            var assembly = typeof(Version15Host).Assembly;

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (var reader = new StreamReader(stream ?? throw new InvalidOperationException(
                                                         $"Failed to load resource {resourceName}")))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        #endregion
    }



}
