using System.Text;

namespace F23.PlistParser.Internal.Model.ObjectTableItems;

internal class StringItem : Item<string>
{
    private readonly string _value;
    
    public static StringItem Ascii(byte lengthNibble, IRandomAccessReader mmap, long offset)
    {
        var byteCount = ComputeLength(lengthNibble, mmap, ref offset);

        var bytes = mmap.ReadBytes(offset, byteCount);

        var value = Encoding.ASCII.GetString(bytes);

        return new StringItem(PlistObjectTypes.AsciiString, value);
    }

    public static StringItem Unicode(byte lengthNibble, IRandomAccessReader mmap, long offset)
    {
        var charLength = ComputeLength(lengthNibble, mmap, ref offset);

        var byteCount = charLength * 2;

        var bytes = mmap.ReadBytes(offset, byteCount);

        var value = Encoding.BigEndianUnicode.GetString(bytes);

        return new StringItem(PlistObjectTypes.UnicodeString, value);
    }

    private StringItem(PlistObjectTypes type, string value)
    {
        Type = type;
        _value = value;
    }

    public override PlistObjectTypes Type { get; }

    protected override string? GetValue() => _value;
}