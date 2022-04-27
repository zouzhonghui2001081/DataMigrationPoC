using System.Reflection;
using log4net;

namespace PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.Chromatography
{
	internal class DeviceMethodBaseDao
	{
		protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		internal static string TableName { get; } = "DeviceMethod";
		internal static string IdColumn { get; } = "Id";
		internal static string AcquisitionMethodIdColumn { get; } = "AcquisitionMethodId";
		internal static string ConfigurationColumn { get; } = "Configuration";
		internal static string NameColumn { get; } = "Name";
		internal static string ContentColumn { get; } = "Content";
		internal static string DeviceTypeColumn { get; } = "DeviceType";
		internal static string InstrumentMasterIdColumn { get; } = "InstrumentMasterId";
		internal static string InstrumentIdColumn { get; } = "InstrumentId";
		internal static string DeviceDriverItemIdColumn { get; } = "DeviceDriverItemId";

		protected readonly string SqlInsertInto =
			$"INSERT INTO {TableName} " +
			$"({AcquisitionMethodIdColumn}," +
			$"{ConfigurationColumn}," +
			$"{NameColumn}," +
			$"{ContentColumn}," +
			$"{DeviceTypeColumn}," +
			$"{InstrumentMasterIdColumn}," +
			$"{InstrumentIdColumn}," +
			$"{DeviceDriverItemIdColumn}) " +
			"VALUES " +
			$"(@{AcquisitionMethodIdColumn}," +
			$"@{ConfigurationColumn}," +
			$"@{NameColumn}," +
			$"@{ContentColumn}," +
			$"@{DeviceTypeColumn}," +
			$"@{InstrumentMasterIdColumn}," +
			$"@{InstrumentIdColumn}," +
			$"@{DeviceDriverItemIdColumn}) ";

		protected readonly string SqlSelect =
			$"SELECT {IdColumn}," +
			$"{AcquisitionMethodIdColumn}," +
			$"{ConfigurationColumn}," +
			$"{NameColumn}," +
			$"{ContentColumn}," +
			$"{DeviceTypeColumn}," +
			$"{InstrumentMasterIdColumn}," +
			$"{InstrumentIdColumn}," +
			$"{DeviceDriverItemIdColumn} " +
			$"FROM {TableName} ";
	}
}
