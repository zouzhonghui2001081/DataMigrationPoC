using System;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Processing;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Implementation.Processing
{
	public class SpectrumMethod : ISpectrumMethod
	{
        public object Clone()
        {
			var clonedSpectrum = (SpectrumMethod)this.MemberwiseClone();
			return clonedSpectrum;
        }

        public Guid Guid { get; set; }
        public double StartRetentionTime { get; set; }
        public double EndRetentionTime { get; set; }
        public BaselineCorrectionType BaselineCorrectionType { get; set; }
        public double BaselineStartRetentionTime { get; set; }
        public double BaselineEndRetentionTime { get; set; }
	    
	    public bool IsEqual(ISpectrumMethod other)
	    {
	        if (other == null)
	            return false;

	        return StartRetentionTime.Equals(other.StartRetentionTime)
	               && EndRetentionTime.Equals(other.EndRetentionTime)
	               && BaselineCorrectionType.Equals(other.BaselineCorrectionType)
	               && BaselineStartRetentionTime.Equals(other.BaselineStartRetentionTime)
	               && BaselineEndRetentionTime.Equals(other.BaselineEndRetentionTime);

        }

	    public override bool Equals(object obj)
	    {
	        if (ReferenceEquals(null, obj)) return false;
	        if (ReferenceEquals(this, obj)) return true;
	        var other = obj as ProcessingMethod;
	        return other != null && Equals(other);
	    }

	    public bool Equals(ISpectrumMethod other)
	    {
	        if (ReferenceEquals(null, other)) return false;
	        if (ReferenceEquals(this, other)) return true;
	        return Guid.Equals(other.Guid) 
	               && StartRetentionTime.Equals(other.StartRetentionTime)
	               && EndRetentionTime.Equals(other.EndRetentionTime)
	               && BaselineCorrectionType == other.BaselineCorrectionType
	               && BaselineStartRetentionTime.Equals(other.BaselineStartRetentionTime)
	               && BaselineEndRetentionTime.Equals(other.BaselineEndRetentionTime);
	    }
	}
}
