using System;
using System.Data;
using System.IO;
using Npgsql;
using PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography.AcquisitionMethod;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Acquisition;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.JsonConverter;
using PerkinElmer.Simplicity.Data.Version15.DataTargets.Postgresql.Chromatography;
using PerkinElmer.Simplicity.Data.Version15.Version.Context.TargetContext;


namespace PerkinElmer.Simplicity.Data.Version15.Dummy.DummyData
{
    internal class AcqusitionMethodDummyData
    {
        private const string AcqusitionMethodTemplate = "PerkinElmer.Simplicity.Data.Version15.Dummy.DummyData.Templates.AcqusitionMethod.json";

        public void CreateDummyAcqusitionMethod(PostgresqlTargetContext postgresqlTargetContext, Guid projectGuid,
            int acqusitionMethodCount)
        {
            var acqusitionMethodJson = GetAcqusitionMethodTemplate();
            var acquisitionMethod = JsonConverter.FromJson<IAcquisitionMethod>(acqusitionMethodJson);
            var acqusitionMethodName = "Dummy Acqusition Method ";
            using var connection = new NpgsqlConnection(postgresqlTargetContext.ChromatographyConnectionString);
            if (connection.State != ConnectionState.Open) connection.Open();

            for (var i = 0; i < acqusitionMethodCount; i++)
            {
                acquisitionMethod.Info.Guid = Guid.NewGuid();
                acquisitionMethod.Info.Name = acqusitionMethodName + i + Guid.NewGuid().ToString().Substring(0,8);
                var acqusitionMethodEntity = new AcquisitionMethod();
                DomainContractAdaptor.PopulateAcquisitionMethodEntity(acquisitionMethod, acqusitionMethodEntity);
                AcquisitionMethodTarget.CreateProjectAcquisitionMethod(connection, projectGuid, acqusitionMethodEntity);
            }
        }

        private string GetAcqusitionMethodTemplate()
        {
            var assembly = typeof(AcqusitionMethodDummyData).Assembly;

            using var stream = assembly.GetManifestResourceStream(AcqusitionMethodTemplate);
            using var reader = new StreamReader(stream ?? throw new InvalidOperationException(
                                                    $"Failed to load resource {AcqusitionMethodTemplate}"));
            return reader.ReadToEnd();
        }
        
    }
}
