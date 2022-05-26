using System;
using System.Linq;
using System.Threading.Tasks.Dataflow;
using PerkinElmer.Simplicity.DataMigration.Implementation.Common;

namespace PerkinElmer.Simplicity.DataMigration.Implementation
{
    public class MigrationManager
    {
        private readonly string _startVersion;

        private readonly string _endVersion;

        private readonly MigrationMessageHandler _migrationMessageHandler;

        private readonly MigrationPipeline _migrationPipeline;

        public MigrationManager(string startVersion, string endVersion)
        {
            _startVersion = startVersion;
            _endVersion = endVersion;
            _migrationMessageHandler = new MigrationMessageHandler();
            _migrationPipeline = new MigrationPipeline(startVersion, endVersion);
            RegisterMessageHandler();
        }

        public void CancelMigration()
        {
            _migrationPipeline.Cancellation.Cancel();
        }

        public bool CanStartMigration()
        {
            return _migrationPipeline.SourceBlock != null && 
                   _migrationPipeline.TargetBlock != null;
        }

        public void StartMigration(MigrationContext migrationContext)
        {
           var targetComponent = MigrationComponenetsFactory.Versions.FirstOrDefault(versionComponent =>
                versionComponent.Version == _endVersion);
           var setTargetSettingMethod = MigrationComponenetsFactory.GetTargetSettingMethodInfo(targetComponent);
           if (targetComponent?.VersionBlock == null)
                throw new ArgumentException("Target version block should not be null!");
           setTargetSettingMethod.Invoke(targetComponent.VersionBlock, new object[]{ migrationContext.TargetConfig } );

           var sourceComponent = MigrationComponenetsFactory.Versions.FirstOrDefault(versionComponent =>
                versionComponent.Version == _startVersion);
           var startDataFlowMethod = MigrationComponenetsFactory.GetStartMethodInfo(sourceComponent);
           if (sourceComponent?.VersionBlock == null)
                throw new ArgumentException("Source version block should not be null!");
           startDataFlowMethod.Invoke(sourceComponent.VersionBlock, new object[] { migrationContext.TargetConfig });
        }

        private void RegisterMessageHandler()
        {
            if (_migrationPipeline.SourceBlock is ISourceBlock<string> messageSource)
                messageSource.LinkTo(_migrationMessageHandler);
            if (_migrationPipeline.SourceBlock is ISourceBlock<string> messageTarget)
                messageTarget.LinkTo(_migrationMessageHandler);
            if (_migrationPipeline.TransformBlocks.Count > 0)
            {
                foreach (var transformBlock in _migrationPipeline.TransformBlocks)
                {
                    if (transformBlock is ISourceBlock<string> messageTransform)
                        messageTransform.LinkTo(_migrationMessageHandler);
                }
            }
        }
    }
}
