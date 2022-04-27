﻿using System;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Acquisition;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.Implementation.Acquisition
{
	internal class BatchRun : IBatchRun
	{
		public IBatchRunInfo Info { get; set; }

		public Guid AcquisitionMethodGuid { get; set; }

		public Guid ProcessingMethodGuid { get; set; }

		public Guid CalibrationMethodGuid { get; set; }

		public Guid[] BatchRunChannelGuids { get; set; }
	}
}