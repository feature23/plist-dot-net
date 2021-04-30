using System.Collections.Generic;
using System.Linq;

namespace F23.PlistParser.Internal.Model.ObjectTableItems
{
    // Holds a list of offsets into the object table, which can be
    // resolved into IList<object> once the entire object
    // table has been read.
    internal class ArrayItem : Item<IList<object>>
    {
        public ArrayItem(byte lengthNibble, IRandomAccessReader mmap,
            long offset, byte objectRefSize, List<Item> objectTable)
        {
            var count = ComputeLength(lengthNibble, mmap, ref offset);
            var list = new List<long>(count);

            for (var i = 0; i < count; i++)
            {
                var offsetBytes = mmap.ReadBytes(offset + (i * objectRefSize), objectRefSize);

                var offsetIndex = ToInt64(offsetBytes);

                list.Add(offsetIndex);
            }

            Type = PlistObjectTypes.Array;
            ValueGetter = () =>
            {
                return list
                    .Select(item => objectTable[(int)item].Value)
                    .ToList();
            };
        }
    }
}
