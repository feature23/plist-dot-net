namespace F23.PlistParser.Internal.Model.ObjectTableItems;

// TODO.JB - Instead of wrapping a byte[], should this wrap
// a delegate type that returns Span<byte>?
internal class DataItem : Item<byte[]>
{
    private readonly IRandomAccessReader _mmap;
    private readonly long _offset;
    private readonly int _length;
    
    public DataItem(byte val, IRandomAccessReader mmap, long offset)
    {
        _mmap = mmap;
        _offset = offset;
        _length = ComputeLength(val, mmap, ref offset);

        // Type = PlistObjectTypes.Data;
        // ValueGetter = () => mmap.ReadBytes(offset, length).ToArray();
    }

    public override PlistObjectTypes Type => PlistObjectTypes.Data;

    protected override byte[]? GetValue() =>
        _mmap.ReadBytes(_offset, _length).ToArray();
}