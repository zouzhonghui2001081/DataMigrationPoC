using System;
using System.Collections.Generic;
using System.Linq;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Processing;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.Implementation.Processing
{
	internal class CompoundGroup : Compound, ICompoundGroup
	{
		public double StartTime { get; set; }

		public double EndTime { get; set; }

		public IList<Guid> CompoundGuids { get; set; } = new List<Guid>();

        public override object Clone()
		{
			var clonedCompoundGroup = (CompoundGroup)this.MemberwiseClone();
            clonedCompoundGroup.CompoundGuids = new List<Guid>();

            foreach (var compoundGuid in CompoundGuids)
            {
                clonedCompoundGroup.CompoundGuids.Add(compoundGuid);
            }
            clonedCompoundGroup.CalibrationParameters = (ICalibrationParameters)CalibrationParameters?.Clone();
            clonedCompoundGroup.IdentificationParameters = (IIdentificationParameters)IdentificationParameters?.Clone();

            return clonedCompoundGroup;
		}

	    public override bool Equals(ICompound other)
	    {
	        return Equals(other as ICompoundGroup);
	    }

	    public bool Equals(ICompoundGroup other)
		{
			if (other == null) return false;
		    if (other.GetType() != GetType()) return false;

            bool compoundListsEqual = CompoundGuids == null && other.CompoundGuids == null ||
						CompoundGuids != null && other.CompoundGuids != null &&
			            CompoundGuids.Count == other.CompoundGuids.Count && 
						!CompoundGuids.Except(other.CompoundGuids).Any();

			bool groupsEqual = compoundListsEqual &&
			                   base.Equals(other) &&
			                   StartTime == other.StartTime &&
			                   EndTime == other.EndTime;
			return groupsEqual;
		}
    }
}
