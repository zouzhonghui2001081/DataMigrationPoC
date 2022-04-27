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
	internal class InvalidAmountsDao
	{
		protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		public static string TableName { get; } = "InvalidAmounts";
		public static string IdColumn { get; } = "Id";
		public static string CompoundCalibrationResultIdColumn { get; } = "CompoundCalibrationResultsId";
		public static string AmountColumn { get; } = "Amount";

		protected readonly string InsertSql = $"INSERT INTO {TableName} " +
		                                      $"({CompoundCalibrationResultIdColumn}," +
		                                      $"{AmountColumn}) " +
		                                      $"VALUES (@{CompoundCalibrationResultIdColumn}," +
		                                      $"@{AmountColumn}) ";

		protected readonly string SelectSql =
			$"SELECT {IdColumn}," +
			$"{CompoundCalibrationResultIdColumn}," +
			$"{AmountColumn} " +
			$"FROM {TableName} ";

		public virtual void Create(IDbConnection connection, List<InvalidAmounts> invalidAmountsBatchResults)
		{
			try
			{
				connection.Execute(InsertSql, invalidAmountsBatchResults);
			}
			catch (Exception ex)
			{
				Log.Error($"Error occured in CreateChildren()", ex);
				throw;
			}
		}

		public virtual List<InvalidAmounts> GetInvalidAmounts(IDbConnection connection, long compoundCalibrationResultId)
		{
			try
			{
				return connection.Query<InvalidAmounts>(
					SelectSql +
					$"WHERE {CompoundCalibrationResultIdColumn} = {compoundCalibrationResultId}").ToList();
			}
			catch (Exception ex)
			{
				Log.Error($"Error occured in GetInvalidAmounts()", ex);
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
						$"WHERE {TableName}.{CompoundCalibrationResultIdColumn} = {compoundCalibrationResultId}");
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
