// using K4os.Compression.LZ4;
//
// namespace PerkinElmer.Domain.Implementation.Shared.Data3d
// {
//   public class K4OsDataCompressor : IDataCompressor
//   {
//     public (long,byte[]) Compress(byte[] data)
//     {
//       var target = new byte[LZ4Codec.MaximumOutputSize(data.Length)];
//       int len = LZ4Codec.Encode(data,0, data.Length, target, 0, target.Length);
//       return (len, target);
//     }
//
//     public byte[] Decompress(byte[] data, long knownLenth)
//     {
//       var ret = new byte[knownLenth];
//       var decoded = LZ4Codec.Decode(data, 0, data.Length, ret, 0, (int)knownLenth);
//       return ret;
//     }
//   }
// }
