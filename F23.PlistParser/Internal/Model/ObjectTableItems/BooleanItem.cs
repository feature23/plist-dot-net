using System;

namespace F23.PlistParser.Internal.Model.ObjectTableItems
{
    internal class BooleanItem : Item<bool>
    {
        public static bool IsBoolean(byte val) =>
            val == 0b1000 || val == 0b1001;

        // Helpers for unit testing
        internal static readonly BooleanItem False = new(0b1000);
        internal static readonly BooleanItem True = new(0b1001);

        public BooleanItem(byte val)
        {
            // Read value eagerly
            var value = val switch
            {
                0b1000 => false,
                0b1001 => true,
                _ => throw new ArgumentOutOfRangeException(nameof(val),
                    "Provided binary representation could not be parsed as boolean value.")
            };

            ValueGetter = () => value;
        }
    }
}
