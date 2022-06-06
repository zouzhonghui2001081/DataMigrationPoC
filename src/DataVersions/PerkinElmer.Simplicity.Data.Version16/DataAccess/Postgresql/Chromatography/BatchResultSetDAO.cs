using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Dapper;
using log4net;
using PerkinElmer.Simplicity.Data.Version16.Contract.DataEntities.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.Chromatography
{
    internal class BatchResultSetDao
    {
        protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        internal static string TableName { get; } = "BatchResultSet";
        internal static string IdColumn { get; } = "Id";
        internal static string NameColumn { get; } = "Name";
        internal static string ProjectIdColumn { get; } = "ProjectId";
        internal static string GuidColumn { get; } = "Guid";
        internal static string CreatedDateColumn { get; } = "CreatedDate";
        internal static string CreatedUserIdColumn { get; } = "CreatedUserId";
        internal static string ModifiedDateColumn { get; } = "ModifiedDate";
        internal static string ModifiedUserIdColumn { get; } = "ModifiedUserId";
        internal static string IsCompletedColumn { get; } = "IsCompleted";
        internal static string DataSourceTypeColumn { get; } = "DataSourceType";
        internal static string InstrumentMasterIdColumn { get; } = "InstrumentMasterId";
        internal static string InstrumentIdColumn { get; } = "InstrumentId";
        internal static string InstrumentNameColumn { get; } = "InstrumentName";
        internal static string RegulatedColumn { get; } = "Regulated";


        protected readonly string SelectSql = $"SELECT {TableName}.{IdColumn}," +
                                              $"{TableName}.{NameColumn}," +
                                              $"{TableName}.{ProjectIdColumn}," +
                                              $"{TableName}.{GuidColumn}," +
                                              $"{TableName}.{CreatedDateColumn}," +
                                              $"{TableName}.{CreatedUserIdColumn}," +
                                              $"{TableName}.{ModifiedDateColumn}," +
                                              $"{TableName}.{ModifiedUserIdColumn}," +
                                              $"{TableName}.{IsCompletedColumn}," +
                                              $"{TableName}.{DataSourceTypeColumn}," +
                                              $"{TableName}.{InstrumentMasterIdColumn}," +
                                              $"{TableName}.{InstrumentIdColumn}," +
                                              $"{TableName}.{InstrumentNameColumn}," +
                                              $"{TableName}.{RegulatedColumn} ";

        protected readonly string InsertSql = $"INSERT INTO {TableName} " +
                                              $"({ProjectIdColumn}," +
                                              $"{GuidColumn}," +
                                              $"{CreatedDateColumn}," +
                                              $"{CreatedUserIdColumn}," +
                                              $"{ModifiedDateColumn}," +
                                              $"{ModifiedUserIdColumn}," +
                                              $"{IsCompletedColumn}," +
                                              $"{NameColumn}," +
                                              $"{DataSourceTypeColumn}," +
                                              $"{InstrumentMasterIdColumn}," +
                                              $"{InstrumentIdColumn}," +
                                              $"{InstrumentNameColumn}," +
                                              $"{RegulatedColumn}) " +
                                              "VALUES " +
                                              $"(@{ProjectIdColumn}," +
                                              $"@{GuidColumn}," +
                                              $"@{CreatedDateColumn}," +
                                              $"@{CreatedUserIdColumn}," +
                                              $"@{ModifiedDateColumn}," +
                                              $"@{ModifiedUserIdColumn}," +
                                              $"@{IsCompletedColumn}," +
                                              $"@{NameColumn}," +
                                              $"@{DataSourceTypeColumn}," +
                                              $"@{InstrumentMasterIdColumn}," +
                                              $"@{InstrumentIdColumn}," +
                                              $"@{InstrumentNameColumn}," +
                                              $"@{RegulatedColumn}) ";

		internal IList<BatchResultSet> GetBatchResultSetByProjectName(IDbConnection connection, string projectName)
        {
            try
            {
	            projectName = projectName.ToLower();

                return connection.Query<BatchResultSet>(
                    $"{SelectSql}," +
                    $"{ProjectDao.TableName}.{ProjectDao.ProjectNameColumn} AS ProjectName " +
                    $"FROM {TableName} INNER JOIN {ProjectDao.TableName} " +
                    $"ON {TableName}.{ProjectIdColumn} = {ProjectDao.TableName}.{ProjectDao.IdColumn} " +
                    $"WHERE LOWER({ProjectDao.TableName}.{ProjectDao.ProjectNameColumn}) = @ProjectName",
                    new { ProjectName = projectName}).ToList();
            }
            catch (Exception ex)
            {
                Log.Error($"Error in GetBatchResultSetByProjectId method", ex);
                throw;
            }
        }

        internal BatchResultSet GetBatchResultSetByName(IDbConnection connection, string projectName, string batchResultSetName)
        {
            try
            {
	            projectName = projectName.ToLower();
	            batchResultSetName = batchResultSetName.ToLower();

                return connection.QueryFirstOrDefault<BatchResultSet>(
	                $"{SelectSql}," +
                    $"{ProjectDao.TableName}.{ProjectDao.ProjectNameColumn} AS ProjectName " +
                    $"FROM {TableName} INNER JOIN {ProjectDao.TableName} " +
                    $"ON {TableName}.{ProjectIdColumn} = {ProjectDao.TableName}.{ProjectDao.IdColumn} " +
                    $"WHERE LOWER({ProjectDao.TableName}.{ProjectDao.ProjectNameColumn}) = @ProjectName " +
                    $"AND LOWER({TableName}.{NameColumn}) = @BatchResultSetName",
                    new { ProjectName = projectName, BatchResultSetName = batchResultSetName});
            }
            catch (Exception ex)
            {
                Log.Error($"Error in GetBatchResultSetByName method", ex);
                throw;
            }
        }

        internal BatchResultSet GetBatchResultSetByIds(IDbConnection connection, long batchResultSetId)
        {
            try
            {
                return connection.Query<BatchResultSet>(
	                SelectSql +
	                $"FROM {TableName} " +
	                $"WHERE {IdColumn} = {batchResultSetId}").FirstOrDefault();

            }
            catch (Exception ex)
            {
                Log.Error($"Error in GetBatchResultSetByIds method", ex);
                throw;
            }
        }

        internal BatchResultSet GetBatchResultSetByGuid(IDbConnection connection, Guid batchResulSetGuid)
        {
            try
            {
                BatchResultSet batchResultSet = connection.QueryFirstOrDefault<BatchResultSet>(
                    SelectSql +
                    $"FROM {TableName} " +
                    $"WHERE {GuidColumn} = '{batchResulSetGuid}'");

                return batchResultSet;
            }
            catch (Exception ex)
            {
                Log.Error($"Error in GetBatchResultSetByGuid method", ex);
                throw;
            }
        }
        internal BatchResultSet GetBatchResultSet(IDbConnection connection, Guid projectGuid, Guid batchResultSetGuid)
        {
	        try
	        {
		        BatchResultSet batchResultSet = connection.QueryFirstOrDefault<BatchResultSet>(
				SelectSql +
				$"FROM {TableName} " +
				$"INNER JOIN {ProjectDao.TableName} ON {ProjectDao.TableName}.{ProjectDao.IdColumn} = {TableName}.{ProjectIdColumn} " +
				$"WHERE {ProjectDao.TableName}.{ProjectDao.GuidColumn} = @ProjectGuid AND {TableName}.{GuidColumn} = @BatchResultSetGuid",
				new {ProjectGuid = projectGuid, BatchResultSetGuid = batchResultSetGuid});

		        return batchResultSet;
	        }
	        catch (Exception ex)
	        {
		        Log.Error($"Error in GetBatchResultSetByGuid method", ex);
		        throw;
	        }
        }

		internal BatchResultSet CreateBatchResultSet(IDbConnection connection, BatchResultSet batchResultSet)
        {
            try
            {
                batchResultSet.Id = connection.ExecuteScalar<long>(
                        InsertSql + "RETURNING Id", batchResultSet);
            }
            catch (Exception ex)
            {
                Log.Error("Error in CreateBatchResultSet method", ex);
                throw;
            }

            return batchResultSet;
        }

        internal BatchResultSet EndBatchResultSetRun(IDbConnection connection, Guid batchResultSetGuid)
        {
            try
            {
                var batchResultSet = connection.QueryFirstOrDefault<BatchResultSet>(
                    SelectSql +
                    $"FROM {TableName} " +
                    $"WHERE {GuidColumn} = '{batchResultSetGuid}'");

                if (batchResultSet == null)
                    throw new InvalidOperationException($"BatchResultSet with Guid {batchResultSetGuid} not found in database");

                batchResultSet.IsCompleted = true;
                batchResultSet.ModifiedDate = DateTime.UtcNow;

                connection.Execute(
                    $"UPDATE {TableName} " +
                    $"SET {ModifiedDateColumn} = @{ModifiedDateColumn}," +
                    $"{IsCompletedColumn} = @{IsCompletedColumn} " +
                    $"WHERE {GuidColumn} = @{GuidColumn}",
                    new { ModifiedDate = batchResultSet.ModifiedDate,
	                    IsCompleted = true,
	                    Guid = batchResultSetGuid });

                return batchResultSet;
            }
            catch (Exception ex)
            {
                Log.Error("Error in EndBatchResultSetRun method", ex);
                throw;
            }
        }

        public long GetBatchResultSetIdByBatchResultSetName(IDbConnection connection, string projectName, string batchResultSetName)
        {
            try
            {
	            projectName = projectName.ToLower();
	            batchResultSetName = batchResultSetName.ToLower();

				var sql = $"SELECT {TableName}.{IdColumn} " +
                             $"FROM {TableName} INNER JOIN {ProjectDao.TableName} ON {TableName}.{ProjectIdColumn} = {ProjectDao.TableName}.{IdColumn}" +
                             $" WHERE Lower({TableName}.{NameColumn}) = @BatchResultSetName AND Lower({ProjectDao.TableName}.{NameColumn}) = @ProjectName";

                long id = connection.QueryFirstOrDefault<long>(sql, new { BatchResultSetName = batchResultSetName, ProjectName = projectName});


                return id;

            }
            catch (Exception ex)
            {
                Log.Error("Error in GetBatchResultSetInfoByBatchResultSetName method", ex);
                throw;
            }
        }


        public bool IsExists(IDbConnection connection, string projectName, string batchResultSetName)
        {
            if (string.IsNullOrWhiteSpace(projectName))
            {
                throw new ArgumentException(nameof(projectName));
            }

            if (string.IsNullOrEmpty(batchResultSetName))
            {
                throw new ArgumentException(nameof(batchResultSetName));
            }

            BatchResultSet batchResultSet = ValidateBatchResultSetNameForProjectExists(connection, projectName, batchResultSetName);

            if (batchResultSet != null)
            {
                return true;
            }

            return false;
        }

        private BatchResultSet ValidateBatchResultSetNameForProjectExists(IDbConnection connection, string projectName, string batchResultSetName)
        {
	        projectName = projectName.ToLower();
	        batchResultSetName = batchResultSetName.ToLower();

			string sql = $"SELECT {TableName}.{NameColumn} FROM {TableName} INNER JOIN {ProjectDao.TableName} ON {TableName}.{ProjectIdColumn} = {ProjectDao.TableName}.{ProjectDao.IdColumn} " +
                $" WHERE Lower({ProjectDao.TableName}.{ProjectDao.ProjectNameColumn}) =  @ProjectName AND " +
                $" Lower({TableName}.{NameColumn}) = @BatchResultSetName";

            List<BatchResultSet> batchResultSet =
                connection.Query<BatchResultSet>(
	                sql,
	                new { ProjectName = projectName, BatchResultSetName = batchResultSetName}).ToList();
            return batchResultSet.FirstOrDefault();
        }
        public bool IsExists(IDbConnection connection, Guid projectGuid, Guid batchResultSetGuid)
        {

	        string sql = $"SELECT {TableName}.{NameColumn} FROM {TableName} " +
	                     $"INNER JOIN {ProjectDao.TableName} ON {TableName}.{ProjectIdColumn} = {ProjectDao.TableName}.{ProjectDao.IdColumn} " +
	                     $"WHERE {ProjectDao.TableName}.{ProjectDao.GuidColumn} =  @ProjectGuid AND " +
	                     $"{TableName}.{GuidColumn} = @BatchResultSetGuid";

	        var batchResultSet = connection.QueryFirstOrDefault<BatchResultSet>(
		        sql,
		        new {ProjectGuid = projectGuid, BatchResultSetGuid = batchResultSetGuid});

	        return batchResultSet != null;
        }


		public BatchResultSet[] GetAll(IDbConnection connection, Guid projectGuid)
        {
			try
			{
				return connection.Query<BatchResultSet>(
						SelectSql +
						$"FROM {TableName} " +
						$"INNER JOIN {ProjectDao.TableName} ON {TableName}.{ProjectIdColumn} = {ProjectDao.TableName}.{ProjectDao.IdColumn} " +
					$"WHERE {ProjectDao.TableName}.{ProjectDao.GuidColumn} = @ProjectGuid",
					new { ProjectGuid = projectGuid }).ToArray();
			}
			catch (Exception ex)
			{
					Log.Error("Error in GetAllBatchResultSets method", ex);
		        Log.Error("Error in GetAll method", ex);
				throw;
			}
        }
        public BatchResultSet Get(IDbConnection connection, Guid projectGuid, Guid batchResultSetGuid)
        {
	        try
	        {
		        try
		        {
			        return connection.QueryFirstOrDefault<BatchResultSet>(
				        SelectSql +
				        $"FROM {TableName} " +
				        $"INNER JOIN {ProjectDao.TableName} ON {TableName}.{ProjectIdColumn} = {ProjectDao.TableName}.{ProjectDao.IdColumn} " +
				        $"WHERE {ProjectDao.TableName}.{ProjectDao.GuidColumn} = @ProjectGuid AND " +
				        $"{TableName}.{GuidColumn} = @BatchResultSetGuid",
				        new { ProjectGuid = projectGuid, BatchResultSetGuid = batchResultSetGuid });
		        }
		        catch (Exception ex)
		        {
			        Log.Error("Error in GetAll method", ex);
			        throw;
		        }
	        }
	        catch (Exception ex)
	        {
		        Log.Error("Error in Get method", ex);
		        throw;
	        }
        }
        public void Delete(IDbConnection connection, long batchResultSetId)
        {
	        try
	        {
		        connection.Execute($"DELETE FROM {TableName} " +
		                           $"WHERE {IdColumn} = @Id",
			        new { Id = batchResultSetId });
	        }
	        catch (Exception ex)
	        {
		        Log.Error("Error in Delete method", ex);
		        throw;
	        }
        }

	}
}
