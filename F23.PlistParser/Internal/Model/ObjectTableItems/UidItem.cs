namespace F23.PlistParser.Internal.Model.ObjectTableItems;

internal class UidItem : Item<Guid>
{
    public UidItem(byte val)
    {
        throw new NotSupportedException("UID type is not used supported.");

        //Value = val switch
        //{
        //    _ => throw new ArgumentOutOfRangeException(nameof(val), ParseErrorMessage("GUID"))
        //};
    }

    public override PlistObjectTypes Type => PlistObjectTypes.Uid;

    protected override Guid GetValue()
    {
        throw new NotImplementedException();
    }
}