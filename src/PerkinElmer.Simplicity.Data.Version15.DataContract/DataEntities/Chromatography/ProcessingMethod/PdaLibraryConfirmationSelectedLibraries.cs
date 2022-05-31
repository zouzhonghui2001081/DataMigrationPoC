namespace PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography.ProcessingMethod
{
    public class PdaLibraryConfirmationSelectedLibraries
	{
		public long Id { get; set; }
		public long PdaLibraryConfirmationParameterId { get; set; }
		public string SelectedLibraries { get; set; }
	}
}
