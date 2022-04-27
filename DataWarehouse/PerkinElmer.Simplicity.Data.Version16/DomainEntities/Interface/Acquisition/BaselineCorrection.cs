namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Acquisition
{
	public enum BaselineCorrection
	{
		None,
		MostRecentBlank, // Use blank from sequence
		Other  // Use blank from external batch result set
	}
}