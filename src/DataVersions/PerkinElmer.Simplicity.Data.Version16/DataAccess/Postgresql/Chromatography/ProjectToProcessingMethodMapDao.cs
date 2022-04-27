using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Dapper;
using log4net;
using PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography.ProcessingMethod;

namespace PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.Chromatography
{
	internal class ProjectToProcessingMethodMapDao
	{
		private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		internal static string TableName { get; } = "ProjectToProcessingMethodMap";
		internal static string IdColumn { get; } = "Id";
		internal static string ProjectIdColumn { get; } = "ProjectId";
		internal static string ProcessingMethodIdColumn { get; } = "ProcessingMethodId";

		public void Create(IDbConnection connection, ProjectToProcessingMethodMap projectToProcessingMethodMap)
		{
			try
			{
				connection.Execute(
					$"INSERT INTO {TableName} ({ProjectIdColumn}," +
					$"{ProcessingMethodIdColumn}) " +
					$"VALUES (@{ProjectIdColumn}," +
					$"@{ProcessingMethodIdColumn})", projectToProcessingMethodMap);
			}
			catch (Exception ex)
			{
				Log.Error("Error in Create method", ex);
				throw;
			}
		}
		public List<ProjectToProcessingMethodMap> GetAll(IDbConnection connection)
		{
			try
			{
				return connection.Query<ProjectToProcessingMethodMap>($"SELECT {IdColumn}," +
				                                                      $"{ProjectIdColumn}," +
				                                                      $"{ProcessingMethodIdColumn} " +
				                                                      $"FROM {TableName} ").ToList();
			}
			catch (Exception ex)
			{
				Log.Error("Error in GetAll method", ex);
				throw;
			}
		}
		public void Delete(IDbConnection connection, long processingMethodId)
		{
			try
			{
				connection.Execute(
					$"DELETE FROM {TableName} " +
					$"WHERE {ProcessingMethodIdColumn} = {processingMethodId}");
			}
			catch (Exception ex)
			{
				Log.Error("Error in Delete method", ex);
				throw;
			}
		}
	}
}
