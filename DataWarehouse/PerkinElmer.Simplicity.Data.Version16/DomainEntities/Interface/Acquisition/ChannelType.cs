using System;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Acquisition
{
    [Obsolete]
    public enum ChannelType
	{
		None,
		FID,
		TCD,
		ECD,
	    WRFID,
	    TCDandR,
        TCDandRandM,
		AtoD,
		LC200UV, 
		UV,
        FP1,
        FP2,
        RI,
        PDA
	}
}