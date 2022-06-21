using System;
using System.Collections.Generic;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Implementation.Shared.Data3d
{
	public class Data3d : IData3d
	{
		private IData3DStorage _storage;

		public IList<double> Times { get; set; }
		public IList<double> Wavelengths { get; set; }

		public IList<double> GetSpectrum(int timeIndex)
		{
			return _storage.GetSpectrum(timeIndex);;
		}

		public IList<double> GetChromatogram(int wlIndex)
		{
			return _storage.GetChromatogram(wlIndex);
		}

		public IList<IList<double>> GetSpectra(int[] timeIndices)
		{
			return _storage.GetSpectra(timeIndices);
		}

		public IList<IList<double>> GetChromatograms(int[] wlIndices)
		{
			return _storage.GetChromatograms(wlIndices);
		}

        public IList<double> GetAverageSpectrum(int startIndex, int endIndex)
        {
			return _storage.GetAverageSpectrum(startIndex, endIndex);
		}

        public IList<double> GetMaximumIntensityChromatogram()
		{
			return _storage.GetMaximumIntensityChromatogram();
		}

		public bool IsValid()
		{
			return Times != null && Wavelengths != null;
		}

		public void InitWith(IList<double> times, IList<double> wavelengths, IList<IList<double>> intensities)
		{
			Times = times;
			Wavelengths = wavelengths;
			_storage = new SimpleData3DStorage(intensities);
		}

        public void InitWith(I3DDataConverter conv, byte[] raw, Guid origBrGuid)
		{
			var diskStorage = new DiskStorage(new SystemDataCompressorImpl());
			diskStorage.SetData(conv, raw, origBrGuid);
			_storage = new SpectraCache3DStorage(diskStorage);
		}

        public void Reset()
        {
            _storage.Reset();
        }
	}
}