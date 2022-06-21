namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Implementation.Shared.Data3d
{
    public interface IDataCompressor
    {
        (long, byte[]) Compress(byte[] data);
        byte[] Decompress(byte[] data, long knownLength);
    }
}