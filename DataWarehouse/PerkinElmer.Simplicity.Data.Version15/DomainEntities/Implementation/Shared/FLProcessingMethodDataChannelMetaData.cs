namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.Implementation.Shared
{
	internal sealed class FLProcessingMethodDataChannelMetaData : IProcessingMethodDataChannelMetaData
	{
		public readonly double ExcitationInNanometers;
		public readonly double EmissionInNanometers;
		public readonly bool Programmed;
		public FLProcessingMethodDataChannelMetaData(double excitationInNanometers, double emissionInNanometers, bool programmed)
		{
			ExcitationInNanometers = excitationInNanometers;
			EmissionInNanometers = emissionInNanometers;
			Programmed = programmed;
			

		}

		private bool Equals(FLProcessingMethodDataChannelMetaData other)
		{
			return ExcitationInNanometers.Equals(other.ExcitationInNanometers) && EmissionInNanometers.Equals(other.EmissionInNanometers) && Programmed == other.Programmed;
		}

		public override bool Equals(object obj)
		{
			return ReferenceEquals(this, obj) || obj is FLProcessingMethodDataChannelMetaData other && Equals(other);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = ExcitationInNanometers.GetHashCode();
				hashCode = (hashCode * 397) ^ EmissionInNanometers.GetHashCode();
				hashCode = (hashCode * 397) ^ Programmed.GetHashCode();
				return hashCode;
			}
		}

		public string GetDisplayName()
		{
			var displayName = $"{ExcitationInNanometers}/{EmissionInNanometers}";
			return Programmed ? $"{displayName}-P" : displayName;
		}
	}
}