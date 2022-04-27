using System;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Shared
{
	[Flags]
	public enum PostProcessingChannelType 
    {
        None = 0,
        Smoothed = 1,
        BlankSubtracted = 2,
    }
}