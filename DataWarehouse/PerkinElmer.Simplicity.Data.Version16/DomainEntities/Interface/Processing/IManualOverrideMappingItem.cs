﻿using System;
using System.Collections.Generic;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Processing
{
    public interface IManualOverrideMappingItem
    {
        Guid BatchRunChannelGuid { get; set; }
        Guid BatchRunGuid { get; set; }
        IList<IIntegrationEvent> TimedIntegrationParameters { get; set; }
    }
}