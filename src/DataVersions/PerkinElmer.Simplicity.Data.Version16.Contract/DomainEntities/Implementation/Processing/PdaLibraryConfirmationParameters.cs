using System.Collections.Generic;
using System.Linq;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Processing;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Implementation.Processing
{
    internal class PdaLibraryConfirmationParameters:IPdaLibraryConfirmationParameters
    {
        public double MinimumWavelength { get; set; }
        public double MaximumWavelength { get; set; }
        public bool IsBaselineCorrectionEnabled { get; set; }
        public double HitDistanceThreshold { get; set; }

	    public IList<string> SelectedLibraries { get; set; }

        public bool Equals(IPdaLibraryConfirmationParameters other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return MinimumWavelength.Equals(other.MinimumWavelength)
                   && MaximumWavelength.Equals(other.MaximumWavelength) &&
                   IsBaselineCorrectionEnabled == other.IsBaselineCorrectionEnabled
                   && HitDistanceThreshold.Equals(other.HitDistanceThreshold) 
                   && IsListsEquals(SelectedLibraries,other.SelectedLibraries);

        }

        public static bool IsListsEquals(IList<string> pdaSelectedLibraries, IList<string> selectedLibraries)
        {
	        if (pdaSelectedLibraries.Count != selectedLibraries.Count)
		        return false;

	        if (pdaSelectedLibraries.Intersect(selectedLibraries).Count() != pdaSelectedLibraries.Count)
		        return false;

	        return true;
        }

		public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((IPdaLibraryConfirmationParameters)obj);
        }
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = MinimumWavelength.GetHashCode();
                hashCode = (hashCode * 397) ^ MaximumWavelength.GetHashCode();
                hashCode = (hashCode * 397) ^ IsBaselineCorrectionEnabled.GetHashCode();
                hashCode = (hashCode * 397) ^ HitDistanceThreshold.GetHashCode();
                hashCode = (hashCode * 397) ^ SelectedLibraries.GetHashCode();
				return hashCode;
            }
        }
        public object Clone()
        {
            IPdaLibraryConfirmationParameters pdaLibraryConfirmationParametersClone = (IPdaLibraryConfirmationParameters)this.MemberwiseClone();
            pdaLibraryConfirmationParametersClone.SelectedLibraries = new List<string>();
            foreach (string selectedLibrary in this.SelectedLibraries)
            {
                pdaLibraryConfirmationParametersClone.SelectedLibraries.Add(selectedLibrary);
            }
            return pdaLibraryConfirmationParametersClone;
            //return (IPdaLibraryConfirmationParameters)MemberwiseClone();
        }
    }
}
