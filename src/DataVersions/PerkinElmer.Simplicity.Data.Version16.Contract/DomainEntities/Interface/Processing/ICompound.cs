using System;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Processing
{
	public interface ICompound : ICompoundOrGroupId, IEquatable<ICompound>, ICloneable
	{
		CompoundType CompoundType { get; set; }
		IIdentificationParameters IdentificationParameters { get; set; }
		ICalibrationParameters CalibrationParameters { get; set; }
	    bool UsedForSuitability { get; set; }
        bool IsEqual(ICompound other);
    }
}
