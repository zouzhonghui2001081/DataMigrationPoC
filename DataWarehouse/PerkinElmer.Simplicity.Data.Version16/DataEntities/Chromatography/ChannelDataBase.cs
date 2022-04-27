using System;

namespace PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography
{
    public abstract class ChannelDataBase
	{
		public long Id { get; set; }
		public Guid BatchRunChannelGuid { get; set; }
		public int ChannelDataType { get; set; }
		public int ChannelType { get; set; }
		public int ChannelIndex { get; set; }
		public string ChannelMetaData { get; set; }
		public int RawChannelType { get; set; }
		public bool BlankSubtractionApplied { get; set; }
		public bool SmoothApplied { get; set; }
	}
}