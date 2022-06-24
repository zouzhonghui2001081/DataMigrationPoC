using System;
using System.Data;
using System.IO;
using Npgsql;
using PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.LabManagement;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.JsonConverter;
using PerkinElmer.Simplicity.Data.Version15.Version.Context.TargetContext;

namespace PerkinElmer.Simplicity.Data.Version15.Dummy.DummyData
{
    internal class ProjectDummyData
    {
        private const string ProjectTemplate = "PerkinElmer.Simplicity.Data.Version15.Dummy.DummyData.Templates.Project.json";

        public Project CreateDummyProject(PostgresqlTargetContext postgresqlTargetContext)
        {
            var projectJson = GetProjectTemplate();
            var projectInfo = JsonConverter.FromJson<IProjectInfo>(projectJson);
            projectInfo.Guid = Guid.NewGuid();
            projectInfo.Name = "Dummy Project " + Guid.NewGuid().ToString().Substring(0,8);
            using var connection = new NpgsqlConnection(postgresqlTargetContext.ChromatographyConnectionString);
            if (connection.State != ConnectionState.Open) connection.Open();

            var projectEntity = new Project();
            DomainContractAdaptor.PopulateProjectEntity(projectInfo, projectEntity);
            return projectEntity;
        }

        private string GetProjectTemplate()
        {
            var assembly = typeof(ProjectDummyData).Assembly;

            using var stream = assembly.GetManifestResourceStream(ProjectTemplate);
            using var reader = new StreamReader(stream ?? throw new InvalidOperationException(
                                                    $"Failed to load resource {ProjectTemplate}"));
            return reader.ReadToEnd();
        }
    }
}
