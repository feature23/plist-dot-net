using System.Buffers.Binary;

namespace F23.PlistParser.Internal.Model.ObjectTableItems;

internal class RealItem : Item<double>
{
    public RealItem(byte lengthExponent, IRandomAccessReader mmap, long offset)
    {
        var length = (int)Math.Pow(2, lengthExponent);

        var bytes = mmap.ReadBytes(offset, length);

        // Read value eagerly
        var value = length switch
        {
            4 => BinaryPrimitives.ReadSingleBigEndian(bytes),
            8 => BinaryPrimitives.ReadDoubleBigEndian(bytes),
            _ => throw new InvalidOperationException(
                $"Invalid length for floating-point type: {length} bytes.")
        };

        Type = PlistObjectTypes.Real;
        ValueGetter = () => value;
    }
}