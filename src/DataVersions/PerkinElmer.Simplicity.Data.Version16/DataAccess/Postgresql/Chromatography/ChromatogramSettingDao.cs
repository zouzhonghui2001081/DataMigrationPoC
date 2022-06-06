using System;
using System.Data;
using System.Linq;
using System.Reflection;
using Dapper;
using log4net;
using PerkinElmer.Simplicity.Data.Version16.Contract.DataEntities.Chromatography.ProcessingMethod;

namespace PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.Chromatography
{
    internal class ChromatogramSettingDao
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static string TableName { get; } = "ChromatogramSetting";

        internal static string IdColumn { get; } = "Id";
        internal static string ConfigurePeakLabelsColumn { get; } = "ConfigurePeakLabels";

        internal static string IsOrientationVerticalColumn { get; } = "IsOrientationVertical";
        internal static string IsSignalUnitInUvColumn { get; } = "IsSignalUnitInUv";
        internal static string IsTimeUnitInMinuteColumn { get; } = "IsTimeUnitInMinute";
        internal static string IsRescalePlotSignalToFullColumn { get; } = "IsRescalePlotSignalToFull";
        internal static string IsRescalePlotSignalToMaxYColumn { get; } = "IsRescalePlotSignalToMaxY";
        internal static string IsRescalePlotSignalToCustomColumn { get; } = "IsRescalePlotSignalToCustom";
        internal static string IsRescalePlottimeFullColumn { get; } = "IsRescalePlottimeFull";
        internal static string RescalePlotSignalFromColumn { get; } = "RescalePlotSignalFrom";
        internal static string RescalePlotSignalToColumn { get; } = "RescalePlotSignalTo";
        internal static string RescalePlotTimeFromColumn { get; } = "RescalePlotTimeFrom";
        internal static string RescalePlotTimeToColumn { get; } = "RescalePlotTimeTo";
        
        internal static string CreatedDateColumn { get; } = "CreatedDate";
        internal static string ModifiedDateColumn { get; } = "ModifiedDate";
        internal static string CreatedUserIdColumn { get; } = "CreatedUserId";
        internal static string ModifiedUserIdColumn { get; } = "ModifiedUserId";


        public ChromatogramSetting Load(IDbConnection connection)
        {
            try
            {
                ChromatogramSetting chromatogramSetting = connection.Query<ChromatogramSetting>(
                                                            $"SELECT {IdColumn}," +
                                                            $"{ConfigurePeakLabelsColumn}," +
                                                            $"{IsOrientationVerticalColumn}," +
                                                            $"{IsSignalUnitInUvColumn}," +
                                                            $"{IsTimeUnitInMinuteColumn}," +
                                                            $"{IsRescalePlotSignalToFullColumn}," +
                                                            $"{IsRescalePlotSignalToMaxYColumn}," +
                                                            $"{IsRescalePlotSignalToCustomColumn}," +
                                                            $"{IsRescalePlottimeFullColumn}," +
                                                            $"{RescalePlotSignalFromColumn}," +
                                                            $"{RescalePlotSignalToColumn}," +
                                                            $"{RescalePlotTimeFromColumn}," +
                                                            $"{RescalePlotTimeToColumn}," +
                                                            $"{CreatedDateColumn}," +
                                                            $"{CreatedUserIdColumn}," +
                                                            $"{ModifiedDateColumn}," +
                                                            $"{ModifiedUserIdColumn} " +
                                                            $"FROM {TableName}").FirstOrDefault();

                                                            return chromatogramSetting;
            }
            catch (Exception ex)
            {
                Log.Error($"Error in Load method", ex);
                throw;
            }
        }

        public bool Save(IDbConnection connection, ChromatogramSetting chromatogramSetting)
        {
            try
            {
                string sql = string.Empty;
                int count = connection.QueryFirstOrDefault<int>(
                    $"SELECT COUNT(*) " +
                    $"FROM {TableName} ");
                if (count <= 0)
                {

                    sql = $"Insert into {TableName}({ConfigurePeakLabelsColumn},{IsOrientationVerticalColumn},{IsSignalUnitInUvColumn},{IsTimeUnitInMinuteColumn}," +
                                 $"{IsRescalePlotSignalToFullColumn},{IsRescalePlotSignalToMaxYColumn},{IsRescalePlotSignalToCustomColumn},{IsRescalePlottimeFullColumn}," +
                                 $"{RescalePlotSignalFromColumn},{RescalePlotSignalToColumn},{RescalePlotTimeFromColumn},{RescalePlotTimeToColumn}) " +
                                 $"Values(@{ConfigurePeakLabelsColumn},@{IsOrientationVerticalColumn},@{IsSignalUnitInUvColumn},@{IsTimeUnitInMinuteColumn}," +
                                 $"@{IsRescalePlotSignalToFullColumn},@{IsRescalePlotSignalToMaxYColumn},@{IsRescalePlotSignalToCustomColumn},@{IsRescalePlottimeFullColumn}," +
                                 $"@{RescalePlotSignalFromColumn},@{RescalePlotSignalToColumn},@{RescalePlotTimeFromColumn},@{RescalePlotTimeToColumn}) RETURNING Id";
                    chromatogramSetting.Id = connection.ExecuteScalar<long>(sql, chromatogramSetting);
                }
                else
                {
                    sql = $"Update {TableName} set " +
                                 $"{ConfigurePeakLabelsColumn} = '{chromatogramSetting.ConfigurePeakLabels}',{IsOrientationVerticalColumn}={chromatogramSetting.IsOrientationVertical},{IsSignalUnitInUvColumn} ={chromatogramSetting.IsSignalUnitInUv}," +
                                 $"{IsTimeUnitInMinuteColumn} ={chromatogramSetting.IsTimeUnitInMinute},{IsRescalePlotSignalToFullColumn}={chromatogramSetting.IsRescalePlotSignalToFull}," +
                                 $"{IsRescalePlotSignalToMaxYColumn} ={chromatogramSetting.IsRescalePlotSignalToMaxY},{IsRescalePlotSignalToCustomColumn}={chromatogramSetting.IsRescalePlotSignalToCustom}," +
                                 $"{IsRescalePlottimeFullColumn} = {chromatogramSetting.IsRescalePlottimeFull},{RescalePlotSignalFromColumn} = {chromatogramSetting.RescalePlotSignalFrom}," +
                                 $"{RescalePlotSignalToColumn} ={chromatogramSetting.RescalePlotSignalTo},{RescalePlotTimeFromColumn}={chromatogramSetting.RescalePlotTimeFrom},{RescalePlotTimeToColumn} ={chromatogramSetting.RescalePlotTimeTo} RETURNING Id";

                    chromatogramSetting.Id = connection.Execute(sql);

                }

               
                return chromatogramSetting.Id > 0;
            }
            catch (Exception ex)
            {
                Log.Error($"Error in Save method", ex);
                throw;
            }
        }




    }
}
