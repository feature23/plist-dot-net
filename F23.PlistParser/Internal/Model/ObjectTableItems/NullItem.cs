namespace F23.PlistParser.Internal.Model.ObjectTableItems;

internal class NullItem : Item
{
    public static readonly Item Instance = new NullItem();
    
    private NullItem() { }

    public override PlistObjectTypes Type => 
        throw new NotSupportedException("PlistObjectTypes not supported for NullItem");

    public override object? Value => null;
}