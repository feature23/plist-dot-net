using System;
using System.Text;

namespace F23.PlistParser.Internal.Model
{
    internal class Header
    {
        public string Magic { init; get; }

        public string Version { init; get; }

        public static Header Create(IRandomAccessReader reader)
        {
            var magicBytes = reader.ReadBytes(0, 6);
            var versionBytes = reader.ReadBytes(6, 2);

            return new Header
            {
                Magic = Encoding.ASCII.GetString(magicBytes),
                Version = Encoding.ASCII.GetString(versionBytes)
            };
        }

        public void EnsureVersion(string expectedVersion)
        {
            if (Magic != "bplist")
            {
                throw new InvalidOperationException("Provided data is not a binary plist.");
            }

            if (Version != expectedVersion)
            {
                throw new NotSupportedException($"Provided data is version {Version}. Currently only version 00 is supported.");
            }
        }
    }
}
