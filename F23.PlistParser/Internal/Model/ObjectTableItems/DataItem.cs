namespace F23.PlistParser.Internal.Model.ObjectTableItems
{
    // TODO.JB - Instead of wrapping a byte[], should this wrap
    // a delegate type that returns Span<byte>?
    internal class DataItem : Item<byte[]>
    {
        public DataItem(byte val, IRandomAccessReader mmap, long offset)
        {
            var length = ComputeLength(val, mmap, ref offset);

            Type = PlistObjectTypes.Data;
            ValueGetter = () => mmap.ReadBytes(offset, length).ToArray();
        }
    }
}
