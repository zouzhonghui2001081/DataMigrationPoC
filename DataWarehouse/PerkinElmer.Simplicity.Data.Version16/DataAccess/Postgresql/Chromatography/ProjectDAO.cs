using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.Chromatography
{
    public class ProjectDao : ProjectDaoBase
    {
		internal  static long? GetProjectId(IDbConnection connection, string projectName)
	    {
			if (string.IsNullOrWhiteSpace(projectName))
		    {
			    throw new ArgumentException(nameof(projectName));
		    }

		    long? projectId = null;

		    // Retrieve project Id asscoiated with project name
		    Project project =
			    connection.QueryFirstOrDefault<Project>(
				    $"SELECT {ProjectDao.IdColumn} " +
				    $"FROM {ProjectDao.TableName} " +
				    $"WHERE LOWER({ProjectDao.ProjectNameColumn}) = @ProjectName",
                    new { ProjectName = projectName.ToLower() }
			    );

		    if (project != null)
		    {
			    projectId = project.Id;
		    }

		    return projectId;
	    }

        internal  static long? GetProjectId(IDbConnection connection, Guid projectGuid)
        {
            if (projectGuid == Guid.Empty)
            {
                throw new ArgumentException(nameof(projectGuid));
            }

            long? projectId = null;

            // Retrieve project Id asscoiated with project name
            Project project =
                connection.QueryFirstOrDefault<Project>(
                    $"SELECT {ProjectDao.IdColumn} " +
                    $"FROM {ProjectDao.TableName} " +
                    $"WHERE {ProjectDao.GuidColumn} = @ProjectGuid",
                    new { ProjectGuid = projectGuid});

            if (project != null)
            {
                projectId = project.Id;
            }

            return projectId;
        }

	    public Project CreateProject(IDbConnection connection, Project project)
	    {
	        if (project!= null)
	        {
	            if (string.IsNullOrWhiteSpace(project.Name))
	            {
	                throw new ArgumentException(nameof(project.Name));
	            }
                
	            long projectId = connection.ExecuteScalar<long>(
	                InsertSql + "RETURNING Id", project);
	            project.Id = projectId;
            }
	        else
	        {
	            throw new ArgumentNullException(nameof(project));
	        }

		    return project;
	    }

	    public bool DeleteProject(IDbConnection connection, Guid projectGuid)
	    {
		    try
		    {
			    var numberRowsDeleted = connection.Execute(
				    $"DELETE FROM {TableName} " +
				    $"WHERE {GuidColumn} = @ProjectGuid",
				    new { ProjectGuid = projectGuid});

			    return numberRowsDeleted != 0;
		    }
		    catch (Exception ex)
		    {
			    Log.Error($"Error occured in DeleteProject", ex);
			    throw;
		    }
	    }
		public IList<Project> GetAllProjects(IDbConnection connection)
        {
	        var sql = SelectSql;

            sql = sql + $" Order By {TableName}.{ProjectNameColumn}";
            var projects = connection.Query<Project>(sql);

            return projects.ToList();
        }
        public long SaveProject(IDbConnection connection, Project project)
        {
            long projectId;
            try
            {

                if (project != null)
                {
                    var sql =
                        $"UPDATE {TableName} " +
                        "SET " +
                        $"{ModifiedDateColumn} = @{ModifiedDateColumn}, " +
                        $"{ModifiedUserIdColumn} = @{ModifiedUserIdColumn}, " +
                        $"{ProjectNameColumn} = @{ProjectNameColumn}, " +
                        $"{DescriptionColumn} = @{DescriptionColumn}, " +
                        $"{IsEnabledColumn} = @{IsEnabledColumn}, " +
                        $"{IsSecurityOnColumn} =@{IsSecurityOnColumn}, " +
                        $"{IsESignatureOnColumn} =@{IsESignatureOnColumn}, " +
                        $"{IsReviewApprovalOnColumn} =@{IsReviewApprovalOnColumn} " +
                        $"WHERE {GuidColumn} = @{GuidColumn} RETURNING Id";
                    projectId = connection.ExecuteScalar<long>(sql, project);
                }
                else
                {
                    Log.Error("Unable to Save Analysis Result Set as Project Name does not Exist");
                    throw new NullReferenceException();
                }

            }
            catch (Exception ex)
            {
                Log.Error("Error in Saving Project", ex);
                throw;
            }

            return projectId;
        }

        public bool IsExists(IDbConnection connection, string projectName)
        {
            if (string.IsNullOrWhiteSpace(projectName))
            {
                throw new ArgumentException(nameof(projectName));
            }

            var project = GetProjectByName(connection, projectName);

            return project != null;
        }

        internal Project GetProjectByName(IDbConnection connection, string projectName)
        {
            try
            {
                return connection.QueryFirstOrDefault<Project>(
	                SelectSql +
	                $"WHERE Lower({ProjectNameColumn}) = @{ProjectNameColumn}", new { Name = projectName.ToLower() });
            }
            catch (Exception ex)
            {
                Log.Error("Error in GetProjectByName method", ex);
                throw;
            }
        }

        public Project GetProject(IDbConnection connection, Guid projectGuid)
        {
            if (projectGuid == null)
            {
                throw new ArgumentException(nameof(projectGuid));
            }

            try
            {
                return connection.QueryFirstOrDefault<Project>(
	                SelectSql +
	                $"WHERE {TableName}.{GuidColumn} = '{projectGuid}'");
            }
            catch (Exception ex)
            {
                Log.Error("Error in GetProject method", ex);
                throw;
            }
        }

        internal long UpdateProjectStartDate(IDbConnection connection, Project project)
        {
            long projectId;
            try
            {

                if (project != null)
                {
                    var sql =
                        $"UPDATE {TableName} SET {StartDateColumn} = @{StartDateColumn} " +
                        $"WHERE {GuidColumn} = @{GuidColumn} RETURNING Id";
                    projectId = connection.ExecuteScalar<long>(sql, project);
                }
                else
                {
                    Log.Error("Unable to update Project Start date");
                    throw new NullReferenceException();
                }

            }
            catch (Exception ex)
            {
                Log.Error("Error in updating Project Start date", ex);
                throw;
            }

            return projectId;
        }

        internal long DeactivateProject(IDbConnection connection, Project project)
        {
            long projectId;
            try
            {
                if (project != null)
                {
                    var sql =
                        $"UPDATE {TableName} SET {EndDateColumn} = @{EndDateColumn}, {IsEnabledColumn} = @{IsEnabledColumn}, {ModifiedDateColumn} = @{ModifiedDateColumn} " +
                        $"WHERE {GuidColumn} = @{GuidColumn} RETURNING Id";
                    projectId = connection.ExecuteScalar<long>(sql, project);
                }
                else
                {
                    Log.Error("Unable to deactivate project");
                    throw new NullReferenceException();
                }

            }
            catch (Exception ex)
            {
                Log.Error("Error in deactivate project", ex);
                throw;
            }

            return projectId;
        }

        internal long ActivateProject(IDbConnection connection, Project project)
        {
            long projectId;
            try
            {
                if (project != null)
                {
                    var sql =
                        $"UPDATE {TableName} SET {StartDateColumn} = @{StartDateColumn}, {EndDateColumn} = @{EndDateColumn}, {IsEnabledColumn} = @{IsEnabledColumn}, {ModifiedDateColumn} = @{ModifiedDateColumn} " +
                        $"WHERE {GuidColumn} = @{GuidColumn} RETURNING Id";
                    projectId = connection.ExecuteScalar<long>(sql, project);
                }
                else
                {
                    Log.Error("Unable to activate project");
                    throw new NullReferenceException();
                }

            }
            catch (Exception ex)
            {
                Log.Error("Error in activate project", ex);
                throw;
            }

            return projectId;
        }

        internal long UpdateProjectSecurityStatus(IDbConnection connection, Project project)
        {
            long projectId;
            try
            {
                if (project != null)
                {
                    var sql =
                        $"UPDATE {TableName} " +
                        "SET " +
                        $"{IsSecurityOnColumn} = @{IsSecurityOnColumn} " +
                        $"WHERE {GuidColumn} = @{GuidColumn} RETURNING Id";
                    projectId = connection.ExecuteScalar<long>(sql, project);
                }
                else
                {
                    Log.Error("Unable to update project");
                    throw new NullReferenceException();
                }

            }
            catch (Exception ex)
            {
                Log.Error("Error in update project security status!", ex);
                throw;
            }

            return projectId;
        }

        internal long UpdateProjectESignatureStatus(IDbConnection connection, Project project)
        {
            long projectId;
            try
            {
                if (project != null)
                {
                    var sql =
                        $"UPDATE {TableName} " +
                        "SET " +
                        $"{IsESignatureOnColumn} = @{IsESignatureOnColumn} " +
                        $"WHERE {GuidColumn} = @{GuidColumn} RETURNING Id";
                    projectId = connection.ExecuteScalar<long>(sql, project);
                }
                else
                {
                    Log.Error("Unable to update project");
                    throw new NullReferenceException();
                }

            }
            catch (Exception ex)
            {
                Log.Error("Error in update project security status!", ex);
                throw;
            }

            return projectId;
        }

        internal long UpdateProjectReviewApprovalStatus(IDbConnection connection, Project project)
        {
            long projectId;
            try
            {
                if (project != null)
                {
                    var sql =
                        $"UPDATE {TableName} " +
                        "SET " +
                        $"{IsReviewApprovalOnColumn} = @{IsReviewApprovalOnColumn} " +
                        $"WHERE {GuidColumn} = @{GuidColumn} RETURNING Id";
                    projectId = connection.ExecuteScalar<long>(sql, project);
                }
                else
                {
                    Log.Error("Unable to update project");
                    throw new NullReferenceException();
                }

            }
            catch (Exception ex)
            {
                Log.Error("Error in update project security status!", ex);
                throw;
            }

            return projectId;
        }

        internal bool GetProjectESignatureStatus(IDbConnection connection, string projectName)
        {
            bool isOn = false;
            try
            {
                if (!string.IsNullOrEmpty(projectName))
                {
                    var sql = $"SELECT {IsESignatureOnColumn} FROM {TableName} WHERE {ProjectNameColumn} = @{ProjectNameColumn}";
                    isOn = connection.ExecuteScalar<bool>(sql, new {Name = projectName });
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error in select from project!", ex);
            }

            return isOn;
        }

        internal bool GetProjectReviewApproveStatus(IDbConnection connection, string projectName)
        {
            bool isOn = false;
            try
            {
                if (!string.IsNullOrEmpty(projectName))
                {
                    var sql = $"SELECT {IsReviewApprovalOnColumn} FROM {TableName} WHERE {ProjectNameColumn} = @{ProjectNameColumn}";
                    isOn = connection.ExecuteScalar<bool>(sql, new { Name = projectName });
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error in select from project!", ex);
            }

            return isOn;
        }

        internal bool GetProjectSecurityStatus(IDbConnection connection, string projectName)
        {
            bool isOn = false;
            try
            {
                if (!string.IsNullOrEmpty(projectName))
                {
                    var sql = $"SELECT {IsSecurityOnColumn} FROM {TableName} WHERE {ProjectNameColumn} = @{ProjectNameColumn}";
                    isOn = connection.ExecuteScalar<bool>(sql, new { Name = projectName });
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error in select from project!", ex);
            }

            return isOn;
        }
    }
}
