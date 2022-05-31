using System;
using System.Reflection;
using log4net;
using PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography;
using SequenceData = PerkinElmer.Simplicity.Data.Version16.Contract.Version.Chromatography.SequenceData;

namespace PerkinElmer.Simplicity.DataTransform.V15ToV16.Chromatography
{
    internal class SequenceDataTransform 
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
      
        public static SequenceData Transform(Data.Version15.Contract.Version.Chromatography.SequenceData sequenceData)
        {
            if (sequenceData == null) throw new ArgumentNullException(nameof(sequenceData));

            return new SequenceData
            {
                ProjectGuid = sequenceData.ProjectGuid,
                Sequence = Sequence.Transform(sequenceData.Sequence)
            };
        }
    }
}
