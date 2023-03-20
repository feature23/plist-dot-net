using F23.PlistParser.Internal.Model.ObjectTableItems;

namespace F23.PlistParser.Tests;

public class IntegerItemTests : AbstractItemTest
{
    [Fact]
    public void ParseInt32Zero()
    {
        // Arrange
        var reader = CreateReader(0);

        // Act
        var item = new IntegerItem(0b0, reader, 0);
        var value = item.TypedValue();

        // Assert
        Assert.Equal(0, value);
    }

    [Fact]
    public void ParseInt64Zero()
    {
        // Arrange
        var reader = CreateReader(0L);

        // Act
        var item = new IntegerItem(0b0, reader, 0);
        var value = item.TypedValue();

        // Assert
        Assert.Equal(0L, value);
    }
}