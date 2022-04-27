using System;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Processing
{
    public class SelectedPeakChangedEventArgs : EventArgs
    {
	    public Guid BatchRunChannelGuid { get; set; }
		public string PeakName { get; set; }
        public int ChannelIndex { get; set; }
        public string ChannelName { get; set; }
        public int PeakResultIndex { get; set; }
        public bool NoSelection { get; set; }
    }
}
