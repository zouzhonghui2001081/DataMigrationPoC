using System;
using System.Data;
using System.Linq;
using Dapper;
using PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography.AcquisitionMethod;

namespace PerkinElmer.Simplicity.Data.Version15.DataAccess.Postgresql.Chromatography
{
	internal class ExpectedDeviceChannelDescriptorDao : ExpectedDeviceChannelDescriptorBaseDao
	{
		public void Create(IDbConnection connection, ExpectedDeviceChannelDescriptor[] expectedDeviceChannelDescriptors)
		{
			try
			{
				connection.Execute(SqlInsertInto, expectedDeviceChannelDescriptors);
			}
			catch (Exception ex)
			{
				Log.Error("Error in Create method", ex);
				throw;
			}
		}
		public ExpectedDeviceChannelDescriptor[] Get(IDbConnection connection, long deviceMethodId)
		{
			try
			{
				var expectedDeviceChannelDescriptors = connection.Query<ExpectedDeviceChannelDescriptor>(
					SqlSelect +
					$"WHERE {DeviceMethodIdColumn} = {deviceMethodId}").ToArray();

				return expectedDeviceChannelDescriptors;
			}
			catch (Exception ex)
			{
				Log.Error("Error in Get method", ex);
				throw;
			}
		}
		public void Delete(IDbConnection connection, long deviceMethodId)
		{
			try
			{
				connection.Execute($"DELETE FROM {TableName} WHERE {DeviceMethodIdColumn}={deviceMethodId}");
			}
			catch (Exception ex)
			{
				Log.Error("Error in Delete method", ex);
				throw;
			}
		}
	}
}
