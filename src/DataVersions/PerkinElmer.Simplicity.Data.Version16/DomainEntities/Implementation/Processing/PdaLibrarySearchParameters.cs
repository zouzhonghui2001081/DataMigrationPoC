using System.Collections.Generic;
using System.Linq;
using PerkinElmer.Domain.Contracts.Processing;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Processing;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Implementation.Processing
{
    internal class PdaLibrarySearchParameters : IPdaLibrarySearchParameters
	{
		public double MinimumWavelength { get; set; }
		public double MaximumWavelength { get; set; }
		public double MatchRetentionTimeWindow { get; set; }
		public bool IsMatchRetentionTimeWindowEnabled { get; set; }
		public bool IsBaselineCorrectionEnabled { get; set; }
		public double HitDistanceThreshold { get; set; }
		public bool IsPeakLibrarySearch { get; set; }
		public IList<string> SelectedLibraries { get; set; }
        public bool UseWavelengthLimits { get; set; }
		public int MaxNumberOfResults { get; set; }

	    public bool Equals(IPdaLibrarySearchParameters other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return IsPeakLibrarySearch.Equals(other.IsPeakLibrarySearch) && MinimumWavelength.Equals(other.MinimumWavelength) 
			 && MaximumWavelength.Equals(other.MaximumWavelength) && IsBaselineCorrectionEnabled == other.IsBaselineCorrectionEnabled && 
			 IsMatchRetentionTimeWindowEnabled == other.IsMatchRetentionTimeWindowEnabled && MatchRetentionTimeWindow.Equals(other.MatchRetentionTimeWindow)
			&& HitDistanceThreshold.Equals(other.HitDistanceThreshold) && IsListsEquals(SelectedLibraries,other.SelectedLibraries)
			&& UseWavelengthLimits.Equals(other.UseWavelengthLimits) && MaxNumberOfResults.Equals(other.MaxNumberOfResults);
		}

		public static bool IsListsEquals(IList<string> pdaSelectedLibraries, IList<string> selectedLibraries)
		{
			if (pdaSelectedLibraries.Count != selectedLibraries.Count)
				return false;

			if  (pdaSelectedLibraries.Intersect(selectedLibraries).Count() != pdaSelectedLibraries.Count)
				return false;

			return true;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((IPdaLibrarySearchParameters)obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = IsPeakLibrarySearch.GetHashCode();
				hashCode = (hashCode * 397) ^ MinimumWavelength.GetHashCode();
				hashCode = (hashCode * 397) ^ MaximumWavelength.GetHashCode();
				hashCode = (hashCode * 397) ^ IsBaselineCorrectionEnabled.GetHashCode();
				hashCode = (hashCode * 397) ^ IsMatchRetentionTimeWindowEnabled.GetHashCode();
				hashCode = (hashCode * 397) ^ MatchRetentionTimeWindow.GetHashCode();
				hashCode = (hashCode * 397) ^ HitDistanceThreshold.GetHashCode();
				hashCode = (hashCode * 397) ^ MaxNumberOfResults.GetHashCode();
				hashCode = (hashCode * 397) ^ UseWavelengthLimits.GetHashCode();
				hashCode = (hashCode * 397) ^ SelectedLibraries.GetHashCode();
				return hashCode;
			}
		}

		public object Clone()
		{
            IPdaLibrarySearchParameters pdaLibrarySearchParametersClone = (IPdaLibrarySearchParameters)this.MemberwiseClone();
            pdaLibrarySearchParametersClone.SelectedLibraries = new List<string>();
            foreach (string selectedLibrary in this.SelectedLibraries)
            {
                pdaLibrarySearchParametersClone.SelectedLibraries.Add(selectedLibrary);
            }
            return pdaLibrarySearchParametersClone;
            //return this.MemberwiseClone();
        }
	}
}
