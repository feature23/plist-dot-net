using System.Buffers.Binary;
using System.Text;
using F23.PlistParser.Tests.Support;

namespace F23.PlistParser.Tests;

public abstract class AbstractItemTest
{
    protected static readonly DateTime CFDateReferenceEpoch =
        new(2001, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    protected static ByteBufferRandomAccessReader CreateReader(int value)
    {
        var bytes = new byte[4];
        BinaryPrimitives.WriteInt32BigEndian(bytes, value);
        return new ByteBufferRandomAccessReader(bytes);
    }

    protected static ByteBufferRandomAccessReader CreateReader(long value)
    {
        var bytes = new byte[8];
        BinaryPrimitives.WriteInt64BigEndian(bytes, value);
        return new ByteBufferRandomAccessReader(bytes);
    }

    protected static ByteBufferRandomAccessReader CreateReader(float value)
    {
        var bytes = new byte[4];
        BinaryPrimitives.WriteSingleBigEndian(bytes, value);
        return new ByteBufferRandomAccessReader(bytes);
    }

    protected static ByteBufferRandomAccessReader CreateReader(double value)
    {
        var bytes = new byte[8];
        BinaryPrimitives.WriteDoubleBigEndian(bytes, value);
        return new ByteBufferRandomAccessReader(bytes);
    }

    protected static ByteBufferRandomAccessReader CreateReader(DateTime value)
    {            
        var secondsSinceEpoch = (value - CFDateReferenceEpoch).TotalSeconds;

        var bytes = new byte[8];
        BinaryPrimitives.WriteDoubleBigEndian(bytes, secondsSinceEpoch);
        return new ByteBufferRandomAccessReader(bytes);
    }

    protected static ByteBufferRandomAccessReader CreateReader(string value, Encoding encoding)
    {
        var bytes = encoding.GetBytes(value);

        return new ByteBufferRandomAccessReader(bytes);
    }
}