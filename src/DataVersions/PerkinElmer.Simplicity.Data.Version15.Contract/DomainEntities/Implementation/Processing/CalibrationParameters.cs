using System;
using System.Collections.Generic;
using System.Linq;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Processing;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Implementation.Processing
{
    public class CalibrationParameters : ICalibrationParameters
    {
        public CalibrationParameters()
        {
            LevelAmounts = new Dictionary<int, double?>();
        }

        public bool InternalStandard { get; set; }
        public Guid ReferenceInternalStandardGuid { get; set; }
        public double? InternalStandardAmount { get; set; }

        public double Purity { get; set; }


        public bool QuantifyUsingArea { get; set; }

        public CompoundCalibrationType CalibrationType { get; set; }

        public CalibrationWeightingType WeightingType { get; set; }

        public CalibrationScaling Scaling { get; set; }

        public OriginTreatment OriginTreatment { get; set; }

        public double CalibrationFactor { get; set; }

        public Guid ReferenceCompoundGuid { get; set; }



        public IDictionary<int, double?> LevelAmounts { get; set; }

        public bool Equals(ICalibrationParameters other)
        {
            if (other == null)
                return false;

            return InternalStandard == other.InternalStandard
                   && ReferenceInternalStandardGuid.Equals(other.ReferenceInternalStandardGuid)
                   && InternalStandardAmount.Equals(other.InternalStandardAmount)
                   && Purity.Equals(other.Purity)
                   && EqualDictionaryContent(LevelAmounts, other.LevelAmounts)
                   && ReferenceCompoundGuid.Equals(other.ReferenceCompoundGuid)
                   && QuantifyUsingArea == other.QuantifyUsingArea
                   && CalibrationType == other.CalibrationType
                   && WeightingType == other.WeightingType
                   && Scaling == other.Scaling
                   && OriginTreatment == other.OriginTreatment
                   && CalibrationFactor.Equals(other.CalibrationFactor);
            }

        public bool IsEqual(ICalibrationParameters other)
        {
            if (other == null)
                return false;

            return InternalStandard == other.InternalStandard
                   && InternalStandardAmount.Equals(other.InternalStandardAmount)
                   && Purity.Equals(other.Purity)
                   && EqualDictionaryContent(LevelAmounts, other.LevelAmounts)
                   && QuantifyUsingArea == other.QuantifyUsingArea
                   && CalibrationType == other.CalibrationType
                   && WeightingType == other.WeightingType
                   && Scaling == other.Scaling
                   && OriginTreatment == other.OriginTreatment
                   && CalibrationFactor.Equals(other.CalibrationFactor);
        }

        private bool EqualDictionaryContent(IDictionary<int, double?> dictionary1, IDictionary<int, double?> dictionary2)
        {
            return dictionary1.Count == dictionary2.Count
                   && !dictionary1.Except(dictionary2).Any();
        }

        public object Clone()
        {
            var clonedCalibParams = (CalibrationParameters)this.MemberwiseClone();
            clonedCalibParams.LevelAmounts = new Dictionary<int, double?>(this.LevelAmounts);
            return clonedCalibParams;
        }

    }
}
