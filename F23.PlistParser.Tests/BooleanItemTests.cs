using F23.PlistParser.Internal.Model.ObjectTableItems;

namespace F23.PlistParser.Tests;

public class BooleanItemTests
{
    [Fact]
    public void ParseTrue()
    {
        // Arrange
        const byte nibble = 0b1000;

        // Act
        var item = new BooleanItem(nibble);

        // Assert
        Assert.False(item.TypedValue());
    }

    [Fact]
    public void ParseFalse()
    {
        // Arrange
        const byte nibble = 0b1001;

        // Act
        var item = new BooleanItem(nibble);

        // Assert
        Assert.True(item.TypedValue());
    }

    [Fact]
    public void ParseInvalidThrows()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            var item = new BooleanItem(0b1111);
        });
    }
}