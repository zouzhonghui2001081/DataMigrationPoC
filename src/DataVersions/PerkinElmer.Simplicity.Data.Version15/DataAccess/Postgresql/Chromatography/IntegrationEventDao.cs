using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography.ProcessingMethod;

namespace PerkinElmer.Simplicity.Data.Version15.DataAccess.Postgresql.Chromatography
{
	internal class IntegrationEventDao : IntegrationEventBaseDao
	{
        public static string TableName { get; } = "IntegrationEvent";
		public static string ChannelMethodIdColumn { get; } = "ChannelMethodId";

		protected readonly string Select = $"SELECT {IdColumn}," +
		                                   $"{ChannelMethodIdColumn}," +
		                                   $"{EventTypeColumn}," +
		                                   $"{EventIdColumn}," +
		                                   $"{StartTimeColumn}," +
		                                   $"{EndTimeColumn}," +
		                                   $"{ValueColumn} " +
		                                   $"FROM {TableName} ";

		protected readonly string InsertInto = $"INSERT INTO {TableName} " +
		                                       $"({ChannelMethodIdColumn}," +
		                                       $"{EventTypeColumn}," +
		                                       $"{EventIdColumn}," +
		                                       $"{StartTimeColumn}," +
		                                       $"{EndTimeColumn}," +
		                                       $"{ValueColumn}) " +
		                                       "VALUES " +
		                                       $"(@{ChannelMethodIdColumn}," +
		                                       $"@{EventTypeColumn}," +
		                                       $"@{EventIdColumn}," +
		                                       $"@{StartTimeColumn}," +
		                                       $"@{EndTimeColumn}," +
		                                       $"@{ValueColumn}) ";

		public List<IntegrationEvent> GetIntegrationEventsByChannelMethodId(IDbConnection connection, long channelMethodId)
		{
			try
			{
				List<IntegrationEvent> integrationEvents = connection.Query<IntegrationEvent>(
					Select +
					$"WHERE {ChannelMethodIdColumn} = {channelMethodId}").ToList();

				return integrationEvents;
			}
			catch (Exception ex)
			{
				Log.Error("Error in GetIntegrationEventsByChannelMethodId method", ex);
				throw;
			}

		}

		public virtual void Create(IDbConnection connection, IntegrationEvent integrationEvent)
		{
			try
			{
				integrationEvent.Id = connection.ExecuteScalar<long>(
					InsertInto + "RETURNING Id",
					integrationEvent);
			}
			catch (Exception ex)
			{
				Log.Error("Error in Create method", ex);
				throw;
			}
		}
	}
}
