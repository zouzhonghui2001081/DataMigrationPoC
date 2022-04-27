namespace PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography.CalibrationMethod
{
    public class LevelAmount
	{
		public long Id { get; set; }
        public long CompoundId { get; set; }
		public int Level { get; set; }
		public double? Amount { get; set; }
	}
}
