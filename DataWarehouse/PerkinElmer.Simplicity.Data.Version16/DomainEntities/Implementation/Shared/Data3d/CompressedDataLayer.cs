using System;
using System.Collections.Generic;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Implementation.Shared.Data3d.Binary;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Implementation.Shared.Data3d
{
    public class CompressedDataLayer : IDisposable
    {
        private IDataCompressor _compressor;
        private string _fName;
        private int _stride;
        private int _packCount = 1;
        private readonly List<(long, int, bool)> _blocksDirectory = new List<(long, int, bool)>();
        private IBinaryLayer _dataLayer;

        public int Rows { get; private set; }

        private (int ind, byte[] block) _cache = (-1, new byte[0]);

        private byte[] ReadBlock(int ind)
        {
            if (_cache.ind == ind)
                return _cache.block;

            var (offset, size, compressed) = _blocksDirectory[ind];
            var raw = _dataLayer.Read(offset, size);
            var asByte = compressed ? _compressor.Decompress(raw, _stride * _packCount) : raw;
            _cache = (ind, asByte);
            return asByte;
        }

        public IList<ushort> GetRow(int ind)
        {
            var asByte = ReadBlock(ind / _packCount);
            var ret = new ushort[_stride / 2];
            Buffer.BlockCopy(asByte, (ind % _packCount) * _stride, ret, 0, _stride);
            return ret;
        }

        public IList<IList<ushort>> GetRows(int[] indices)
        {
            IList<IList<ushort>> ret = new IList<ushort>[indices.Length];
            for (int i = 0; i < indices.Length; i++)
                ret[i] = GetRow(indices[i]);
            return ret;
        }

        public void Init(string fName, byte[] data, int dataOffset, int stride, IDataCompressor compressor)
        {
            _packCount = 1;
            while (_packCount * stride < 64000)
                _packCount <<= 1;
            
            _stride = stride;
            _fName = fName;
            _compressor = compressor;
            Rows = (data.Length - dataOffset) / stride;
            _dataLayer = BinaryLayerFactory.Create(_fName, data.Length);
            _dataLayer.Write(ToBlocks(data, dataOffset));
        }

        private IEnumerable<(byte[], int)> ToBlocks(byte[] data, int dataOffset)
        {
            var fullBuffer = new byte[_stride * _packCount];
            long offset = 0;
            for (var i = 0; i < Rows; i += _packCount)
            {
                var packs = Math.Min(Rows - i, _packCount);
                var target = packs == _packCount ? fullBuffer : new byte[_stride * packs];
                Buffer.BlockCopy(data, dataOffset + _stride * i, target, 0, packs * _stride);
                var (len, compressed) = _compressor.Compress(target);
                if (len < target.Length * 0.95)
                {
                    _blocksDirectory.Add((offset, (int) len, true));
                    offset += len;
                    yield return (compressed, (int) len);
                }
                else
                {
                    _blocksDirectory.Add((offset, target.Length, false));
                    offset += target.Length;
                    yield return (target, target.Length);
                }
            }
        }

        public void Dispose()
        {
            (_dataLayer as IDisposable)?.Dispose();
        }
    }
}