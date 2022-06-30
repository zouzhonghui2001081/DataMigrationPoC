using System;
using System.IO;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.JsonConverter;
using PerkinElmer.Simplicity.Data.Version15.Contract.Version.Chromatography;
using PerkinElmer.Simplicity.Data.Version15.Version.Context.TargetContext;

namespace PerkinElmer.Simplicity.Data.Version15.Dummy.DummyData
{
    internal class AnalysisResultSetDummyData
    {
        private const string AnalysisResultSetDummyDataTemplate = "PerkinElmer.Simplicity.Data.Version15.Dummy.DummyData.Templates.AnalysisResultSet.json";

        public void CreateDummyAnalysisResultSet(PostgresqlTargetContext postgresqlTargetContext, Guid projectGuid,
            int analysisResultSetCount)
        {
            var analysisResultSetJson = GetAnalysisResultSetTemplate();
            var analysisResultSetMethod = JsonConverter.FromJson<AnalysisResultSetData>(analysisResultSetJson);
           
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
