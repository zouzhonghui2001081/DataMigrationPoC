using System.Data;
using System.Linq;
using System.Reflection;
using Dapper;
using log4net;
using PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version15.DataAccess.Postgresql.Chromatography
{
	internal class DeviceDriverItemDetailsDao
	{
		protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		internal static string TableName { get; } = "DeviceDriverItemDetails";
		internal static string IdColumn { get; } = "Id";
		internal static string BatchResultSetIdColumn { get; } = "BatchResultSetId";
		internal static string ConfigurationColumn { get; } = "Configuration";
		internal static string DeviceTypeColumn { get; } = "DeviceType";
		internal static string NameColumn { get; } = "Name";
		internal static string IsDisplayDriverColumn { get; } = "IsDisplayDriver";
		internal static string InstrumentMasterIdColumn { get; } = "InstrumentMasterId";
		internal static string InstrumentIdColumn { get; } = "InstrumentId";
		internal static string DeviceDriverItemIdColumn { get; } = "DeviceDriverItemId";

		protected readonly string SqlInsert =
			$"INSERT INTO {TableName} " +
			$"({BatchResultSetIdColumn}," +
			$"{ConfigurationColumn}," +
			$"{DeviceTypeColumn}," +
			$"{NameColumn}," +
			$"{IsDisplayDriverColumn}," +
			$"{InstrumentMasterIdColumn}," +
			$"{InstrumentIdColumn}," +
			$"{DeviceDriverItemIdColumn}) " +
			"VALUES " +
			$"(@{BatchResultSetIdColumn}," +
			$"@{ConfigurationColumn}," +
			$"@{DeviceTypeColumn}," +
			$"@{NameColumn}," +
			$"@{IsDisplayDriverColumn}," +
			$"@{InstrumentMasterIdColumn}," +
			$"@{InstrumentIdColumn}," +
			$"@{DeviceDriverItemIdColumn}) ";

		protected readonly string SqlSelect =
			$"SELECT {IdColumn}," +
			$"{BatchResultSetIdColumn}," +
			$"{ConfigurationColumn}," +
			$"{DeviceTypeColumn}," +
			$"{NameColumn}," +
			$"{IsDisplayDriverColumn}," +
			$"{InstrumentMasterIdColumn}," +
			$"{InstrumentIdColumn}," +
			$"{DeviceDriverItemIdColumn} " +
			$"FROM {TableName} ";
		public void Create(IDbConnection connection, DeviceDriverItemDetails[] deviceDriverItems)
		{
			connection.Execute(SqlInsert, deviceDriverItems);
		}

		public DeviceDriverItemDetails[] Get(IDbConnection connection, long batchResultId)
		{
			return connection.Query<DeviceDriverItemDetails>(
				SqlSelect + 
				$"WHERE {BatchResultSetIdColumn} = @BatchResultSetId",
				new { BatchResultSetId = batchResultId}).ToArray();
		}
	}
}
