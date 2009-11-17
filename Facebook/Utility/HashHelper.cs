using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Facebook.Utility
{
    /// <summary>
    /// Utility used for creating Facebook hashes (for example, Facebook Connect email hashes).
    /// </summary>
    public class HashHelper
    {
        ///
        /// Based on solution proposed at:
        /// http://www.vbaccelerator.com/home/net/code/libraries/crc32/article.asp
        /// 
        
        private readonly UInt32[] crc32Table;
        private const int BUFFER_SIZE = 1024;
        
        /// <summary>
        /// Construct an instance of the CRC32 class, pre-initialising the table
        /// for speed of lookup.
        /// </summary>
        public HashHelper()
        {
            unchecked
            {
                // This is the official polynomial used by CRC32 in PKZip.
                // Often the polynomial is shown reversed as 0x04C11DB7.
                const uint dwPolynomial = 0xEDB88320;
                UInt32 i, j;

                crc32Table = new UInt32[256];

                for (i = 0; i < 256; i++)
                {
                    uint dwCrc = i;
                    for (j = 8; j > 0; j--)
                    {
                        if ((dwCrc & 1) == 1)
                        {
                            dwCrc = (dwCrc >> 1) ^ dwPolynomial;
                        }
                        else
                        {
                            dwCrc >>= 1;
                        }
                    }
                    crc32Table[i] = dwCrc;
                }
            }
        }

        /// <summary>
        /// Returns a 32bit Cyclic Redundancy Checksum (CRC) for the specified string.
        /// </summary>
        /// <param name="input">The string to calculate the CRC32 for</param>
        /// <returns>An unsigned integer containing the CRC32 calculation</returns>
        public UInt32 GetCrc32(string input)
        {
            unchecked
            {
                uint crc32Result = 0xFFFFFFFF;
                byte[] buffer = new byte[BUFFER_SIZE];
                const int readSize = BUFFER_SIZE;
                var stream = GetStream(input);

                var count = stream.Read(buffer, 0, readSize);
                while (count > 0)
                {
                    for (int i = 0; i < count; i++)
                    {
                        crc32Result = ((crc32Result) >> 8) ^ crc32Table[(buffer[i]) ^ ((crc32Result) & 0x000000FF)];
                    }
                    count = stream.Read(buffer, 0, readSize);
                }

                return ~crc32Result;
            }
        }
        
        private static Stream GetStream(string input)
        {
            var encoder = new UTF8Encoding();
            var bytes = encoder.GetBytes(input);
            return new MemoryStream(bytes);
        }
    }
}
