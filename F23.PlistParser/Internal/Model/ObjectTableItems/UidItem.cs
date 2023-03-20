namespace F23.PlistParser.Internal.Model.ObjectTableItems;

internal class UidItem : Item<Guid>
{
    public UidItem(byte val)
    {
        Type = PlistObjectTypes.Uid;

        throw new NotSupportedException("UID typs is not used supported.");

        //Value = val switch
        //{
        //    _ => throw new ArgumentOutOfRangeException(nameof(val), ParseErrorMessage("GUID"))
        //};
    }
}