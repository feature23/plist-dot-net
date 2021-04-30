using System.Text;
using F23.PlistParser.Internal.Model.ObjectTableItems;
using Xunit;

namespace F23.PlistParser.Tests
{
    public class StringItemTests : AbstractItemTest
    {
        [Fact]
        public void ParseShortAsciiString()
        {
            // Arrange
            const string expected = "Hello";
            var reader = CreateReader(expected, Encoding.ASCII);
            byte lengthNibble = (byte)expected.Length;

            // Act
            var item = StringItem.Ascii(lengthNibble, reader, 0);
            var str = item.TypedValue();

            // Assert
            Assert.Equal(expected, str);
        }

        [Fact]
        public void ParseShortUnicodeString()
        {
            // Arrange
            const string expected = "Hello";
            var reader = CreateReader(expected, Encoding.BigEndianUnicode);
            byte lengthNibble = (byte)expected.Length;

            // Act
            var item = StringItem.Unicode(lengthNibble, reader, 0);
            var str = item.TypedValue();

            // Assert
            Assert.Equal(expected, str);
        }
    }
}
