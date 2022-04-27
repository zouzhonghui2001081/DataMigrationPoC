﻿using System;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Processing
{
	public interface ICalibrationBatchRunInfo : ICloneable
    {
		Guid BatchRunGuid { get; set; }
		Guid BatchResultSetGuid { get; set; } //from IBatchResultSetInfo.Guid
		string BatchRunName { get; set; }
		string ResultSetName { get; set; }
		DateTime BatchRunAcquisitionTime { get; set; }
	}
}