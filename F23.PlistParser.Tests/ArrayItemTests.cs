using F23.PlistParser.Internal;
using F23.PlistParser.Internal.Model.ObjectTableItems;
using F23.PlistParser.Tests.Extensions;
using F23.PlistParser.Tests.Support;

namespace F23.PlistParser.Tests;

public class ArrayItemTests : AbstractItemTest
{
    [Fact]
    public void ParseSingleItemArray_OneByteRefs()
    {
        // Arrange
        const byte objectRefSize = 0x1;
        const byte sizeNibble = 0x1;
        var objectTable = new List<Item> { BooleanItem.True };
        var reader = CreateArrayReader(objectTable, objectRefSize);

        // Act
        var item = new ArrayItem(
            sizeNibble,
            reader,
            offset: 0,
            objectRefSize,
            objectTable
        );
        var list = item.TypedValue();

        // Assert
        Assert.Equal(1, list.Count);
        Assert.IsType<bool>(list[0]);
    }

    [Fact]
    public void ParseMultiItemArray_OneByteRefs()
    {
        // Arrange
        const byte objectRefSize = 0x1;
        const byte sizeNibble = 0x1;

        var objectTable = new List<Item>();
        foreach (var _ in 0..8) objectTable.Add(BooleanItem.True);

        var reader = CreateArrayReader(objectTable, objectRefSize);

        // Act
        var item = new ArrayItem(
            sizeNibble,
            reader,
            offset: 0,
            objectRefSize,
            objectTable
        );
        var list = item.TypedValue();

        // Assert
        Assert.Equal(1, list.Count);
        Assert.IsType<bool>(list[0]);
    }

    [Fact]
    public void ParseSingleItemArray_TwoByteRefs()
    {
        // Arrange
        const byte objectRefSize = 0x2;
        const byte sizeNibble = 0x1;
        var objectTable = new List<Item> { BooleanItem.True };
        var reader = CreateArrayReader(objectTable, objectRefSize);

        // Act
        var item = new ArrayItem(
            sizeNibble,
            reader,
            offset: 0,
            objectRefSize,
            objectTable
        );
        var list = item.TypedValue();

        // Assert
        Assert.Equal(1, list.Count);
        Assert.IsType<bool>(list[0]);
    }

    [Fact]
    public void ParseSingleItemArray_FourByteRefs()
    {
        // Arrange
        const byte objectRefSize = 0x4;
        const byte sizeNibble = 0x1;
        var objectTable = new List<Item> { BooleanItem.True };
        var reader = CreateArrayReader(objectTable, objectRefSize);

        // Act
        var item = new ArrayItem(
            sizeNibble,
            reader,
            offset: 0,
            objectRefSize,
            objectTable
        );
        var list = item.TypedValue();

        // Assert
        Assert.Equal(1, list.Count);
        Assert.IsType<bool>(list[0]);
    }

    [Fact]
    public void ParseSingleItemArray_EightByteRefs()
    {
        // Arrange
        const byte objectRefSize = 0x8;
        const byte sizeNibble = 0x1;
        var objectTable = new List<Item> { BooleanItem.True };
        var reader = CreateArrayReader(objectTable, objectRefSize);

        // Act
        var item = new ArrayItem(
            sizeNibble,
            reader,
            offset: 0,
            objectRefSize,
            objectTable
        );
        var list = item.TypedValue();

        // Assert
        Assert.Equal(1, list.Count);
        Assert.IsType<bool>(list[0]);
    }

    private static IRandomAccessReader CreateArrayReader(List<Item> items, byte objectRefSize)
    {
        var count = items.Count;
        var bytes = new byte[objectRefSize * count];

        foreach (var i in 0..count)
        {
            var span = new Span<byte>(bytes).Slice(i * objectRefSize, objectRefSize);

            i.WriteBigEndian(span);
        }

        return new ByteBufferRandomAccessReader(bytes);
    }
}