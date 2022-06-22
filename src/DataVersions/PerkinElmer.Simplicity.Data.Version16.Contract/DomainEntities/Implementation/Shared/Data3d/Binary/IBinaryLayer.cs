using System.Collections.Generic;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Implementation.Shared.Data3d.Binary
{
    public interface IBinaryLayer
    {
        byte[] Read(long offset, int count);
        void Write(IEnumerable<(byte[], int)> dataList);
    }
}