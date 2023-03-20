using F23.PlistParser.Internal;

namespace F23.PlistParser.Tests.Support;

public class ByteBufferRandomAccessReader : IRandomAccessReader
{
    private readonly byte[] _buffer;

    public ByteBufferRandomAccessReader(byte @byte)
    {
        _buffer = new [] { @byte };
    }

    public ByteBufferRandomAccessReader(byte[] buffer)
    {
        _buffer = buffer;
    }

    public long Length => _buffer.Length;

    public byte ReadByte(long offset) => _buffer[offset];

    public Span<byte> ReadBytes(long offset, int num)
    {
        // TODO.JB - This cast will fail if offset > Int32.MaxValue
        return new Span<byte>(_buffer).Slice((int)offset, num);
    }
            

    protected virtual void Dispose(bool disposing) { } // No Op. Nothing to dispose.

    public void Dispose()
    {
        // Do not change this code.
        // Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}