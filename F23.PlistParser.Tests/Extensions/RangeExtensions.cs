namespace F23.PlistParser.Tests.Extensions;

internal static class RangeExtensions
{
    public static IEnumerator<int> GetEnumerator(this Range range)
    {
        int start = range.Start.Value;
        int end = range.End.Value;

        for (var i = start; i < end; i++)
        {
            yield return i;
        }
    }
}