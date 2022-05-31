using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using PerkinElmer.Simplicity.Data.Version16.Contract.DataEntities.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.Chromatography
{
	internal class ManualOverrideIntegrationEventDao : IntegrationEventBaseDao
	{
 		public static string TableName { get; } = "ManualOverrideIntegrationEvent";
		public static string ManualOverrideMapIdColumn { get; } = "ManualOverrideMapId";

		protected readonly string InsertSql =
			$"INSERT INTO {TableName} " +
			$"({ManualOverrideMapIdColumn}," +
			$"{EventTypeColumn}," +
			$"{EventIdColumn}," +
			$"{StartTimeColumn}," +
			$"{EndTimeColumn}," +
			$"{ValueColumn}) " +
			"VALUES " +
			$"(@{ManualOverrideMapIdColumn}," +
			$"@{EventTypeColumn}," +
			$"@{EventIdColumn}," +
			$"@{StartTimeColumn}," +
			$"@{EndTimeColumn}," +
			$"@{ValueColumn})";

		public List<ManualOverrideIntegrationEvent> GetIntegrationEventsByManualOverrideMapId(IDbConnection connection, long manualOverrideMapId)
		{
			try
			{
				var integrationEvents = connection.Query<ManualOverrideIntegrationEvent>(
					$"SELECT {IdColumn}," +
					$"{ManualOverrideMapIdColumn}," +
					$"{EventTypeColumn}," +
					$"{EventIdColumn}," +
					$"{StartTimeColumn}," +
					$"{EndTimeColumn}," +
					$"{ValueColumn} " +
					$"FROM {TableName} " +
					$"WHERE {ManualOverrideMapIdColumn} = {manualOverrideMapId}").ToList();

				return integrationEvents;
			}
			catch (Exception ex)
			{
				Log.Error("Error in GetIntegrationEventsByManualOverrideMapId method", ex);
				throw;
			}

		}

		public virtual void Create(IDbConnection connection, List<ManualOverrideIntegrationEvent> integrationEvents)
		{
			try
			{
				connection.Execute(InsertSql, integrationEvents);
			}
			catch (Exception ex)
			{
				Log.Error("Error in Create method", ex);
				throw;
			}
		}

		public void Delete(IDbConnection connection, long manualOverrideMapId)
		{
			connection.Execute($"DELETE FROM {TableName} " +
			                   $"WHERE {ManualOverrideMapIdColumn} = {manualOverrideMapId}");
		}
	}
}
