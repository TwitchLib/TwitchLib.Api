using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TwitchLib.Api.Helix.Models.Helpers;

namespace TwitchLib.Api.Helpers;

/// <summary>
/// Extension Analytics Helper
/// </summary>
public static class ExtensionAnalyticsHelper
{
    public static async Task<List<ExtensionAnalytics>> HandleUrlAsync(string url)
    {
        var cnts = await GetContentsAsync(url).ConfigureAwait(false);
        var data = ExtractData(cnts);

        return data.Select(line => new ExtensionAnalytics(line)).ToList();
    }

    private static IEnumerable<string> ExtractData(IEnumerable<string> cnts)
    {
        return cnts.Where(line => line.Any(char.IsDigit)).ToList();
    }

    private static async Task<string[]> GetContentsAsync(string url)
    {
        var client = new HttpClient();

        var str = await client.GetStringAsync(url).ConfigureAwait(false);
        var lines = str
#if NET
            .Split(Environment.NewLine, StringSplitOptions.None);
#else
            .Split(new[] { Environment.NewLine }, StringSplitOptions.None);
#endif
        return lines;
    }
}
