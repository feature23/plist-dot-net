using F23.PlistParser.Internal;

namespace F23.PlistParser.Tests.Support;

public class MemoryStreamRandomAccessReader : IRandomAccessReader
{
    private readonly BinaryReader _reader;
    private bool _disposed;

    public MemoryStreamRandomAccessReader(Stream stream)
    {
        _reader = new BinaryReader(stream);
    }

    public long Length => _reader.BaseStream.Length;

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _reader.Dispose();
            }

            _disposed = true;
        }
    }

    public void Dispose()
    {
        // Do not change this code.
        // Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    public byte ReadByte(long offset)
    {
        lock (_reader)
        {
            _reader.BaseStream.Position = offset;
            return _reader.ReadByte();
        }
    }

    public Span<byte> ReadBytes(long offset, int num)
    {
        lock (_reader)
        {
            _reader.BaseStream.Position = offset;
            return _reader.ReadBytes(num);
        }
    }
}