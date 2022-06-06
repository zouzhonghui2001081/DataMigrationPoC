using System;
using System.Data;
using System.Reflection;
using Dapper;
using log4net;
using PerkinElmer.Simplicity.Data.Version16.Contract.DataEntities.Chromatography.ProcessingMethod;

namespace PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.Chromatography
{
	internal class SuitabilityMethodDao
	{
		protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		internal static string TableName { get; } = "SuitabilityMethod";
		internal static string IdColumn { get; } = "Id";
		internal static string ProcessingMethodIdColumn { get; } = "ProcessingMethodId";
		internal static string EnabledColumn { get; } = "Enabled";
		internal static string SelectedPharmacopeiaTypeColumn { get; } = "SelectedPharmacopeiaType";
		internal static string IsEfficiencyInPlatesColumn { get; } = "IsEfficiencyInPlates";
		internal static string ColumnLengthColumn { get; } = "ColumnLength";
		internal static string SignalToNoiseWindowStartColumn { get; } = "SignalToNoiseWindowStart";
		internal static string SignalToNoiseWindowEndColumn { get; } = "SignalToNoiseWindowEnd";
		internal static string SignalToNoiseEnabledColumn { get; } = "SignalToNoiseEnabled";
        internal static string PerformAdditionalSearchForNoiseWindowColumn { get; } = "PerformAdditionalSearchForNoiseWindow";
        internal static string AnalyzeAdjacentPeaksColumn { get; } = "AnalyzeAdjacentPeaks";
		internal static string CompoundPharmacopeiaDefinitionsColumn { get; } = "CompoundPharmacopeiaDefinitions";
		internal static string VoidTimeTypeColumn { get; } = "VoidTimeType";
		internal static string VoidTimeCustomValueInSecondsColumn { get; } = "VoidTimeCustomValueInSeconds";

        protected readonly string InsertSql =
			$"INSERT INTO {TableName} " +
			"(" +
			$"{ProcessingMethodIdColumn}," +
			$"{EnabledColumn}," +
			$"{SelectedPharmacopeiaTypeColumn}," +
			$"{IsEfficiencyInPlatesColumn}," +
			$"{ColumnLengthColumn}," +
			$"{SignalToNoiseWindowStartColumn}," +
			$"{SignalToNoiseWindowEndColumn}," +
			$"{SignalToNoiseEnabledColumn}," +
			$"{AnalyzeAdjacentPeaksColumn}," +
			$"{VoidTimeTypeColumn}," +
			$"{VoidTimeCustomValueInSecondsColumn}," +
            $"{PerformAdditionalSearchForNoiseWindowColumn}," +
            $"{CompoundPharmacopeiaDefinitionsColumn}" +
            ")" +
			"VALUES (" +
			$"@{ProcessingMethodIdColumn}," +
			$"@{EnabledColumn}," +
			$"@{SelectedPharmacopeiaTypeColumn}," +
			$"@{IsEfficiencyInPlatesColumn}," +
			$"@{ColumnLengthColumn}," +
			$"@{SignalToNoiseWindowStartColumn}," +
			$"@{SignalToNoiseWindowEndColumn}," +
			$"@{SignalToNoiseEnabledColumn}," +
			$"@{AnalyzeAdjacentPeaksColumn}," +
			$"@{VoidTimeTypeColumn}," +
			$"@{VoidTimeCustomValueInSecondsColumn}," +
            $"@{PerformAdditionalSearchForNoiseWindowColumn}," +
            $"@{CompoundPharmacopeiaDefinitionsColumn}) ";

		protected readonly string SelectSql =
			$"SELECT {IdColumn}," +
			$"{ProcessingMethodIdColumn}," +
			$"{EnabledColumn}," +
			$"{SelectedPharmacopeiaTypeColumn}," +
			$"{IsEfficiencyInPlatesColumn}," +
			$"{ColumnLengthColumn}," +
			$"{SignalToNoiseWindowStartColumn}," +
			$"{SignalToNoiseWindowEndColumn}," +
			$"{SignalToNoiseEnabledColumn}," +
			$"{AnalyzeAdjacentPeaksColumn}," +
			$"{VoidTimeTypeColumn}," +
			$"{VoidTimeCustomValueInSecondsColumn}," +
            $"{PerformAdditionalSearchForNoiseWindowColumn}," +
            $"{CompoundPharmacopeiaDefinitionsColumn} " +
			$"FROM {TableName} ";

		public void Create(IDbConnection connection, SuitabilityMethod suitabilityMethod)
		{
			try
			{
				connection.Execute(InsertSql, suitabilityMethod);
			}
			catch (Exception ex)
			{
				Log.Error($"Error in Create method", ex);
				throw;
			}
		}
		public SuitabilityMethod Get(IDbConnection connection, long processingMethodId)
		{
			try
			{
				return connection.QueryFirstOrDefault<SuitabilityMethod>(
					SelectSql + $"WHERE {ProcessingMethodIdColumn} = @ProcessingMethodId",
					new { ProcessingMethodId  = processingMethodId});
			}
			catch (Exception ex)
			{
				Log.Error($"Get in Create method", ex);
				throw;
			}
		}
		public void Delete(IDbConnection connection, long processingMethodId)
		{
			try
			{
				connection.Execute(
					$"DELETE FROM {TableName} " +
					$"WHERE {ProcessingMethodIdColumn} = @ProcessingMethodId",
					new { ProcessingMethodId = processingMethodId});
			}
			catch (Exception ex)
			{
				Log.Error($"Get in Delete method", ex);
				throw;
			}
		}
	}
}
