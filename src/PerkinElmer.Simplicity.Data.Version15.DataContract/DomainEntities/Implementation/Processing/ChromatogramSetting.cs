using System;
using System.Collections.Generic;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Processing;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Implementation.Processing
{
    public class ChromatogramSetting : IChromatogramSetting
    {
        public string ConfigurePeakLabels { get  ; set  ; }
        public bool IsOrientationVertical { get  ; set  ; }
        public bool IsSignalUnitInUv { get  ; set  ; }
        public bool IsTimeUnitInMinute { get  ; set  ; }
        public bool IsRescalePlotSignalToFull { get  ; set  ; }
        public bool IsRescalePlotSignalToMaxY { get  ; set  ; }
        public bool IsRescalePlotSignalToCustom { get  ; set  ; }
        public bool IsRescalePlottimeFull { get  ; set  ; }
        public bool IsRescalePlotWavelengthFull { get; set; }
        public long RescalePlotSignalFrom { get  ; set  ; }
        public long RescalePlotSignalTo { get  ; set  ; }
        public double RescalePlotTimeFrom { get  ; set  ; }
        public double RescalePlotTimeTo { get  ; set  ; }
        public int RescalePlotWavelengthFrom { get; set; }
        public int RescalePlotWavelengthTo { get; set; }
        public IList<PeakProperties> PeakPropertiesList { get; set; }
        public string Name { get  ; set  ; }
        public Guid Guid { get  ; set  ; }
        public DateTime CreatedDateUtc { get  ; set  ; }
        public IUserInfo CreatedByUser { get  ; set  ; }
        public DateTime ModifiedDateUtc { get  ; set  ; }
        public IUserInfo ModifiedByUser { get  ; set  ; }
        public bool IsRescalePlotSpectraSignalToFull { get ; set ; }
        public bool IsRescalePlotSpectraSignalToMaxY { get ; set ; }
        public bool IsRescalePlotSpectraSignalToCustom { get ; set ; }
        public long RescalePlotSpectraSignalFrom { get ; set ; }
        public long RescalePlotSpectraSignalTo { get ; set ; }
        public bool IsPDAChromatogramSetting { get; set; }
        public bool IsPDASpectraSetting { get; set; }

        public object Clone()
        {
            var chromatogramSetting = new ChromatogramSetting()
            {
                ConfigurePeakLabels = ConfigurePeakLabels,
                IsOrientationVertical = IsOrientationVertical,
                IsSignalUnitInUv = IsSignalUnitInUv,
                IsTimeUnitInMinute = IsTimeUnitInMinute,
                IsRescalePlotSignalToFull = IsRescalePlotSignalToFull,
                IsRescalePlotSignalToMaxY = IsRescalePlotSignalToMaxY,
                IsRescalePlotSignalToCustom = IsRescalePlotSignalToCustom,
                IsRescalePlottimeFull = IsRescalePlottimeFull,
                IsRescalePlotWavelengthFull = IsRescalePlotWavelengthFull,
                RescalePlotSignalFrom = RescalePlotSignalFrom,
                RescalePlotSignalTo = RescalePlotSignalTo,
                RescalePlotTimeFrom = RescalePlotTimeFrom,
                RescalePlotTimeTo = RescalePlotTimeTo,
                RescalePlotWavelengthFrom = RescalePlotWavelengthFrom,
                RescalePlotWavelengthTo = RescalePlotWavelengthTo,
                IsRescalePlotSpectraSignalToFull = IsRescalePlotSpectraSignalToFull,
                IsRescalePlotSpectraSignalToMaxY = IsRescalePlotSpectraSignalToMaxY,
                IsRescalePlotSpectraSignalToCustom = IsRescalePlotSpectraSignalToCustom,
                RescalePlotSpectraSignalFrom = RescalePlotSpectraSignalFrom,
                RescalePlotSpectraSignalTo = RescalePlotSpectraSignalTo,
                IsPDAChromatogramSetting = IsPDAChromatogramSetting,
                IsPDASpectraSetting = IsPDASpectraSetting,
                Name = Name,
                Guid = Guid,
                CreatedDateUtc = CreatedDateUtc,
                CreatedByUser = CreatedByUser,
                ModifiedDateUtc = ModifiedDateUtc,
                ModifiedByUser = ModifiedByUser,
                PeakPropertiesList= PeakPropertiesList
            };
            return chromatogramSetting;
        }
        public bool Equals(IChromatogramSetting chromatogramSetting)
        {
            if (chromatogramSetting == null)
            {
                return false;
            }

            return StringComparer.Ordinal.Equals(ConfigurePeakLabels, chromatogramSetting.ConfigurePeakLabels)
                   && StringComparer.Ordinal.Equals(IsOrientationVertical, chromatogramSetting.IsOrientationVertical)
                   && StringComparer.Ordinal.Equals(IsSignalUnitInUv, chromatogramSetting.IsSignalUnitInUv)
                   && StringComparer.Ordinal.Equals(IsTimeUnitInMinute, chromatogramSetting.IsTimeUnitInMinute)
                   && StringComparer.Ordinal.Equals(IsRescalePlotSignalToFull, chromatogramSetting.IsRescalePlotSignalToFull)
                   && StringComparer.Ordinal.Equals(IsRescalePlotSignalToMaxY, chromatogramSetting.IsRescalePlotSignalToMaxY)
                   && StringComparer.Ordinal.Equals(IsRescalePlotSignalToCustom, chromatogramSetting.IsRescalePlotSignalToCustom)
                   && StringComparer.Ordinal.Equals(RescalePlotSignalFrom, chromatogramSetting.RescalePlotSignalFrom)
                   && StringComparer.Ordinal.Equals(RescalePlotSignalTo, chromatogramSetting.RescalePlotSignalTo)
                   && StringComparer.Ordinal.Equals(IsRescalePlottimeFull, chromatogramSetting.IsRescalePlottimeFull)
                   && StringComparer.Ordinal.Equals(RescalePlotTimeFrom, chromatogramSetting.RescalePlotTimeFrom)
                   && StringComparer.Ordinal.Equals(RescalePlotTimeTo, chromatogramSetting.RescalePlotTimeTo);
        }
    }
}
