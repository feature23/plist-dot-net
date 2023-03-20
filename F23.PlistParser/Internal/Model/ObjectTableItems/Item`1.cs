using System.Buffers.Binary;
using F23.PlistParser.Internal.Extensions;

namespace F23.PlistParser.Internal.Model.ObjectTableItems;

internal abstract class Item<T> : Item
{
    public Func<T> ValueGetter { init; get; }

    public override object Value => ValueGetter();

    public T TypedValue() => (T)Value;

    protected static int ComputeLength(byte lengthNibble, IRandomAccessReader mmap, ref long offset)
    {
        if (lengthNibble == 0b1111)
        {
            var nextByte = mmap.ReadByte(offset++);
            var (_, lsb) = nextByte.GetNibblesBigEndian();

            var sizeBytesLength = (int)Math.Pow(2, lsb);
            var sizeBytes = mmap.ReadBytes(offset, sizeBytesLength);

            offset += sizeBytesLength;

            // This Int64->Int32 cast will fail if data length is > 2GB.
            // If that's a problem, MemoryMappedViewAccessorExtensions
            // would need to be updated to read in 2GB windows. In any case,
            // that's too much to read into a heap-allocated .NET byte array,
            // so the method for reading binary/string items will need to be reworked.
            return (int)ToInt64(sizeBytes);
        }
        else
        {
            return lengthNibble;
        }
    }

    protected static long ToInt64(Span<byte> bytes)
    {
        return bytes.Length switch
        {
            1 => bytes[0],
            2 => BinaryPrimitives.ReadInt16BigEndian(bytes),
            4 => BinaryPrimitives.ReadInt32BigEndian(bytes),
            8 => BinaryPrimitives.ReadInt64BigEndian(bytes),
            _ => throw new InvalidOperationException(
                $"Invalid length for integer type: {bytes.Length} bytes.")
        };
    }
}