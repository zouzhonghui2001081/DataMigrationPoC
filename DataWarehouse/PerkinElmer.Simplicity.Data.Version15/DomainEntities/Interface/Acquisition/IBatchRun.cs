﻿using System;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Acquisition
{
	public interface IBatchRun : IBatchRunBase
	{
		Guid[] BatchRunChannelGuids { get; set; }
	}
}