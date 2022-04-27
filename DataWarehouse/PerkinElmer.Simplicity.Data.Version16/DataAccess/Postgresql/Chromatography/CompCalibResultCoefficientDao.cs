using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Dapper;
using log4net;
using PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography.CalibrationMethod;

namespace PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.Chromatography
{
	internal class CompCalibResultCoefficientDao
	{
		protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		public static string TableName { get; } = "CompCalibResultCoefficient";
		public static string IdColumn { get; } = "Id";
		public static string CompoundCalibrationResultsIdColumn { get; } = "CompoundCalibrationResultsId";
		public static string CoefficientsColumn { get; } = "Coefficients";

		protected readonly string InsertSql = $"INSERT INTO {TableName} " +
		                                      $"({CompoundCalibrationResultsIdColumn}," +
		                                      $"{CoefficientsColumn}) " +
		                                      "VALUES " +
		                                      $"(@{CompoundCalibrationResultsIdColumn}," +
		                                      $"@{CoefficientsColumn}) ";

		protected readonly string SelectSql =
			$"SELECT {IdColumn}," +
			$"{CompoundCalibrationResultsIdColumn}," +
			$"{CoefficientsColumn} " +
			$"FROM {TableName} ";

		public virtual void Create(IDbConnection connection, List<CompCalibResultCoefficient> compCalibResultCoeffs)
		{
			try
			{
				connection.Execute(InsertSql, compCalibResultCoeffs);
			}
			catch (Exception ex)
			{
				Log.Error($"Error occured in CreateChildren()", ex);
				throw;
			}
		}

		public List<CompCalibResultCoefficient> GetCompCalibResultCoeff(IDbConnection connection, long compoundCalibrationResultId)
		{
			try
			{
				string sql =
					SelectSql +
					$"WHERE {CompoundCalibrationResultsIdColumn} = {compoundCalibrationResultId}";

				return connection.Query<CompCalibResultCoefficient>(sql).ToList();
			}
			catch (Exception ex)
			{
				Log.Error($"Error occured in GetCompCalibResultCoeff()", ex);
				throw;
			}
		}

		public bool Delete(IDbConnection connection, List<long> compoundCalibrationResultIds)
		{
			try
			{
				foreach (var compoundCalibrationResultId in compoundCalibrationResultIds)
				{
					var count = connection.Execute(
						$"DELETE FROM {TableName} " +
						$"WHERE {TableName}.{CompoundCalibrationResultsIdColumn} = {compoundCalibrationResultId}");
				}

				return true;
			}
			catch (Exception ex)
			{
				Log.Error($"Error in Delete method", ex);
				throw;
			}
		}
	}
}
