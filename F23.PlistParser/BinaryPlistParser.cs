using System;
using F23.PlistParser.Internal;
using F23.PlistParser.Internal.Model;

namespace F23.PlistParser
{
    public class BinaryPlistParser : IDisposable
    {
        private readonly IRandomAccessReader _reader;
        private bool _disposed;

        public BinaryPlistParser(string filePath)
        {
            _reader = new FileRandomAccessReader(filePath);
        }

        // JB - To support unit testing
        internal BinaryPlistParser(IRandomAccessReader reader)
        {
            _reader = reader;
        }

        public T ParseObject<T>() where T : new()
        {            
            var header = Header.Create(_reader);
            header.EnsureVersion("00");

            var objectTable = ObjectTable.Parse(_reader);

            return ObjectTableObjectMapper<T>.MapObject(objectTable);
        }

        private void Dispose(bool disposing)
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
    }
}
