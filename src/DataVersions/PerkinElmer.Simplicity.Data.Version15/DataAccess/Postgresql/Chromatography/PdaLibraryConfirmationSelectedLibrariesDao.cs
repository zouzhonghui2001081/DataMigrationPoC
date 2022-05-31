using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Dapper;
using log4net;
using PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography.ProcessingMethod;

namespace PerkinElmer.Simplicity.Data.Version15.DataAccess.Postgresql.Chromatography
{
	internal class PdaLibraryConfirmationSelectedLibrariesDao
	{
		protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		internal static string TableName { get; } = "PdaLibraryConfirmationSelectedLibraries";
		internal static string IdColumn { get; } = "Id";
		internal static string PdaLibraryConfirmationParameterIdColumn { get; } = "PdaLibraryConfirmationParameterId";
		internal static string SelectedLibrariesColumn { get; } = "SelectedLibraries";

		protected readonly string SelectSql = $"SELECT {IdColumn}," +
											  $"{PdaLibraryConfirmationParameterIdColumn}," +
											  $"{SelectedLibrariesColumn} ";

		protected readonly string InsertSql = $"INSERT INTO {TableName} " +
		                                      $"({PdaLibraryConfirmationParameterIdColumn}," +
		                                      $"{SelectedLibrariesColumn}) " +
		                                      "VALUES " +
		                                      $"(@{PdaLibraryConfirmationParameterIdColumn}," +
		                                      $"@{SelectedLibrariesColumn}) ";

		internal virtual void Create(IDbConnection connection, List<PdaLibraryConfirmationSelectedLibraries> pdaLibraryConfirmationSelectedLibraries)
		{

			try
			{
				if (pdaLibraryConfirmationSelectedLibraries.Count > 0)
				{
					foreach (var pdaLibraryConfirmationSelectedLibrary in pdaLibraryConfirmationSelectedLibraries)
					{
						pdaLibraryConfirmationSelectedLibrary.Id = connection.ExecuteScalar<long>(InsertSql + "RETURNING Id", pdaLibraryConfirmationSelectedLibrary);
					}
				}
			}
			catch (Exception ex)
			{
				Log.Error($"Error occured in Create()", ex);
				throw;
			}
		}

		public List<PdaLibraryConfirmationSelectedLibraries> GetByPdaLibraryConfirmationParameterId(IDbConnection connection, long pdaLibraryConfirmationParameterId)
		{
			try
			{
				return connection.Query<PdaLibraryConfirmationSelectedLibraries>(
					$"{SelectSql} " +
					$"FROM {TableName} " +
					$"WHERE {PdaLibraryConfirmationParameterIdColumn} = {pdaLibraryConfirmationParameterId}").ToList();

			}
			catch (Exception ex)
			{
				Log.Error("Error in GetByPdaLibraryConfirmationParameterId method", ex);
				throw;
			}
		}
	}
}
