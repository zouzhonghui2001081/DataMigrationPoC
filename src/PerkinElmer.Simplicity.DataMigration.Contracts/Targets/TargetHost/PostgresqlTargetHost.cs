using System;
using System.Configuration;
using System.Data;
using System.Reflection;
using Dapper;
using log4net;
using Npgsql;
using PerkinElmer.Simplicity.DataMigration.Contracts.Common;
using PerkinElmer.Simplicity.DataMigration.Contracts.Targets.TargetContext;

namespace PerkinElmer.Simplicity.DataMigration.Contracts.Targets.TargetHost
{ 
    public abstract class PostgresqlTargetHost : TargetHostBase
    {
        protected NpgsqlConnectionStringBuilder DefaultDbConnectionStrBuilder;

        protected readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        protected PostgresqlTargetHost()
        {
            var connectionStringName = ConfigurationManager.AppSettings[ConstNames.PostgresqlDefaultDb];
            var defaultConnectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
            DefaultDbConnectionStrBuilder = new NpgsqlConnectionStringBuilder(defaultConnectionString) { Pooling = false };
        }

        public override TargetTypes TargetType => TargetTypes.Posgresql;

        protected abstract Version AuditTrailSchemaVersion { get; }

        protected abstract string AuditTrailDbSchema { get; }

        protected abstract Version SecuritySchemaVersion { get; }

        protected abstract string SecurityDbSchema { get; }

        protected abstract string SecurityData { get; }

        protected abstract Version ChromatographySchemaVersion { get; }

        protected abstract int ChromatographyMajorDataVersion { get; }

        protected abstract int ChromatographyMinorDataVersion { get; }

        protected abstract string ChromatographyDbSchema { get; }

        protected abstract string ChromatographyNotificationFunctionTriggers { get; }

        protected abstract string ChromatographyDummyData { get; }

        public override void PrepareTargetHost(TargetContextBase targetContext)
        {
            if (!(targetContext is PostgresqlTargetContext postgresqlTargetContext))
                throw new ArgumentException(nameof(targetContext));

            ResetChromatographyDatabase(new NpgsqlConnectionStringBuilder(postgresqlTargetContext.ChromatographyConnection));
            ResetAuditTrailDatabase(new NpgsqlConnectionStringBuilder(postgresqlTargetContext.AuditTrailConnection));
            ResetSecurityDatabase(new NpgsqlConnectionStringBuilder(postgresqlTargetContext.SecurityConnection));
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

        protected abstract string GetSqlScript(string resourceName);

        protected void ResetChromatographyDatabase(NpgsqlConnectionStringBuilder chromatographyConnBuilder)
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

        protected void ResetAuditTrailDatabase(NpgsqlConnectionStringBuilder auditTrailConnBuilder)
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

        protected void ResetSecurityDatabase(NpgsqlConnectionStringBuilder securityConnBuilder)
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

        private void CreateChromatographyDatabase(NpgsqlConnectionStringBuilder chromatographyConnBuilder)
        {
            InitializeDatabase(chromatographyConnBuilder.Database);
            using (var dbConnection = new NpgsqlConnection(chromatographyConnBuilder.ConnectionString))
            {
                dbConnection.Open();
                // Create schema
                dbConnection.Execute(GetSqlScript(ChromatographyDbSchema));
                InsertSchemaVersion(dbConnection, ConstNames.SchemaTableName, ChromatographySchemaVersion);
                InsertDataVersion(dbConnection, ConstNames.SchemaTableName, ChromatographyMajorDataVersion, ChromatographyMinorDataVersion);
                // Insert dummy data
                dbConnection.Execute(GetSqlScript(ChromatographyDummyData));
                dbConnection.Execute(GetSqlScript(ChromatographyNotificationFunctionTriggers));
                dbConnection.Close();
            }
            Log.Info($"Created database {chromatographyConnBuilder.Database}");
        }

        private void CreateAuditTrailDatabase(NpgsqlConnectionStringBuilder auditTrailConnBuilder)
        {
            // Create database
            InitializeDatabase(auditTrailConnBuilder.Database);

            using (var dbConnection = new NpgsqlConnection(auditTrailConnBuilder.ConnectionString))
            {
                dbConnection.Open();
                // Create schema
                dbConnection.Execute(GetSqlScript(AuditTrailDbSchema));
                InsertSchemaVersion(dbConnection, ConstNames.SchemaTableName, AuditTrailSchemaVersion);
                dbConnection.Close();
            }
            Log.Info($"Created database {auditTrailConnBuilder.Database}");
        }

        private void CreateSecurityDatabase(NpgsqlConnectionStringBuilder securityConnBuilder)
        {
            // Create database
            InitializeDatabase(securityConnBuilder.Database);

            using (IDbConnection dbConnection = new NpgsqlConnection(securityConnBuilder.ConnectionString))
            {
                dbConnection.Open();
                // Create schema
                dbConnection.Execute(GetSqlScript(SecurityDbSchema));
                InsertSchemaVersion(dbConnection, ConstNames.SchemaTableName, SecuritySchemaVersion);
                // Insert data
                dbConnection.Execute(GetSqlScript(SecurityData));
                dbConnection.Close();
            }
            Log.Info($"Created database {securityConnBuilder.Database}");
        }

        private void InsertSchemaVersion(IDbConnection dbConnection, string tableName, Version schemaVersion)
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

        private void InsertDataVersion(IDbConnection dbConnection, string schemaTableName, int majorVersion, int minorVersion)
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
    }
}
