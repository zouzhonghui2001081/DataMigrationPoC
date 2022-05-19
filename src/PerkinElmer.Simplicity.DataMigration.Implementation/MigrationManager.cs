using System;
using System.Collections.Generic;
using System.Threading.Tasks.Dataflow;
using PerkinElmer.Simplicity.Data.Version15;
using PerkinElmer.Simplicity.Data.Version16;
using PerkinElmer.Simplicity.DataMigration.Implementation.Common;
using PerkinElmer.Simplicity.DataTransform.V15ToV16;

namespace PerkinElmer.Simplicity.DataMigration.Implementation
{
    internal class VersionBlockInfo
    {
        public string BlockName { get; set; }

        public IDataflowBlock DataflowBlock { get; set; }
    }

    internal class TransformBlockInfo
    {
        public string FromVersionBlockName { get; set; }

        public string ToVersionBlockName { get; set; }

        public IDataflowBlock DataflowBlock { get; set; }
    }

    public class MigrationManager
    {
        private readonly VersionBlockInfo _sourceVersionBlockInfo;

        private readonly Stack<TransformBlockInfo> _transformBlockInfos;

        private readonly VersionBlockInfo _targetVersionBlockInfo;

        public MigrationManager(string startVersion, string endVersion)
        {
            _sourceVersionBlockInfo = GenerateSourceBlock(startVersion);
            _transformBlockInfos = GenerateTransformBlock(startVersion, endVersion);
            _targetVersionBlockInfo = GenerateTargetBlock(endVersion);
            if (!BuildPipeline())
                throw new ArgumentException($"Failed setup pipeline from {startVersion} to {endVersion}");
        }

        private IDictionary<string, IDictionary<string, TransformBlockInfo>> TransformMaps => new Dictionary<string, IDictionary<string, TransformBlockInfo>>
        {
            { VersionNames.Version15, new Dictionary<string, TransformBlockInfo>
                {
                    { VersionNames.Version16, new TransformBlockInfo{FromVersionBlockName = VersionNames.Version15, ToVersionBlockName = VersionNames.Version16, DataflowBlock = new Version15ToVersion16()} }
                }
            }
        };

        public void Start(MigrationContext migrationContext)
        {
            SetTargetType(migrationContext.TargetConfig);
            StartDataflowInternal(migrationContext.SourceConfig);
        }

        private void SetTargetType(string targetConfig)
        {
            switch (_targetVersionBlockInfo.BlockName)
            {
                case VersionNames.Version15:
                    if (_targetVersionBlockInfo.DataflowBlock is Version15 version15)
                        version15.ApplyTargetConfiguration(targetConfig);
                    break;
                case VersionNames.Version16:
                    if (_targetVersionBlockInfo.DataflowBlock is Version16 version16)
                        version16.ApplyTargetConfiguration(targetConfig);
                    break;
            }
        }

        private void StartDataflowInternal(string sourceFlowConfig)
        {
            switch (_sourceVersionBlockInfo.BlockName)
            {
                case VersionNames.Version15:
                    if (_sourceVersionBlockInfo.DataflowBlock is Version15 version15)
                        version15.StartSourceDataflow(sourceFlowConfig);
                    break;
                case VersionNames.Version16:
                    if (_sourceVersionBlockInfo.DataflowBlock is Version16 version16)
                        version16.StartSourceDataflow(sourceFlowConfig);
                    break;
            }
        }

        private bool BuildPipeline()
        {
            if (_sourceVersionBlockInfo == null || _targetVersionBlockInfo == null) return false;
            if (_sourceVersionBlockInfo.BlockName != _targetVersionBlockInfo.BlockName && _transformBlockInfos.Count == 0) return false;

            if (_transformBlockInfos != null && _transformBlockInfos.Count > 0)
            {
                var previousTransformInfo = _transformBlockInfos.Pop();
                if (!LinkTo(previousTransformInfo, _targetVersionBlockInfo))
                    return false;
                while (_transformBlockInfos.Count > 0)
                {
                    var currentTransoformInfo = _transformBlockInfos.Pop();
                    if (!LinkTo(previousTransformInfo, currentTransoformInfo))
                        return false;

                    previousTransformInfo = currentTransoformInfo;
                }
                if (!LinkTo(_sourceVersionBlockInfo, previousTransformInfo)) return false;
            }
            else
            {
                if (!LinkTo(_sourceVersionBlockInfo, _targetVersionBlockInfo)) return false;
            }
            return true;
        }

        private bool LinkTo(TransformBlockInfo currentTransformInfo, TransformBlockInfo nextTransformInfo)
        {
            return false;
        }

        private bool LinkTo(VersionBlockInfo sourceBlockInfo, TransformBlockInfo transformBlock)
        {
            if (sourceBlockInfo.BlockName != transformBlock.FromVersionBlockName)
                throw new ArgumentException(nameof(transformBlock));

            switch (sourceBlockInfo.BlockName)
            {
                case VersionNames.Version15:
                    if (sourceBlockInfo.DataflowBlock is Version15 sourceVersion15)
                    {
                        switch (transformBlock.ToVersionBlockName)
                        {
                            case VersionNames.Version16:
                                if (transformBlock.DataflowBlock is Version15ToVersion16 version15To16)
                                {
                                    sourceVersion15.LinkTo(version15To16, new DataflowLinkOptions { PropagateCompletion = true });
                                    return true;
                                }
                                break;
                        }
                    }
                    break;
            }
            return false;
        }

        private bool LinkTo(TransformBlockInfo transformBlock, VersionBlockInfo targetBlockInfo)
        {
            if (transformBlock.ToVersionBlockName != targetBlockInfo.BlockName)
                throw new ArgumentException(nameof(targetBlockInfo));
            switch (targetBlockInfo.BlockName)
            {
                case VersionNames.Version16:
                    if (targetBlockInfo.DataflowBlock is Version16 targetVersion16)
                    {
                        switch (transformBlock.FromVersionBlockName)
                        {
                            case VersionNames.Version15:
                                if (transformBlock.DataflowBlock is Version15ToVersion16 ver15ToVer16)
                                {
                                    ver15ToVer16.LinkTo(targetVersion16, new DataflowLinkOptions { PropagateCompletion = true });
                                    return true;
                                }
                                break;
                        }
                    }
                    break;
            }
            return false;
        }

        private bool LinkTo(VersionBlockInfo sourceBlockInfo, VersionBlockInfo targetBlockInfo)
        {
            switch (sourceBlockInfo.BlockName)
            {
                case VersionNames.Version15:
                    if (sourceBlockInfo.DataflowBlock is Version15 sourceVersion15)
                    {
                        switch (targetBlockInfo.BlockName)
                        {
                            case VersionNames.Version15:
                                if (targetBlockInfo.DataflowBlock is Version15 targetVersion15)
                                {
                                    sourceVersion15.LinkTo(targetVersion15, new DataflowLinkOptions { PropagateCompletion = true });
                                    return true;
                                }
                                break;
                        }
                    }
                    break;
                case VersionNames.Version16:
                    if (sourceBlockInfo.DataflowBlock is Version16 sourceVersion16)
                    {
                        switch (targetBlockInfo.BlockName)
                        {
                            case VersionNames.Version16:
                                if (targetBlockInfo.DataflowBlock is Version16 targetVersion16)
                                {
                                    sourceVersion16.LinkTo(targetVersion16, new DataflowLinkOptions { PropagateCompletion = true });
                                    return true;
                                }
                                break;
                        }
                    }
                    break;
            }

            return false;
        }

        private VersionBlockInfo GenerateSourceBlock(string sourceVersionName)
        {
            switch (sourceVersionName)
            {
                case VersionNames.Version15:
                    return new VersionBlockInfo{BlockName = sourceVersionName, DataflowBlock = new Version15()};
                case VersionNames.Version16:
                    return new VersionBlockInfo{BlockName = sourceVersionName, DataflowBlock = new Version16()};
            }
            return null;
        }

        private VersionBlockInfo GenerateTargetBlock(string targetVersionName)
        {
            switch (targetVersionName)
            {
                case VersionNames.Version15:
                    return new VersionBlockInfo { BlockName = targetVersionName, DataflowBlock = new Version15() };
                case VersionNames.Version16:
                    return new VersionBlockInfo { BlockName = targetVersionName, DataflowBlock = new Version16() };
            }
            return null;
        }

        private Stack<TransformBlockInfo> GenerateTransformBlock(string startVersionName, string endVersionName)
        {
            var transformBlocks = new Stack<TransformBlockInfo>();
            if (!TransformMaps.ContainsKey(startVersionName)) return transformBlocks;

            var endBlockMaps = TransformMaps[startVersionName];
            if (!endBlockMaps.ContainsKey(endVersionName))
                return GetTransformBlocksRecursively(endVersionName, endBlockMaps);

            transformBlocks.Push(endBlockMaps[endVersionName]);
            return transformBlocks;
        }

        private Stack<TransformBlockInfo> GetTransformBlocksRecursively(string endVersionName,
            IDictionary<string, TransformBlockInfo> targets)
        {
            var transformBlocks = new Stack<TransformBlockInfo>();
            foreach (var target in targets)
            {
                if (TransformMaps.ContainsKey(target.Key))
                {
                    var nextTargets = TransformMaps[target.Key];
                    if (nextTargets != null && nextTargets.Count > 0)
                    {
                        if (nextTargets.ContainsKey(endVersionName))
                        {
                            transformBlocks.Push(nextTargets[endVersionName]);
                            transformBlocks.Push(targets[target.Key]);
                            return transformBlocks;
                        }

                        var subResult = GetTransformBlocksRecursively(endVersionName, nextTargets);
                        if (subResult != null && subResult.Count > 0)
                        {
                            transformBlocks = subResult;
                            transformBlocks.Push(targets[target.Key]);

                            return transformBlocks;
                        }
                    }
                }
            }
            return transformBlocks;
        }
    }
}
