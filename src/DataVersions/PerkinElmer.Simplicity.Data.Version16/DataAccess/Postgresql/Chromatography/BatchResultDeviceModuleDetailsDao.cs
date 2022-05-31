using System;
using System.Data;
using System.Linq;
using System.Reflection;
using Dapper;
using log4net;
using PerkinElmer.Simplicity.Data.Version16.Contract.DataEntities.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.Chromatography
{
	internal class BatchResultDeviceModuleDetailsDao
	{
		protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		internal static string TableName = "BatchResultDeviceModuleDetails";
		internal static string BatchResultSetIdColumn = "BatchResultSetId";
		internal static string IdColumn { get; } = "Id";
		internal static string NameColumn { get; } = "Name";
		internal static string DeviceTypeColumn { get; } = "DeviceType";
		internal static string IsDisplayDriverColumn { get; } = "IsDisplayDriver";
		internal static string DeviceModuleIdColumn { get; } = "DeviceModuleId";
		internal static string InstrumentMasterIdColumn { get; } = "InstrumentMasterId";
		internal static string InstrumentIdColumn { get; } = "InstrumentId";
		internal static string DeviceDriverItemIdColumn { get; } = "DeviceDriverItemId";
		internal static string SettingsUserInterfaceSupportedColumn { get; } = "SettingsUserInterfaceSupported";
		internal static string SimulationColumn { get; } = "Simulation";
		internal static string CommunicationTestedSuccessfullyColumn { get; } = "CommunicationTestedSuccessfully";
		internal static string FirmwareVersionColumn { get; } = "FirmwareVersion";
		internal static string SerialNumberColumn { get; } = "SerialNumber";
		internal static string ModelNameColumn { get; } = "ModelName";
		internal static string UniqueIdentifierColumn { get; } = "UniqueIdentifier";
		internal static string InterfaceAddressColumn { get; } = "InterfaceAddress";

		protected string SqlInsertInto =
			$"INSERT INTO {TableName} " +
			$"({BatchResultSetIdColumn}," +
			$"{NameColumn}," +
			$"{DeviceTypeColumn}," +
			$"{IsDisplayDriverColumn}," +
			$"{DeviceModuleIdColumn}," +
			$"{InstrumentMasterIdColumn}," +
			$"{InstrumentIdColumn}," +
			$"{DeviceDriverItemIdColumn}," +
			$"{SettingsUserInterfaceSupportedColumn}," +
			$"{SimulationColumn}," +
			$"{CommunicationTestedSuccessfullyColumn}," +
			$"{FirmwareVersionColumn}," +
			$"{SerialNumberColumn}," +
			$"{ModelNameColumn}," +
			$"{UniqueIdentifierColumn}," +
			$"{InterfaceAddressColumn}) " +
			"VALUES " +
			$"(@{BatchResultSetIdColumn}," +
			$"@{NameColumn}," +
			$"@{DeviceTypeColumn}," +
			$"@{IsDisplayDriverColumn}," +
			$"@{DeviceModuleIdColumn}," +
			$"@{InstrumentMasterIdColumn}," +
			$"@{InstrumentIdColumn}," +
			$"@{DeviceDriverItemIdColumn}," +
			$"@{SettingsUserInterfaceSupportedColumn}," +
			$"@{CommunicationTestedSuccessfullyColumn}," +
			$"@{SimulationColumn}," +
			$"@{FirmwareVersionColumn}," +
			$"@{SerialNumberColumn}," +
			$"@{ModelNameColumn}," +
			$"@{UniqueIdentifierColumn}," +
			$"@{InterfaceAddressColumn}) ";

		protected string SqlSelect =
			$"SELECT {IdColumn}," +
			$"{BatchResultSetIdColumn}," +
			$"{NameColumn}," +
			$"{DeviceTypeColumn}," +
			$"{IsDisplayDriverColumn}," +
			$"{DeviceModuleIdColumn}," +
			$"{InstrumentMasterIdColumn}," +
			$"{InstrumentIdColumn}," +
			$"{DeviceDriverItemIdColumn}," +
			$"{SettingsUserInterfaceSupportedColumn}," +
			$"{SimulationColumn}," +
			$"{CommunicationTestedSuccessfullyColumn}," +
			$"{FirmwareVersionColumn}," +
			$"{SerialNumberColumn}," +
			$"{ModelNameColumn}," +
			$"{UniqueIdentifierColumn}," +
			$"{InterfaceAddressColumn} " +
			$"FROM {TableName} ";

		public void Create(IDbConnection connection, BatchResultDeviceModuleDetails[] deviceModulesDetails)
		{
			try
			{
				connection.Execute(SqlInsertInto, deviceModulesDetails);
			}
			catch (Exception ex)
			{
				Log.Error("Error in Create method", ex);
				throw;
			}
		}
		public BatchResultDeviceModuleDetails[] GetDeviceModules(IDbConnection connection, long batchResultSetId)
		{
			try
			{
				var deviceModules = connection.Query<BatchResultDeviceModuleDetails>(
					SqlSelect +
					$"WHERE {BatchResultSetIdColumn} = {batchResultSetId}").ToArray();

				return deviceModules;
			}
			catch (Exception ex)
			{
				Log.Error("Error in GetDeviceModules method", ex);
				throw;
			}
		}
		public void Delete(IDbConnection connection, long batchResultSetId)
		{
			try
			{
				connection.Execute($"DELETE FROM {TableName} WHERE {BatchResultSetIdColumn}={batchResultSetId}");
			}
			catch (Exception ex)
			{
				Log.Error("Error in Delete DeviceModules method", ex);
				throw;
			}
		}
	}
}
