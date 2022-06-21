using System;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Processing;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Implementation.Processing
{
	class CalibrationGlobalParameters : ICalibrationGlobalParameters
	{
		public int NumberOfLevels { get; set; }

		public string AmountUnits { get; set; }

		public UnidentifiedPeakCalibrationType UnidentifiedPeakCalibrationType { get; set; }

		public double UnidentifiedPeakCalibrationFactor { get; set; }

		public Guid UnidentifiedPeakReferenceCompoundGuid { get; set; }
	    public bool IsEqual(ICalibrationGlobalParameters other)
	    {
	        if (other == null) return false;
	        
	        return NumberOfLevels == other.NumberOfLevels
	               && string.Equals(AmountUnits, other.AmountUnits)
	               && UnidentifiedPeakCalibrationType == other.UnidentifiedPeakCalibrationType
	               && UnidentifiedPeakCalibrationFactor.Equals(other.UnidentifiedPeakCalibrationFactor);
        }

	    public object Clone()
		{
			return this.MemberwiseClone();
		}


	    public bool Equals(ICalibrationGlobalParameters other)
	    {
	        if (ReferenceEquals(null, other)) return false;
	        if (ReferenceEquals(this, other)) return true;

	        return NumberOfLevels == other.NumberOfLevels 
	               && string.Equals(AmountUnits, other.AmountUnits) 
	               && UnidentifiedPeakCalibrationType == other.UnidentifiedPeakCalibrationType
	               && UnidentifiedPeakCalibrationFactor.Equals(other.UnidentifiedPeakCalibrationFactor) 
	               && UnidentifiedPeakReferenceCompoundGuid.Equals(other.UnidentifiedPeakReferenceCompoundGuid);
	    }

	    public override bool Equals(object obj)
	    {
	        if (ReferenceEquals(null, obj)) return false;
	        if (ReferenceEquals(this, obj)) return true;
	        if (obj.GetType() != this.GetType()) return false;
	        return Equals((CalibrationGlobalParameters) obj);
	    }
	}
}
