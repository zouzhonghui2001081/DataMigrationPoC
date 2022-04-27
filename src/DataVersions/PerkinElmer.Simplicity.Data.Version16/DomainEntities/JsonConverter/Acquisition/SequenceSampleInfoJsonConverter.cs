using System;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Acquisition;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.JsonConverter.Acquisition
{
    public class SequenceSampleInfoList
    {
        public const string PropertyName = "Sequence Sample Info List";
    }

    internal class SequenceSampleInfoJsonConverter: IJsonConverter<ISequenceSampleInfo>
    {
        private const int CurrentVersion = 2;
        private const int Version1 = 1;
        private const string VersionKeyName = "Version";
        private const string GuidKeyName = "Guid";
        private const string SampleNameKeyName = "SampleName";
        private const string SelectedKeyName = "Selected";
        private const string SampleIdKeyName = "SampleId";
        //private const string NumberKeyName = "Number";
        private const string UserCommentsKeyName = "UserComments";
        private const string SampleTypeKeyName = "SampleType";
        private const string NumberOfRepeatsKeyName = "NumberOfRepeats";
        private const string LevelKeyName = "Level";
        private const string SampleAmountKeyName = "SampleAmount";
        private const string MultiplierKeyName = "Multiplier";
        private const string DivisorKeyName = "Divisor";
        private const string AddendKeyName = "Addend";
        private const string DilutionFactorKeyName = "DilutionFactor";
        private const string NormalizationFactorKeyName = "NormalizationFactor";
        private const string UnknownAmountAdjustmentKeyName = "UnknownAmountAdjustment";
        private const string InternalStandardAmountAdjustmentKeyName = "InternalStandardAmountAdjustment";
        private const string BaselineCorrectionKeyName = "BaselineCorrection";
        private const string BaselineRunGuidKeyName = "BaselineRunGuid";
        private const string RackCodeKeyName = "RackCode";
        private const string RackPositionKeyName = "RackPosition";
        private const string PlateCodeKeyName = "PlateCode";
        private const string PlatePositionKeyName = "PlatePosition";
        private const string VialPositionKeyName = "VialPosition";
        private const string DestinationVialKeyName = "DestinationVial";
        private const string PlatePositionAsIntegerKeyName = "PlatePositionAsInteger";
        private const string PlateCodeAsIntegerKeyName = "PlateCodeAsInteger";
        private const string VialPositionAsIntegerKeyName = "VialPositionAsInteger";
        private const string DestinationVialAsIntegerKeyName = "DestinationVialAsInteger";
        private const string InjectionVolumeKeyName = "InjectionVolume";
        private const string InjectionTypeKeyName = "InjectionType";
        private const string InjectionTypeAsIntegerKeyName = "InjectionTypeAsInteger";
        private const string IsSelectedKeyName = "IsSelected";
        private const string InjectionPortAsIntegerKeyName = "InjectionPortAsInteger";
        private const string InjectionPortKeyName = "InjectionPort";
        private const string AcquisitionMethodNameKeyName = "AcquisitionMethodName";
        private const string AcquisitionMethodVersionNumberKeyName = "AcquisitionMethodVersionNumber";
        private const string AcquisitionMethodStateKeyName = "AcquisitionMethodState";
        private const string ProcessingMethodNameKeyName = "ProcessingMethodName";
        private const string ProcessingMethodVersionNumberKeyName = "ProcessingMethodVersionNumber";
        private const string ProcessingMethodStateKeyName = "ProcessingMethodState";
        private const string CalibrationMethodNameKeyName = "CalibrationMethodName";
        private const string CalibrationBracketKeyName = "CalibrationBracket";
        private const string StandardAmountAdjustmentKeyName = "StandardAmountAdjustment";
        private const string SampleReportTemplateKeyName = "SampleReportTemplate";
        private const string SummaryReportGroupKeyName = "SummaryReportGroup";
        private const string SuitabilitySampleTypeKeyName = "SuitabilitySampleType";
        public ISequenceSampleInfo FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;
            var version = (int)jObject[VersionKeyName];
            if (jObject.ContainsKey(VersionKeyName))
            {
                if (version > CurrentVersion)
                    throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);
            }

            var sequenceSampleInfo = DomainFactory.Create<ISequenceSampleInfo>();
            sequenceSampleInfo.Guid = (Guid)jObject[GuidKeyName];
            sequenceSampleInfo.SampleName = (string)jObject[SampleNameKeyName];
            sequenceSampleInfo.Selected = (bool)jObject[SelectedKeyName];
            sequenceSampleInfo.SampleId = (string)jObject[SampleIdKeyName];
            //sequenceSampleInfo.Number = (string)jObject[NumberKeyName];
            sequenceSampleInfo.UserComments = (string)jObject[UserCommentsKeyName];
            string sampleType = (string)jObject[SampleTypeKeyName];
            SampleType sampleTypeValue;
            if (Enum.TryParse(sampleType, out sampleTypeValue))
            {
                sequenceSampleInfo.SampleType = sampleTypeValue;
            }
            sequenceSampleInfo.NumberOfRepeats = (int)jObject[NumberOfRepeatsKeyName];
            sequenceSampleInfo.Level = (int)jObject[LevelKeyName];
            sequenceSampleInfo.SampleAmount = (double)jObject[SampleAmountKeyName];
            sequenceSampleInfo.Multiplier = (double)jObject[MultiplierKeyName];
            sequenceSampleInfo.Divisor = (double)jObject[DivisorKeyName];
            sequenceSampleInfo.Addend = (double)jObject[AddendKeyName];
            sequenceSampleInfo.DilutionFactor = (double)jObject[DilutionFactorKeyName];
            sequenceSampleInfo.NormalizationFactor = (double)jObject[NormalizationFactorKeyName];
            sequenceSampleInfo.UnknownAmountAdjustment = (double)jObject[UnknownAmountAdjustmentKeyName];
            sequenceSampleInfo.InternalStandardAmountAdjustment = (double?)jObject[InternalStandardAmountAdjustmentKeyName];
            string baselineCorrection = (string)jObject[BaselineCorrectionKeyName];
            BaselineCorrection baselineCorrectionValue;
            if (Enum.TryParse(baselineCorrection, out baselineCorrectionValue))
            {
                sequenceSampleInfo.BaselineCorrection = baselineCorrectionValue;
            }
            sequenceSampleInfo.BaselineRunGuid = (Guid)jObject[BaselineRunGuidKeyName];
            sequenceSampleInfo.RackCode = (string)jObject[RackCodeKeyName];
            sequenceSampleInfo.RackPosition = (int)jObject[RackPositionKeyName];
            sequenceSampleInfo.PlateCode = (string)jObject[PlateCodeKeyName];
            sequenceSampleInfo.PlatePosition = (string)jObject[PlatePositionKeyName];
            sequenceSampleInfo.VialPosition = (string)jObject[VialPositionKeyName];
            sequenceSampleInfo.DestinationVial = (string)jObject[DestinationVialKeyName];
            sequenceSampleInfo.PlatePositionAsInteger = jObject[PlatePositionAsIntegerKeyName].Type == JTokenType.Null? null:
                JsonConverterRegistry.GetConverter<IntValueWithDeviceName>().FromJson((JObject)jObject[PlatePositionAsIntegerKeyName]);
            sequenceSampleInfo.PlateCodeAsInteger = jObject[PlateCodeAsIntegerKeyName].Type == JTokenType.Null ? null :
                JsonConverterRegistry.GetConverter<IntValueWithDeviceName>().FromJson((JObject)jObject[PlateCodeAsIntegerKeyName]);
            sequenceSampleInfo.VialPositionAsInteger = jObject[VialPositionAsIntegerKeyName].Type == JTokenType.Null ? null :
                JsonConverterRegistry.GetConverter<IntValueWithDeviceName>().FromJson((JObject)jObject[VialPositionAsIntegerKeyName]);
            sequenceSampleInfo.DestinationVialAsInteger = jObject[DestinationVialAsIntegerKeyName].Type == JTokenType.Null ? null :
                JsonConverterRegistry.GetConverter<IntValueWithDeviceName>().FromJson((JObject)jObject[DestinationVialAsIntegerKeyName]);
            sequenceSampleInfo.InjectionVolume = jObject[InjectionVolumeKeyName].Type == JTokenType.Null ? null :
                JsonConverterRegistry.GetConverter<DoubleValueWithDeviceName>().FromJson((JObject)jObject[InjectionVolumeKeyName]);
            sequenceSampleInfo.InjectionType = (string)jObject[InjectionTypeKeyName];
            sequenceSampleInfo.InjectionTypeAsInteger = jObject[InjectionTypeAsIntegerKeyName].Type == JTokenType.Null ? null :
                JsonConverterRegistry.GetConverter<IntValueWithDeviceName>().FromJson((JObject)jObject[InjectionTypeAsIntegerKeyName]);
            sequenceSampleInfo.IsSelected = (bool)jObject[IsSelectedKeyName];
            sequenceSampleInfo.InjectionPortAsInteger = jObject[InjectionPortAsIntegerKeyName].Type == JTokenType.Null ? null :
                JsonConverterRegistry.GetConverter<IntValueWithDeviceName>().FromJson((JObject)jObject[InjectionPortAsIntegerKeyName]);
            sequenceSampleInfo.InjectionPort = (string)jObject[InjectionPortKeyName];
            sequenceSampleInfo.AcquisitionMethodName = (string)jObject[AcquisitionMethodNameKeyName];
            sequenceSampleInfo.AcquisitionMethodVersionNumber = (int) jObject[AcquisitionMethodVersionNumberKeyName];
            sequenceSampleInfo.AcquisitionMethodState = (short) jObject[AcquisitionMethodStateKeyName];
            sequenceSampleInfo.ProcessingMethodName = (string)jObject[ProcessingMethodNameKeyName];
            sequenceSampleInfo.ProcessingMethodVersionNumber = (int) jObject[ProcessingMethodVersionNumberKeyName];
            sequenceSampleInfo.ProcessingMethodState = (short) jObject[ProcessingMethodStateKeyName];
            sequenceSampleInfo.CalibrationMethodName = (string)jObject[CalibrationMethodNameKeyName];
            sequenceSampleInfo.CalibrationBracket = (int)jObject[CalibrationBracketKeyName];
            sequenceSampleInfo.StandardAmountAdjustment = (double?)jObject[StandardAmountAdjustmentKeyName];
            sequenceSampleInfo.SampleReportTemplate = (string)jObject[SampleReportTemplateKeyName];
            sequenceSampleInfo.SummaryReportGroup = (string)jObject[SummaryReportGroupKeyName];

            if (version == Version1)
            {
                sequenceSampleInfo.SuitabilitySampleType = SuitabilitySampleType.None;
            }
            else
            {
                string suitabilitySampleType = (string)jObject[SuitabilitySampleTypeKeyName];

                if (Enum.TryParse(suitabilitySampleType, out SuitabilitySampleType suitabilitySampleTypeValue))
                {
                    sequenceSampleInfo.SuitabilitySampleType = suitabilitySampleTypeValue;
                }
            }

            return sequenceSampleInfo;
        }

        public JObject ToJson(ISequenceSampleInfo instance)
        {
            if (instance == null)
                return null;
            var jObject = new JObject
            {
                {VersionKeyName, new JValue(CurrentVersion)},
                {GuidKeyName, new JValue(instance.Guid)},
                {SampleNameKeyName, new JValue(instance.SampleName)},
                {SelectedKeyName, new JValue(instance.Selected)},
                {SampleIdKeyName, new JValue(instance.SampleId)},
                //{NumberKeyName, new JValue(instance.Number)},
                {UserCommentsKeyName, new JValue(instance.UserComments)},
                {SampleTypeKeyName, new JValue(instance.SampleType.ToString())},
                {NumberOfRepeatsKeyName, new JValue(instance.NumberOfRepeats)},
                {LevelKeyName, new JValue(instance.Level)},
                {SampleAmountKeyName, new JValue(instance.SampleAmount)},
                {MultiplierKeyName, new JValue(instance.Multiplier)},
                {DivisorKeyName, new JValue(instance.Divisor)},
                {AddendKeyName, new JValue(instance.Addend)},
                {DilutionFactorKeyName, new JValue(instance.DilutionFactor)},
                {NormalizationFactorKeyName, new JValue(instance.NormalizationFactor)},
                {UnknownAmountAdjustmentKeyName, new JValue(instance.UnknownAmountAdjustment)},
                {InternalStandardAmountAdjustmentKeyName, new JValue(instance.InternalStandardAmountAdjustment)},
                {BaselineCorrectionKeyName, new JValue(instance.BaselineCorrection.ToString())},
                {BaselineRunGuidKeyName, new JValue(instance.BaselineRunGuid)},
                {RackCodeKeyName, new JValue(instance.RackCode)},
                {RackPositionKeyName, new JValue(instance.RackPosition)},
                {PlateCodeKeyName, new JValue(instance.PlateCode) },
                {PlatePositionKeyName, new JValue(instance.PlatePosition)},
                {VialPositionKeyName, new JValue(instance.VialPosition)},
                {DestinationVialKeyName, new JValue(instance.DestinationVial)},
                {PlatePositionAsIntegerKeyName, JsonConverterRegistry.GetConverter<IntValueWithDeviceName>().ToJson(instance.PlatePositionAsInteger)},
                {PlateCodeAsIntegerKeyName, JsonConverterRegistry.GetConverter<IntValueWithDeviceName>().ToJson(instance.PlateCodeAsInteger)},
                {VialPositionAsIntegerKeyName, JsonConverterRegistry.GetConverter<IntValueWithDeviceName>().ToJson(instance.VialPositionAsInteger)},
                {DestinationVialAsIntegerKeyName, JsonConverterRegistry.GetConverter<IntValueWithDeviceName>().ToJson(instance.DestinationVialAsInteger)},
                {InjectionVolumeKeyName, JsonConverterRegistry.GetConverter<DoubleValueWithDeviceName>().ToJson(instance.InjectionVolume)},
                {InjectionTypeKeyName, new JValue(instance.InjectionType)},
                {InjectionTypeAsIntegerKeyName, JsonConverterRegistry.GetConverter<IntValueWithDeviceName>().ToJson(instance.InjectionTypeAsInteger)},
                {IsSelectedKeyName, new JValue(instance.IsSelected)},
                { InjectionPortAsIntegerKeyName, JsonConverterRegistry.GetConverter<IntValueWithDeviceName>().ToJson(instance.InjectionPortAsInteger)},
                {InjectionPortKeyName, new JValue(instance.InjectionPort)},
                {AcquisitionMethodNameKeyName, new JValue(instance.AcquisitionMethodName)},
                {AcquisitionMethodVersionNumberKeyName, new JValue(instance.AcquisitionMethodVersionNumber) },
                {AcquisitionMethodStateKeyName, new JValue(instance.AcquisitionMethodState) },
                {ProcessingMethodNameKeyName, new JValue(instance.ProcessingMethodName)},
                {ProcessingMethodVersionNumberKeyName, new JValue(instance.ProcessingMethodVersionNumber) },
                {ProcessingMethodStateKeyName, new JValue(instance.ProcessingMethodState) },
                {CalibrationMethodNameKeyName, new JValue(instance.CalibrationMethodName)},
                {CalibrationBracketKeyName, new JValue(instance.CalibrationBracket)},
                {StandardAmountAdjustmentKeyName, new JValue(instance.StandardAmountAdjustment)},
                {SampleReportTemplateKeyName, new JValue(instance.SampleReportTemplate)},
                {SummaryReportGroupKeyName, new JValue(instance.SummaryReportGroup)},
                {SuitabilitySampleTypeKeyName, new JValue(instance.SuitabilitySampleType.ToString())},
            };
            
            return jObject;
        }
    }
}
