using System.Reflection;
using log4net;

namespace PerkinElmer.Simplicity.Data.Version15.DataAccess.Postgresql.Chromatography
{
	internal class StreamDataBaseDao
	{
		protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		public static string IdColumn { get; } = "Id";
		public static string StreamIndexColumn { get; } = "StreamIndex";
		public static string MetaDataColumn { get; } = "MetaData";
		public static string MetaDataTypeColumn { get; } = "MetaDataType";
		public static string YDataColumn { get; } = "YData";
		public static string DeviceIdColumn { get; } = "DeviceId";
		public static string LargeObjectOidColumn { get; } = "LargeObjectOid";
		public static string UseLargeObjectStreamColumn { get; } = "UseLargeObjectStream";
		public static string FirmwareVersionColumn { get; } = "FirmwareVersion";
		public static string SerialNumberColumn { get; } = "SerialNumber";
		public static string ModelNameColumn { get; } = "ModelName";
		public static string UniqueIdentifierColumn { get; } = "UniqueIdentifier";
		public static string InterfaceAddressColumn { get; } = "InterfaceAddress";
	}
}
