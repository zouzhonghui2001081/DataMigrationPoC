using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version15.DataSources.Postgresql.Chromatography;
using PerkinElmer.Simplicity.Data.Version15.DataTargets;
using PerkinElmer.Simplicity.Data.Version15.DataTargets.Postgresql.Chromatography;
using PerkinElmer.Simplicity.Data.Version15.Version;
using PerkinElmer.Simplicity.Data.Version15.Version.Context;
using PerkinElmer.Simplicity.Data.Version15.Contract.Version;
using PerkinElmer.Simplicity.Data.Version15.Contract.Version.Chromatography;
using PerkinElmer.Simplicity.Data.Version15.Version.Context.SourceContext;
using PerkinElmer.Simplicity.Data.Version15.Version.Context.TargetContext;

namespace PerkinElmer.Simplicity.Data.Version15
{
    public sealed class Version15 : ISourceBlock<object>, ITargetBlock<object>,
        ISourceBlock<string>
    {
        private readonly ActionBlock<object> _targetData;
        private readonly BufferBlock<object> _sourceData;
        private readonly BufferBlock<string> _sourceMessage;

        public Version15(CancellationToken cancellToken)
        {
            var dataflowOption = new DataflowBlockOptions {CancellationToken = cancellToken};
            var executeDataFlowOption = new ExecutionDataflowBlockOptions
            {
                MaxDegreeOfParallelism = Environment.ProcessorCount,
                CancellationToken = cancellToken
            };
            _sourceData = new BufferBlock<object>(dataflowOption);
            _sourceMessage = new BufferBlock<string>(dataflowOption);
            _targetData = new ActionBlock<object>(SaveVersionData, executeDataFlowOption);
        }

        internal TargetType TargetType { get; set; } = TargetType.Unknown;

        internal PostgresqlTargetContext PostgresqlTargetContext { get; set; }

        public void StartDataflow(string sourceConfig)
        {
            var source = JObject.Parse(sourceConfig);
            var migrationType = (string)source["MigrationType"];
            var payload = (string) source["Payload"];            
            
            switch (migrationType)
            {
                case MigrationTypes.Upgrade:
                    var postgresqlSourceContext = JsonConvert.DeserializeObject<PostgresqlSourceContext>(payload);
                    Version15Host.CreateChromatographyDatabaseIndex(postgresqlSourceContext.ChromatographyConnectionString);
                    UpgradePostgresql(postgresqlSourceContext);
                    break;
            }

            _sourceData.Complete();
        }

        public void PrepareTarget(string targetConfig)
        {
            var target = JObject.Parse(targetConfig);
            var migrationType = (string)target["MigrationType"];
            var payload = (string)target["Payload"];
            switch (migrationType)
            {
                case MigrationTypes.Upgrade:
                    TargetType = TargetType.Posgresql;
                    PostgresqlTargetContext = JsonConvert.DeserializeObject<PostgresqlTargetContext>(payload);
                    Version15Host.PreparePostgresqlHost(PostgresqlTargetContext);
                    break;
            }
        }

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

        private void UpgradePostgresql(PostgresqlSourceContext posgresqlSourceContext)
        {
            var projects = ProjectSource.GetAllProjects(posgresqlSourceContext);
            foreach (var project in projects)
            {
                if (!(project is ProjectData projectData)) continue;
                
                var projectGuid = projectData.Project.Guid;
                var projectId = projectData.Project.Id;

                _sourceData.SendAsync(projectData);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                Thread.Sleep(15000);

                var acqusitionMethods = AcquisitionMethodSource.GetAcqusitionMethods(projectGuid, posgresqlSourceContext);
                foreach (var acqusitionMethod in acqusitionMethods)
                    _sourceData.Post(acqusitionMethod);

                var analysisResultGuids = AnalysisResultSetSource.GetAnalysisResultSetGuids(projectId, posgresqlSourceContext);
                foreach (var analysisResultGuid in analysisResultGuids)
                {
                    var stopWatch = new Stopwatch();
                    stopWatch.Start();
                    var analysisResultSets = AnalysisResultSetSource.GetAnalysisResultSets(projectGuid,
                        new List<Guid> { analysisResultGuid }, posgresqlSourceContext);
                    stopWatch.Stop();
                    var cost = stopWatch.ElapsedMilliseconds;
                    foreach (var analysisResultSet in analysisResultSets)
                        Task.Run(async () => { await _sourceData.SendAsync(analysisResultSet); });
                }
                //Parallel.ForEach(analysisResultGuids, new ParallelOptions { MaxDegreeOfParallelism = 1 }, analysisResultGuid =>
                //{
                //    var analysisResultSets = AnalysisResultSetSource.GetAnalysisResultSets(projectGuid,
                //        new List<Guid> { analysisResultGuid }, posgresqlSourceContext);
                //    foreach (var analysisResultSet in analysisResultSets)
                //        Task.Run(async () => { await _sourceData.SendAsync(analysisResultSet); });
                //});

                var compoundLibraries = CompoundLibrarySource.GetCompoundLibrary(projectGuid, posgresqlSourceContext);
                foreach (var compoundLibrary in compoundLibraries)
                    _sourceData.Post(compoundLibrary);

                var processingMethods = ProcessingMethodSource.GetProcessingMethods(projectGuid, posgresqlSourceContext);
                foreach (var processingMethod in processingMethods)
                    _sourceData.Post(processingMethod);

                var reportTemplates = ReportTemplateSource.GetReportTemplates(projectGuid, posgresqlSourceContext);
                foreach (var reportTemplate in reportTemplates)
                    _sourceData.Post(reportTemplate);

                var sequences = SequenceSource.GetSequence(projectGuid, posgresqlSourceContext);
                foreach (var sequence in sequences)
                    _sourceData.Post(sequence);
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
                    if (PostgresqlTargetContext == null)
                        throw new ArgumentException("Target context not set!");
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
                        AcquisitionMethodTarget.SaveAcquisitionMethod(acqusitionMethodData, PostgresqlTargetContext);
                    break;
                case Version15DataTypes.AnalysisResultSet:
                    if (versionData is AnalysisResultSetData analysisResultSetData)
                        AnalysisResultSetTarget.SaveAnalysisResultSet(analysisResultSetData, PostgresqlTargetContext);
                    break;
                case Version15DataTypes.BatchResultSet:
                    if (versionData is BatchResultSetData batchResultSetData)
                        BatchResultSetTarget.SaveBatchResultSet(batchResultSetData, PostgresqlTargetContext);
                    break;
                case Version15DataTypes.CompoundLibrary:
                    if (versionData is CompoundLibraryData compoundLibraryData)
                        CompoundLibraryTarget.SaveCompoundLibrary(compoundLibraryData, PostgresqlTargetContext);
                    break;
                case Version15DataTypes.ProcessingMethod:
                    if (versionData is ProcessingMethodData processingMethodData)
                        ProcessingMethodTarget.SaveProcessingMethod(processingMethodData, PostgresqlTargetContext);
                    break;

                case Version15DataTypes.Project:
                    if (versionData is ProjectData projectData)
                        ProjectTarget.SaveProject(projectData, PostgresqlTargetContext);
                    break;
                case Version15DataTypes.ReportTemplate:
                    if (versionData is ReportTemplateData reportTemplateData)
                        ReportTemplateTarget.SaveReportTemplate(reportTemplateData, PostgresqlTargetContext);
                    break;
                case Version15DataTypes.Sequence:
                    if (versionData is SequenceData sequenceData)
                        SequenceTarget.SaveSequence(sequenceData, PostgresqlTargetContext);
                    break;
            }
        }
    }
}
