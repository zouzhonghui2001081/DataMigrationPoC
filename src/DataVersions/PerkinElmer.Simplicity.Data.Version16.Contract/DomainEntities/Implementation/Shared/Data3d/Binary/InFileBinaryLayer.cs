using System;
using System.Collections.Generic;
using System.IO;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Implementation.Shared.Data3d.Binary
{
    public class InFileBinaryLayer : IBinaryLayer,IDisposable
    {
        private string _path;
        public InFileBinaryLayer(string fname)
        {
            var path = Path.GetDirectoryName(fname);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            _path = fname;
        }
        public byte[] Read(long offset, int count)
        {
            using (var fs = new FileStream(_path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                fs.Seek(offset, SeekOrigin.Begin);
                using (var r = new BinaryReader(fs))
                {
                    return r.ReadBytes(count);
                }
            }
        }

        public void Write(IEnumerable<(byte[], int)> dataList)
        {
            using (var fs = new FileStream(_path, FileMode.Create))
            {
                using (var writer = new BinaryWriter(fs))
                {
                    foreach (var data in dataList)
                    {
                        writer.Write(data.Item1,0,data.Item2);    
                    }
                    writer.Flush();
                }
            }
        }

        public void Write(byte[] data)
        {
            using (var fs = new FileStream(_path, FileMode.Create))
            {
                using (var writer = new BinaryWriter(fs))
                {
                    writer.Write(data);
                    writer.Flush();
                }
            }
        }

        public void Dispose()
        {
        }
    }
}