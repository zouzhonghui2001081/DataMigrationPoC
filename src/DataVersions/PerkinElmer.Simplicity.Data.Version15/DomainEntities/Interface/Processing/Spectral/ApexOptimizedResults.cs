using System.Collections.Generic;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Processing.Spectral
{
    public class ApexOptimizedResults
    {
        public (IList<double> TimeInSeconds, IList<double> Response) ChromatogramData { get; set; }
        public IList<double> WavelengthOfMaximum { get; set; }

        public ApexOptimizedResults()
        {
            ChromatogramData = (new double[0], new double[0]);
            WavelengthOfMaximum= new List<double>();
        }
    }
}
