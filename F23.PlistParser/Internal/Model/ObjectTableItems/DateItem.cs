using System.Buffers.Binary;

namespace F23.PlistParser.Internal.Model.ObjectTableItems;

internal class DateItem : Item<DateTime>
{
    private static readonly DateTime CFDateReferenceEpoch =
        new(2001, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    public DateItem(IRandomAccessReader mmap, long offset)
    {
        var bytes = mmap.ReadBytes(offset, 8);
        var secondsSinceEpoch = BinaryPrimitives.ReadDoubleBigEndian(bytes);

        // Read value eagerly
        var value = CFDateReferenceEpoch.AddSeconds(secondsSinceEpoch);

        Type = PlistObjectTypes.Date;
        ValueGetter = () => value;
    }
}