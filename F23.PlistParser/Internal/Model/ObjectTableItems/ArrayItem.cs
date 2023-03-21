namespace F23.PlistParser.Internal.Model.ObjectTableItems;

// Holds a list of offsets into the object table, which can be
// resolved into IList<object> once the entire object
// table has been read.
internal class ArrayItem : Item<IList<object?>>
{
    private readonly IList<Item> _objectTable;
    private readonly IList<long> _list;
    
    public ArrayItem(byte lengthNibble, IRandomAccessReader mmap,
        long offset, byte objectRefSize, List<Item> objectTable)
    {
        _objectTable = objectTable;
        
        var count = ComputeLength(lengthNibble, mmap, ref offset);
        _list = new List<long>(count);

        for (var i = 0; i < count; i++)
        {
            var offsetBytes = mmap.ReadBytes(offset + (i * objectRefSize), objectRefSize);

            var offsetIndex = ToInt64(offsetBytes);

            _list.Add(offsetIndex);
        }

        // Type = PlistObjectTypes.Array;
        // ValueGetter = () =>
        // {
        //     return list
        //         .Select(item => objectTable[(int)item].Value)
        //         .ToList();
        // };
    }

    public override PlistObjectTypes Type => PlistObjectTypes.Array;

    protected override IList<object?>? GetValue()
    {
        return _list
            .Select(item => _objectTable[(int)item].Value)
            .ToList();
    }
}