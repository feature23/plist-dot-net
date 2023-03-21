using System.Buffers.Binary;

namespace F23.PlistParser.Internal.Model;

internal class Trailer
{
    public byte SortVersion { init; get; }

    public byte OffsetTableOffsetSize { init; get; }

    public byte ObjectRefSize { init; get; }

    public long NumObjects { init; get; }

    public long TopObjectOffset { init; get; }

    public long OffsetTableStart { init; get; }

    public long OffsetTableLength => NumObjects * OffsetTableOffsetSize;

    public long OffsetTableItemStart(long itemIndex)
    {
        return OffsetTableStart + (itemIndex * OffsetTableOffsetSize);
    }

    public static Trailer Create(IRandomAccessReader reader)
    {
        var b = reader.ReadBytes(reader.Length - 32, 32);

        return new Trailer
        {
            SortVersion = b.Slice(5, 1)[0],
            OffsetTableOffsetSize = b.Slice(6, 1)[0],
            ObjectRefSize = b.Slice(7, 1)[0],
            NumObjects = BinaryPrimitives.ReadInt64BigEndian(b.Slice(8, 8)),
            TopObjectOffset = BinaryPrimitives.ReadInt64BigEndian(b.Slice(16, 8)),
            OffsetTableStart = BinaryPrimitives.ReadInt64BigEndian(b.Slice(24, 8))
        };
    }
}