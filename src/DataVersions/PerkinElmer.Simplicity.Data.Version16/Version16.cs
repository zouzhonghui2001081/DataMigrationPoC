using System;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Newtonsoft.Json;
using PerkinElmer.Simplicity.Data.Version16.DataSources.Postgresql.Chromatography;
using PerkinElmer.Simplicity.Data.Version16.DataTargets;
using PerkinElmer.Simplicity.Data.Version16.DataTargets.Postgresql.Chromatography;
using PerkinElmer.Simplicity.Data.Version16.Version;
using PerkinElmer.Simplicity.Data.Version16.Version.Context;
using PerkinElmer.Simplicity.Data.Version16.Contract.Version;
using PerkinElmer.Simplicity.Data.Version16.Contract.Version.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version16
{
    public class Version16 : ISourceBlock<object>, ITargetBlock<object>,
        ISourceBlock<string>
    {
        private readonly ActionBlock<object> _targetData;
        private readonly BufferBlock<object> _sourceData;
        private readonly BufferBlock<string> _sourceMessage;

        public Version16(CancellationToken cancellToken)
        {
            var dataflowOption = new DataflowBlockOptions { CancellationToken = cancellToken };
            var executeDataFlowOption = new ExecutionDataflowBlockOptions
            {
                //MaxDegreeOfParallelism = Environment.ProcessorCount,
                MaxDegreeOfParallelism = 1,
                CancellationToken = cancellToken
            };
            _sourceData = new BufferBlock<object>(dataflowOption);
            _sourceMessage = new BufferBlock<string>(dataflowOption);
            _targetData = new ActionBlock<object>(SaveVersionData, executeDataFlowOption);
        }

        public void StartSourceDataflow(string sourceConfig)
        {
            var migrationSourceContext = JsonConvert.DeserializeObject<MigrationSourceContext>(sourceConfig);
            switch (migrationSourceContext.MigrationType)
            {
                case MigrationTypes.Upgrade:
                    UpgradePostgresql(migrationSourceContext.IsIncludeAuditTrailLog);
                    break;
            }

            _sourceData.Complete();
        }

        public void PrepareTarget(string targetConfig)
        {
            var migrationTargetContext = JsonConvert.DeserializeObject<MigrationTargetContext>(targetConfig);
            switch (migrationTargetContext.MigrationType)
            {
                case MigrationTypes.Upgrade:
                    TargetType = TargetType.Posgresql;
                    Version16Host.PreparePostgresqlHost();
                    break;
            }
        }

        public TargetType TargetType { get; set; } = TargetType.Unknown;

        #region ISourceBlock<object> members

        public object ConsumeMessage(DataflowMessageHeader messageHeader, ITargetBlock<object> target, out bool messageConsumed)
        {
            return ((ISourceBlock<object>)_sourceData).ConsumeMessage(messageHeader, target, out messageConsumed);
        }

        public IDisposable LinkTo(ITargetBlock<object> target, DataflowLinkOptions linkOptions)
        {
            return _sourceData.LinkTo(target, linkOptions);
        }

        public bool ReserveMessage(DataflowMessageHeader messageHeader, ITargetBlock<object> target)
        {
            return ((ISourceBlock<object>)_sourceData).ReserveMessage(messageHeader, target);
        }

        public void ReleaseReservation(DataflowMessageHeader messageHeader, ITargetBlock<object> target)
        {
            ((ISourceBlock<object>)_sourceData).ReleaseReservation(messageHeader, target);
        }

        #endregion

        #region ISourceBlock<string> members

        public IDisposable LinkTo(ITargetBlock<string> target, DataflowLinkOptions linkOptions)
        {
            return _sourceMessage.LinkTo(target);
        }

        public string ConsumeMessage(DataflowMessageHeader messageHeader, ITargetBlock<string> target, out bool messageConsumed)
        {
            return ((ISourceBlock<string>)_sourceMessage).ConsumeMessage(messageHeader, target, out messageConsumed);
        }

        public bool ReserveMessage(DataflowMessageHeader messageHeader, ITargetBlock<string> target)
        {
            return ((ISourceBlock<string>)_sourceMessage).ReserveMessage(messageHeader, target);
        }

        public void ReleaseReservation(DataflowMessageHeader messageHeader, ITargetBlock<string> target)
        {
            ((ISourceBlock<string>)_sourceMessage).ReleaseReservation(messageHeader, target);
        }
        #endregion

        #region ITargetBlock<object> members

        public DataflowMessageStatus OfferMessage(DataflowMessageHeader messageHeader, object messageValue, ISourceBlock<object> source,
            bool consumeToAccept)
        {
            return ((ITargetBlock<object>)_targetData).OfferMessage(messageHeader, messageValue, source, consumeToAccept);
        }

        #endregion

        #region IDataflowBlock members

        public void Complete()
        {
            _targetData.Complete();
        }

        public void Fault(Exception error)
        {
            ((IDataflowBlock)_targetData).Fault(error);
        }

        public Task Completion => _targetData.Completion;

        #endregion

        private void UpgradePostgresql(bool isIncludeAuditTrail)
        {
            var projects = ProjectSource.GetAllProjects();

            foreach (var project in projects)
            {
                if (!(project is ProjectData projectData)) continue;

                var projectGuid = projectData.Project.Guid;
                Task.Run(async () => { await _sourceData.SendAsync(projectData); });

                var acqusitionMethods = AcquisitionMethodSource.GetAcqusitionMethods(projectGuid, isIncludeAuditTrail);
                foreach (var acqusitionMethod in acqusitionMethods)
                    _sourceData.Post(acqusitionMethod);

                var analysisResultSets = AnalysisResultSetSource.GetAnalysisResultSets(projectGuid, isIncludeAuditTrail);
                foreach (var analysisResultSet in analysisResultSets)
                    Task.Run(async () => { await _sourceData.SendAsync(analysisResultSet); });

                var compoundLibraries = CompoundLibrarySource.GetCompoundLibrary(projectGuid);
                foreach (var compoundLibrary in compoundLibraries)
                    _sourceData.Post(compoundLibrary);

                var processingMethods = ProcessingMethodSource.GetProcessingMethods(projectGuid, isIncludeAuditTrail);
                foreach (var processingMethod in processingMethods)
                    _sourceData.Post(processingMethod);

                var reportTemplates = ReportTemplateSource.GetReportTemplates(projectGuid, isIncludeAuditTrail);
                foreach (var reportTemplate in reportTemplates)
                    _sourceData.Post(reportTemplate);

                var sequences = SequenceSource.GetSequence(projectGuid, isIncludeAuditTrail);
                foreach (var sequence in sequences)
                    _sourceData.Post(sequence);
            }
        }

        private void SaveVersionData(object versionData)
        {
            if (!(versionData is Version16DataBase version16DataBase))
                throw new ArgumentException("Version data type is incorrect!");
            if(TargetType == TargetType.Unknown)
                throw new ArgumentException("Target type not set!");
            switch (TargetType)
            {
                case TargetType.Posgresql:
                    SavePostgresqlVersionData(version16DataBase);
                    break;
            }
        }

        private void SavePostgresqlVersionData(Version16DataBase versionData)
        {
            switch (versionData.Version16DataTypes)
            {
                case Version16DataTypes.AcqusitionMethod:
                    if (versionData is AcqusitionMethodData acqusitionMethodData)
                    {
                        AcquisitionMethodTarget.SaveAcquisitionMethod(acqusitionMethodData);
                        _sourceMessage.Post($"Acuqistion method {acqusitionMethodData.AcquisitionMethod.MethodName} saved");
                    }
                        
                    break;
                case Version16DataTypes.AnalysisResultSet:
                    if (versionData is AnalysisResultSetData analysisResultSetData)
                        AnalysisResultSetTarget.SaveAnalysisResultSet(analysisResultSetData);
                    break;
                case Version16DataTypes.BatchResultSet:
                    if (versionData is BatchResultSetData batchResultSetData)
                        BatchResultSetTarget.SaveBatchResultSet(batchResultSetData);
                    break;
                case Version16DataTypes.CompoundLibrary:
                    if (versionData is CompoundLibraryData compoundLibraryData)
                        CompoundLibraryTarget.SaveCompoundLibrary(compoundLibraryData);
                    break;
                case Version16DataTypes.ProcessingMethod:
                    if (versionData is ProcessingMethodData processingMethodData)
                        ProcessingMethodTarget.SaveProcessingMethod(processingMethodData);
                    break;

                case Version16DataTypes.Project:
                    if (versionData is ProjectData projectData)
                        ProjectTarget.SaveProject(projectData);
                    break;
                case Version16DataTypes.ReportTemplate:
                    if (versionData is ReportTemplateData reportTemplateData)
                        ReportTemplateTarget.SaveReportTemplate(reportTemplateData);
                    break;
                case Version16DataTypes.Sequence:
                    if (versionData is SequenceData sequenceData)
                        SequenceTarget.SaveSequence(sequenceData);
                    break;
            }
        }
    }
}
