namespace F23.PlistParser.Internal;

internal interface IRandomAccessReader : IDisposable
{
    long Length { get; }

    byte ReadByte(long offset);

    Span<byte> ReadBytes(long offset, int num);
}