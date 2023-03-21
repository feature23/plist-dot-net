namespace F23.PlistParser.Internal.Model.ObjectTableItems;

internal class IntegerItem : Item<long>
{
    public IntegerItem(byte lengthExponent, IRandomAccessReader mmap, long offset)
    {
        var length = (int)Math.Pow(2, lengthExponent);

        var bytes = mmap.ReadBytes(offset, length);

        // Read value eagerly
        var value = ToInt64(bytes);

        Type = PlistObjectTypes.Integer;
        ValueGetter = () => value;
    }
}