using F23.PlistParser.Internal.Model.ObjectTableItems;

namespace F23.PlistParser.Internal.Model;

internal class ObjectTable
{
    public static ObjectTable Parse(IRandomAccessReader reader)
    {
        var trailer = Trailer.Create(reader);
        var offsetTable = OffsetTable.Create(reader, trailer);

        var items = new List<Item>((int)Math.Min(trailer.NumObjects, int.MaxValue));

        for (var i = 0; i < trailer.NumObjects; i++)
        {
            var offset = offsetTable[i];

            var item = Item.Create(
                reader,
                offset,
                trailer,
                items
            );

            items.Add(item);
        }

        return new ObjectTable(items);
    }

    public IList<Item> Items { get; }

    public PlistObjectTypes TopLevelObjectType => Items[0].Type;

    public bool TopLevelIsArray =>
        TopLevelObjectType == PlistObjectTypes.Array;

    public bool TopLevelIsDictionary =>
        TopLevelObjectType == PlistObjectTypes.Dictionary;

    public IList<object?> TopLevelArray =>
        Items[0] is ArrayItem arrayItem
            ? arrayItem.TypedValue() ?? throw new InvalidOperationException("Failed to load typed value for ArrayItem")
            : throw new InvalidOperationException("Top-level object is not an array.");

    public IDictionary<string, object?> TopLevelDictionary =>
        Items[0] is DictionaryItem dictionaryItem
            ? dictionaryItem.TypedValue() ?? throw new InvalidOperationException("Failed to load typed value for DictionaryItem")
            : throw new InvalidOperationException("Top-level object is not a dictionary.");

    private ObjectTable(IList<Item> items)
    {
        Items = items;
    }      
}