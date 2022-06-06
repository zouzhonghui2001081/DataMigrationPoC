using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Dapper;
using log4net;
using PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography.ProcessingMethod;

namespace PerkinElmer.Simplicity.Data.Version15.DataAccess.Postgresql.Chromatography
{
	internal class ProcessingDeviceMethodDao
	{
		protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		internal static string TableName { get; } = "ProcessingDeviceMethod";
		public static string IdColumn { get; } = "Id";
		internal static string ProcessingMethodIdColumn { get; } = "ProcessingMethodId";
		public static string DeviceClassColumn { get; } = "DeviceClass";
		public static string DeviceIndexColumn { get; } = "DeviceIndex";
		public static string MetaDataColumn { get; } = "MetaData";

		protected readonly string InsertSql = $"INSERT INTO {TableName} " +
		                                      $"({ProcessingMethodIdColumn}," +
		                                      $"{MetaDataColumn}," +
		                                      $"{DeviceClassColumn}," +
		                                      $"{DeviceIndexColumn}) " +
		                                      "VALUES " +
		                                      $"(@{ProcessingMethodIdColumn}," +
		                                      $"@{MetaDataColumn}," +
		                                      $"@{DeviceClassColumn}," +
		                                      $"@{DeviceIndexColumn}) ";

		protected readonly string SelectSql =
			"SELECT " +
			$"{IdColumn}," +
			$"{ProcessingMethodIdColumn}," +
			$"{DeviceClassColumn}," +
			$"{DeviceIndexColumn}," +
			$"{MetaDataColumn}" +
			$" FROM {TableName} ";

		public virtual void Create(IDbConnection connection, ProcessingDeviceMethod processingDeviceMethod)
		{
			try
			{
				processingDeviceMethod.Id =
					connection.ExecuteScalar<long>(InsertSql + "RETURNING Id", processingDeviceMethod);

				//Save DeviceMethodChannelMetaData


			}
			catch (Exception ex)
			{
				Log.Error($"Error in Create method", ex);
				throw;
			}
		}

		public virtual List<ProcessingDeviceMethod> GetProcessingDeviceMethods(IDbConnection connection, long processingMethodId)
		{
			try
			{
				List<ProcessingDeviceMethod> processingDeviceMethods = connection.Query<ProcessingDeviceMethod>(
					SelectSql +
					$"WHERE {ProcessingMethodIdColumn} = {processingMethodId}"
				).ToList();

				return processingDeviceMethods;
			}
			catch (Exception ex)
			{
				Log.Error($"Error in GetProcessingDeviceMethods method", ex);
				throw;
			}

		}

	}
}
