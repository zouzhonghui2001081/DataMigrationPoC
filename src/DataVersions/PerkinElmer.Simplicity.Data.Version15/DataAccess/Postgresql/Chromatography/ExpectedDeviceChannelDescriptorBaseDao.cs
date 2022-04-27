using System.Reflection;
using log4net;

namespace PerkinElmer.Simplicity.Data.Version15.DataAccess.Postgresql.Chromatography
{
	internal class ExpectedDeviceChannelDescriptorBaseDao
	{
		protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		internal static string TableName { get; } = "ExpectedDeviceChannelDescriptor";
		internal static string IdColumn { get; } = "Id";
		internal static string DeviceMethodIdColumn { get; } = "DeviceMethodId";
		internal static string DeviceChannelDescriptorColumn { get; } = "DeviceChannelDescriptor";

		protected readonly string SqlInsertInto =
			$"INSERT INTO {TableName} " +
			$"({DeviceMethodIdColumn}," +
			$"{DeviceChannelDescriptorColumn}) " +
			"VALUES " +
			$"(@{DeviceMethodIdColumn}," +
			$"@{DeviceChannelDescriptorColumn}) ";

		protected readonly string SqlSelect =
			$"SELECT {IdColumn}," +
			$"{DeviceMethodIdColumn}," +
			$"{DeviceChannelDescriptorColumn} " +
			$"FROM {TableName} ";
	}
}
