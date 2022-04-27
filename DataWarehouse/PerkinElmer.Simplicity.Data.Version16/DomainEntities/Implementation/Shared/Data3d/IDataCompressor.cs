namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Implementation.Shared.Data3d
{
    public interface IDataCompressor
    {
        (long, byte[]) Compress(byte[] data);
        byte[] Decompress(byte[] data, long knownLength);
    }
}