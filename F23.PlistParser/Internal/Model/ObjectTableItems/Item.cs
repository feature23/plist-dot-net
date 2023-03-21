using F23.PlistParser.Internal.Extensions;

namespace F23.PlistParser.Internal.Model.ObjectTableItems;

internal abstract class Item
{
    public abstract PlistObjectTypes Type { get; }

    public abstract object? Value { get; }

    internal static Item Create(IRandomAccessReader mmapView,
        long offset, Trailer trailer, List<Item> objectTable)
    {
        var markerByte = mmapView.ReadByte(offset);
        var (msb, lsb) = markerByte.GetNibblesBigEndian();

        var type = (PlistObjectTypes)msb;
        var dataOffset = offset + 1;

        var objectRefSize = trailer.ObjectRefSize;

        return type switch
        {
            PlistObjectTypes.Singleton when lsb == 0b0 =>
                NullItem.Instance,

            PlistObjectTypes.Singleton when BooleanItem.IsBoolean(lsb) =>
                new BooleanItem(lsb),

            PlistObjectTypes.Integer =>
                new IntegerItem(lsb, mmapView, dataOffset),

            PlistObjectTypes.Real =>
                new RealItem(lsb, mmapView, dataOffset),

            PlistObjectTypes.Date =>
                new DateItem(mmapView, dataOffset),

            PlistObjectTypes.Data =>
                new DataItem(lsb, mmapView, dataOffset),

            PlistObjectTypes.AsciiString =>
                StringItem.Ascii(lsb, mmapView, dataOffset),

            PlistObjectTypes.UnicodeString =>
                StringItem.Unicode(lsb, mmapView, dataOffset),

            PlistObjectTypes.Dictionary =>
                new DictionaryItem(lsb, mmapView, dataOffset, objectRefSize, objectTable),

            PlistObjectTypes.Array =>
                new ArrayItem(lsb, mmapView, dataOffset, objectRefSize, objectTable),

            // TODO.JB - Set Type?

            _ => throw new NotSupportedException($"Unsupported type: {type}")
        };
    }
}