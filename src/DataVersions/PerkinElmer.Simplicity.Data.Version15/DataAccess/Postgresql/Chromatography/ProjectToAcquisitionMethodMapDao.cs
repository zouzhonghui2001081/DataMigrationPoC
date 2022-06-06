using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Dapper;
using log4net;
using PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography.AcquisitionMethod;

namespace PerkinElmer.Simplicity.Data.Version15.DataAccess.Postgresql.Chromatography
{
	internal class ProjectToAcquisitionMethodMapDao
	{
        protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        internal static string TableName { get; } = "ProjectToAcquisitionMethodMap";
        internal static string IdColumn { get; } = "Id";
        internal static string ProjectIdColumn { get; } = "ProjectId";
        internal static string AcquisitionMethodIdColumn { get; } = "AcquisitionMethodId";

        public virtual void Create(IDbConnection connection, ProjectToAcquisitionMethodMap projectToAcquisitionMethodMap)
        {
	        try
	        {
		       connection.ExecuteScalar<long>(
			       $"INSERT INTO {TableName} ({ProjectIdColumn}," +
			       $"{AcquisitionMethodIdColumn}) " +
			       $"VALUES (@{ProjectIdColumn}," +
			       $"@{AcquisitionMethodIdColumn})", projectToAcquisitionMethodMap);
	        }
	        catch (Exception ex)
	        {
		        Log.Error("Error in CreateChildren method", ex);
		        throw;
	        }
        }

        public List<ProjectToAcquisitionMethodMap> GetByProjectIdAcquisitionMethodId(IDbConnection connection, long projectId, long acquisitionMethodId)
        {
	        try
	        {
		        return connection.Query<ProjectToAcquisitionMethodMap>($"SELECT {IdColumn}," +
		                                                               $"{ProjectIdColumn}," +
		                                                               $"{AcquisitionMethodIdColumn} " +
		                                                               $"FROM {TableName} " +
		                                                               $"WHERE {ProjectIdColumn} = {projectId} AND {AcquisitionMethodIdColumn} = {acquisitionMethodId}").ToList();
	        }
	        catch (Exception ex)
	        {
		        Log.Error("Error in GetByProjectId method", ex);
		        throw;
	        }
        }
		public void Delete(IDbConnection connection, string projectName, string methodName)
        {
	        try
	        {
		        var result = connection.QueryFirstOrDefault(
			        $"SELECT {TableName}.{ProjectIdColumn} AS ProjectId,{TableName}.{AcquisitionMethodIdColumn} AS AcquisitionMethodId " +
			        $"FROM {TableName} " +
			        $"INNER JOIN {ProjectDao.TableName} ON {ProjectDao.TableName}.{ProjectDao.IdColumn} = {TableName}.{ProjectIdColumn} " +
			        $"WHERE {ProjectDao.TableName}.{ProjectDao.ProjectNameColumn} = @ProjectName", new { ProjectName  = projectName });

		        if (result != null)
		        {
			        connection.Execute($"DELETE FROM {TableName} WHERE {ProjectIdColumn} = {result.projectid} AND {AcquisitionMethodIdColumn} = {result.acquisitionmethodid}");
		        }
	        }
			catch (Exception ex)
	        {
		        Log.Error("Error in Delete method", ex);
		        throw;
	        }
        }
        public void Delete(IDbConnection connection, Guid projectGuid, Guid acquisitionMethodGuid)
        {
	        try
	        {
		        var result = connection.QueryFirstOrDefault<ProjectToAcquisitionMethodMap>(
			        $"SELECT {TableName}.{ProjectIdColumn},{TableName}.{AcquisitionMethodIdColumn} " +
			        $"FROM {TableName} " +
			        $"INNER JOIN {ProjectDao.TableName} ON {ProjectDao.TableName}.{ProjectDao.IdColumn} = {TableName}.{ProjectIdColumn} " +
			        $"INNER JOIN {AcquisitionMethodDao.TableName} ON {TableName}.{AcquisitionMethodIdColumn} = {AcquisitionMethodDao.TableName}.{IdColumn} " +
			        $"WHERE {ProjectDao.TableName}.{ProjectDao.GuidColumn} = @ProjectGuid AND " +
			        $"{AcquisitionMethodDao.TableName}.{AcquisitionMethodDao.GuidColumn} = @AcquisitionMethodGuid",
			        new { ProjectGuid = projectGuid, AcquisitionMethodGuid = acquisitionMethodGuid});

		        if (result != null)
		        {
			        connection.Execute(
				        $"DELETE FROM {TableName} WHERE {ProjectIdColumn} = @ProjectId AND " +
				        $"{AcquisitionMethodIdColumn} = @AcquisitionMethodId",
				        new { ProjectId = result.ProjectId, AcquisitionMethodId = result.AcquisitionMethodId });
		        }
	        }
	        catch (Exception ex)
	        {
		        Log.Error("Error in Delete method", ex);
		        throw;
	        }
        }

		public void DeleteAll(IDbConnection connection, string projectName)
	    {
		        try
		        {
			        long? projectId = ProjectDao.GetProjectId(connection, projectName);

			        if (projectId.HasValue)
			        {
				        connection.Execute($"DELETE FROM {TableName} WHERE {ProjectIdColumn} = {projectId.Value}");
			        }
		        }
		        catch (Exception ex)
		        {
			        Log.Error("Error in Delete method", ex);
			        throw;
		        }
	    }
		public void DeleteAll(IDbConnection connection, Guid projectGuid)
		{
			try
			{
				long? projectId = ProjectDao.GetProjectId(connection, projectGuid);

				if (projectId.HasValue)
				{
					connection.Execute($"DELETE FROM {TableName} WHERE {ProjectIdColumn} = {projectId.Value}");
				}
			}
			catch (Exception ex)
			{
				Log.Error("Error in Delete method", ex);
				throw;
			}
		}
	}
}
