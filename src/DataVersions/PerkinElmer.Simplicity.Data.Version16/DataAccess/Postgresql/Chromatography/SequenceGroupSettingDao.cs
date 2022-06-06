using System;
using System.Data;
using System.Reflection;
using Dapper;
using log4net;
using PerkinElmer.Simplicity.Data.Version16.Contract.DataEntities.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.Chromatography
{
    internal class SequenceGroupSettingDao
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        internal static string TableName { get; } = "SequenceGroupSetting";
        internal static string IdColumn { get; } = "Id";
        internal static string ExportPathColumn { get; } = "ExportPath";
        internal static string ReportGroupsColumn { get; } = "ReportGroups";
        internal static string ProjectIdColumn { get; } = "ProjectId";
        internal static string IsGlobalColumn { get; } = "IsGlobal";
        internal static string IsDefaultColumn { get; } = "IsDefault";
        
        private readonly string _insertInto = 
            $"INSERT INTO {TableName} " +
            $"({ExportPathColumn}," +
            $"{ReportGroupsColumn}," +
            $"{ProjectIdColumn}," +
            $"{IsGlobalColumn}," +
            $"{IsDefaultColumn}) " +
            $"Values(@{ExportPathColumn}," +
            $" @{ReportGroupsColumn}," +
            $"@{ProjectIdColumn}," +
            $"@{IsGlobalColumn}," +
            $" @{IsDefaultColumn}) RETURNING Id";

        public SequenceGroupSettingEntity Load(IDbConnection connection, long? projectId)
        {
            try
            {
                SequenceGroupSettingEntity sequenceGroupSetting = connection.QueryFirstOrDefault<SequenceGroupSettingEntity>(
                                                            $"SELECT {IdColumn}," +
                                                            $"{ExportPathColumn}," +
                                                            $"{ReportGroupsColumn}," +
                                                            $"{ProjectIdColumn}," +
                                                            $"{IsGlobalColumn}," +
                                                            $"{IsDefaultColumn}" +
                                                            $" FROM {TableName} Where {ProjectIdColumn} = {projectId}");
                return sequenceGroupSetting;
            }
            catch (Exception ex)
            {
                Log.Error($"Error in Load method", ex);
                throw;
            }
        }
        public bool Save(IDbConnection connection, SequenceGroupSettingEntity sequenceGroupSetting)
        {
            try
            {
                int count = connection.QueryFirstOrDefault<int>(
                    $"SELECT COUNT(*) " +
                    $"FROM {TableName}  Where {IdColumn}= {sequenceGroupSetting.Id}");
                if (count <= 0)
                {
                    sequenceGroupSetting.Id = connection.ExecuteScalar<long>(_insertInto, sequenceGroupSetting);
                }
                else
                {
                    connection.ExecuteScalar(
                    $"UPDATE {TableName} SET {ExportPathColumn} = @{ExportPathColumn},{ReportGroupsColumn} = @{ReportGroupsColumn}, " +
                    $" {IsGlobalColumn} = @{IsGlobalColumn}, {IsDefaultColumn} = @{IsDefaultColumn}" +
                    $" WHERE {IdColumn} = @{IdColumn}", sequenceGroupSetting);

                }
                return sequenceGroupSetting.Id > 0;
            }
            catch (Exception ex)
            {
                Log.Error($"Error in Save method", ex);
                throw;
            }
        }

        public bool Create(IDbConnection connection, SequenceGroupSettingEntity sequenceGroupSetting)
        {
            try
            {
                sequenceGroupSetting.Id = connection.ExecuteScalar<long>(_insertInto, sequenceGroupSetting);
                return sequenceGroupSetting.Id > 0;
            }
            catch (Exception ex)
            {
                Log.Error($"Error in Create method", ex);
                throw;
            }
        }

        public bool Delete(IDbConnection connection, long projectId)
        {
            try
            {
                var rowsDeleted = connection.Execute(
                    $"DELETE FROM {TableName} " +
                    $"WHERE {ProjectIdColumn} = @ProjectId",
                    new {ProjectId = projectId});
                
                return rowsDeleted > 0;
            }
            catch (Exception ex)
            {
                Log.Error($"Error in Delete method", ex);
                throw;
            }
        }
    }
}
