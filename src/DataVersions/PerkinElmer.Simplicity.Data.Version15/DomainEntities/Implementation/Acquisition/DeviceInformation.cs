﻿using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Acquisition;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.Implementation.Acquisition
{
    public class DeviceInformation : IDeviceInformation
    {
        /// <inheritdoc />
        public string FirmwareVersion { get; set; }

        /// <inheritdoc />
        public string SerialNumber { get; set; }

        /// <inheritdoc />
        public string ModelName { get; set; }

        /// <inheritdoc />
        public string UniqueIdentifier { get; set; }

        /// <inheritdoc />
        public string InterfaceAddress { get; set; }
    }
}
