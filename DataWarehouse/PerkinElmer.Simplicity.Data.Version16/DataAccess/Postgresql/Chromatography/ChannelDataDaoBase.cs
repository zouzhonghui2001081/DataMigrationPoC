using System;
using System.Reflection;
using log4net;

namespace PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.Chromatography
{
    [Obsolete]
    internal abstract class ChannelDataDaoBase
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static string IdColumn { get; } = "Id";
        public static string ChannelDataTypeColumn { get; } = "ChannelDataType";
        public static string ChannelTypeColumn { get; } = "ChannelType";
        public static string ChannelIndexColumn { get; } = "ChannelIndex";
        public static string ChannelMetaDataColumn { get; } = "ChannelMetaData";
        public static string RawChannelTypeColumn { get; } = "RawChannelType";
      
        public static string BlankSubtractionAppliedColumn { get; } = "BlankSubtractionApplied";
        public static string SmoothAppliedColumn { get; } = "SmoothApplied";
        public static string BatchRunChannelGuidColumn { get; } = "BatchRunChannelGuid";
    }
}