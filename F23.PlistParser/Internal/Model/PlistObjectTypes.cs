namespace F23.PlistParser.Internal.Model
{
    internal enum PlistObjectTypes
    {
        Singleton = 0b0000,
        Integer = 0b0001,
        Real = 0b0010,
        Date = 0b0011,
        Data = 0b0100,
        AsciiString = 0b0101,
        UnicodeString = 0b0110,

        Uid = 0b1000,

        Array = 0b1010,
        Set = 0b1100,
        Dictionary = 0b1101
    }
}
