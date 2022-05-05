﻿using System;
using System.Threading.Tasks.Dataflow;
using PerkinElmer.Simplicity.DataMigration.Contracts.Migration;

namespace PerkinElmer.Simplicity.DataMigration.Contracts.Targets
{
    public abstract class TargetBlockCreatorBase
    {
        public abstract MigrationVersion TargetVersion { get; }

        public abstract TargetTypes TargetType { get; }

        public abstract ITargetBlock<MigrationDataBase> CreateTargetBlock(TargetContextBase targetContext);
    }
}
