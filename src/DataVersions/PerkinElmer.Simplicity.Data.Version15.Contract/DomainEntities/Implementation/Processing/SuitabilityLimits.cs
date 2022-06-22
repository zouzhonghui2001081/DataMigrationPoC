using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Processing;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Implementation.Processing
{
	public class SuitabilityLimits : ISuitabilityLimits
	{
		public object Clone()
		{
			var suitabilityLimits = new SuitabilityLimits()
			{
				PeakWidth = PeakWidth,
				UspResolution = UspResolution,
				Area = Area,
				TailingFactorSymmetry = TailingFactorSymmetry,
				SignalToNoise = SignalToNoise,
				Resolution = Resolution,
				NTan = NTan,
				Alpha = Alpha,
				KPrime = KPrime,
				NFoley = NFoley,
				Height = Height
			};

			return suitabilityLimits;
		}

		public SuitabilityLimit Area { get; set; }
		public SuitabilityLimit Height { get; set; }
		public SuitabilityLimit NTan { get; set; }
		public SuitabilityLimit NFoley { get; set; }
		public SuitabilityLimit TailingFactorSymmetry { get; set; }
		public SuitabilityLimit UspResolution { get; set; }
		public SuitabilityLimit KPrime { get; set; }
		public SuitabilityLimit Resolution { get; set; }
		public SuitabilityLimit Alpha { get; set; }
		public SuitabilityLimit SignalToNoise { get; set; }
		public SuitabilityLimit PeakWidth { get; set; }

	    public virtual bool Equals(ISuitabilityLimits other)
	    {
	        if (other == null) return false;
	        if (other.GetType() != GetType()) return false;

	        bool equal = Equals(Area, other.Area) &&
	                     Equals(Height, other.Height) &&
	                     Equals(NTan, other.NTan) &&
	                     Equals(NFoley, other.NFoley) &&
	                     Equals(TailingFactorSymmetry, other.TailingFactorSymmetry) &&
	                     Equals(UspResolution, other.UspResolution) &&
	                     Equals(KPrime, other.KPrime) &&
	                     Equals(Resolution, other.Resolution) &&
	                     Equals(Alpha, other.Alpha) &&
	                     Equals(SignalToNoise, other.SignalToNoise) &&
	                     Equals(PeakWidth, other.PeakWidth);
	        return equal;
	    }

	    public override bool Equals(object obj)
	    {
	        if (ReferenceEquals(null, obj)) return false;
	        if (ReferenceEquals(this, obj)) return true;
	        if (obj.GetType() != this.GetType()) return false;
	        return Equals((SuitabilityLimits) obj);
	    }
	}
}
