using System;
using System.Reflection;
using System.Threading.Tasks.Dataflow;
using log4net;
using PerkinElmer.Simplicity.DataMigration.Contracts.Migration;
using PerkinElmer.Simplicity.DataMigration.Contracts.Transform;
using PerkinElmer.Simplicity.DataMigration.Contracts.Transform.TransformContext;
using PerkinElmer.Simplicity.Data.Version16.MigrationData.Chromatography;
using PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography;

namespace PerkinElmer.Simplicity.DataTransform.V15ToV16.Chromatography
{
    public class SequenceDataTransform : TransformBlockCreatorBase
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public override MigrationVersions FromVersion => MigrationVersions.Version15;

        public override MigrationVersions ToVersion => MigrationVersions.Version16;

        public override TransformBlock<MigrationDataBase, MigrationDataBase> CreateTransform(TransformContextBase transformContext)
        {
            var sequenceTransformBlock = new TransformBlock<MigrationDataBase, MigrationDataBase>(fromVersionData =>
            {
                if (fromVersionData.MigrationVersion != MigrationVersions.Version15 ||
                    !(fromVersionData is Data.Version15.MigrationData.Chromatography.SequenceMigrationData sequenceData15))
                    throw new ArgumentException("From version data is incorrect!");
                return Transform(sequenceData15);
            }, transformContext.BlockOption);
            sequenceTransformBlock.Completion.ContinueWith(_ =>
            {
                Log.Info($"sequence transform complete with State{_.Status}");
            });
            return sequenceTransformBlock;
        }

        internal static SequenceMigrationData Transform(Data.Version15.MigrationData.Chromatography.SequenceMigrationData sequenceMigrationData)
        {
            if (sequenceMigrationData == null) throw new ArgumentNullException(nameof(sequenceMigrationData));

            return new SequenceMigrationData
            {
                ProjectGuid = sequenceMigrationData.ProjectGuid,
                Sequence = Sequence.Transform(sequenceMigrationData.Sequence)
            };
        }
    }
}
