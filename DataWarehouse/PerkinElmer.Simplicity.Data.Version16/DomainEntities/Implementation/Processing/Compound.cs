using System;
using PerkinElmer.Domain.Contracts.Processing;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Processing;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Implementation.Processing
{
	class Compound : ICompound
	{
		public Guid Guid { get; set; }
		public int CompoundNumber { get; set; }
		public string Name { get; set; }
		public int ChannelIndex { get; set; }
		public Guid ProcessingMethodChannelGuid { get; set; }
		public CompoundType CompoundType { get; set; }
		public IIdentificationParameters IdentificationParameters { get; set; }
	    public ICalibrationParameters CalibrationParameters { get; set; }
	    public bool UsedForSuitability { get; set; }

	    public virtual bool Equals(ICompound other)
	    {
	        if (other == null) return false;
		    if (other.GetType() != GetType()) return false;

            bool equal = Guid.Equals(other.Guid) && string.Equals(Name, other.Name) && 
                         ProcessingMethodChannelGuid == other.ProcessingMethodChannelGuid &&
						 CompoundType == other.CompoundType &&
                         (IdentificationParameters == null && other.IdentificationParameters == null ||
                          IdentificationParameters != null && other.IdentificationParameters != null && IdentificationParameters.Equals(other.IdentificationParameters)) 
                         && (CalibrationParameters == null && other.CalibrationParameters == null ||
                          CalibrationParameters != null && other.CalibrationParameters != null && CalibrationParameters.Equals(other.CalibrationParameters))
                         && UsedForSuitability.Equals(other.UsedForSuitability);
            return equal;
        }

	    public override bool Equals(object obj)
	    {
	        if (ReferenceEquals(null, obj)) return false;
	        if (ReferenceEquals(this, obj)) return true;
	        if (obj.GetType() != this.GetType()) return false;
	        return Equals((ICompound) obj);
	    }

	    public virtual object Clone()
	    {
		    var clonedCompound = (Compound)this.MemberwiseClone();
		    clonedCompound.IdentificationParameters = (IIdentificationParameters) IdentificationParameters?.Clone();
		    clonedCompound.CalibrationParameters = (ICalibrationParameters)CalibrationParameters?.Clone();

		    return clonedCompound;
	    }

	    public bool IsEqual(ICompound other)
	    {
	        if (other == null) return false;
	        if (other.GetType() != GetType()) return false;

	        bool equal = string.Equals(Name, other.Name) 
	                     && CompoundType == other.CompoundType
                         && IdentificationParameters.IsEqual(other.IdentificationParameters)
                         && CalibrationParameters.IsEqual(other.CalibrationParameters)
                         && UsedForSuitability.Equals(other.UsedForSuitability);
	        return equal;
        }

    }
}
