using F23.PlistParser.Internal.Model.ObjectTableItems;

namespace F23.PlistParser.Tests;

public class DateItemTests : AbstractItemTest
{
    [Fact]
    public void ParseEpochDate()
    {
        // Arrange
        var reader = CreateReader(CFDateReferenceEpoch);

        // Act
        var item = new DateItem(reader, 0);
        DateTime value = item.TypedValue();

        // Assert
        Assert.Equal(CFDateReferenceEpoch, value);
    }

    [Fact]
    public void ParseArbitraryDate()
    {
        // Arrange
        const double seconds = 525600.0;
        var expectedDate = CFDateReferenceEpoch.AddSeconds(seconds);

        var reader = CreateReader(expectedDate);

        // Act
        var item = new DateItem(reader, 0);
        DateTime value = item.TypedValue();

        // Assert
        Assert.Equal(expectedDate, value);
    }
}