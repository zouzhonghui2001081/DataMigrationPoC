using System;
using System.Data;
using System.Linq;
using Dapper;
using PerkinElmer.Simplicity.Data.Version16.Contract.DataEntities.Chromatography.AcquisitionMethod;

namespace PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.Chromatography
{
	internal class DeviceMethodDao : DeviceMethodBaseDao
	{
		public DeviceMethod[] GetMethodDevices(IDbConnection connection, long acquisitionMethodId)
		{
			try
			{
				DeviceMethod[] deviceMethods = connection.Query<DeviceMethod>(
					SqlSelect +
					$"WHERE {AcquisitionMethodIdColumn} = {acquisitionMethodId}").ToArray();

				return deviceMethods;
			}
			catch (Exception ex)
			{
				Log.Error("Error in GetMethodDevices method", ex);
				throw;
			}
		}

		public DeviceMethod[] GetDeviceMethodsByAcquisitionMethodId(IDbConnection connection, long acquisitionMethodId)
		{
			try
			{
				var deviceMethods = connection.Query<DeviceMethod>(
					SqlSelect +
					$"WHERE {AcquisitionMethodIdColumn} = {acquisitionMethodId}").ToArray();

				return deviceMethods;

			}
			catch (Exception ex)
			{
				Log.Error("Error in GetDeviceMethodsByAcquisitionMethodId method", ex);
				throw;
			}
		}

		public void Create(IDbConnection connection, DeviceMethod[] deviceMethods)
		{
			try
			{
				var insertSql = SqlInsertInto + $"RETURNING { IdColumn}";

				foreach (var deviceMethod in deviceMethods)
				{
					deviceMethod.Id = connection.ExecuteScalar<long>(insertSql, deviceMethod);
				}
			}
			catch (Exception ex)
			{
				Log.Error("Error in Create method", ex);
				throw;
			}
		}

		public void Delete(IDbConnection connection, long acquisitionMethodId)
		{
			try
			{
				connection.Execute(
					$"DELETE FROM {TableName} " +
					$"WHERE {AcquisitionMethodIdColumn} = {acquisitionMethodId}"
				);
			}
			catch (Exception ex)
			{
				Log.Error("Error in DeleteByAcquisitionMethodId method", ex);
				throw;
			}
		}

		public void UpdateContent(IDbConnection connection, long acquisitionMethodId, DeviceMethod[] deviceMethods)
		{
			try
			{
				foreach (DeviceMethod deviceMethod in deviceMethods)
				{
					connection.Execute(
						$"UPDATE {TableName} " +
						$"SET {ContentColumn} = @{ContentColumn} " +
						$"WHERE {AcquisitionMethodIdColumn} = {acquisitionMethodId} AND {NameColumn} = @DeviceMethodName ",
						new {Content=deviceMethod.Content, DeviceMethodName = deviceMethod.Name }

					);
				}
			}
			catch (Exception ex)
			{
				Log.Error("Error in UpdateContent method", ex);
				throw;
			}
		}
	}
}
