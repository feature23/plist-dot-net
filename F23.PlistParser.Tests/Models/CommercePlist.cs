namespace F23.PlistParser.Tests.Models;

public class CommercePlist
{
    public bool AutoUpdate { get; set; }

    public string AutoUpdateMajorOSVersion { get; set; }

    public string AutoUpdateRestartRequiredMajorOSVersion { get; set; }

    public bool LastDoItLaterLogoutFailed { get; set; }

    public IList<string> LockedFilePaths { get; set; }

    // TODO.JB - MajorOSUpdate (dictionary, but unsure of structure)

    /*
     * ========================================================
     * Plist in domain: /Library/Preferences/com.apple.commerce
     * ========================================================
     * 
     * {
     *     AutoUpdate = 1;
     *     AutoUpdateMajorOSVersion = "10.13";
     *     AutoUpdateRestartRequiredMajorOSVersion = "10.13";
     *     LastDoItLaterLogoutFailed = 0;
     *     LockedFilePaths = (
     *         "/Applications/Xcode.app",
     *         "/Applications/iTunes.app"
     *     );
     *     MajorOSUpdate = {
     *     };
     * }
     * 
     */
}