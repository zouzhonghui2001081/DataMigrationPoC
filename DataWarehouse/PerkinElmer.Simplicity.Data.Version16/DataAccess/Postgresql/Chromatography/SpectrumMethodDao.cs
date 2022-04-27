using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Dapper;
using log4net;
using PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography.ProcessingMethod;

namespace PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.Chromatography
{
	internal class SpectrumMethodDao
	{
		protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		internal static string TableName { get; } = "SpectrumMethod";
		internal static string IdColumn { get; } = "Id";
		internal static string ProcessingMethodIdColumn { get; } = "ProcessingMethodId";
		internal static string GuidColumn { get; } = "Guid";
		internal static string StartRetentionTimeColumn { get; } = "StartRetentionTime";
		internal static string EndRetentionTimeColumn { get; } = "EndRetentionTime";
		internal static string BaselineCorrectionTypeColumn { get; } = "BaselineCorrectionType";
		internal static string BaselineStartRetentionTimeColumn { get; } = "BaselineStartRetentionTime";
		internal static string BaselineEndRetentionTimeColumn { get; } = "BaselineEndRetentionTime";
		

		protected readonly string InsertSql = $"INSERT INTO {TableName} " +
		                                      $"({ProcessingMethodIdColumn}," +
		                                      $"{GuidColumn}," +
		                                      $"{StartRetentionTimeColumn}," +
		                                      $"{EndRetentionTimeColumn}," +
		                                      $"{BaselineCorrectionTypeColumn}," +
		                                      $"{BaselineStartRetentionTimeColumn}," +
		                                      $"{BaselineEndRetentionTimeColumn})" +
		                                      "VALUES " +
		                                      $"(@{ProcessingMethodIdColumn}," +
		                                      $"@{GuidColumn}," +
		                                      $"@{StartRetentionTimeColumn}," +
		                                      $"@{EndRetentionTimeColumn}," +
		                                      $"@{BaselineCorrectionTypeColumn}," +
		                                      $"@{BaselineStartRetentionTimeColumn}," +
		                                      $"@{BaselineEndRetentionTimeColumn}) ";

		protected readonly string SelectSql = $"SELECT {IdColumn}," +
		                                      $"{ProcessingMethodIdColumn}," +
		                                      $"{GuidColumn}," +
		                                      $"{StartRetentionTimeColumn}," +
		                                      $"{EndRetentionTimeColumn}," +
		                                      $"{BaselineCorrectionTypeColumn}," +
		                                      $"{BaselineStartRetentionTimeColumn}," +
		                                      $"{BaselineEndRetentionTimeColumn} ";
		public virtual void Create(IDbConnection connection, SpectrumMethod spectrumMethod)
		{
			try
			{
				spectrumMethod.Id = connection.ExecuteScalar<long>(InsertSql + $"RETURNING {IdColumn}", spectrumMethod);
			}
			catch (Exception ex)
			{
				Log.Error("Error in Create method", ex);
				throw;
			}
		}

		public virtual List<SpectrumMethod> GetSpectrumMethods(IDbConnection connection, long processingMethodId)
		{
			List<SpectrumMethod> spectrumMethods =  connection.Query<SpectrumMethod>
			(SelectSql +
			 $"FROM {TableName} " +
			 $"WHERE {ProcessingMethodIdColumn} = {processingMethodId}").ToList();

			return spectrumMethods;
		}

		public void Delete(IDbConnection connection, long processingMethodId)
		{
			try
			{
				connection.Execute(
					$"DELETE FROM {TableName} " +
					$"WHERE {TableName}.{ProcessingMethodIdColumn} = {processingMethodId}");
			}
			catch (Exception ex)
			{
				Log.Error($"Error in Delete method", ex);
				throw;
			}
		}
	}
}
