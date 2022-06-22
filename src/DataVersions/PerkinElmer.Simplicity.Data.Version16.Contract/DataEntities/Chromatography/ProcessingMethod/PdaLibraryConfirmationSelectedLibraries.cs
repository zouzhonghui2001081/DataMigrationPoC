namespace PerkinElmer.Simplicity.Data.Version16.Contract.DataEntities.Chromatography.ProcessingMethod
{
    public class PdaLibraryConfirmationSelectedLibraries
	{
		public long Id { get; set; }
		public long PdaLibraryConfirmationParameterId { get; set; }
		public string SelectedLibraries { get; set; }
	}
}
