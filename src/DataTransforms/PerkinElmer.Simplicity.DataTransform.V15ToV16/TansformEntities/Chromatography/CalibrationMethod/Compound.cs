using Compound15 = PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography.CalibrationMethod.Compound;
using Compound16 = PerkinElmer.Simplicity.Data.Version16.Contract.DataEntities.Chromatography.CalibrationMethod.Compound;

namespace PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography.CalibrationMethod
{
    public class Compound
    {
        public static Compound16 Transform(Compound15 compound)
        {
            var compound16 = new Compound16
            {
                Id = compound.Id,
                ProcessingMethodId = compound.ProcessingMethodId,
                Guid = compound.Guid,
                Name = compound.Name,
                ProcessingMethodChannelGuid = compound.ProcessingMethodChannelGuid,
                CompoundType = compound.CompoundType,
                ExpectedRetentionTime = compound.ExpectedRetentionTime,
                RetentionTimeWindowAbsolute = compound.RetentionTimeWindowAbsolute,
                RetentionTimeWindowInPercents = compound.RetentionTimeWindowInPercents,
                RetTimeWindowStart = compound.RetTimeWindowStart,
                RetTimeWindowEnd = compound.RetTimeWindowEnd,
                IsRetTimeReferencePeak = compound.IsRetTimeReferencePeak,
                RetTimeReferencePeakGuid = compound.RetTimeReferencePeakGuid,
                RetentionIndex = compound.RetentionIndex,
                UseClosestPeak = compound.UseClosestPeak,
                IsIntStdReferencePeak = compound.IsIntStdReferencePeak,
                IntStdReferenceGuid = compound.IntStdReferenceGuid,
                Index = compound.Index,
                IsRrtReferencePeak = compound.IsRrtReferencePeak,
                InternalStandard = compound.InternalStandard,
                ReferenceInternalStandardGuid = compound.ReferenceInternalStandardGuid,
                Purity = compound.Purity,
                QuantifyUsingArea = compound.QuantifyUsingArea,
                CalibrationType = compound.CalibrationType,
                WeightingType = compound.WeightingType,
                Scaling = compound.Scaling,
                OriginTreatment = compound.OriginTreatment,
                CalibrationFactor = compound.CalibrationFactor,
                ReferenceCompoundGuid = compound.ReferenceCompoundGuid,
                InternalStandardAmount = compound.InternalStandardAmount,
                CompoundNumber = compound.CompoundNumber,
                IsCompoundGroup = compound.IsCompoundGroup,
                StartTime = compound.StartTime,
                EndTime = compound.EndTime,
                UsedForSuitability = compound.UsedForSuitability
            };
            if (compound.LevelAmounts != null)
            {
                foreach (var levelAmount in compound.LevelAmounts)
                    compound16.LevelAmounts.Add(LevelAmount.Transform(levelAmount));
            }

            if (compound.CompoundGuids != null)
            {
                foreach (var compoundGuid in compound.CompoundGuids)
                    compound16.CompoundGuids.Add(CompoundGuids.Transform(compoundGuid));
            }

            return compound16;
        }
    }
}
