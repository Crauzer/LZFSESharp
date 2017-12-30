using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace LZFSESharp
{
    /// <summary>
    /// Provides functions for compressing and decompressing LZFSE data
    /// </summary>
    public static class LZFSE
    {
        [DllImport("lzfse.dll")]
        private static extern int lzfse_encode_buffer(byte[] compressedBuffer, int compressedSize, byte[] decompressedBuffer, int decompressedSize, int scratchBuffer = 0);

        [DllImport("lzfse.dll")]
        private static extern int lzfse_decode_buffer(byte[] decompressedBuffer, int decompressedSize, byte[] compressedBuffer, int compressedSize, int scratchBuffer = 0);

        /// <summary>
        /// Compresses <paramref name="buffer"/>
        /// </summary>
        /// <param name="buffer">The buffer to compress</param>
        public static byte[] Compress(byte[] buffer)
        {
            byte[] compressedBuffer = new byte[buffer.Length];
            int actualCompressedSize = lzfse_encode_buffer(compressedBuffer, buffer.Length, buffer, buffer.Length);

            if (actualCompressedSize == 0)
            {
                throw new Exception("There was an error compressing the specified buffer.");
            }

            return compressedBuffer.Take(actualCompressedSize).ToArray();
        }

        /// <summary>
        /// Decompresses a LZFSE compressed buffer
        /// </summary>
        /// <param name="buffer">The buffer to decompress</param>
        /// <param name="uncompressedSize">The uncompressed size of <paramref name="buffer"/></param>
        /// <param name="decompressionSizeFactor">If <paramref name="uncompressedSize"/> is not provided, this is used to determine the size of the output buffer</param>
        public static byte[] Decompress(byte[] buffer, int uncompressedSize = 0, int decompressionSizeFactor = 8)
        {
            int decompressedSize = uncompressedSize == 0 ? buffer.Length * decompressionSizeFactor : uncompressedSize;
            byte[] decompressedBuffer = new byte[decompressedSize];
            int actualDecompressedSize = lzfse_decode_buffer(decompressedBuffer, decompressedSize, buffer, buffer.Length);

            if (actualDecompressedSize == 0)
            {
                throw new Exception("There was an error decompressing the specified buffer.");
            }

            return decompressedBuffer.Take(actualDecompressedSize).ToArray();
        }
    }
}
