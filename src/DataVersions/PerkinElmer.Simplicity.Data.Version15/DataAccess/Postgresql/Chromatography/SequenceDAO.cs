using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Dapper;
using log4net;
using PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version15.DataAccess.Postgresql.Chromatography
{
    internal class SequenceDao
    {
        protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        internal static string ProjectIdColumn { get; } = "ProjectId";

        public static string TableName { get; } = "Sequence";
        internal static string IdColumn { get; } = "Id";
        internal static string GuidColumn { get; } = "Guid";
        internal static string NameColumn { get; } = "Name";
        internal static string CreatedDateColumn { get; } = "CreatedDate";
        internal static string ModifiedDateColumn { get; } = "ModifiedDate";
        internal static string CreatedUserIdColumn { get; } = "CreatedUserId";
        internal static string ModifiedUserIdColumn { get; } = "ModifiedUserId";
        internal static string CreatedUserNameColumn { get; } = "CreatedUserName";
        internal static string ModifiedUserNameColumn { get; } = "ModifiedUserName";

        protected readonly string InsertSql =
			$"INSERT INTO {TableName} ({ProjectIdColumn}," +
			$"{NameColumn}," +
			$"{GuidColumn}," +
			$"{CreatedDateColumn}," +
			$"{ModifiedDateColumn}, " +
			$"{CreatedUserIdColumn}," +
            $"{CreatedUserNameColumn}," +
            $"{ModifiedUserNameColumn}," +
            $"{ModifiedUserIdColumn}) " +
			$"VALUES (@{ProjectIdColumn}," +
			$"@{NameColumn}," +
			$"@{GuidColumn}," +
			$"@{CreatedDateColumn}," +
			$"@{ModifiedDateColumn}, " +
			$"@{CreatedUserIdColumn}," +
            $"@{CreatedUserNameColumn}," +
            $"@{ModifiedUserNameColumn}," +
            $"@{ModifiedUserIdColumn}) ";

		public IList<Sequence> GetSequenceInfos(IDbConnection connection, string projectName)
	    {
		    try
		    {
			    return connection.Query<Sequence>(
				    $"SELECT {TableName}.{IdColumn},{TableName}.{GuidColumn},{TableName}.{NameColumn},{TableName}.{CreatedDateColumn}," +
				    $"{TableName}.{ModifiedDateColumn},{TableName}.{CreatedUserIdColumn},{TableName}.{CreatedUserNameColumn},{TableName}.{ModifiedUserNameColumn},{TableName}.{ModifiedUserIdColumn} " +
				    $"FROM {TableName} " +
				    $"INNER JOIN {ProjectDao.TableName} " +
				    $"ON {ProjectDao.TableName}.{ProjectDao.IdColumn} = {TableName}.{ProjectIdColumn} " +
				    $"WHERE {ProjectDao.TableName}.{ProjectDao.ProjectNameColumn} = @ProjectName",
				    new { ProjectName = projectName}).ToList();

		    }
		    catch (Exception ex)
		    {
			    Log.Error($"Error in GetSequenceInfos method", ex);
			    throw;
		    }
	    }

		public IList<Sequence> GetSequenceInfos(IDbConnection connection, Guid projectGuid)
		{
			try
			{
				return connection.Query<Sequence>(
					$"SELECT {TableName}.{IdColumn},{TableName}.{GuidColumn},{TableName}.{NameColumn},{TableName}.{CreatedDateColumn}," +
					$"{TableName}.{ModifiedDateColumn},{TableName}.{CreatedUserIdColumn},{TableName}.{CreatedUserNameColumn},{TableName}.{ModifiedUserNameColumn},{TableName}.{ModifiedUserIdColumn} " +
					$"FROM {TableName} " +
					$"INNER JOIN {ProjectDao.TableName} ON {ProjectDao.TableName}.{ProjectDao.IdColumn} = {TableName}.{ProjectIdColumn} " +
					$"WHERE {ProjectDao.TableName}.{ProjectDao.GuidColumn} = @ProjectGuid",
					new { ProjectGuid = projectGuid }).ToList();

			}
			catch (Exception ex)
			{
				Log.Error($"Error in GetSequenceInfos method", ex);
				throw;
			}
		}
		public Sequence GetSequenceInfo(IDbConnection connection, string projectName, string sequenceName)
	    {
		    try
		    {
			    Sequence sequence = GetSequenceObj(connection, projectName, sequenceName);

			    return sequence;
		    }
		    catch (Exception ex)
		    {
			    Log.Error($"Error in GetSequenceInfo method", ex);
			    throw;
		    }
	    }

		public bool IsExists(IDbConnection connection, string projectName, string sequenceName)
		{
			Sequence sequence;
			try
			{
				sequence = GetSequenceObj(connection, projectName, sequenceName);
			}
			catch (Exception ex)
			{
				Log.Error($"Error in IsExists method", ex);
				throw;
			}

			return sequence != null;
		}
		public bool IsExists(IDbConnection connection, Guid projectGuid, string sequenceName)
		{
			try
			{
				var sequence = GetSequenceInfo(connection, projectGuid, sequenceName);
				return sequence != null;
			}
			catch (Exception ex)
			{
				Log.Error($"Error in IsExists method", ex);
				throw;
			}

		}

		public Sequence SaveAsSequence(IDbConnection connection, string projectName, string sequenceName, Guid guid,
		    SequenceSampleInfo[] sequenceSamplesInfo, UserInfo userInfo)
	    {
		    try
		    {
			    if (string.IsNullOrWhiteSpace(projectName))
			    {
				    throw new ArgumentException(nameof(projectName));
			    }

			    if (string.IsNullOrWhiteSpace(sequenceName))
			    {
				    throw new ArgumentException(nameof(sequenceName));
			    }

			    if (sequenceSamplesInfo == null)
			    {
				    throw new NullReferenceException(nameof(sequenceSamplesInfo));
			    }

			    long? projectId = ProjectDao.GetProjectId(connection, projectName);

			    if (projectId.HasValue == false)
			    {
				    throw new ArgumentException(nameof(projectName));
			    }
			    else
			    {
				    Sequence sequence = new Sequence()
				    {
					    ProjectId = projectId.Value,
					    CreatedDate = DateTime.UtcNow,
					    ModifiedDate = DateTime.UtcNow,
                        CreatedUserId= userInfo?.UserId,
                        CreatedUserName=userInfo?.UserFullName,
                        ModifiedUserId= userInfo?.UserId,
                        ModifiedUserName=userInfo?.UserFullName,
                        Name = sequenceName,
					    Guid = guid
				    };

				    using (IDbTransaction transaction = connection.BeginTransaction())
				    {
					    long sequenceId = connection.ExecuteScalar<long>(
						    InsertSql + "RETURNING Id", sequence);

					    sequence.Id = sequenceId;
					    Array.ForEach(sequenceSamplesInfo, s => s.SequenceId = sequenceId); // Add sequenceId to SequenceSamples
					    SequenceSampleDao sequenceSampleDao = new SequenceSampleDao();
						sequenceSampleDao.SaveAsSequenceSamples(connection, sequenceSamplesInfo);

				        // Update Project Start Date in case of first sequence for the project
				        ProjectDao projectDao = new ProjectDao();
				        var project = projectDao.GetProjectByName(connection, projectName);

				        if (project.StartDate == null)
				        {
				            project.StartDate = DateTime.UtcNow;
				            projectDao.UpdateProjectStartDate(connection, project);
				        }

                        transaction.Commit();

					    return sequence;
				    }
			    }
		    }
		    catch (Exception ex)
		    {
			    Log.Error($"Error in SaveAsSequence method", ex);
			    throw;
		    }
		}

	    public bool CreateSequence(IDbConnection connection, Guid projectGuid, Sequence sequence)
	    {
		    var projectId = ProjectDao.GetProjectId(connection, projectGuid);

		    if (projectId.HasValue)
		    {
			    sequence.ProjectId = projectId.Value;
			    long sequenceId = connection.ExecuteScalar<long>(
				    InsertSql + "RETURNING Id", sequence);

			    sequence.Id = sequenceId;
			    foreach (var sequenceSequenceSampleInfo in sequence.SequenceSampleInfos)
			    {
				    sequenceSequenceSampleInfo.SequenceId = sequenceId;
			    }
			    SequenceSampleDao sequenceSampleDao = new SequenceSampleDao();
			    sequenceSampleDao.SaveAsSequenceSamples(connection, sequence.SequenceSampleInfos.ToArray());

			    // Update Project Start Date in case of first sequence for the project
			    ProjectDao projectDao = new ProjectDao();
			    var project = projectDao.GetProject(connection, projectGuid);

			    if (project.StartDate == null)
			    {
				    project.StartDate = DateTime.UtcNow;
				    projectDao.UpdateProjectStartDate(connection, project);
			    }

			    return true;
		    }

		    return false;
	    }
	    public Sequence SaveSequence(IDbConnection connection, string projectName, string sequenceName,
		    SequenceSampleInfo[] sequenceSamplesInfo, UserInfo userInfo)
	    {
		    try
		    {
			    if (string.IsNullOrWhiteSpace(projectName))
			    {
				    throw new ArgumentException(nameof(projectName));
			    }

			    if (string.IsNullOrWhiteSpace(sequenceName))
			    {
				    throw new ArgumentException(nameof(sequenceName));
			    }

			    if (sequenceSamplesInfo == null)
			    {
				    throw new NullReferenceException(nameof(sequenceSamplesInfo));
			    }

			    Sequence sequence = GetSequenceObj(connection, projectName, sequenceName);

			    if (sequence != null)
			    {
				    sequence.Name = sequenceName;
				    sequence.ModifiedDate = DateTime.UtcNow;
                    sequence.ModifiedUserId = userInfo?.UserId;
                    sequence.ModifiedUserName = userInfo?.UserFullName;
                    using (IDbTransaction transaction = connection.BeginTransaction())
				    {
                        // Update modify values
                        connection.Query(
	                        $"UPDATE {TableName} " +
	                        $"SET {NameColumn} = @{NameColumn}," +
	                        $"{ModifiedDateColumn} = @{ModifiedDateColumn}," +
	                        $"{ModifiedUserNameColumn} = @{ModifiedUserNameColumn}, " +
	                        $"{ModifiedUserIdColumn} = @{ModifiedUserIdColumn} " +
	                        $"WHERE {IdColumn} = @{IdColumn}",
	                        new
	                        {
		                        Name = sequence.Name,
		                        ModifiedDate = sequence.ModifiedDate,
		                        ModifiedUserName = sequence.ModifiedUserName,
		                        ModifiedUserId = sequence.ModifiedUserId,
		                        Id = sequence.Id
	                        });

					    Array.ForEach(sequenceSamplesInfo, s => s.SequenceId = sequence.Id); // Add sequenceId to SequenceSamples
					    SequenceSampleDao sequenceSampleDao = new SequenceSampleDao();
						sequenceSampleDao.SaveSequenceSamples(connection, sequence.Id, sequenceSamplesInfo);
					    transaction.Commit();
				    }
			    }
                //throw exception when the sequence is null

			    return sequence;
		    }
		    catch (Exception ex)
		    {
			    Log.Error($"Error in SaveSequence method", ex);
			    throw;
		    }
	    }

	    public void DeleteSequence(IDbConnection connection, string projectName, string sequenceName)
	    {
		    if (string.IsNullOrWhiteSpace(projectName))
		    {
			    throw new ArgumentException(nameof(projectName));
		    }

		    if (string.IsNullOrWhiteSpace(sequenceName))
		    {
			    throw new ArgumentException(nameof(sequenceName));
		    }

		    Sequence sequence = GetSequenceObj(connection, projectName, sequenceName);

		    if (sequence != null)
		    {
			    SequenceSampleDao sequenceSampleDao = new SequenceSampleDao();
				IDbTransaction transaction = connection.BeginTransaction();
				sequenceSampleDao.DeleteSequenceSamples(connection, sequence.Id);
				DeleteSequence(connection, sequence.Id);

				transaction.Commit();
			}
		}

	    public void DeleteAllSequence(IDbConnection connection, string projectName)
	    {
		    if (string.IsNullOrWhiteSpace(projectName))
		    {
			    throw new ArgumentException(nameof(projectName));
		    }

			List<Sequence> sequences = connection.Query<Sequence>(
				$"SELECT {TableName}.{IdColumn} " +
				$"FROM {TableName} " +
				$"INNER JOIN {ProjectDao.TableName} " +
				$"ON {ProjectDao.TableName}.{ProjectDao.IdColumn} = {TableName}.{ProjectIdColumn} " +
				$"WHERE {ProjectDao.TableName}.{ProjectDao.ProjectNameColumn} = @ProjectName",
				new { ProjectName = projectName}).ToList();

		    foreach (var sequence in sequences)
		    {
				SequenceSampleDao sequenceSampleDao = new SequenceSampleDao();
				sequenceSampleDao.DeleteSequenceSamples(connection, sequence.Id);
				DeleteSequence(connection, sequence.Id);
		    }
		}
	    public void DeleteAllSequence(IDbConnection connection, Guid projectGuid)
	    {
		    var sequences = connection.Query<Sequence>(
			    $"SELECT {TableName}.{IdColumn} " +
			    $"FROM {TableName} " +
			    $"INNER JOIN {ProjectDao.TableName} " +
			    $"ON {ProjectDao.TableName}.{ProjectDao.IdColumn} = {TableName}.{ProjectIdColumn} " +
			    $"WHERE {ProjectDao.TableName}.{ProjectDao.GuidColumn} = @ProjectGuid",
			    new { ProjectGuid = projectGuid }).ToList();
		    foreach (var sequence in sequences)
		    {
			    var sequenceSampleDao = new SequenceSampleDao();
			    sequenceSampleDao.DeleteSequenceSamples(connection, sequence.Id);
			    DeleteSequence(connection, sequence.Id);
		    }
	    }
	    public void DeleteSequence(IDbConnection connection, Guid projectGuid, Guid sequenceGuid)
	    {
		    var sequence = connection.QueryFirstOrDefault<Sequence>(
			    $"SELECT {TableName}.{IdColumn} " +
			    $"FROM {TableName} " +
			    $"INNER JOIN {ProjectDao.TableName} " +
			    $"ON {ProjectDao.TableName}.{ProjectDao.IdColumn} = {TableName}.{ProjectIdColumn} " +
			    $"WHERE {ProjectDao.TableName}.{ProjectDao.GuidColumn} = @ProjectGuid AND " +
			    $"{TableName}.{GuidColumn} = @SequenceGuid",
			    new { ProjectGuid = projectGuid, SequenceGuid = sequenceGuid });
		    if (sequence != null)
		    {
			    var sequenceSampleDao = new SequenceSampleDao();
				sequenceSampleDao.DeleteSequenceSamples(connection, sequence.Id);
				DeleteSequence(connection, sequence.Id);
		    }
		}

		private void DeleteSequence(IDbConnection connection, long iD)
	    {
		    connection.Query(
			    $"DELETE FROM {TableName} " +
			    $"WHERE {IdColumn} = {iD}");
		}

		public void RenameSequence(IDbConnection connection, string projectName, string sequenceName, string newSequenceName)
	    {
		    if (string.IsNullOrWhiteSpace(projectName))
		    {
			    throw new ArgumentException(nameof(projectName));
		    }

		    if (string.IsNullOrWhiteSpace(sequenceName))
		    {
			    throw new ArgumentException(nameof(sequenceName));
		    }

		    if (string.IsNullOrWhiteSpace(newSequenceName))
		    {
			    throw new ArgumentException(nameof(newSequenceName));
		    }

	        IList<Sequence> sequences = GetSequenceInfos(connection, projectName);
            Sequence sequence = sequences.FirstOrDefault(n => n.Name.Equals(sequenceName, StringComparison.OrdinalIgnoreCase));

	        if (sequence != null)
	        {
	            connection.Query(
	                $"UPDATE {TableName} " +
	                $"SET {NameColumn} = @NewSequenceName " +
	                $"WHERE {IdColumn} = @SequenceId",
	                new { NewSequenceName = newSequenceName, SequenceId = sequence.Id});
	        }
	    }

		private Sequence GetSequenceObj(IDbConnection connection, string projectName, string sequenceName)
	    {
		    Sequence sequence = connection.QueryFirstOrDefault<Sequence>(
			    $"SELECT {TableName}.{IdColumn},{TableName}.{GuidColumn},{TableName}.{NameColumn}," +
			    $"{TableName}.{CreatedDateColumn},{TableName}.{ModifiedDateColumn}," +
			    $"{TableName}.{CreatedUserIdColumn},{TableName}.{CreatedUserNameColumn},{TableName}.{ModifiedUserNameColumn},{TableName}.{ModifiedUserIdColumn} " +
			    $"FROM {TableName} " +
			    $"INNER JOIN {ProjectDao.TableName} " +
			    $"ON {ProjectDao.TableName}.{ProjectDao.IdColumn} = {TableName}.{ProjectIdColumn} " +
			    $"WHERE {ProjectDao.TableName}.{ProjectDao.ProjectNameColumn} = @ProjectName AND " +
			    $"LOWER({TableName}.{NameColumn}) = @SequenceName",
			    new { ProjectName = projectName, SequenceName = sequenceName.ToLower()});

		    return sequence;
	    }
		public Sequence GetSequenceInfo(IDbConnection connection, Guid projectGuid, Guid sequenceGuid)
		{
			Sequence sequence = connection.QueryFirstOrDefault<Sequence>(
				$"SELECT {TableName}.{IdColumn},{TableName}.{GuidColumn},{TableName}.{NameColumn}," +
				$"{TableName}.{CreatedDateColumn},{TableName}.{ModifiedDateColumn}," +
				$"{TableName}.{CreatedUserIdColumn},{TableName}.{CreatedUserNameColumn},{TableName}.{ModifiedUserNameColumn},{TableName}.{ModifiedUserIdColumn} " +
				$"FROM {TableName} " +
				$"INNER JOIN {ProjectDao.TableName} " +
				$"ON {ProjectDao.TableName}.{ProjectDao.IdColumn} = {TableName}.{ProjectIdColumn} " +
				$"WHERE {ProjectDao.TableName}.{ProjectDao.GuidColumn} = @ProjectGuid AND {TableName}.{GuidColumn} = @SequenceGuid",
				new { ProjectGuid = projectGuid, SequenceGuid = sequenceGuid });

			return sequence;
		}
		public Sequence GetSequenceInfo(IDbConnection connection, Guid projectGuid, string sequenceName)
		{
			sequenceName = sequenceName.ToLower();

			var sequence = connection.QueryFirstOrDefault<Sequence>(
				$"SELECT {TableName}.{IdColumn},{TableName}.{GuidColumn},{TableName}.{NameColumn}," +
				$"{TableName}.{CreatedDateColumn},{TableName}.{ModifiedDateColumn}," +
				$"{TableName}.{CreatedUserIdColumn},{TableName}.{CreatedUserNameColumn},{TableName}.{ModifiedUserNameColumn},{TableName}.{ModifiedUserIdColumn} " +
				$"FROM {TableName} " +
				$"INNER JOIN {ProjectDao.TableName} " +
				$"ON {ProjectDao.TableName}.{ProjectDao.IdColumn} = {TableName}.{ProjectIdColumn} " +
				$"WHERE {ProjectDao.TableName}.{ProjectDao.GuidColumn} = @ProjectGuid AND LOWER({TableName}.{NameColumn}) = @SequenceName",
				new { ProjectGuid = projectGuid, SequenceName = sequenceName });

			return sequence;
		}
	}
}
