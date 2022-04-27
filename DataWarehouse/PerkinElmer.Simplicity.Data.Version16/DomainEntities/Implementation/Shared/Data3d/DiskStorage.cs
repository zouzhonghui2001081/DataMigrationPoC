using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Implementation.Shared.Data3d
{
    public class DiskStorage : IData3DStorage, IDisposable
    {
        private DataHeader _header;
        private I3DDataConverter _converter;
        private readonly IDataCompressor _compressor;
        private readonly CompressedDataLayer _data = new CompressedDataLayer();
        private readonly CompressedDataLayer _dataT = new CompressedDataLayer();
        private readonly IDictionary<string, IList<double>> _calculatedColumnsCache = new Dictionary<string, IList<double>>();

        public DiskStorage(IDataCompressor compressor)
        {
            _compressor = compressor;
        }

        public void SetData(I3DDataConverter converter, byte[] data, Guid origBrGuid)
        {
            _converter = converter;
            _header = _converter.Initialize(data);

            var dataT = TransposeData(data);

            var (filePathForSpectra, filePathForChromatograms) = Data3dFileCacheUtil.GetFileNamesForBatchRun(origBrGuid);
            Parallel.Invoke(
                () => _data.Init(filePathForSpectra, data, _header.DataOffset, _header.Stride, _compressor),
                () => _dataT.Init(filePathForChromatograms, dataT, 0, _header.Rows * _header.ItemSize, _compressor)
            );
        }

        private byte[] TransposeData(byte[] data)
        {
            var dataT = new byte[data.Length - _header.DataOffset];
            for (var columnIndex = 0; columnIndex < Cols; columnIndex += 8)
            {
                Parallel.For(0, 8, threadIndex =>
                {
                    var columnIndexForThread = columnIndex + threadIndex;
                    if (columnIndexForThread >= Cols) return;
                    for (var rowIndex = 0; rowIndex < Rows; rowIndex++)
                    {
                        for (var byteIndex = 0; byteIndex < _header.ItemSize; byteIndex++)
                        {
                            dataT[(rowIndex + columnIndexForThread * Rows) * _header.ItemSize + byteIndex] =
                                data[(columnIndexForThread + rowIndex * Cols) * _header.ItemSize + byteIndex + _header.DataOffset];
                        }
                    }
                });
            }

            return dataT;
        }

        private int Rows => _header.Rows;

        private int Cols => _header.Cols;

        public IList<double> GetChromatogram(int index)
        {
            var rawIndex = _converter.GetRawColIndex(index);
            var raw = _dataT.GetRow(rawIndex);
            return _converter.ConvertCol(raw, rawIndex);
        }

        public IList<IList<double>> GetChromatograms(int[] indices)
        {
            var raw = _dataT.GetRows(indices);
            var ret = new IList<double>[indices.Length];
            for (int i = 0; i < indices.Length; i++)
            {
                ret[i] = _converter.ConvertCol(raw[i], indices[i]);
            }

            return ret;
        }

        public IList<double> GetMaximumIntensityChromatogram()
        {
            return GetCalculatedCol("max", r => r.Max());
        }

        public IList<double> GetSpectrum(int index)
        {
            return _converter.ConvertRow(_data.GetRow(index));
        }

        public IList<IList<double>> GetSpectra(int[] indices)
        {
            var raws = _data.GetRows(indices);
            var ret = new IList<double>[raws.Count];
            for (var i = 0; i < raws.Count; i++)
            {
                ret[i] = _converter.ConvertRow(raws[i]);
            }

            return ret;
        }

        public IList<double> GetAverageSpectrum(int startIndex, int endIndex)
        {
            var timeIndices = Enumerable.Range(startIndex, endIndex - startIndex + 1).ToArray();
            var spectra = GetSpectra(timeIndices);
            return SpectrumUtility.AverageSpectrum(spectra);
        }

        public void Reset()
        {
        }

        private IList<double> GetCalculatedCol(string key, Func<IList<double>, double> func)
        {
            if (!_calculatedColumnsCache.ContainsKey(key))
            {
                var col = new double[Rows];
                for (var i = 0; i < Rows; i++)
                {
                    col[i] = func(_converter.ConvertRow(_data.GetRow(i)));
                }

                _calculatedColumnsCache[key] = col;
                return col;
            }

            return _calculatedColumnsCache[key];
        }
        
        public void Dispose()
        {
            _data.Dispose();
            _dataT.Dispose();
        }
    }
}