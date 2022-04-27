using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Implementation.Shared.Data3d.Binary
{
    public class InMemoryBinaryLayer : IBinaryLayer, IDisposable
    {
        private readonly MemoryStream stream = new MemoryStream();

        public byte[] Read(long offset, int count)
        {
            stream.Seek(offset, SeekOrigin.Begin);
            using (var r = new BinaryReader(stream, new UTF8Encoding(), true))
            {
                return r.ReadBytes(count);
            }
        }

        public void Write(IEnumerable<(byte[], int)> dataList)
        {
            using (var writer = new BinaryWriter(stream, new UTF8Encoding(), true))
            {
                foreach (var (bytes, count) in dataList)
                {
                    writer.Write(bytes, 0, count);
                }

                writer.Flush();
            }
        }

        public void Write(byte[] data)
        {
            using (var writer = new BinaryWriter(stream, new UTF8Encoding(), true))
            {
                writer.Write(data);
                writer.Flush();
            }
        }

        public void Dispose()
        {
            stream.Dispose();
        }
    }
}