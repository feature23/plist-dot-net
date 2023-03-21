using System.Buffers.Binary;

namespace F23.PlistParser.Internal.Model;

internal class OffsetTable
{
    private readonly long[] _offsets;

    public long this[int index] => _offsets[index];

    private OffsetTable(long[] offsets)
    {
        _offsets = offsets;
    }

    public static OffsetTable Create(IRandomAccessReader reader, Trailer trailer)
    {
        var offsetSize = trailer.OffsetTableOffsetSize;
        var table = new long[trailer.NumObjects];

        for (var i = 0; i < trailer.NumObjects; i++)
        {
            var start = trailer.OffsetTableItemStart(i);
            var offsetBytes = reader.ReadBytes(start, trailer.OffsetTableOffsetSize);

            table[i] = offsetSize switch
            {
                0x1 => offsetBytes[0],
                0x2 => BinaryPrimitives.ReadInt16BigEndian(offsetBytes),
                0x4 => BinaryPrimitives.ReadInt32BigEndian(offsetBytes),
                0x8 => BinaryPrimitives.ReadInt64BigEndian(offsetBytes),
                _ => throw new NotSupportedException($"Unsupported offset size: {trailer.OffsetTableOffsetSize}")
            };
        }

        return new OffsetTable(table);
    }
}