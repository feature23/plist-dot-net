using System.Text;

namespace F23.PlistParser.Internal.Model;

internal class Header
{
    public string Magic { get; }

    public string Version { get; }

    public static Header Create(IRandomAccessReader reader)
    {
        var magicBytes = reader.ReadBytes(0, 6);
        var versionBytes = reader.ReadBytes(6, 2);

        return new Header(
            Encoding.ASCII.GetString(magicBytes),
            Encoding.ASCII.GetString(versionBytes)  
        );
    }

    private Header(string magic, string version)
    {
        Magic = magic;
        Version = version;
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