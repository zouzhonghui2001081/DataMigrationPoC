using System.Collections.Generic;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Implementation.Shared.Data3d
{
    public interface I3DDataConverter
    {
        DataHeader Initialize(byte[] raw);
        IList<double> ConvertRow(IList<ushort> raw);
        IList<double> ConvertCol(IList<ushort> raw, int ind);
        int GetRawColIndex(int ind);
    }
}