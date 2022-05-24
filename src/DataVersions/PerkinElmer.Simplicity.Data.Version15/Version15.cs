using System;
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
    public sealed class Version15 : ISourceBlock<object>, ITargetBlock<object>
    {
        private readonly ActionBlock<object> _target;
        private readonly BufferBlock<object> _source;

        public Version15()
        {
            _source = new BufferBlock<object>();
            _target = new ActionBlock<object>(SaveVersionData);
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

        public void PrepareTarget(string targetConfig)
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

        public object ConsumeMessage(DataflowMessageHeader messageHeader, ITargetBlock<object> target, out bool messageConsumed)
        {
            return ((ISourceBlock<object>)_source).ConsumeMessage(messageHeader, target, out messageConsumed);
        }

        public IDisposable LinkTo(ITargetBlock<object> target, DataflowLinkOptions linkOptions)
        {
            return _source.LinkTo(target, linkOptions);
        }

        public bool ReserveMessage(DataflowMessageHeader messageHeader, ITargetBlock<object> target)
        {
            return ((ISourceBlock<object>)_source).ReserveMessage(messageHeader, target);
        }

        public void ReleaseReservation(DataflowMessageHeader messageHeader, ITargetBlock<object> target)
        {
            ((ISourceBlock<object>)_source).ReleaseReservation(messageHeader, target);
        }

        #endregion

        #region ITargetBlock members

        public DataflowMessageStatus OfferMessage(DataflowMessageHeader messageHeader, object messageValue, ISourceBlock<object> source,
            bool consumeToAccept)
        {
            return ((ITargetBlock<object>)_target).OfferMessage(messageHeader, messageValue, source, consumeToAccept);
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

        private void SaveVersionData(object versionData)
        {
            if (!(versionData is Version15DataBase version15DataBase))
                throw new ArgumentException("Version data type is incorrect!");
            if (TargetType == TargetType.Unknown)
                throw new ArgumentException("Target type not set!");
            switch (TargetType)
            {
                case TargetType.Posgresql:
                    SavePostgresqlVersionData(version15DataBase);
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
