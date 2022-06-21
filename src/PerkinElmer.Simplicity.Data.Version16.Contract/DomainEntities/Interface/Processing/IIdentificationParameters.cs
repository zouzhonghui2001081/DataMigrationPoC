using System;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Processing
{
	public interface IIdentificationParameters: ICloneable, IEquatable<IIdentificationParameters>
	{
		double ExpectedRetentionTime { get; set; }

		double RetentionTimeWindowAbsolute { get; set; }

		double RetentionTimeWindowInPercents { get; set; }

		double RetTimeWindowStart { get; set; }

		double RetTimeWindowEnd { get; set; }

		bool IsRetTimeReferencePeak { get; set; }

		Guid RetTimeReferencePeakGuid { get; set; } // TODO: This is the same as RetentionTimeReferenceCompound 

		int RetentionIndex { get; set; }

		bool UseClosestPeak { get; set; }			//use closest or tallest

		int Index { get; set; }                     // TODO Remove it - UI logic 

        bool? IsIntStdReferencePeak { get; set; }

        Guid IntStdReferenceGuid { get; set; } //TODO: remove it. ItdStd is a Calibration parameter, not IdentifParam

	    bool IsRrtReferencePeak { get; set; }
	    bool IsEqual(IIdentificationParameters other);
	}
}