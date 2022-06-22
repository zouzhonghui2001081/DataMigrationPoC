using System;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Processing;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.JsonConverter.Processing
{
    internal class IdentificationParametersJsonConverter : IJsonConverter<IIdentificationParameters>
    {
        private const int CurrentVersion = 1;
        private const string VersionKeyName = "Version";
        private const string ExpectedRetentionTimeKeyName = "ExpectedRetentionTime";
        private const string RetentionTimeWindowAbsoluteKeyName = "RetentionTimeWindowAbsolute";
        private const string RetentionTimeWindowInPercentsKeyName = "RetentionTimeWindowInPercents";
        private const string RetTimeWindowStartKeyName = "RetTimeWindowStart";
        private const string RetTimeWindowEndKeyName = "RetTimeWindowEnd";
        private const string IsRetTimeReferencePeakKeyName = "IsRetTimeReferencePeak";
        private const string RetTimeReferencePeakGuidKeyName = "RetTimeReferencePeakGuid";
        private const string RetentionIndexKeyName = "RetentionIndex";
        private const string UseClosestPeakKeyName = "UseClosestPeak";
        private const string IndexKeyName = "Index";
        private const string IsIntStdReferencePeakKeyName = "IsIntStdReferencePeak";
        private const string IntStdReferenceGuidKeyName = "IntStdReferenceGuid";
        private const string IsRrtReferencePeakKeyName = "IsRrtReferencePeak";

        public JObject ToJson(IIdentificationParameters instance)
        {
            return instance == null ? null : new JObject
            {
                {VersionKeyName, new JValue(CurrentVersion)},
                {ExpectedRetentionTimeKeyName, new JValue(instance.ExpectedRetentionTime)},
                {RetentionTimeWindowAbsoluteKeyName, new JValue(instance.RetentionTimeWindowAbsolute)},
                {RetentionTimeWindowInPercentsKeyName, new JValue(instance.RetentionTimeWindowInPercents)},
                {RetTimeWindowStartKeyName, new JValue(instance.RetTimeWindowStart)},
                {RetTimeWindowEndKeyName, new JValue(instance.RetTimeWindowEnd)},
                {IsRetTimeReferencePeakKeyName, new JValue(instance.IsRetTimeReferencePeak)},
                {RetTimeReferencePeakGuidKeyName, new JValue(instance.RetTimeReferencePeakGuid)},
                {RetentionIndexKeyName, new JValue(instance.RetentionIndex)},
                {UseClosestPeakKeyName, new JValue(instance.UseClosestPeak)},
                {IndexKeyName, new JValue(instance.Index)},
                {IsIntStdReferencePeakKeyName, new JValue(instance.IsIntStdReferencePeak)},
                {IntStdReferenceGuidKeyName, new JValue(instance.IntStdReferenceGuid)},
                {IsRrtReferencePeakKeyName, new JValue(instance.IsRrtReferencePeak)},
            };
        }

        public IIdentificationParameters FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;
            var version = (int)jObject[VersionKeyName];
            if (version > CurrentVersion)
                throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);

            var identificationParameters = DomainFactory.Create<IIdentificationParameters>();

            identificationParameters.ExpectedRetentionTime = (double)jObject[ExpectedRetentionTimeKeyName];
            identificationParameters.RetentionTimeWindowAbsolute = (double)jObject[RetentionTimeWindowAbsoluteKeyName];
            identificationParameters.RetentionTimeWindowInPercents = (double)jObject[RetentionTimeWindowInPercentsKeyName];
            identificationParameters.RetTimeWindowStart = (double)jObject[RetTimeWindowStartKeyName];
            identificationParameters.RetTimeWindowEnd = (double)jObject[RetTimeWindowEndKeyName];
            identificationParameters.IsRetTimeReferencePeak = (bool)jObject[IsRetTimeReferencePeakKeyName];
            identificationParameters.RetTimeReferencePeakGuid = (Guid)jObject[RetTimeReferencePeakGuidKeyName];
            identificationParameters.RetentionIndex = (int)jObject[RetentionIndexKeyName];
            identificationParameters.UseClosestPeak = (bool)jObject[UseClosestPeakKeyName];
            identificationParameters.Index = (int)jObject[IndexKeyName];
            identificationParameters.IsIntStdReferencePeak = (bool?)jObject[IsIntStdReferencePeakKeyName];
            identificationParameters.IntStdReferenceGuid = (Guid)jObject[IntStdReferenceGuidKeyName];
            identificationParameters.IsRrtReferencePeak = (bool)jObject[IsRrtReferencePeakKeyName];
            return identificationParameters;
        }
    }
}
