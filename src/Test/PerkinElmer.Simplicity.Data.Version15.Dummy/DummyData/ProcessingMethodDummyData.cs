using System;
using System.Data;
using System.IO;
using Npgsql;
using PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography.ProcessingMethod;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Processing;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.JsonConverter;
using PerkinElmer.Simplicity.Data.Version15.DataTargets.Postgresql.Chromatography;
using PerkinElmer.Simplicity.Data.Version15.Version.Context.TargetContext;

namespace PerkinElmer.Simplicity.Data.Version15.Dummy.DummyData
{
    internal class ProcessingMethodDummyData
    {
        private const string ProcessingMethodTemplate = "PerkinElmer.Simplicity.Data.Version15.Dummy.DummyData.Templates.ProcessingMethod.json";

        public void CreateDummyProcessingMethod(PostgresqlTargetContext postgresqlTargetContext, Guid projectGuid,
            int processingMethodCount)
        {
            var processingMethodJson = GetProcessingMethodTemplate();
            var processingMethod = JsonConverter.FromJson<IProcessingMethod>(processingMethodJson);
            var processingMethodName = "Dummy Processing Method ";
            using var connection = new NpgsqlConnection(postgresqlTargetContext.ChromatographyConnectionString);
            if (connection.State != ConnectionState.Open) connection.Open();

            for (var i = 1; i <= processingMethodCount; i++)
            {
                processingMethod.Info.Guid = Guid.NewGuid();
                processingMethod.Info.Name = processingMethodName + i.ToString("0000") + " " + Guid.NewGuid().ToString().Substring(0, 8);
                var processingMethodEntity = new ProcessingMethod();
                DomainContractAdaptor.PopulateProcessingMethodEntity(processingMethod, processingMethodEntity);
                ProcessingMethodTarget.CreateProcessingMethod(connection, projectGuid, processingMethodEntity);
            }
        }

        private string GetProcessingMethodTemplate()
        {
            var assembly = typeof(ProcessingMethodDummyData).Assembly;

            using var stream = assembly.GetManifestResourceStream(ProcessingMethodTemplate);
            using var reader = new StreamReader(stream ?? throw new InvalidOperationException(
                                                    $"Failed to load resource {ProcessingMethodTemplate}"));
            return reader.ReadToEnd();
        }
    }
}
