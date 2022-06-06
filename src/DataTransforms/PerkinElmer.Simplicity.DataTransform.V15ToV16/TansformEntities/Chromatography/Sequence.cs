using System.Collections.Generic;
using Sequence15 = PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography.Sequence;
using Sequence16 = PerkinElmer.Simplicity.Data.Version16.Contract.DataEntities.Chromatography.Sequence;

namespace PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography
{
    public class Sequence
    {
        public static Sequence16 Transform(Sequence15 sequence)
        {
            if (sequence == null) return null;

            var sequence16 = new Sequence16
            {
                Id = sequence.Id,
                Name = sequence.Name,
                Guid = sequence.Guid,
                CreatedDate = sequence.CreatedDate,
                CreatedUserId = sequence.CreatedUserId,
                CreatedUserName = sequence.CreatedUserName,
                ModifiedDate = sequence.ModifiedDate,
                ModifiedUserId = sequence.ModifiedUserId,
                ModifiedUserName = sequence.ModifiedUserName,
                ProjectId = sequence.ProjectId
            };
            if (sequence.SequenceSampleInfos == null) return sequence16;

            sequence16.SequenceSampleInfos = new List<Data.Version16.Contract.DataEntities.Chromatography.SequenceSampleInfo>();
            foreach (var sequenceSampleInfo in sequence.SequenceSampleInfos)
                sequence16.SequenceSampleInfos.Add(SequenceSampleInfo.Transform(sequenceSampleInfo));
            return sequence16;
        }
    }
}
