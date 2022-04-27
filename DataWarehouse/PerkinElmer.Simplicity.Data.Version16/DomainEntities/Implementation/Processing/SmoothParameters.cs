﻿using PerkinElmer.Domain.Contracts.Processing;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Processing;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Implementation.Processing
{
	internal class SmoothParameters : ISmoothParameters
	{
		public SmoothType Function { get; set; }
		public int Width { get; set; }
		public int Passes { get; set; }
		public int Order { get; set; }
		public int Cycles { get; set; }
	}
}
