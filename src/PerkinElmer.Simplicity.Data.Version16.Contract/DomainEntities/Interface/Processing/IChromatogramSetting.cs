using System;
using System.Collections.Generic;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Processing
{
    public interface IChromatogramSetting : IPersistable, ICloneable, IEquatable<IChromatogramSetting>
    {
        string ConfigurePeakLabels { get; set; }
        bool IsOrientationVertical { get; set; }
        bool IsSignalUnitInUv { get; set; }
        bool IsTimeUnitInMinute { get; set; }
        bool IsRescalePlotSignalToFull { get; set; }
        bool IsRescalePlotSignalToMaxY { get; set; }
        bool IsRescalePlotSignalToCustom { get; set; }
        bool IsRescalePlotSpectraSignalToFull { get; set; }
        bool IsRescalePlotSpectraSignalToMaxY { get; set; }
        bool IsRescalePlotSpectraSignalToCustom { get; set; }
        bool IsRescalePlottimeFull { get; set; }
        bool IsRescalePlotWavelengthFull { get; set; }
        long RescalePlotSignalFrom { get; set; }
        long RescalePlotSignalTo { get; set; }
        long RescalePlotSpectraSignalFrom { get; set; }
        long RescalePlotSpectraSignalTo { get; set; }
        double RescalePlotTimeFrom { get; set; }
        double RescalePlotTimeTo { get; set; }
        int RescalePlotWavelengthFrom { get; set; }
        int RescalePlotWavelengthTo { get; set; }
        IList<PeakProperties> PeakPropertiesList { get; set; }
        bool IsPDAChromatogramSetting { get; set; }
        bool IsPDASpectraSetting { get; set; }

    }
}