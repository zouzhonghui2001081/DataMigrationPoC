using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Dapper;
using log4net;
using PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.AuditTrail;

namespace PerkinElmer.Simplicity.Data.Version15.DataAccess.Postgresql.AuditTrail
{
	internal static class AuditTrailLogsTableDefinition
	{
		internal static string TableName { get; } = "AuditTrailLogs";

		internal static string IdColumn { get; } = "Id";

		internal const string LogTimeColumn = "LogTime";

		internal static string ScopeTypeColumn { get; } = "ScopeType";

		internal static string RecordTypeColumn { get; } = "RecordType";

		internal static string EntityIdColumn { get; } = "EntityId";

		internal static string EntityTypeColumn { get; } = "EntityType";

		internal static string ActionTypeIdColumn { get; } = "ActionTypeId";

		internal static string ActionTypeColumn { get; } = "ActionType";

		internal static string ActionDescriptionColumn { get; } = "ActionDescription";

		internal static string ItemIdColumn { get; } = "ItemId";

		internal static string ItemNameColumn { get; } = "ItemName";

		internal static string ItemTypeColumn { get; } = "ItemType";

		internal static string ItemVersionIdColumn { get; } = "ItemVersionId";

		internal static string UserIdColumn { get; } = "UserId";

		internal static string UserLoginColumn { get; } = "UserLogin";

		internal static string UserFullNameColumn { get; } = "UserFullName";

		internal static string UserRoleIdColumn { get; } = "UserRoleId";

		internal static string UserRoleColumn { get; } = "UserRole";

		internal static string ProjectIdColumn { get; } = "ProjectId";

		internal static string ProjectColumn { get; } = "ProjectName";

		internal static string WorkstationIdColumn { get; } = "WorkstationId";

		internal static string WorkstationNameColumn { get; } = "WorkstationName";

		internal static string InstrumentIdColumn { get; } = "InstrumentId";

		internal static string InstrumentNameColumn { get; } = "InstrumentName";

		internal static string JustificationColumn { get; } = "Justification";

		internal static string JustificationTimestampColumn { get; } = "JustificationTimestamp";

		internal static string CommentColumn { get; } = "Comment";
	}

	internal static class SqlAuditTrailLogs
	{
		internal static string SqlSelect =
			$"SELECT {AuditTrailLogsTableDefinition.IdColumn}," +
			$"{AuditTrailLogsTableDefinition.LogTimeColumn}," +
			$"{AuditTrailLogsTableDefinition.ScopeTypeColumn}, " +
			$"{AuditTrailLogsTableDefinition.RecordTypeColumn}, " +
			$"{AuditTrailLogsTableDefinition.ActionTypeIdColumn}," +
			$"{AuditTrailLogsTableDefinition.ActionTypeColumn}," +
			$"{AuditTrailLogsTableDefinition.ActionDescriptionColumn}," +
			$"{AuditTrailLogsTableDefinition.EntityIdColumn}," +
			$"{AuditTrailLogsTableDefinition.EntityTypeColumn}," +
			$"{AuditTrailLogsTableDefinition.ItemIdColumn}," +
			$"{AuditTrailLogsTableDefinition.ItemNameColumn}, " +
			$"{AuditTrailLogsTableDefinition.ItemTypeColumn}, " +
			$"{AuditTrailLogsTableDefinition.ItemVersionIdColumn}, " +
			$"{AuditTrailLogsTableDefinition.UserIdColumn}," +
			$"{AuditTrailLogsTableDefinition.UserLoginColumn}," +
			$"{AuditTrailLogsTableDefinition.UserFullNameColumn}," +
			$"{AuditTrailLogsTableDefinition.UserRoleIdColumn}," +
			$"{AuditTrailLogsTableDefinition.UserRoleColumn}," +
			$"{AuditTrailLogsTableDefinition.ProjectIdColumn}," +
			$"{AuditTrailLogsTableDefinition.ProjectColumn}," +
			$"{AuditTrailLogsTableDefinition.WorkstationIdColumn}," +
			$"{AuditTrailLogsTableDefinition.WorkstationNameColumn}, " +
			$"{AuditTrailLogsTableDefinition.InstrumentIdColumn}, " +
			$"{AuditTrailLogsTableDefinition.InstrumentNameColumn}, " +
			$"{AuditTrailLogsTableDefinition.JustificationColumn}, " +
			$"{AuditTrailLogsTableDefinition.JustificationTimestampColumn}," +
			$"{AuditTrailLogsTableDefinition.CommentColumn} ";

		internal static string SqlSelectFromTable =
			SqlSelect +
			$"FROM {AuditTrailLogsTableDefinition.TableName} ";

		internal static string SqlSelectByEntity =
			SqlSelectFromTable +
			$"where lower({AuditTrailLogsTableDefinition.EntityIdColumn}) = lower('{{0}}') " +
			$"and lower({AuditTrailLogsTableDefinition.EntityTypeColumn}) = lower('{{1}}') ";

		internal static string SqlSelectById =
			SqlSelectFromTable +
			$"where {AuditTrailLogsTableDefinition.IdColumn} =  '{{0}}' ";


		internal static string SqlInsert =
			$"INSERT INTO {AuditTrailLogsTableDefinition.TableName} " +
			"(" +
			$"{AuditTrailLogsTableDefinition.LogTimeColumn}, " +
			$"{AuditTrailLogsTableDefinition.ScopeTypeColumn}, " +
			$"{AuditTrailLogsTableDefinition.RecordTypeColumn}, " +
			$"{AuditTrailLogsTableDefinition.EntityIdColumn}," +
			$"{AuditTrailLogsTableDefinition.EntityTypeColumn}," +
			$"{AuditTrailLogsTableDefinition.ActionTypeIdColumn}," +
			$"{AuditTrailLogsTableDefinition.ActionTypeColumn}," +
			$"{AuditTrailLogsTableDefinition.ActionDescriptionColumn}," +
			$"{AuditTrailLogsTableDefinition.ItemIdColumn}," +
			$"{AuditTrailLogsTableDefinition.ItemNameColumn}, " +
			$"{AuditTrailLogsTableDefinition.ItemTypeColumn}, " +
			$"{AuditTrailLogsTableDefinition.ItemVersionIdColumn}, " +
			$"{AuditTrailLogsTableDefinition.UserIdColumn}," +
			$"{AuditTrailLogsTableDefinition.UserLoginColumn}," +
			$"{AuditTrailLogsTableDefinition.UserFullNameColumn}," +
			$"{AuditTrailLogsTableDefinition.UserRoleIdColumn}," +
			$"{AuditTrailLogsTableDefinition.UserRoleColumn}," +
			$"{AuditTrailLogsTableDefinition.ProjectIdColumn}," +
			$"{AuditTrailLogsTableDefinition.ProjectColumn}," +
			$"{AuditTrailLogsTableDefinition.WorkstationIdColumn}," +
			$"{AuditTrailLogsTableDefinition.WorkstationNameColumn}," +
			$"{AuditTrailLogsTableDefinition.InstrumentIdColumn}," +
			$"{AuditTrailLogsTableDefinition.InstrumentNameColumn}," +
			$"{AuditTrailLogsTableDefinition.JustificationColumn}, " +
			$"{AuditTrailLogsTableDefinition.JustificationTimestampColumn}," +
			$"{AuditTrailLogsTableDefinition.CommentColumn} )" +
			"Values( " +
			$"@{AuditTrailLogsTableDefinition.LogTimeColumn}," +
			$"@{AuditTrailLogsTableDefinition.ScopeTypeColumn}," +
			$"@{AuditTrailLogsTableDefinition.RecordTypeColumn}," +
			$"@{AuditTrailLogsTableDefinition.EntityIdColumn}," +
			$"@{AuditTrailLogsTableDefinition.EntityTypeColumn}," +
			$"@{AuditTrailLogsTableDefinition.ActionTypeIdColumn}," +
			$"@{AuditTrailLogsTableDefinition.ActionTypeColumn}," +
			$"@{AuditTrailLogsTableDefinition.ActionDescriptionColumn}," +
			$"@{AuditTrailLogsTableDefinition.ItemIdColumn}," +
			$"@{AuditTrailLogsTableDefinition.ItemNameColumn}," +
			$"@{AuditTrailLogsTableDefinition.ItemTypeColumn}," +
			$"@{AuditTrailLogsTableDefinition.ItemVersionIdColumn}," +
			$"@{AuditTrailLogsTableDefinition.UserIdColumn}," +
			$"@{AuditTrailLogsTableDefinition.UserLoginColumn}," +
			$"@{AuditTrailLogsTableDefinition.UserFullNameColumn}," +
			$"@{AuditTrailLogsTableDefinition.UserRoleIdColumn}," +
			$"@{AuditTrailLogsTableDefinition.UserRoleColumn}," +
			$"@{AuditTrailLogsTableDefinition.ProjectIdColumn}," +
			$"@{AuditTrailLogsTableDefinition.ProjectColumn}," +
			$"@{AuditTrailLogsTableDefinition.WorkstationIdColumn}," +
			$"@{AuditTrailLogsTableDefinition.WorkstationNameColumn}," +
			$"@{AuditTrailLogsTableDefinition.InstrumentIdColumn}," +
			$"@{AuditTrailLogsTableDefinition.InstrumentNameColumn}," +
			$"@{AuditTrailLogsTableDefinition.JustificationColumn}," +
			$"@{AuditTrailLogsTableDefinition.JustificationTimestampColumn}," +
			$"@{AuditTrailLogsTableDefinition.CommentColumn} ) ";

		internal static string SqlInsertAndReturnId =
			SqlInsert + $"RETURNING {AuditTrailLogsTableDefinition.IdColumn} ";

		internal static string UpdateVersionInfoById =
			$"update {AuditTrailLogsTableDefinition.TableName} " +
			"set " +
			$"{AuditTrailLogsTableDefinition.EntityIdColumn} = @{AuditTrailLogsTableDefinition.EntityIdColumn}, " +
			$"{AuditTrailLogsTableDefinition.EntityTypeColumn} = @{AuditTrailLogsTableDefinition.EntityTypeColumn}, " +
			$"{AuditTrailLogsTableDefinition.ItemVersionIdColumn} = @{AuditTrailLogsTableDefinition.ItemVersionIdColumn} " +
			$"where {AuditTrailLogsTableDefinition.IdColumn} = '{{0}}' ";
	}

	internal class AuditTrailDao
	{
		internal static string FullCountColumn { get; } = "FullCount";

		private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		internal (IList<AuditTrailLogEntry>, long) GetAll(IDbConnection connection, int limit, long offset, string sortField, string sortDirection, string searchText = null, IList<Filter> filterList = null)
		{
			Log.Info($"GetAll(limit:{limit}, offset:{offset}) called");
			try
			{
				var filterParams = CreateFilterParameterDictionary(filterList, limit, offset);
				var whereClause = string.Empty;

				if (filterList != null)
				{
					var valueIndex = 0;

					for (var i = 0; i < filterList.Count; i++)
					{
						switch (filterList[i].FilterType)
						{
							case FilterType.Range:
								{
									whereClause += $" AND {filterParams[$"column_{valueIndex}"]} BETWEEN @fromVal_{valueIndex} AND @toVal_{valueIndex} ";
									valueIndex++;
								}
								break;
							case FilterType.Term:
								{
									TermFilter termFilter = (TermFilter)filterList[i];

									switch (termFilter.Operator)
									{
										case QueryOperators.Equal:
											{
												whereClause += " AND (";
												break;
											}
										case QueryOperators.NotEqual:
											{
												whereClause += " AND NOT (";
												break;
											}
										default:
											throw new InvalidOperationException();
									}

									if (termFilter.Values.Count == 1)
									{
										whereClause += $"{filterParams[$"column_{valueIndex}"]} = @value_{valueIndex}";
										valueIndex++;
									}
									else
									{
										for (int j = 0; j < termFilter.Values.Count; j++)
										{
											if (j != 0)
												whereClause += " OR ";

											whereClause +=
												$"{filterParams[$"column_{valueIndex}"]} = @value_{valueIndex}";

											valueIndex++;
										}
									}
									whereClause += ")";
								}
								break;
						}
					}
				}

				if (!string.IsNullOrEmpty(searchText))
				{
					searchText = "'%" + searchText.ToLower().Replace("'", "''").Replace("%", "\\%").Replace("_", "\\_") + "%'";
					whereClause += " AND( " +
							  $"LOWER({AuditTrailLogsTableDefinition.UserLoginColumn})" + " LIKE " + searchText +
							  $" OR LOWER({AuditTrailLogsTableDefinition.UserFullNameColumn})" + " LIKE " + searchText +
							  $" OR LOWER({AuditTrailLogsTableDefinition.UserRoleColumn})" + " LIKE " + searchText +
							  $" OR LOWER({AuditTrailLogsTableDefinition.ProjectColumn})" + " LIKE " + searchText +
							  $" OR LOWER({AuditTrailLogsTableDefinition.WorkstationNameColumn})" + " LIKE " + searchText +
							  $" OR LOWER({AuditTrailLogsTableDefinition.InstrumentNameColumn})" + " LIKE " + searchText +
							  $" OR LOWER({AuditTrailLogsTableDefinition.CommentColumn})" + " LIKE " + searchText +
							  $" OR LOWER({AuditTrailLogsTableDefinition.JustificationColumn})" + " LIKE " + searchText +
							  $" OR LOWER({AuditTrailLogsTableDefinition.ItemNameColumn})" + " LIKE " + searchText +
							  $" OR LOWER({AuditTrailLogsTableDefinition.ItemTypeColumn})" + " LIKE " + searchText +
							  $" OR LOWER({AuditTrailLogsTableDefinition.ActionTypeColumn})" + " LIKE " + searchText +
							  $" OR LOWER({AuditTrailLogsTableDefinition.ActionDescriptionColumn})" + " LIKE " + searchText +
							  $" OR LOWER({AuditTrailLogsTableDefinition.RecordTypeColumn})" + " LIKE " + searchText +
							  $" OR LOWER({AuditTrailLogsTableDefinition.ScopeTypeColumn})" + " LIKE " + searchText +
							  ")";
				}

				var records = connection.Query<AuditTrailLogEntry>(
				"WITH cte AS(" +
				SqlAuditTrailLogs.SqlSelectFromTable +
				"WHERE 1 = 1" +
				$"{whereClause}) " +
				SqlAuditTrailLogs.SqlSelect +
				$", {FullCountColumn} " +
				"FROM (TABLE cte " +
				$"ORDER BY {sortField} {sortDirection} " +
				"LIMIT @limit OFFSET @offset) sub " +
				$"RIGHT JOIN (SELECT COUNT(1) FROM cte) c({FullCountColumn}) ON true", filterParams).ToList();

				var fullCount = records.First().FullCount;

				if (offset >= fullCount)
				{
					var fullCountRecord = records.FirstOrDefault(n => n.Id == 0);
					if (fullCountRecord != null)
						records.Remove(fullCountRecord);
				}

				return (records, fullCount);

			}
			catch (Exception ex)
			{
				Log.Error("Error in GetAll", ex);
				throw;
			}
		}

		internal IList<AuditTrailLogEntry> GetAuditTrailByEntity(IDbConnection connection, string entityId, string entityType)
		{
			if (string.IsNullOrWhiteSpace(entityId))
				throw new ArgumentException(nameof(entityId));
			if (string.IsNullOrWhiteSpace(entityType))
				throw new ArgumentException(nameof(entityType));

			try
			{
				return connection.Query<AuditTrailLogEntry>(string.Format(SqlAuditTrailLogs.SqlSelectByEntity, entityId, entityType)).ToList();
			}
			catch (Exception ex)
			{
				Log.Error("Error in GetAuditTrailByEntity method", ex);
				throw;
			}
		}

		internal AuditTrailLogEntry GetAuditTrailById(IDbConnection connection, long logEntryId)
		{
			if (logEntryId < 0)
				throw new ArgumentException(nameof(logEntryId));

			try
			{
				return connection.QueryFirstOrDefault<AuditTrailLogEntry>(string.Format(SqlAuditTrailLogs.SqlSelectById, logEntryId));
			}
			catch (Exception ex)
			{
				Log.Error("Error in GetAuditTrailById method", ex);
				throw;
			}
		}

		internal AuditTrailLogEntry CreateLog(IDbConnection connection, AuditTrailLogEntry logEntry)
		{
			Log.Info("CreateLog() called");

			try
			{
				logEntry.Id = connection.ExecuteScalar<long>(
				SqlAuditTrailLogs.SqlInsertAndReturnId, logEntry);
				return logEntry;
			}
			catch (Exception ex)
			{
				Log.Error("Error in CreateLog", ex);
				throw;
			}
		}

		internal IList<AuditTrailLogEntry> CreateLog(IDbConnection connection, IList<AuditTrailLogEntry> logEntryCollection)
		{
			Log.Info("CreateLog() called");

			try
			{
				var insertedLogEntries = new List<AuditTrailLogEntry>();
				foreach (var logEntry in logEntryCollection)
				{
					insertedLogEntries.Add(CreateLog(connection, logEntry));
				}
				return insertedLogEntries;
			}
			catch (Exception ex)
			{
				Log.Error("Error in CreateLog", ex);
				throw;
			}
		}

		internal IList<T> GetDistinctRecords<T>(IDbConnection connection, string columnName, RangeFilter rangeFilter = null)
		{
			string sql;

			if (rangeFilter != null)
			{
				sql = $"SELECT DISTINCT({columnName}) FROM {AuditTrailLogsTableDefinition.TableName} " +
					  $"WHERE {rangeFilter.ColumnName} BETWEEN @From AND @To " +
					  $"AND {columnName} IS NOT NULL;";

				return connection.Query<T>(sql, new { From = rangeFilter.FromValue, To = rangeFilter.ToValue }).ToList();
			}

			sql = $"SELECT DISTINCT({columnName}) FROM {AuditTrailLogsTableDefinition.TableName} " +
				  $"WHERE {columnName} IS NOT NULL;";
			return connection.Query<T>(sql).ToList();
		}

		internal bool UpdateLogEntryVersionInfo(IDbConnection connection, long logEntryId, AuditTrailLogEntry logEntry)
		{
			if (logEntryId < 0 ||
				logEntry == null)
				throw new ArgumentException("Parameter Incorrect!");

			try
			{
				var result = connection.Execute(string.Format(SqlAuditTrailLogs.UpdateVersionInfoById, logEntryId), logEntry);
				return result == 1;
			}
			catch (Exception ex)
			{
				Log.Error("Error in UpdateEntityVersion method", ex);
				throw;
			}
		}

		private Dictionary<string, object> CreateFilterParameterDictionary(IList<Filter> filterList, int limit, long offset)
		{
			var parameterDictionary = new Dictionary<string, object>()
			{
				{ "@limit", limit },
				{ "@offset", offset }
			};

			if (filterList != null)
			{
				int valueIndex = 0;
				for (int i = 0; i < filterList.Count; i++)
				{
					Filter filter = filterList[i];

					switch (filter.FilterType)
					{
						case FilterType.Range:
							{
								parameterDictionary.Add($"column_{valueIndex}", filterList[i].ColumnName);
								parameterDictionary.Add($"@fromVal_{valueIndex}", ((RangeFilter)filter).FromValue);
								parameterDictionary.Add($"@toVal_{valueIndex}", ((RangeFilter)filter).ToValue);
								valueIndex++;
							}
							break;
						case FilterType.Term:
							{
								for (int j = 0; j < ((TermFilter)filter).Values.Count; j++)
								{
									parameterDictionary.Add($"column_{valueIndex}", filterList[i].ColumnName);
									parameterDictionary.Add($"@value_{valueIndex}", ((TermFilter)filter).Values[j]);
									valueIndex++;
								}
							}
							break;
					}
				}
			}

			return parameterDictionary;
		}
	}
}
