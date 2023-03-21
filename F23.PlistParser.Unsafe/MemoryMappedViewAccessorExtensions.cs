using System.IO.MemoryMappedFiles;

namespace F23.PlistParser.Unsafe;

public static class MemoryMappedViewAccessorExtensions
{
    public static unsafe Span<byte> ReadBytes(this MemoryMappedViewAccessor
        accessor, long offset, int num)
    {
        lock (accessor)
        {
            var handle = accessor.SafeMemoryMappedViewHandle;

            try
            {
                byte* mmapPtr = (byte*)0;
                handle.AcquirePointer(ref mmapPtr);
                return new Span<byte>(mmapPtr + offset, num);
            }
            finally
            {
                handle.ReleasePointer();
            }
        }
    }
}