namespace F23.PlistParser.Internal.Model.ObjectTableItems;

// Holds a dictionary of offsets into the object table, which can be
// resolved into IDictionary<string, object> once the entire object
// table has been read.
internal class DictionaryItem : Item<IDictionary<string, object?>>
{
    private readonly IList<Item> _objectTable;
    private readonly IDictionary<long, long> _dictionary;

    public DictionaryItem(byte val, IRandomAccessReader mmap,
        long dataOffsetStart, byte objectRefSize, List<Item> objectTable)
    {
        _objectTable = objectTable;
        
        var count = ComputeLength(val, mmap, ref dataOffsetStart);
        _dictionary = new Dictionary<long, long>(count);

        for (var i = 0; i < count; i++)
        {
            var keyPosition = dataOffsetStart + (i * objectRefSize);
            var valPosition = dataOffsetStart + ((count + i) * objectRefSize);

            var keyOffsetBytes = mmap.ReadBytes(keyPosition, objectRefSize);
            var valOffsetBytes = mmap.ReadBytes(valPosition, objectRefSize);

            var keyOffset = ToInt64(keyOffsetBytes);
            var valOffset = ToInt64(valOffsetBytes);

            _dictionary[keyOffset] = valOffset;
        }

        // Type = PlistObjectTypes.Dictionary;
        // ValueGetter = () =>
        // {
        //     return dictionary.ToDictionary(
        //         kvp => (string)objectTable[(int)kvp.Key].Value,
        //         kvp => objectTable[(int)kvp.Value].Value
        //     );
        // };
    }

    protected override IDictionary<string, object?>? GetValue()
    {
        return _dictionary.ToDictionary(
            kvp => (string)_objectTable[(int)kvp.Key].Value,
            kvp => _objectTable[(int)kvp.Value].Value
        );
    }

    public override PlistObjectTypes Type => PlistObjectTypes.Dictionary;
}