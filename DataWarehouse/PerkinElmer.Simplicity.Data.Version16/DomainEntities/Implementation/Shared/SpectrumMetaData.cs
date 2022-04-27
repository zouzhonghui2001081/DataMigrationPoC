﻿using System.Collections.Generic;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Implementation.Shared
{
	public class SpectrumMetaData : ISpectrumMetaData
	{
		public double TimeInSeconds { get; set; }

		public bool UsePreviousXValues { get; set; }

		public IList<double> XValues { get; set; }
	}
}
