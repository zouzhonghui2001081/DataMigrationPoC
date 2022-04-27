using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Processing;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.JsonConverter.Processing
{
    internal class SmoothParametersJsonConverter : IJsonConverter<ISmoothParameters>
    {
        private const int CurrentVersion = 1;
        private const string VersionKeyName = "Version";
        private const string FunctionKeyName = "Function";
        private const string WidthKeyName = "Width";
        private const string PassesKeyName = "Passes";
        private const string OrderKeyName = "Order";
        private const string CyclesKeyName = "Cycles";

        public JObject ToJson(ISmoothParameters instance)
        {
            return instance == null ? null : new JObject
            {
                {VersionKeyName, new JValue(CurrentVersion)},
                {FunctionKeyName, new JValue(JsonConvert.SerializeObject(instance.Function, new StringEnumConverter()))},
                {WidthKeyName, new JValue(instance.Width)},
                {PassesKeyName, new JValue(instance.Passes)},
                {OrderKeyName, new JValue(instance.Order)},
                {CyclesKeyName, new JValue(instance.Cycles)},
            };
        }

        public ISmoothParameters FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;
            var version = (int)jObject[VersionKeyName];
            if (version > CurrentVersion)
                throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);

            var smoothParameters = DomainFactory.Create<ISmoothParameters>();

            smoothParameters.Function = JsonConvert.DeserializeObject<SmoothType>((string)jObject[FunctionKeyName]);
            smoothParameters.Width = (int)jObject[WidthKeyName];
            smoothParameters.Passes = (int)jObject[PassesKeyName];
            smoothParameters.Order = (int)jObject[OrderKeyName];
            smoothParameters.Cycles = (int)jObject[CyclesKeyName];
            return smoothParameters;
        }
    }
}
