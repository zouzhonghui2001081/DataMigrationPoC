
namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Shared
{
	public enum ExtractionType
	{
		None = 0, // Naturally 2D, no need to extract
		PdaSimple = 1,
        PdaProgrammed = 2,
		PdaApexOptimized = 3,
		PdaMic = 4, 
		//MsXic, MsXic, etc
	}
}
