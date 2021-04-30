using System;
using System.Buffers.Binary;

namespace F23.PlistParser.Tests.Extensions
{
    internal static class IntegerExtensions
    {
        public static void WriteBigEndian(this int i, Span<byte> span)
        {
            switch (span.Length)
            {
                case 1:
                    span[0] = (byte)i;
                    break;
                case 2:
                    BinaryPrimitives.WriteInt16BigEndian(span, (short)i);
                    break;
                case 4:
                    BinaryPrimitives.WriteInt32BigEndian(span, i);
                    break;
                case 8:
                    BinaryPrimitives.WriteInt64BigEndian(span, i);
                    break;
                default:
                    throw new NotSupportedException($"Invalid length for integer type: {span.Length}");
            }
        }
    }
}
