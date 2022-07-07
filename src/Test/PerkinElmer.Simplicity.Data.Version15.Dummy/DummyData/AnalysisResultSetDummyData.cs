using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Npgsql;
using PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.JsonConverter;
using PerkinElmer.Simplicity.Data.Version15.Contract.Version.Chromatography;
using PerkinElmer.Simplicity.Data.Version15.DataAccess.Postgresql.Chromatography;
using PerkinElmer.Simplicity.Data.Version15.DataTargets.Postgresql.Chromatography;
using PerkinElmer.Simplicity.Data.Version15.Dummy.DummyData.Extensions;
using PerkinElmer.Simplicity.Data.Version15.Version.Context.TargetContext;

namespace PerkinElmer.Simplicity.Data.Version15.Dummy.DummyData
{
    internal class AnalysisResultSetDummyData
    {
        private const string AnalysisResultSetDummyDataTemplate =
            "PerkinElmer.Simplicity.Data.Version15.Dummy.DummyData.Templates.AnalysisResultSet.json";

        public void CreateDummyAnalysisResultSet(PostgresqlTargetContext postgresqlTargetContext, Guid projectGuid,
            int analysisResultSetCount, int batchRunCount)
        {
            var projectDao = new ProjectDao();
            using var connection = new NpgsqlConnection(postgresqlTargetContext.ChromatographyConnectionString);
            var project = projectDao.GetProject(connection, projectGuid);
            if (project == null) throw new ArgumentNullException(nameof(projectGuid));

            var analysisResultSetJson = GetAnalysisResultSetTemplate();
            var analysisResultSetData = JsonConverter.FromJson<AnalysisResultSetData>(analysisResultSetJson);

            if (analysisResultSetData.BatchResultSetData == null || analysisResultSetData.BatchResultSetData.Count == 0)
                throw new ArgumentException("Batch result set should not be null!");

            var batchResultSetTemplate = analysisResultSetData.BatchResultSetData[0];
            var batchRunTemplate = batchResultSetTemplate.BatchRuns[0];

            var batchRunAnalysisResultSetTemplate = analysisResultSetData.BatchRunAnalysisResults.FirstOrDefault(
                batchRunAnalysisResultSet => batchRunAnalysisResultSet.BatchRunAnalysisResult.BatchRunId == batchRunTemplate.BatchRun.Id);
            if (batchRunAnalysisResultSetTemplate == null) throw new ArgumentException("Batch run analysis result set should not be null");

            var batchRunChannelMapTemplates = analysisResultSetData.BatchRunChannelMaps;

            for (var i = 0; i < analysisResultSetCount; i++)
            {
                var suffix = Guid.NewGuid().ToString().Substring(0, 8);
                var analysisResultSetCopy = analysisResultSetData.Copy();
                analysisResultSetCopy.ProjectGuid = projectGuid;
                analysisResultSetData.BatchResultSetData[0].BatchResultSet.Id = i + 1;

                //Batch Result Set
                var batchResultSetName = "DummyResultSet" + (i + 1).ToString("0000") + "_" + suffix;
                analysisResultSetCopy.BatchResultSetData.Clear();
                var batchResultSetData = CreateDummyBatchResultSetData(projectGuid, project.Id, batchResultSetName, batchResultSetTemplate, batchRunTemplate, batchRunCount);
                analysisResultSetCopy.BatchResultSetData.Add(batchResultSetData);

                //Analysis Result Set Info
                analysisResultSetCopy.AnalysisResultSet.Id = i + 1;
                analysisResultSetCopy.AnalysisResultSet.Guid = Guid.NewGuid();
                analysisResultSetCopy.AnalysisResultSet.Name = batchResultSetData.BatchResultSet.Name;
                analysisResultSetCopy.AnalysisResultSet.BatchResultSetGuid = batchResultSetData.BatchResultSet.Guid;
                analysisResultSetCopy.AnalysisResultSet.BatchResultSetName = batchResultSetData.BatchResultSet.Name;
                analysisResultSetCopy.AnalysisResultSet.OriginalAnalysisResultSetGuid = analysisResultSetCopy.AnalysisResultSet.Guid;
                analysisResultSetCopy.AnalysisResultSet.ProjectId = project.Id;

                //Batch Run Analysis Result Set
                analysisResultSetCopy.BatchRunAnalysisResults.Clear();
                analysisResultSetCopy.BatchRunAnalysisResults = CreateDummyBatchRunAnalysisResultData(project.Id, analysisResultSetCopy.AnalysisResultSet.Id, batchResultSetData, batchRunAnalysisResultSetTemplate);

                //Batch Run Channel Map
                analysisResultSetCopy.BatchRunChannelMaps.Clear();
                analysisResultSetCopy.BatchRunChannelMaps = CreateDummyBatchRunChannelMaps(analysisResultSetCopy.BatchRunAnalysisResults, batchRunChannelMapTemplates);

                AnalysisResultSetTarget.SaveAnalysisResultSet(analysisResultSetCopy, postgresqlTargetContext);
            }
        }


        private BatchResultSetData CreateDummyBatchResultSetData(Guid projectGuid, long projectId, string batchResultSetName,
            BatchResultSetData batchResultSetTemplate, BatchRunData batchRunTemplate, int batchRunCount)
        {
            if (batchResultSetTemplate== null) 
                throw new ArgumentNullException(nameof(batchResultSetTemplate));
            if(batchRunTemplate == null)
                throw new ArgumentNullException(nameof(batchRunTemplate));

            var batchRunDataTemplate = batchRunTemplate;
            var batchResultSetDataCopy = batchResultSetTemplate.Copy();
            batchResultSetDataCopy.ProjectGuid = projectGuid;
            batchResultSetDataCopy.BatchResultSet.Name = batchResultSetName;
            batchResultSetDataCopy.BatchResultSet.ProjectId = projectId;
            batchResultSetDataCopy.BatchResultSet.Guid = Guid.NewGuid();
            batchResultSetDataCopy.BatchRuns.Clear();
            for (var i = 1; i <= batchRunCount; i++)
            {
                var batchRunDataCopy = batchRunDataTemplate.Copy();
                
                batchRunDataCopy.SequenceSampleInfoBatchResult.Id = i ;
                batchRunDataCopy.SequenceSampleInfoBatchResult.Guid = Guid.NewGuid();
                batchRunDataCopy.SequenceSampleInfoBatchResult.SampleName = "DummySample" + (i).ToString("0000");
                batchRunDataCopy.SequenceSampleInfoBatchResult.BatchResultSetId = batchResultSetDataCopy.BatchResultSet.Id;

                batchRunDataCopy.BatchRun.Id = i ;
                batchRunDataCopy.BatchRun.Guid = Guid.NewGuid();
                batchRunDataCopy.BatchRun.Name = batchRunDataCopy.SequenceSampleInfoBatchResult.SampleName;
                batchRunDataCopy.BatchRun.SequenceSampleInfoBatchResultId = batchRunDataCopy.SequenceSampleInfoBatchResult.Id;
                batchRunDataCopy.BatchRun.BatchResultSetId = batchResultSetDataCopy.BatchResultSet.Id;

                foreach (var namedContent in batchRunDataCopy.NamedContents)
                    namedContent.BatchRunId = batchRunDataCopy.BatchRun.Id;

                foreach (var streamDataBatchResult in batchRunDataCopy.StreamDataBatchResults)
                    streamDataBatchResult.BatchRunId = batchRunDataCopy.BatchRun.Id;

                batchResultSetDataCopy.BatchRuns.Add(batchRunDataCopy);
            }

            return batchResultSetDataCopy;
        }

        private List<BatchRunAnalysisResultData> CreateDummyBatchRunAnalysisResultData(long projectId, long analysisResultSetId,
            BatchResultSetData batchResultSet, BatchRunAnalysisResultData batchRunAnalysisResultTemplate)
        {
            if (batchResultSet == null) throw new ArgumentNullException(nameof(batchResultSet));
            if (batchRunAnalysisResultTemplate == null) throw new ArgumentNullException(nameof(batchResultSet));

            var calculatedChannelId = 1;
            var batchRunAnalysisResults = new List<BatchRunAnalysisResultData>();
            for (var i = 1; i <= batchResultSet.BatchRuns.Count; i++)
            {
                var batchRun = batchResultSet.BatchRuns[i - 1];
                var batchRunAnalysisResultCopy = batchRunAnalysisResultTemplate.Copy();

                //Batch Run Analysis Result 
                batchRunAnalysisResultCopy.BatchRunAnalysisResult.Id = i;
                batchRunAnalysisResultCopy.BatchRunAnalysisResult.ProjectId = projectId;
                batchRunAnalysisResultCopy.BatchRunAnalysisResult.AnalysisResultSetId = analysisResultSetId;
                batchRunAnalysisResultCopy.BatchRunAnalysisResult.BatchRunId = batchRun.BatchRun.Id;
                batchRunAnalysisResultCopy.BatchRunAnalysisResult.BatchRunName = batchRun.BatchRun.Name;
                batchRunAnalysisResultCopy.BatchRunAnalysisResult.OriginalBatchResultSetGuid = batchResultSet.BatchResultSet.Guid;
                batchRunAnalysisResultCopy.BatchRunAnalysisResult.OriginalBatchRunInfoGuid = batchRun.BatchRun.Guid;
                batchRunAnalysisResultCopy.BatchRunAnalysisResult.ModifiableBatchRunInfoGuid = Guid.NewGuid();
                batchRunAnalysisResultCopy.BatchRunAnalysisResult.SequenceSampleInfoModifiableId = i;

                //Sequence Sample Info Modifiable
                batchRunAnalysisResultCopy.SequenceSampleInfoModifiable.Id = i;
                batchRunAnalysisResultCopy.SequenceSampleInfoModifiable.Guid = batchRun.SequenceSampleInfoBatchResult.Guid;
                batchRunAnalysisResultCopy.SequenceSampleInfoModifiable.SampleName = batchRun.BatchRun.Name;

                //Calculated Channel Data
                foreach (var calculatedChannelData in batchRunAnalysisResultCopy.CalculatedChannelData)
                {
                    calculatedChannelData.CalculatedChannelData.Id = calculatedChannelId;
                    calculatedChannelId++;
                    calculatedChannelData.CalculatedChannelData.BatchRunAnalysisResultId = analysisResultSetId;

                    if (calculatedChannelData.CalculatedChannelData.BatchRunChannelGuid == Guid.Empty) continue;

                    calculatedChannelData.CalculatedChannelData.BatchRunChannelGuid = Guid.NewGuid();
                    if (calculatedChannelData.RunPeakResults != null)
                    {
                        foreach (var runPeakResult in calculatedChannelData.RunPeakResults)
                            runPeakResult.BatchRunChannelGuid = calculatedChannelData.CalculatedChannelData.BatchRunChannelGuid;
                    }

                }

                batchRunAnalysisResults.Add(batchRunAnalysisResultCopy);

            }

            return batchRunAnalysisResults;
        }

        private List<BatchRunChannelMap> CreateDummyBatchRunChannelMaps(IList<BatchRunAnalysisResultData> batchRunAnalysisResults, 
            IList<BatchRunChannelMap> batchRunChannelMapTemplates)
        {
            if (batchRunAnalysisResults == null) throw new ArgumentNullException(nameof(batchRunAnalysisResults));
            if (batchRunChannelMapTemplates == null) throw new ArgumentNullException(nameof(batchRunChannelMapTemplates));

            var batchRunChannelMaps = new List<BatchRunChannelMap>();
            for (var i = 1; i <= batchRunAnalysisResults.Count; i++)
            {
                var batchRunAnalysisResultData = batchRunAnalysisResults[i - 1];
                var batchRunChannelMapTemplateIndex = 0;
                foreach (var calculatedChannelCompositeData in batchRunAnalysisResultData.CalculatedChannelData)
                {
                    if (calculatedChannelCompositeData.CalculatedChannelData.BatchRunChannelGuid == Guid.Empty) continue;
                    if (batchRunChannelMapTemplateIndex == batchRunChannelMapTemplates.Count) batchRunChannelMapTemplateIndex = 0;

                    var batchRunChannelMapTemplate = batchRunChannelMapTemplates[batchRunChannelMapTemplateIndex];
                    batchRunChannelMapTemplateIndex++;
                    var batchRunChannelMapTemplateCopy = batchRunChannelMapTemplate.Copy();
                    batchRunChannelMapTemplateCopy.AnalysisResultSetId = batchRunAnalysisResultData.BatchRunAnalysisResult.AnalysisResultSetId;
                    batchRunChannelMapTemplateCopy.BatchRunChannelGuid = calculatedChannelCompositeData.CalculatedChannelData.BatchRunChannelGuid;
                    batchRunChannelMapTemplateCopy.BatchRunGuid = batchRunAnalysisResultData.BatchRunAnalysisResult.ModifiableBatchRunInfoGuid;
                    batchRunChannelMapTemplateCopy.OriginalBatchRunGuid = batchRunAnalysisResultData.BatchRunAnalysisResult.OriginalBatchRunInfoGuid;

                    batchRunChannelMaps.Add(batchRunChannelMapTemplateCopy);
                }
            }

            return batchRunChannelMaps;

        }

        private string GetAnalysisResultSetTemplate()
        {
            var assembly = typeof(AnalysisResultSetDummyData).Assembly;

            using var stream = assembly.GetManifestResourceStream(AnalysisResultSetDummyDataTemplate);
            using var reader = new StreamReader(stream ?? throw new InvalidOperationException(
                                                    $"Failed to load resource {AnalysisResultSetDummyDataTemplate}"));
            return reader.ReadToEnd();
        }
    }
}
