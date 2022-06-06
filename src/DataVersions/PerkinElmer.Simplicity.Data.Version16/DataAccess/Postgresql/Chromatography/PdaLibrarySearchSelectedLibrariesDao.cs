using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Dapper;
using log4net;
using PerkinElmer.Simplicity.Data.Version16.Contract.DataEntities.Chromatography.ProcessingMethod;

namespace PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.Chromatography
{
	internal class PdaLibrarySearchSelectedLibrariesDao
	{
		protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		internal static string TableName { get; } = "PdaLibrarySearchSelectedLibraries";
		internal static string IdColumn { get; } = "Id";
		internal static string PdaLibrarySearchParameterIdColumn { get; } = "PdaLibrarySearchParameterId";
		internal static string SelectedLibrariesColumn { get; } = "SelectedLibraries";
		

		protected readonly string InsertSql = $"INSERT INTO {TableName} " +
		                                      $"({PdaLibrarySearchParameterIdColumn}," +
		                                      $"{SelectedLibrariesColumn}) " +
		                                      "VALUES " +
		                                      $"(@{PdaLibrarySearchParameterIdColumn}," +
											  $"@{SelectedLibrariesColumn}) ";

		protected readonly string SelectSql = $"SELECT {IdColumn}," +
		                                      $"{PdaLibrarySearchParameterIdColumn}," +
		                                      $"{SelectedLibrariesColumn} ";
		

		internal virtual void Create(IDbConnection connection, List<PdaLibrarySearchSelectedLibraries> pdaLibrarySearchSelectedLibraries)
		{
			
			try
			{
				if (pdaLibrarySearchSelectedLibraries.Count > 0)
				{
					foreach (var pdaLibrarySearchSelectedLibrary in pdaLibrarySearchSelectedLibraries)
					{
						pdaLibrarySearchSelectedLibrary.Id = connection.ExecuteScalar<long>(InsertSql + $"RETURNING {IdColumn}", pdaLibrarySearchSelectedLibrary);
					}
				}
			}
			catch (Exception ex)
			{
				Log.Error($"Error occured in Create()", ex);
				throw;
			}
		}

		public List<PdaLibrarySearchSelectedLibraries> GetByPdaLibrarySearchParameterId(IDbConnection connection, long pdaLibrarySearchParameterId)
		{
			try
			{
				return connection.Query<PdaLibrarySearchSelectedLibraries>(
					$"{SelectSql} " +
					$"FROM {TableName} " +
					$"WHERE {PdaLibrarySearchParameterIdColumn} = {pdaLibrarySearchParameterId}").ToList();

			}
			catch (Exception ex)
			{
				Log.Error("Error in GetByPdaLibrarySearchParameterId method", ex);
				throw;
			}
		}
	}
}
