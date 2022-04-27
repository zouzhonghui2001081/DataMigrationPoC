﻿using System;
using System.Collections.Generic;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Processing
{
    public interface IChannelMethod : ICloneable, IEquatable<IChannelMethod>
    {
        [Obsolete]int ChannelIndex { set; get; } // deprecated

        IChromatographicChannelDescriptor ChannelDescriptor { get; set; }

        IProcessingMethodChannelIdentifier ChannelIdentifier { get; set; }

		bool ExtraProcMethodChannelForAllSamples { get; set; } //TODO: add it to clone

		bool AutoGeneratedFromData { get; set; } //TODO: add it to clone

		Guid ChannelGuid { get; set; } // This is the id of this channel method. Used for referencing from ICompound and other places where ChannelIndex was previously used.
        Guid ParentChannelGuid { get; set; }

        //IDeviceIdentifier DeviceMethodIdentifier { get; set; } // Links this Channel Method to its parent IProcessingDeviceMethod

        ISmoothParameters SmoothParams { get; set; }

		bool IsPdaMethod { get; set; }

		IPdaParameters PdaParameters { get; set; }

		double WidthRatio { get; set; }

		double ValleyToPeakRatio { get; set; }

		double TimeAdjustment { get; set; }

		double TangentSkimWidth { get; set; }

		double PeakHeightRatio { get; set; }

		double AdjustedHeightRatio { get; set; }

		double ValleyHeightRatio { get; set; }

		// used in peak identification & RRT calc (done after Peak ID)
		double VoidTime { get; set; }

		VoidTimeType VoidTimeType { get; set; }

		// used in RRT calc (done after Peak ID)
		Guid RrtReferenceCompound { get; set; } //TODO: Should stay here, but use compound name instead of GUID

		RrtReferenceType RrtReferenceType { get; set; }
	    double CalibrationFactor { get; set; }

        UnidentifiedPeakCalibrationType UnidentifiedPeakCalibrationType { get; set; }

        string AmountUnit { get; set; }

		// used in Quant (applying calibration)
//		Guid UnidentifiedPeakQuantCompoundGuid { get; set; } //TODO: To be moved to "Calibration" Method
		//other items not yet implemented but probably will be included
		// used in Peak ID
//		Guid RetentionTimeReferenceCompound { get; set; }  //TODO: To be moved to "Calibration" Method, has to be changed per compound

        IList<IIntegrationEvent> TimedIntegrationParameters { get; set; }

        int? BunchingFactor { get; set; }

        double? NoiseThreshold { get; set; }

        double? AreaThreshold { get; set; }

	    bool IsEqual(IChannelMethod other);
	}
}