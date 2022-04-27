using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Dapper;
using log4net;
using PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography;
using PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography.ReviewApprove;

namespace PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.Chromatography
{
    internal class ReportTemplateDao
    {
        protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        internal static string TableName { get; } = "ReportTemplates";

        internal static string IdColumn { get; } = "Id";

        internal static string TemplateNameColumn { get; } = "Name";

        internal static string CategoryColumn { get; } = "Category";

        internal static string CreatedDateColumn { get; } = "CreatedDate";
        internal static string CreatedUserIdColumn { get; } = "CreatedUserId";
        internal static string CreatedUserNameColumn { get; } = "CreatedUserName";
        internal static string ModifiedDateColumn { get; } = "ModifiedDate";
        internal static string ModifiedUserIdColumn { get; } = "ModifiedUserId";
        internal static string ModifiedUserNameColumn { get; } = "ModifiedUserName";
        internal static string ContentColumn { get; } = "Content";
        internal static string ConfigColumn { get; } = "Config";
        internal static string ProjectIdColumn { get; } = "ProjectId";

        internal static string IsGlobalColumn { get; } = "IsGlobal";
        internal static string IsDefaultColumn { get; } = "IsDefault";     
        internal static string ReviewApproveStateColumn { get; } = "ReviewApproveState";

        protected readonly string InsertSql =
	        $"INSERT INTO {TableName} ({IdColumn}," +
	        $"{TemplateNameColumn}," +
	        $"{CategoryColumn}," +
	        $"{CreatedDateColumn}," +
	        $"{ModifiedDateColumn}, " +
	        $"{ContentColumn}," +
	        $"{ConfigColumn}," +
	        $"{IsDefaultColumn}," +
	        $"{ProjectIdColumn}," +
	        $"{IsGlobalColumn}," +
	        $"{CreatedUserIdColumn}," +
	        $"{CreatedUserNameColumn}," +
	        $"{ModifiedUserIdColumn}," +
	        $"{ModifiedUserNameColumn}) " +
	        $"VALUES (@{IdColumn}," +
	        $"@{TemplateNameColumn}," +
	        $"@{CategoryColumn}," +
	        $"@{CreatedDateColumn}," +
	        $"@{ModifiedDateColumn}, " +
	        $"@{ContentColumn}," +
	        $"@{ConfigColumn}," +
	        $"@{IsDefaultColumn}," +
	        $"@{ProjectIdColumn}," +
	        $"@{IsGlobalColumn}," +
	        $"@{CreatedUserIdColumn}," +
	        $"@{CreatedUserNameColumn}," +
	        $"@{ModifiedUserIdColumn}," +
	        $"@{ModifiedUserNameColumn}) ";
        protected readonly string InsertSql_ReviewApprove =
	        $"INSERT INTO {TableName} ({IdColumn}," +
	        $"{TemplateNameColumn}," +
	        $"{CategoryColumn}," +
	        $"{CreatedDateColumn}," +
	        $"{ModifiedDateColumn}, " +
	        $"{ContentColumn}," +
	        $"{ConfigColumn}," +
	        $"{IsDefaultColumn}," +
	        $"{ProjectIdColumn}," +
	        $"{IsGlobalColumn}," +
	        $"{CreatedUserIdColumn}," +
	        $"{CreatedUserNameColumn}," +
	        $"{ModifiedUserIdColumn}," +
	        $"{ModifiedUserNameColumn}," +
	        $"{ReviewApproveStateColumn}) " +
	        $"VALUES (@{IdColumn}," +
	        $"@{TemplateNameColumn}," +
	        $"@{CategoryColumn}," +
	        $"@{CreatedDateColumn}," +
	        $"@{ModifiedDateColumn}, " +
	        $"@{ContentColumn}," +
	        $"@{ConfigColumn}," +
	        $"@{IsDefaultColumn}," +
	        $"@{ProjectIdColumn}," +
	        $"@{IsGlobalColumn}," +
	        $"@{CreatedUserIdColumn}," +
	        $"@{CreatedUserNameColumn}," +
	        $"@{ModifiedUserIdColumn}," +
	        $"@{ModifiedUserNameColumn}," +
	        $"@{ReviewApproveStateColumn}) ";
		protected readonly string SelectSql =
	        $"SELECT {TableName}.{IdColumn}, " +
	        $"{TableName}.{TemplateNameColumn}, " +
	        $"{TableName}.{CategoryColumn}," +
	        $"{TableName}.{CreatedDateColumn}, " +
	        $"{TableName}.{CreatedUserIdColumn}," +
	        $"{TableName}.{CreatedUserNameColumn}, " +
	        $"{TableName}.{ModifiedDateColumn}," +
	        $"{TableName}.{ModifiedUserIdColumn}," +
	        $"{TableName}.{ModifiedUserNameColumn}," +
	        $"{TableName}.{ContentColumn}, " +
	        $"{TableName}.{ConfigColumn}," +
	        $"{TableName}.{ProjectIdColumn}," +
	        $"{TableName}.{IsGlobalColumn}," +
	        $"{TableName}.{IsDefaultColumn}," +
	        $"{TableName}.{ReviewApproveStateColumn} " +
	        $"FROM {TableName} ";
        protected readonly string SelectInfoAndConfigSql =
            $"SELECT {TableName}.{IdColumn}, " +
            $"{TableName}.{TemplateNameColumn}, " +
            $"{TableName}.{CategoryColumn}," +
            $"{TableName}.{CreatedDateColumn}, " +
            $"{TableName}.{CreatedUserIdColumn}," +
            $"{TableName}.{CreatedUserNameColumn}, " +
            $"{TableName}.{ModifiedDateColumn}," +
            $"{TableName}.{ModifiedUserIdColumn}," +
            $"{TableName}.{ModifiedUserNameColumn}," +
            $"{TableName}.{ConfigColumn}," +
            $"{TableName}.{ProjectIdColumn}," +
            $"{TableName}.{IsGlobalColumn}," +
            $"{TableName}.{IsDefaultColumn}," +
            $"{TableName}.{ReviewApproveStateColumn} " +
            $"FROM {TableName} ";
        protected readonly string SelectInfoSql =
            $"SELECT {TableName}.{IdColumn}, " +
            $"{TableName}.{TemplateNameColumn}, " +
            $"{TableName}.{CategoryColumn}," +
            $"{TableName}.{CreatedDateColumn}, " +
            $"{TableName}.{CreatedUserIdColumn}," +
            $"{TableName}.{CreatedUserNameColumn}, " +
            $"{TableName}.{ModifiedDateColumn}," +
            $"{TableName}.{ModifiedUserIdColumn}," +
            $"{TableName}.{ModifiedUserNameColumn}," +
            $"{TableName}.{ProjectIdColumn}," +
            $"{TableName}.{IsGlobalColumn}," +
            $"{TableName}.{IsDefaultColumn}," +
            $"{TableName}.{ReviewApproveStateColumn} " +
            $"FROM {TableName} ";

        protected readonly string ExistsSql = $"SELECT EXISTS(SELECT 1 FROM {TableName} WHERE {IdColumn} = @{IdColumn})";

        public IList<ReportTemplate> GetList(IDbConnection connection, Guid projectGuid, bool includeContent)
        {
            try
            {
                var sql = SelectSql;
                if(!includeContent)
				{
                    sql = SelectInfoAndConfigSql;
                }
                var tempTemplates = connection.Query<ReportTemplate>(
                    sql +
                    $"INNER JOIN {ProjectDao.TableName} ON {TableName}.{ProjectIdColumn} = {ProjectDao.TableName}.{ProjectDao.IdColumn} " +
                    $"WHERE {ProjectDao.TableName}.{ProjectDao.GuidColumn} = @ProjectGuid",
                    new { ProjectGuid = projectGuid }).ToList();
                if (tempTemplates == null)
                {
                    return new List<ReportTemplate>();
                }
                return tempTemplates.ToList();
            }
            catch (Exception e)
            {
                Log.Error(
                    $"Error occured in GetList() method of class{GetType().Name} - {e.Message}");
                throw;
            }
        }
        public IList<ReportTemplate> GetGlobalReportTemplates(IDbConnection connection)
        {
            try
            {
                string query = $"SELECT {IdColumn}, {TemplateNameColumn}, {CategoryColumn},{CreatedDateColumn}," +
                               $"{ModifiedDateColumn}, {ContentColumn},{ConfigColumn},{IsDefaultColumn}, {ReviewApproveStateColumn}" +
                               $" FROM {TableName} Where {ProjectIdColumn} Is NULL and {IsGlobalColumn} = true";

                var reportTemplates = connection.Query<ReportTemplate>(query);

                return reportTemplates.ToList();
            }
            catch (Exception e)
            {
                Log.Error(
                    $"Error occured in GetAllGlobalReportTemplates() method of class{GetType().Name} - {e.Message}");
                throw;
            }
        }

		public ReportTemplate Get(IDbConnection connection, Guid projectGuid, string templateName)
        {
	        try
	        {
		        templateName = templateName.ToLower();

		        return connection.QueryFirstOrDefault<ReportTemplate>(
			        SelectSql +
			        $"INNER JOIN {ProjectDao.TableName} ON {TableName}.{ProjectIdColumn} = {ProjectDao.TableName}.{ProjectDao.IdColumn} " +
			        $"WHERE {ProjectDao.TableName}.{ProjectDao.GuidColumn} = @ProjectGuid AND LOWER({TableName}.{TemplateNameColumn}) = @TemplateColumnName",
			        new { ProjectGuid = projectGuid, TemplateColumnName = templateName });
	        }
			catch (Exception ex)
	        {
		        Log.Error("Error in GetByName method", ex);
		        throw;
	        }
		}

        public ReportTemplate Get(IDbConnection connection, Guid projectGuid, Guid templateId)
        {
            try
            {
                return connection.QueryFirstOrDefault<ReportTemplate>(
                    SelectSql +
                    $"INNER JOIN {ProjectDao.TableName} ON {TableName}.{ProjectIdColumn} = {ProjectDao.TableName}.{ProjectDao.IdColumn} " +
                    $"WHERE {ProjectDao.TableName}.{ProjectDao.GuidColumn} = @ProjectGuid AND {TableName}.{IdColumn} = @Id",
                    new { ProjectGuid = projectGuid, Id = templateId });
            }
            catch (Exception ex)
            {
                Log.Error("Error in GetByTemplateId method", ex);
                throw;
            }
        }

        internal ReportTemplate Get(IDbConnection connection, string projectName, string templateName)
        {
            try
            {
                var projectId = ProjectDao.GetProjectId(connection, projectName);
                if(projectId == null || !projectId.HasValue)
				{
                    return null;
				}

                templateName = templateName.ToLower();
                return connection.QueryFirstOrDefault<ReportTemplate>(
                    SelectSql +
                    $"WHERE {TableName}.{ProjectIdColumn} = @{ProjectIdColumn} AND LOWER({TableName}.{TemplateNameColumn}) = @{TemplateNameColumn}",
                    new { projectid = projectId.Value, name = templateName });
            }
            catch (Exception ex)
            {
                Log.Error("Error in GetByName method", ex);
                throw;
            }
        }
        
        internal ReportTemplate Get(IDbConnection connection, string projectName, Guid templateId)
        {
            try
            {
                var projectId = ProjectDao.GetProjectId(connection, projectName);
                if (projectId == null || !projectId.HasValue)
                {
                    return null;
                }

                return connection.QueryFirstOrDefault<ReportTemplate>(
                    SelectSql +
                    $"WHERE {TableName}.{ProjectIdColumn} = @{ProjectIdColumn} AND {TableName}.{IdColumn} = @{IdColumn}",
                    new { projectid = projectId.Value, id = templateId });
            }
            catch (Exception ex)
            {
                Log.Error("Error in GetByName method", ex);
                throw;
            }
        }

        public IList<ReportTemplate> Get(IDbConnection connection, string templateName)
        {
            try
            {
                templateName = templateName.ToLower();
                return connection.Query<ReportTemplate>(
                    SelectSql +
                    $"WHERE LOWER({TableName}.{TemplateNameColumn}) = @{TemplateNameColumn}",
                    new { name = templateName }).ToList();
            }
            catch (Exception ex)
            {
                Log.Error("Error in GetByName method", ex);
                throw;
            }
        }

        public ReportTemplate GetInfoByName(IDbConnection connection, Guid projectGuid, string reportTemplateName)
        {
            try
            {
                reportTemplateName = reportTemplateName.ToLower();

                return connection.QueryFirstOrDefault<ReportTemplate>(
                    SelectInfoSql +
                    $"INNER JOIN {ProjectDao.TableName} ON {TableName}.{ProjectIdColumn} = {ProjectDao.TableName}.{ProjectDao.IdColumn} " +
                    $"WHERE {ProjectDao.TableName}.{ProjectDao.GuidColumn} = @ProjectGuid AND LOWER({TableName}.{TemplateNameColumn}) = @TemplateColumnName",
                    new { ProjectGuid = projectGuid, TemplateColumnName = reportTemplateName });
            }
            catch (Exception ex)
            {
                Log.Error("Error in GetByName method", ex);
                throw;
            }
        }

        internal IList<ReportTemplate> GetReportTemplateInfos(IDbConnection connection, Guid projectGuid, ReportTemplateType type)
		{
            try
            {
                var templates = connection.Query<ReportTemplate>(
                    SelectInfoSql +
                    $"INNER JOIN {ProjectDao.TableName} ON {TableName}.{ProjectIdColumn} = {ProjectDao.TableName}.{ProjectDao.IdColumn} " +
                    $"WHERE {ProjectDao.TableName}.{ProjectDao.GuidColumn} = @ProjectGuid AND {TableName}.{CategoryColumn} = @{CategoryColumn}",
                    new { ProjectGuid = projectGuid, category = type.GetDescription() }).ToList();
                return templates;
            }
            catch (Exception e)
            {
                Log.Error($"Error occured in GetReportTemplateInfos() method - {e.Message}");
                throw;
            }
        }

        internal IList<ReportTemplate> GetReportTemplateInfos(IDbConnection connection, Guid projectGuid)
        {
            try
            {
                var templates = connection.Query<ReportTemplate>(SelectInfoSql +
                    $"INNER JOIN {ProjectDao.TableName} ON {TableName}.{ProjectIdColumn} = {ProjectDao.TableName}.{ProjectDao.IdColumn} " +
                    $"WHERE {ProjectDao.TableName}.{ProjectDao.GuidColumn} = @ProjectGuid ",
                    new { ProjectGuid = projectGuid }).ToList();

                return templates;
            }
            catch (Exception e)
            {
                Log.Error(
                    $"Error occured in GetList() method of class{GetType().Name} - {e.Message}");
                throw;
            }
        }

        internal bool IsExists(IDbConnection connection, Guid templateGuid)
        {
            try
            {
                bool exist = connection.ExecuteScalar<bool>(ExistsSql, new { id = templateGuid });
                return exist;
            }
            catch (Exception e)
            {
                Log.Error(
                    $"Error occured in IsExists() method of class{GetType().Name} - {e.Message}");
                throw;
            }
        }

        public bool IsExists(IDbConnection connection, Guid projectGuid, string reportTemplateName)
        {
	        try
	        {
		        var reportTemplate = GetInfoByName(connection, projectGuid, reportTemplateName);
				return reportTemplate != null;
	        }
			catch (Exception e)
	        {
		        Log.Error(
			        $"Error occured in IsExists() method of class{GetType().Name} - {e.Message}");
		        throw;
	        }
        }

        public void Insert(IDbConnection connection, ReportTemplate template)
        {
            try
            {
                template.ModifiedDate = DateTime.UtcNow;
                template.CreatedDate = DateTime.UtcNow;
                connection.ExecuteScalar(InsertSql, template);
            }
            catch (Exception e)
            {
                Log.Error(
                    $"Error occured in Insert() method of class{GetType().Name} - {e.Message}");
                throw;
            }
        }

        public void InsertCopy(IDbConnection connection, ReportTemplate template)
        {
	        try
	        {
		        connection.ExecuteScalar(InsertSql_ReviewApprove, template);
	        }
	        catch (Exception e)
	        {
		        Log.Error(
			        $"Error occured in InsertCopy() method of class{GetType().Name} - {e.Message}");
		        throw;
	        }
        }

		public void Update(IDbConnection connection, ReportTemplate template)
        {
            try
            {
                template.ModifiedDate = DateTime.UtcNow;
                connection.ExecuteScalar(
                $"UPDATE {TableName} SET {TemplateNameColumn} = @{TemplateNameColumn}, {ModifiedDateColumn} = @{ModifiedDateColumn}, " +
                $"{ContentColumn} = @{ContentColumn}, {ConfigColumn} = @{ConfigColumn}, {ModifiedUserIdColumn} = @{ModifiedUserIdColumn},  " +
                $"{ModifiedUserNameColumn} = @{ModifiedUserNameColumn},{ReviewApproveStateColumn} = @{ReviewApproveStateColumn}, "+
                $"{CreatedUserIdColumn} = @{CreatedUserIdColumn},{CreatedUserNameColumn} =@{CreatedUserNameColumn} WHERE {IdColumn} =@{IdColumn}", template);
            }
            catch (Exception e)
            {
                Log.Error(
                    $"Error occured in Update() method of class{GetType().Name} - {e.Message}");
                throw;
            }
        }

        public void Delete(IDbConnection connection, Guid templateId)
        {
            try
            {
                connection.ExecuteScalar($"DELETE FROM {TableName} WHERE {IdColumn} ='{templateId}'");
            }
            catch (Exception e)
            {
                Log.Error(
                    $"Error occured in Insert() method of class{GetType().Name} - {e.Message}");
                throw;
            }
        }

        public void UpdateReviewApproveState(IDbConnection connection, string projectName, string templateId, ReviewApproveState state, string modifiedUser)
        {
            try
            {
                long? projectId = ProjectDao.GetProjectId(connection, projectName);
                if (projectId.HasValue)
                {
                    var newState = (short)state;
                    string sql =
                        $"UPDATE {TableName} SET {ModifiedDateColumn} = '{DateTime.UtcNow}', {ModifiedUserIdColumn} = '{modifiedUser}', " +
                        $"{ReviewApproveStateColumn} = {newState} " +
                        $"WHERE {IdColumn} = '{templateId}'";
                    connection.Execute(sql);
                }
                else
                {
                    Log.Error("Unable to Update Report Template as Project Name does not Exist");
                    throw new NullReferenceException();
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error in UpdateReviewApproveState method", ex);
                throw;
            }
        }

    }
}
