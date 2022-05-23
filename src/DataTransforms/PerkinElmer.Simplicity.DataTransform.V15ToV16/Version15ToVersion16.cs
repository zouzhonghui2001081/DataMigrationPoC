using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using PerkinElmer.Simplicity.Data.Version15.Version.Data;
using PerkinElmer.Simplicity.DataTransform.V15ToV16.Chromatography;
using AcqusitionMethodData15 = PerkinElmer.Simplicity.Data.Version15.Version.Data.Chromatography.AcqusitionMethodData;
using AnalysisResultSetData15 = PerkinElmer.Simplicity.Data.Version15.Version.Data.Chromatography.AnalysisResultSetData;
using BatchResultSetData15 = PerkinElmer.Simplicity.Data.Version15.Version.Data.Chromatography.BatchResultSetData;
using CompoundLibraryData15 = PerkinElmer.Simplicity.Data.Version15.Version.Data.Chromatography.CompoundLibraryData;
using ProcessingMethodData15 = PerkinElmer.Simplicity.Data.Version15.Version.Data.Chromatography.ProcessingMethodData;
using ProjectData15 = PerkinElmer.Simplicity.Data.Version15.Version.Data.Chromatography.ProjectData;
using ReportTemplateData15 = PerkinElmer.Simplicity.Data.Version15.Version.Data.Chromatography.ReportTemplateData;
using SequenceData15 = PerkinElmer.Simplicity.Data.Version15.Version.Data.Chromatography.SequenceData;

namespace PerkinElmer.Simplicity.DataTransform.V15ToV16
{
    public sealed class Version15ToVersion16 : IPropagatorBlock<object, object>
    {
        private readonly TransformBlock<object, object> _transformBlock;

        public Version15ToVersion16()
        {
            _transformBlock = new TransformBlock<object, object>(Transform);
        }

        #region IPropagatorBlock members

        public DataflowMessageStatus OfferMessage(DataflowMessageHeader messageHeader, object messageValue, ISourceBlock<object> source,
            bool consumeToAccept)
        {
            return ((ITargetBlock<object>)_transformBlock).OfferMessage(messageHeader, messageValue, source, consumeToAccept);
        }

        public IDisposable LinkTo(ITargetBlock<object> target, DataflowLinkOptions linkOptions)
        {
            return _transformBlock.LinkTo(target, linkOptions);
        }

        public object ConsumeMessage(DataflowMessageHeader messageHeader, ITargetBlock<object> target, out bool messageConsumed)
        {
            return ((ISourceBlock<object>)_transformBlock).ConsumeMessage(messageHeader, target, out messageConsumed);
        }

        public bool ReserveMessage(DataflowMessageHeader messageHeader, ITargetBlock<object> target)
        {
            return ((ISourceBlock<object>)_transformBlock).ReserveMessage(messageHeader, target);
        }

        public void ReleaseReservation(DataflowMessageHeader messageHeader, ITargetBlock<object> target)
        {
            ((ISourceBlock<object>)_transformBlock).ReleaseReservation(messageHeader, target);
        }

        #endregion

        #region IDataflowBlock members

        public void Complete()
        {
            _transformBlock.Complete();
        }

        public void Fault(Exception error)
        {
            ((IDataflowBlock)_transformBlock).Fault(error);
        }

        public Task Completion => _transformBlock.Completion;

        #endregion

        public object Transform(object input)
        {
            if(!(input is Version15DataBase version15DataBase)) throw new ArgumentException("The input type is incorrect!");

            switch (version15DataBase.Version15DataTypes)
            {
                case Version15DataTypes.AcqusitionMethod:
                    if (!(input is AcqusitionMethodData15 acqusitionMethodData)) return null;
                    return AcquisitionMethodDataTransform.Transform(acqusitionMethodData);
                case Version15DataTypes.AnalysisResultSet:
                    if (!(input is AnalysisResultSetData15 analysisResultSetData)) return null;
                    return AnalysisResultSetDataTransform.Transform(analysisResultSetData);
                case Version15DataTypes.BatchResultSet:
                    if (!(input is BatchResultSetData15 batchResultSetData)) return null;
                    return BatchResultSetDataTransform.Transform(batchResultSetData);
                case Version15DataTypes.CompoundLibrary:
                    if (!(input is CompoundLibraryData15 compoundLibraryData)) return null;
                    return CompoundLibraryDataTransform.Transform(compoundLibraryData);
                case Version15DataTypes.ProcessingMethod:
                    if (!(input is ProcessingMethodData15 processingMethodData)) return null;
                    return ProcessingMethodDataTransform.Transform(processingMethodData);
                case Version15DataTypes.Project:
                    if (!(input is ProjectData15 projectData)) return null;
                    return ProjectDataTransform.Transform(projectData);
                case Version15DataTypes.ReportTemplate:
                    if (!(input is ReportTemplateData15 reportTemplateData)) return null;
                    return ReportTemplateDataTransform.Transform(reportTemplateData);
                case Version15DataTypes.Sequence:
                    if (!(input is SequenceData15 sequenceData)) return null;
                    return SequenceDataTransform.Transform(sequenceData);
            }
            return null;
        }
    }
}
