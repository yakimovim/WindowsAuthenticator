using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace WindowsAuthenticator.Models
{
    public class Base32
    {
        private const string StandardBase32Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567";

        private readonly static Dictionary<char, uint> Index;

        static Base32()
        {
            Index = StandardBase32Alphabet.Select((c, i) => new { Char = Char.ToLowerInvariant(c), Index = i })
                .ToDictionary(o => o.Char, o => (uint)o.Index);
        }

        public static byte[] Decode(string input)
        {
            using (var ms = new MemoryStream(Math.Max((int)Math.Ceiling(input.Length * 5 / 8.0), 1)))
            {
                // take input eight bytes at a time to chunk it up for encoding
                for (int i = 0; i < input.Length; i += 8)
                {
                    int chars = Math.Min(input.Length - i, 8);
                    int bytes = (int)Math.Floor(chars * (5 / 8.0));
                    var val = GetValueForChars(input, chars, i, bytes);
                    byte[] buff = BitConverter.GetBytes(val);
                    Array.Reverse(buff);
                    ms.Write(buff, buff.Length - (bytes + 1), bytes);
                }
                return ms.ToArray();
            }
        }

        private static ulong GetValueForChars(string input, int chars, int index, int bytes)
        {
            ulong val = 0;
            for (int charOffset = 0; charOffset < chars; charOffset++)
            {
                uint cbyte;
                if (!Index.TryGetValue(Char.ToLowerInvariant(input[index + charOffset]), out cbyte))
                {
                    throw new ArgumentException("Invalid character '" + input.Substring(index + charOffset, 1) +
                                                "' in base32 string, valid characters are: " + StandardBase32Alphabet);
                }
                val |= (((ulong) cbyte) << ((((bytes + 1)*8) - (charOffset*5)) - 5));
            }
            return val;
        }
    }
}