﻿using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Newtonsoft.Json;
using PerkinElmer.Simplicity.Data.Version15.DataSources.Postgresql.Chromatography;
using PerkinElmer.Simplicity.Data.Version15.DataTargets;
using PerkinElmer.Simplicity.Data.Version15.DataTargets.Postgresql.Chromatography;
using PerkinElmer.Simplicity.Data.Version15.Version;
using PerkinElmer.Simplicity.Data.Version15.Version.Context;
using PerkinElmer.Simplicity.Data.Version15.Version.Data;
using PerkinElmer.Simplicity.Data.Version15.Version.Data.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version15
{
    public sealed class Version15 : ISourceBlock<Version15DataBase>, ITargetBlock<Version15DataBase>
    {
        private readonly ActionBlock<Version15DataBase> _target;
        private readonly BufferBlock<Version15DataBase> _source;

        public Version15()
        {
            _source = new BufferBlock<Version15DataBase>();
            _target = new ActionBlock<Version15DataBase>(SaveVersionData);
        }

        internal TargetType TargetType { get; set; } = TargetType.Unknown;

        public void StartSourceDataflow(string sourceConfig)
        {
            var migrationSourceContext = JsonConvert.DeserializeObject<MigrationSourceContext>(sourceConfig);
            switch (migrationSourceContext.MigrationType)
            {
                case MigrationTypes.Upgrade:
                    UpgradePostgresql(migrationSourceContext.IsIncludeAuditTrailLog);
                    break;
            }
        }

        public void ApplyTargetConfiguration(string targetConfig)
        {
            var migrationTargetContext = JsonConvert.DeserializeObject<MigrationTargetContext>(targetConfig);
            switch (migrationTargetContext.MigrationType)
            {
                case MigrationTypes.Upgrade:
                    TargetType = TargetType.Posgresql;
                    Version15Host.PreparePostgresqlHost();
                    break;
            }
        }

        #region ISourceBlock members

        public Version15DataBase ConsumeMessage(DataflowMessageHeader messageHeader, ITargetBlock<Version15DataBase> target, out bool messageConsumed)
        {
            return ((ISourceBlock<Version15DataBase>)_source).ConsumeMessage(messageHeader, target, out messageConsumed);
        }

        public IDisposable LinkTo(ITargetBlock<Version15DataBase> target, DataflowLinkOptions linkOptions)
        {
            return _source.LinkTo(target, linkOptions);
        }

        public bool ReserveMessage(DataflowMessageHeader messageHeader, ITargetBlock<Version15DataBase> target)
        {
            return ((ISourceBlock<Version15DataBase>)_source).ReserveMessage(messageHeader, target);
        }

        public void ReleaseReservation(DataflowMessageHeader messageHeader, ITargetBlock<Version15DataBase> target)
        {
            ((ISourceBlock<Version15DataBase>)_source).ReleaseReservation(messageHeader, target);
        }

        #endregion

        #region ITargetBlock members

        public DataflowMessageStatus OfferMessage(DataflowMessageHeader messageHeader, Version15DataBase messageValue, ISourceBlock<Version15DataBase> source,
            bool consumeToAccept)
        {
            return ((ITargetBlock<Version15DataBase>)_target).OfferMessage(messageHeader, messageValue, source, consumeToAccept);
        }

        #endregion

        #region IDataflowBlock members

        public void Complete()
        {
            _target.Complete();
        }

        public void Fault(Exception error)
        {
            ((IDataflowBlock)_target).Fault(error);
        }

        public Task Completion => _source.Completion;

        #endregion

        private void UpgradePostgresql(bool isIncludeAuditTrail)
        {
            var projects = ProjectSource.GetAllProjects();

            foreach (var project in projects)
            {
                if (!(project is ProjectData projectData)) continue;
                
                var projectGuid = projectData.Project.Guid;
                Task.Run(async () => { await _source.SendAsync(projectData); });

                var acqusitionMethods = AcquisitionMethodSource.GetAcqusitionMethods(projectGuid, isIncludeAuditTrail);
                foreach (var acqusitionMethod in acqusitionMethods)
                    _source.Post(acqusitionMethod);

                var analysisResultSets = AnalysisResultSetSource.GetAnalysisResultSets(projectGuid, isIncludeAuditTrail);
                foreach(var analysisResultSet in analysisResultSets)
                    Task.Run(async () => { await _source.SendAsync(analysisResultSet); });

                var compoundLibraries = CompoundLibrarySource.GetCompoundLibrary(projectGuid);
                foreach(var compoundLibrary in compoundLibraries)
                    _source.Post(compoundLibrary);

                var processingMethods = ProcessingMethodSource.GetProcessingMethods(projectGuid, isIncludeAuditTrail);
                foreach (var processingMethod in processingMethods)
                    _source.Post(processingMethod);

                var reportTemplates = ReportTemplateSource.GetReportTemplates(projectGuid, isIncludeAuditTrail);
                foreach (var reportTemplate in reportTemplates)
                    _source.Post(reportTemplate);

                var sequences = SequenceSource.GetSequence(projectGuid, isIncludeAuditTrail);
                foreach (var sequence in sequences)
                    _source.Post(sequence);
            }
        }

        private void SaveVersionData(Version15DataBase versionData)
        {
            if (TargetType == TargetType.Unknown)
                throw new ArgumentException("Target type not set!");

            switch (TargetType)
            {
                case TargetType.Posgresql:
                    SavePostgresqlVersionData(versionData);
                    break;
            }
        }

        private void SavePostgresqlVersionData(Version15DataBase versionData)
        {
            switch (versionData.Version15DataTypes)
            {
                case Version15DataTypes.AcqusitionMethod:
                    if (versionData is AcqusitionMethodData acqusitionMethodData)
                        AcquisitionMethodTarget.SaveAcquisitionMethod(acqusitionMethodData);
                    break;
                case Version15DataTypes.AnalysisResultSet:
                    if (versionData is AnalysisResultSetData analysisResultSetData)
                        AnalysisResultSetTarget.SaveAnalysisResultSet(analysisResultSetData);
                    break;
                case Version15DataTypes.BatchResultSet:
                    if (versionData is BatchResultSetData batchResultSetData)
                        BatchResultSetTarget.SaveBatchResultSet(batchResultSetData);
                    break;
                case Version15DataTypes.CompoundLibrary:
                    if (versionData is CompoundLibraryData compoundLibraryData)
                        CompoundLibraryTarget.SaveCompoundLibrary(compoundLibraryData);
                    break;
                case Version15DataTypes.ProcessingMethod:
                    if (versionData is ProcessingMethodData processingMethodData)
                        ProcessingMethodTarget.SaveProcessingMethod(processingMethodData);
                    break;

                case Version15DataTypes.Project:
                    if (versionData is ProjectData projectData)
                        ProjectTarget.SaveProject(projectData);
                    break;
                case Version15DataTypes.ReportTemplate:
                    if (versionData is ReportTemplateData reportTemplateData)
                        ReportTemplateTarget.SaveReportTemplate(reportTemplateData);
                    break;
                case Version15DataTypes.Sequence:
                    if (versionData is SequenceData sequenceData)
                        SequenceTarget.SaveSequence(sequenceData);
                    break;
            }
        }
    }

}