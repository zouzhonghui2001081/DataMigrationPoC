using System;
using System.Configuration;
using System.Data;
using System.Reflection;
using Dapper;
using log4net;
using Npgsql;
using PerkinElmer.Simplicity.DataMigration.Common.Postgresql.Utils.Resources;

namespace PerkinElmer.Simplicity.DataMigration.Common.Postgresql.Utils
{
    public class DatabaseUtil
    {
        private static readonly NpgsqlConnectionStringBuilder AppDbConnectionStrBuilder;
        private static readonly NpgsqlConnectionStringBuilder DefaultDbConnectionStrBuilder;

        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        static DatabaseUtil()
        {
            // Connection string for CDS application database
            var connectionStringName = ConfigurationManager.AppSettings["ChromatographyConn"];
            var connectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
            AppDbConnectionStrBuilder = new NpgsqlConnectionStringBuilder(connectionString) { Pooling = false };

            // Connection string to default Postgres database 'postgres'
            connectionStringName = ConfigurationManager.AppSettings["ConnectionStringDefaultDb"];
            connectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
            DefaultDbConnectionStrBuilder = new NpgsqlConnectionStringBuilder(connectionString) { Pooling = false };
        }

        public static Version GetChromatographyDatabaseVersion()
        {
            try
            {
                using (var connection = new NpgsqlConnection(DefaultDbConnectionStrBuilder.ConnectionString))
                {
                    connection.Open();

                    var chromatographyDatabaseExists = connection.ExecuteScalar<bool>(
                        $"SELECT EXISTS(SELECT 1 FROM pg_catalog.pg_database where datname = '{AppDbConnectionStrBuilder.Database}');");
                    connection.Close();

                    if (chromatographyDatabaseExists == false)
                        throw new ArgumentException("Chromatogram database not exist!");

                }

                using (IDbConnection connection = new NpgsqlConnection(AppDbConnectionStrBuilder.ConnectionString))
                {
                    var databaseVersion =
                        connection.QueryFirstOrDefault(
                            $"SELECT * FROM {ChromatographyResourceNames.SchemaVersionTable} WHERE {ChromatographyResourceNames.MajorVersionColumn} != -1;");

                    var dbSchemaVer = new Version((int)databaseVersion.majorversion,
                        (int)databaseVersion.minorversion);
                    connection.Close();
                    return dbSchemaVer;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }
            return null;
        }
    }
}
