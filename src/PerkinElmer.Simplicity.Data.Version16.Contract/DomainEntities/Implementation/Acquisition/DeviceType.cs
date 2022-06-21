

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Implementation.Acquisition
{
    public enum DeviceType
    {
        /// <summary>
        /// Mass Spectrometer
        /// </summary>
        MassSpec = 0,

        /// <summary>
        /// Full LC Stack (like Agilent LC or Shimadzu LC)
        /// </summary>
        LcStack,

        /// <summary>
        /// Standalone autosampler
        /// </summary>
        Autosampler,

        /// <summary>
        /// Standalone LC pump
        /// </summary>
        Pump,

        /// <summary>
        /// Standalone column oven
        /// </summary>
        Oven,

        /// <summary>
        /// Standalone UV detector
        /// </summary>
        UvDetector,

        /// <summary>
        /// Standalone Valve
        /// </summary>
        Valve,

        /// <summary>
        /// Standalone PDA Detector
        /// </summary>
        PhotodiodeArrayDetector,

        /// <summary>
        /// Refractive Index Detector
        /// </summary>
        RefractiveIndexDetector,

        /// <summary>
        /// Fluorescence Detector
        /// </summary>
        FluorescenceDetector,

        /// <summary>
        /// Gc Stack
        /// </summary>
        GcStack

    }
}
