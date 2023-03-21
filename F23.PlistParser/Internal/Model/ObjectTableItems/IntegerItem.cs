namespace F23.PlistParser.Internal.Model.ObjectTableItems;

internal class IntegerItem : Item<long>
{
    private readonly long _value;
    
    public IntegerItem(byte lengthExponent, IRandomAccessReader mmap, long offset)
    {
        var length = (int)Math.Pow(2, lengthExponent);

        var bytes = mmap.ReadBytes(offset, length);

        // Read value eagerly
        _value = ToInt64(bytes);

        // Type = PlistObjectTypes.Integer;
        // ValueGetter = () => value;
    }

    public override PlistObjectTypes Type => PlistObjectTypes.Integer;

    protected override long GetValue() => _value;
}