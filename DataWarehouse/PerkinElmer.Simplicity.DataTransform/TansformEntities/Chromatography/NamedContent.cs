using NamedContent15 = PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography.NamedContent;
using NamedContent16 = PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography.NamedContent;

namespace PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography
{
    public class NamedContent
    {
        public static NamedContent16 Transform(NamedContent15 namedContent)
        {
            if (namedContent == null) return null;

            return new NamedContent16
            {
                Id = namedContent.Id,
                BatchRunId = namedContent.BatchRunId,
                Key = namedContent.Key,
                Value = namedContent.Value
            };
        }
    }
}
