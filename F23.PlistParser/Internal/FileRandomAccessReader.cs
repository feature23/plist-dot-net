using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using F23.PlistParser.Unsafe;

namespace F23.PlistParser.Internal
{
    internal class FileRandomAccessReader : IRandomAccessReader
    {
        private readonly MemoryMappedFile _mmap;
        private readonly MemoryMappedViewAccessor _accessor;
        private bool _disposed;

        public FileRandomAccessReader(string filePath)
        {
            var fs = File.OpenRead(filePath);

            Length = fs.Length;

            _mmap = MemoryMappedFile.CreateFromFile(
                fs,
                null,
                0,
                MemoryMappedFileAccess.Read,
                HandleInheritability.None,
                leaveOpen: false
            );

            _accessor = _mmap.CreateViewAccessor(0, Length, MemoryMappedFileAccess.Read);
        }

        public long Length { init; get; }

        public byte ReadByte(long offset) => _accessor.ReadByte(offset);

        public Span<byte> ReadBytes(long offset, int num) =>
             _accessor.ReadBytes(offset, num);

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _accessor.Dispose();
                    _mmap.Dispose();
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
    }
}
