
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Npgsql;
using PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography;
using PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography.AcquisitionMethod;
using PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography.ProcessingMethod;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Acquisition;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.JsonConverter;
using PerkinElmer.Simplicity.Data.Version15.Contract.Version.Chromatography;
using PerkinElmer.Simplicity.Data.Version15.DataTargets.Postgresql.Chromatography;
using PerkinElmer.Simplicity.Data.Version15.Version.Context.TargetContext;

namespace PerkinElmer.Simplicity.Data.Version15.Dummy.DummyData
{
    internal class SequenceDummyData
    {
        private const string SequenceTemplate = "PerkinElmer.Simplicity.Data.Version15.Dummy.DummyData.Templates.Sequence.json";

        public void CreateDummySequence(PostgresqlTargetContext postgresqlTargetContext, Guid projectGuid,
            int sequenceCount)
        {
            var sequenceJson = GetSequenceTemplate();
            var sequence = JsonConverter.FromJson<ISequence>(sequenceJson);

            using var connection = new NpgsqlConnection(postgresqlTargetContext.ChromatographyConnectionString);
            if (connection.State != ConnectionState.Open) connection.Open();

            var sequenceInfoEntity = new Sequence();
            PopulateSequenceEntity(sequenceInfoEntity, sequence);
           
            foreach (var acquisitionMethod in sequence.ExternalBaselineRunsAcquisitionMethods)
            {
                var acqusitionMethodEntity = new AcquisitionMethod();
                DomainContractAdaptor.PopulateAcquisitionMethodEntity(acquisitionMethod, acqusitionMethodEntity);
                AcquisitionMethodTarget.CreateProjectAcquisitionMethod(connection, projectGuid, acqusitionMethodEntity);
            }

            foreach (var processingMethod in sequence.ExternalBaselineRunsProcessingMethods)
            {
                var processingMethodEntity = new ProcessingMethod();
                DomainContractAdaptor.PopulateProcessingMethodEntity(processingMethod, processingMethodEntity);
                ProcessingMethodTarget.CreateProcessingMethod(connection, projectGuid, processingMethodEntity);
            }

            var dummySequenceName = "Dummy Acqusition Method ";
            for (var i = 0; i < sequenceCount; i++)
            {
                sequenceInfoEntity.Guid = Guid.NewGuid();
                sequenceInfoEntity.Name = dummySequenceName + i + Guid.NewGuid().ToString().Substring(0, 8);
                foreach (var sequenceSampeInfo in sequenceInfoEntity.SequenceSampleInfos)
                    sequenceSampeInfo.Guid = Guid.NewGuid();

                var sequenceData = new SequenceData
                {
                    ProjectGuid = projectGuid,
                    Sequence = sequenceInfoEntity
                };
                SequenceTarget.SaveSequence(sequenceData, postgresqlTargetContext);
            }
        }

        private void PopulateSequenceEntity(Sequence sequenceEntity, ISequence sequenceDomain)
        {
            sequenceEntity.Name = sequenceDomain.Info.Name;
            sequenceEntity.Guid = sequenceDomain.Info.Guid;
            sequenceEntity.CreatedDate = sequenceDomain.Info.CreatedDateUtc;
            sequenceEntity.CreatedUserId = sequenceDomain.Info.CreatedByUser.UserId;
            sequenceEntity.CreatedUserName = sequenceDomain.Info.CreatedByUser.UserFullName;
            sequenceEntity.ModifiedDate = sequenceDomain.Info.ModifiedDateUtc;
            sequenceEntity.ModifiedUserId = sequenceDomain.Info.ModifiedByUser.UserId;
            sequenceEntity.ModifiedUserName = sequenceDomain.Info.ModifiedByUser.UserFullName;

            sequenceEntity.SequenceSampleInfos = new List<SequenceSampleInfo>();
            foreach (var sample in sequenceDomain.Samples)
            {
                var sequenceSampleInfo = new SequenceSampleInfo();
                DomainContractAdaptor.PopulateSequenceSampleInfoEntity(sample, sequenceSampleInfo);
                sequenceEntity.SequenceSampleInfos.Add(sequenceSampleInfo);
            }
        }

        private string GetSequenceTemplate()
        {
            var assembly = typeof(SequenceDummyData).Assembly;

            using var stream = assembly.GetManifestResourceStream(SequenceTemplate);
            using var reader = new StreamReader(stream ?? throw new InvalidOperationException(
                                                    $"Failed to load resource {SequenceTemplate}"));
            return reader.ReadToEnd();
        }
    }
}
