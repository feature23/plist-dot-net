namespace F23.PlistParser.Tests.Models;

public class SoftwareUpdatePlist
{
    public bool AutomaticallyInstallMacOSUpdates { get; set; }

    public string LastAttemptBuildVersion { get; set; }

    public string LastAttemptSystemVersion { get; set; }

    public DateTime LastBackgroundSuccessfulDate { get; set; }

    public DateTime LastFullSuccessfulDate { get; set; }

    public string LastRecommendedMajorOSBundleIdentifier { get; set; }

    public long LastRecommendedUpdatesAvailable { get; set; }

    public long LastResultCode { get; set; }

    public bool LastSessionSuccessful { get; set; }

    public DateTime LastSuccessfulDate { get; set; }

    public long LastUpdatesAvailable { get; set; }

    public IList<string> PrimaryLanguages { get; set; }

    public IList<RecommendedUpdate> RecommendedUpdates { get; set; }

    public bool SkipLocalCDN { get; set; }

    public class RecommendedUpdate
    {
        public string DisplayName { get; set; }

        public string DisplayVersion { get; set; }

        public string Identifier { get; set; }

        public string ProductKey { get; set; }
    }

    /*
     * ==============================================================
     * Plist in domain: /Library/Preferences/com.apple.SoftwareUpdate
     * ==============================================================
     * 
     * {
     *      AutomaticallyInstallMacOSUpdates = 1;
     *      LastAttemptBuildVersion = "10.15.7 (19H114)";
     *      LastAttemptSystemVersion = "10.15.7 (19H114)";
     *      LastBackgroundSuccessfulDate = "2021-04-07 18:39:46 +0000";
     *      LastFullSuccessfulDate = "2021-04-08 15:46:36 +0000";
     *      LastRecommendedMajorOSBundleIdentifier = "com.apple.InstallAssistant.macOSBigSur";
     *      LastRecommendedUpdatesAvailable = 3;
     *      LastResultCode = 0;
     *      LastSessionSuccessful = 1;
     *      LastSuccessfulDate = "2021-04-08 15:46:36 +0000";
     *      LastUpdatesAvailable = 3;
     *      PrimaryLanguages =     (
     *          "en-US",
     *          en
     *      );
     *      RecommendedUpdates =     (
     *          {
     *              "Display Name" = "macOS Catalina 10.15.7 Supplemental Update";
     *              "Display Version" = "10.15.7";
     *              Identifier = "macOS Catalina 10.15.7 Supplemental Update";
     *              "Product Key" = "071-05425";
     *          },
     *          {
     *              "Display Name" = "macOS Catalina Security Update 2021-001";
     *              "Display Version" = "10.15.7";
     *              Identifier = "macOS Catalina Security Update 2021-001";
     *              "Product Key" = "001-93719";
     *          },
     *          {
     *              "Display Name" = Safari;
     *              "Display Version" = "14.0.3";
     *              Identifier = "Safari14.0.3CatalinaAuto";
     *              "Product Key" = "071-10831";
     *          }
     *     );
     *     SkipLocalCDN = 0;
     * }
     * 
     */
}