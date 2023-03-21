using System.Reflection;
using F23.PlistParser.Tests.Models;
using F23.PlistParser.Tests.Support;

namespace F23.PlistParser.Tests;

public class ParserTests
{
    [Fact]
    public void Test_OneByteOffset_OneByteObjectRef()
    {
        // Arrange
        using var stream = LoadStream("com.apple.Commerce");
        using var reader = new MemoryStreamRandomAccessReader(stream);
        using var parser = new BinaryPlistParser(reader);

        // Act
        var plist = parser.ParseObject<CommercePlist>();

        // Assert
        Assert.NotNull(plist);

        Assert.True(plist.AutoUpdate);
        Assert.Equal("10.13", plist.AutoUpdateMajorOSVersion);
        Assert.Equal("10.13", plist.AutoUpdateRestartRequiredMajorOSVersion);
        Assert.False(plist.LastDoItLaterLogoutFailed);
        Assert.Equal(2, plist.LockedFilePaths.Count);

        Assert.Equal("/Applications/Xcode.app", plist.LockedFilePaths[0]);
        Assert.Equal("/Applications/iTunes.app", plist.LockedFilePaths[1]);
    }

    [Fact]
    public void Test_OneByteOffset_TwoByteObjectRef()
    {
        // Arrange
        using var stream = LoadStream("com.apple.SoftwareUpdate");
        using var reader = new MemoryStreamRandomAccessReader(stream);
        using var parser = new BinaryPlistParser(reader);

        // Act
        var plist = parser.ParseObject<SoftwareUpdatePlist>();

        // Assert
        Assert.NotNull(plist);

        Assert.True(plist.AutomaticallyInstallMacOSUpdates);
        Assert.Equal("11.2.3 (20D91)", plist.LastAttemptBuildVersion);
        Assert.Equal("11.2.3 (20D91)", plist.LastAttemptSystemVersion);

    }

    private static Stream LoadStream(string resourceName)
    {
        var asm = Assembly.GetExecutingAssembly();
        var fullResourceName = $"{asm.GetName().Name}.Resources.TestCases.{resourceName}.plist";

        return asm.GetManifestResourceStream(fullResourceName)
            ?? throw new InvalidOperationException($"Failed to load embedded resource: \"{fullResourceName}\"");
    }
}