using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using PerkinElmer.Simplicity.Data.Version16.DataSources.Postgresql.Chromatography;
using PerkinElmer.Simplicity.Data.Version16.DataTargets;
using PerkinElmer.Simplicity.Data.Version16.DataTargets.Postgresql.Chromatography;
using PerkinElmer.Simplicity.Data.Version16.Version.Data;
using PerkinElmer.Simplicity.Data.Version16.Version.Data.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version16
{
    public class Version16 : ISourceBlock<Version16DataBase>, ITargetBlock<Version16DataBase>
    {
        private readonly ActionBlock<Version16DataBase> _target;
        private readonly BufferBlock<Version16DataBase> _source;

        public Version16()
        {
            _source = new BufferBlock<Version16DataBase>();
            _target = new ActionBlock<Version16DataBase>(SaveVersionData);
        }

        public void StartSourceDataflow(string sourceConfig)
        {
            //TODO: Hard code now.
            UpgradePostgresql();
        }

        

        public TargetType TargetType { get; set; } = TargetType.Posgresql;

        #region ISourceBlock members

        public Version16DataBase ConsumeMessage(DataflowMessageHeader messageHeader, ITargetBlock<Version16DataBase> target, out bool messageConsumed)
        {
            return ((ISourceBlock<Version16DataBase>)_source).ConsumeMessage(messageHeader, target, out messageConsumed);
        }

        public IDisposable LinkTo(ITargetBlock<Version16DataBase> target, DataflowLinkOptions linkOptions)
        {
            return _source.LinkTo(target, linkOptions);
        }

        public bool ReserveMessage(DataflowMessageHeader messageHeader, ITargetBlock<Version16DataBase> target)
        {
            return ((ISourceBlock<Version16DataBase>)_source).ReserveMessage(messageHeader, target);
        }

        public void ReleaseReservation(DataflowMessageHeader messageHeader, ITargetBlock<Version16DataBase> target)
        {
            ((ISourceBlock<Version16DataBase>)_source).ReleaseReservation(messageHeader, target);
        }

        #endregion

        #region ITargetBlock members

        public DataflowMessageStatus OfferMessage(DataflowMessageHeader messageHeader, Version16DataBase messageValue, ISourceBlock<Version16DataBase> source,
            bool consumeToAccept)
        {
            return ((ITargetBlock<Version16DataBase>)_target).OfferMessage(messageHeader, messageValue, source, consumeToAccept);
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

        private void UpgradePostgresql()
        {
            var projects = ProjectSource.GetAllProjects();

            foreach (var project in projects)
            {
                if (!(project is ProjectData projectData)) continue;

                var projectGuid = projectData.Project.Guid;
                Task.Run(async () => { await _source.SendAsync(projectData); });

                var acqusitionMethods = AcquisitionMethodSource.GetAcqusitionMethods(projectGuid, true);
                foreach (var acqusitionMethod in acqusitionMethods)
                    _source.Post(acqusitionMethod);

                var analysisResultSets = AnalysisResultSetSource.GetAnalysisResultSets(projectGuid, true);
                foreach (var analysisResultSet in analysisResultSets)
                    Task.Run(async () => { await _source.SendAsync(analysisResultSet); });

                var compoundLibraries = CompoundLibrarySource.GetCompoundLibrary(projectGuid);
                foreach (var compoundLibrary in compoundLibraries)
                    _source.Post(compoundLibrary);

                var processingMethods = ProcessingMethodSource.GetProcessingMethods(projectGuid, true);
                foreach (var processingMethod in processingMethods)
                    _source.Post(processingMethod);

                var reportTemplates = ReportTemplateSource.GetReportTemplates(projectGuid, true);
                foreach (var reportTemplate in reportTemplates)
                    _source.Post(reportTemplate);

                var sequences = SequenceSource.GetSequence(projectGuid, true);
                foreach (var sequence in sequences)
                    _source.Post(sequence);
            }
        }

        private void SaveVersionData(Version16DataBase versionData)
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

        private void SavePostgresqlVersionData(Version16DataBase versionData)
        {
            switch (versionData.Version16DataTypes)
            {
                case Version16DataTypes.AcqusitionMethod:
                    if (versionData is AcqusitionMethodData acqusitionMethodData)
                        AcquisitionMethodTarget.SaveAcquisitionMethod(acqusitionMethodData);
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
