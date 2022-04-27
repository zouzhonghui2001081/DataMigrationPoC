using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Dapper;
using log4net;
using PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version15.DataAccess.Postgresql.Chromatography
{
	internal class BrChannelsWithExceededNumberOfPeaksDao
	{
		protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		internal static string TableName { get; } = "BrChannelsWithExceededNumberOfPeaks";
		internal static string IdColumn { get; } = "Id";
		internal static string AnalysisResultSetIdColumn { get; } = "AnalysisResultSetId";
		internal static string BatchRunChannelGuidColumn { get; } = "BatchRunChannelGuid";

		protected readonly string InsertSql =
			$"INSERT INTO {TableName} " +
			$"({AnalysisResultSetIdColumn}," +
			$"{BatchRunChannelGuidColumn}) " +
			"VALUES " +
			$"(@{AnalysisResultSetIdColumn}," +
			$"@{BatchRunChannelGuidColumn}) ";

		protected readonly string SelectSql =
			$"SELECT {TableName}.{IdColumn}," +
			$"{TableName}.{AnalysisResultSetIdColumn}," +
			$"{TableName}.{BatchRunChannelGuidColumn} ";

		public bool Create(IDbConnection connection, IList<BrChannelsWithExceededNumberOfPeaks> brChannelsWithExceededNumberOfPeaks)
		{
			try
			{
				var numberOfRecordsSaved = connection.Execute(InsertSql, brChannelsWithExceededNumberOfPeaks);
				return (numberOfRecordsSaved != 0);
			}
			catch (Exception ex)
			{
				Log.Error($"Error occurred in Create() method", ex);
				throw;
			}
		}
		public bool Create(IDbConnection connection,
			Guid projectGuid,
			Guid analysisResultSetGuid, 
			IList<BrChannelsWithExceededNumberOfPeaks> brChannelsWithExceededNumberOfPeaks)
		{
			try
			{
				var analysisResultSetId = connection.QueryFirstOrDefault<long>(
					$"SELECT {AnalysisResultSetDao.TableName}.{AnalysisResultSetDao.IdColumn} " +
					$"FROM {ProjectDaoBase.TableName} " +
					$"INNER JOIN {AnalysisResultSetDao.TableName} ON {AnalysisResultSetDao.TableName}.{AnalysisResultSetDao.ProjectIdColumn} = {ProjectDao.TableName}.{ProjectDao.IdColumn} " +
					$"WHERE {ProjectDao.TableName}.{ProjectDao.GuidColumn} = @ProjectGuid AND " +
					$"{AnalysisResultSetDao.TableName}.{AnalysisResultSetDao.GuidColumn} = @AnalysisResultSetGuid",
					new { ProjectGuid = projectGuid, AnalysisResultSetGuid = analysisResultSetGuid });

				if (analysisResultSetId != 0)
				{
					foreach (var brChannelsWithExceededNumberOfPeak in brChannelsWithExceededNumberOfPeaks)
					{
						brChannelsWithExceededNumberOfPeak.AnalysisResultSetId = analysisResultSetId;
					}
					
					connection.Execute(InsertSql, brChannelsWithExceededNumberOfPeaks);
					return true;
				}

				return false;
			}
			catch (Exception ex)
			{
				Log.Error($"Error occurred in Create() method", ex);
				throw;
			}
		}
		public IList<BrChannelsWithExceededNumberOfPeaks> Get(IDbConnection connection, Guid projectGuid, Guid analysisResultSetGuid)
		{
			try
			{
				var brChannelsWithExceededNumberOfPeaks = 
					connection.Query<BrChannelsWithExceededNumberOfPeaks>(
						SelectSql + 
						$"FROM {ProjectDaoBase.TableName} " +
						$"INNER JOIN {AnalysisResultSetDao.TableName} ON {AnalysisResultSetDao.TableName}.{AnalysisResultSetDao.ProjectIdColumn} = {ProjectDao.TableName}.{ProjectDao.IdColumn} " +
						$"INNER JOIN {TableName} ON {TableName}.{AnalysisResultSetIdColumn} = {AnalysisResultSetDao.TableName}.{AnalysisResultSetDao.IdColumn} " +
						$"WHERE {ProjectDao.TableName}.{ProjectDao.GuidColumn} = @ProjectGuid AND " +
						$"{AnalysisResultSetDao.TableName}.{AnalysisResultSetDao.GuidColumn} = @AnalysisResultSetGuid",
						new { ProjectGuid = projectGuid, AnalysisResultSetGuid = analysisResultSetGuid}).ToList();

				return brChannelsWithExceededNumberOfPeaks;
			}
			catch (Exception ex)
			{
				Log.Error($"Error occurred in Get() method ", ex);
				throw;
			}
		}
		public virtual IList<BrChannelsWithExceededNumberOfPeaks> Get(IDbConnection connection, long analysisResultSetId)
		{
			try
			{
				var brChannelsWithExceededNumberOfPeaks =
					connection.Query<BrChannelsWithExceededNumberOfPeaks>(
						SelectSql +
						$"FROM {TableName} " +
						$"WHERE {AnalysisResultSetIdColumn} = @AnalysisResultSetId",
						new { AnalysisResultSetId = analysisResultSetId }).ToList();

				return brChannelsWithExceededNumberOfPeaks;
			}
			catch (Exception ex)
			{
				Log.Error($"Error occurred in Get() method ", ex);
				throw;
			}
		}

		public void Delete(IDbConnection connection, long analysisResultSetId)
		{
			try
			{
				connection.Execute(
					$"DELETE FROM {TableName} " +
					$"WHERE {AnalysisResultSetIdColumn} = @AnalysisResultSetId",
					new { AnalysisResultSetId = analysisResultSetId } );
			}
			catch (Exception ex)
			{
				Log.Error($"Error occurred in Delete() method", ex);
				throw;
			}
		}
	}
}
