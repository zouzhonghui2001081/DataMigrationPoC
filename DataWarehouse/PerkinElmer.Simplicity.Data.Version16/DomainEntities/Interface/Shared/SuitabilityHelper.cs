using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Acquisition;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Shared
{
    public static class SuitabilityHelper
    {
        public static SuitabilitySampleType ColumnValueToSuitabilitySampleTypeTranslater(bool sstInjection, bool sstGroup)
        {
            SuitabilitySampleType suitabilitySampleType = SuitabilitySampleType.None;

            if (sstGroup && sstInjection)
            {
                suitabilitySampleType = SuitabilitySampleType.UsedForSuitabilityAndGrouped;
            }

            if (!sstGroup && sstInjection)
            {
                suitabilitySampleType = SuitabilitySampleType.UsedForSuitability;
            }

            if (!sstGroup && !sstInjection)
            {
                suitabilitySampleType = SuitabilitySampleType.None;
            }

            return suitabilitySampleType;
        }

        public static (bool sstInjection, bool sstGroup) SuitabilitySampleTypeToColumnValueTranslater(SuitabilitySampleType suitabilitySampleType)
        {
            bool sstInjection = false;
            bool sstGroup = false;
            switch (suitabilitySampleType)
            {
                case SuitabilitySampleType.UsedForSuitabilityAndGrouped:
                    sstGroup = true;
                    sstInjection = true;
                    break;
                case SuitabilitySampleType.UsedForSuitability:
                    sstInjection = true;
                    break;
                case SuitabilitySampleType.None:
                    break;
            }

            return (sstInjection, sstGroup);
        }
    }
}
