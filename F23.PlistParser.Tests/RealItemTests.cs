using F23.PlistParser.Internal.Model.ObjectTableItems;

namespace F23.PlistParser.Tests;

public class RealItemTests : AbstractItemTest
{
    [Fact]
    public void ParseSinglePrecisionZero()
    {
        // Arrange
        var reader = CreateReader(0.0f);

        // Act
        var item = new RealItem(0x2, reader, 0);
        var value = item.TypedValue();

        // Assert
        Assert.Equal(0, value);
    }

    [Fact]
    public void ParseDoublePrecisionZero()
    {
        // Arrange
        var reader = CreateReader(0.0);

        // Act
        var item = new RealItem(0x3, reader, 0);
        var value = item.TypedValue();

        // Assert
        Assert.Equal(0, value);
    }
}