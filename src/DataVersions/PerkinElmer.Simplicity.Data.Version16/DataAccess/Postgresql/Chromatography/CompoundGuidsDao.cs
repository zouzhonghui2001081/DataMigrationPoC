using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Dapper;
using log4net;
using PerkinElmer.Simplicity.Data.Version16.Contract.DataEntities.Chromatography.CalibrationMethod;

namespace PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.Chromatography
{
	internal class CompoundGuidsDao
	{
		protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		public static string TableName { get; } = "CompoundGuids";
		public static string IdColumn { get; } = "Id";
		public static string CompoundIdColumn { get; } = "CompoundId";
		public static string CompoundGuidColumn { get; } = "CompoundGuid";

		protected readonly string InsertSql = $"INSERT INTO {TableName} " +
		                                      $"({CompoundIdColumn}, " +
		                                      $"{CompoundGuidColumn}) " +
		                                      "VALUES " +
		                                      $"(@{CompoundIdColumn}," +
		                                      $"@{CompoundGuidColumn}) ";

		protected readonly string SelectSql =
			$"SELECT {IdColumn}," +
			$"{CompoundIdColumn}," +
			$"{CompoundGuidColumn} " +
			$"FROM {TableName} ";

		public virtual void Create(IDbConnection connection, List<CompoundGuids> compoundGuidList)
		{
			try
			{
				connection.Execute(InsertSql, compoundGuidList);
			}
			catch (Exception ex)
			{
				Log.Error("Error in CreateChildren method", ex);
				throw;
			}
		}
		public List<CompoundGuids> GetCompondGuidsByCompoundId(IDbConnection connection, long compoundId)
		{
			try
			{
				string sql = 
					SelectSql +
					$"WHERE {CompoundIdColumn} = {compoundId}";

				return connection.Query<CompoundGuids>(sql).ToList();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}
	}
}
