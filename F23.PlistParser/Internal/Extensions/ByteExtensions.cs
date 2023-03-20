namespace F23.PlistParser.Internal.Extensions;

internal static class ByteExtensions
{
    public static (byte msb, byte lsb) GetNibblesBigEndian(this byte @byte)
    {
        var lsb = (byte)(@byte & 0x0F);
        var msb = (byte)((@byte & 0xF0) >> 4);

        return (msb, lsb);
    }
}