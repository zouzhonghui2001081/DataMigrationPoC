using System.Reflection;
using log4net;

namespace PerkinElmer.Simplicity.Data.Version15.DataAccess.Postgresql.Chromatography
{
	internal class IntegrationEventBaseDao
	{
		protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		public static string IdColumn { get; } = "Id";
		public static string EventTypeColumn { get; } = "EventType";
		public static string StartTimeColumn { get; } = "StartTime";
		public static string EndTimeColumn { get; } = "EndTime";
		public static string EventIdColumn { get; } = "EventId";
		public static string ValueColumn { get; } = "Value";
	}
}
