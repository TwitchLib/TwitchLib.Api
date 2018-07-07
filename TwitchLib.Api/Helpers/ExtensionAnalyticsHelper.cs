using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Api.Models.Helpers;

namespace TwitchLib.Api.Helpers
{
    public static class ExtensionAnalyticsHelper
    {
        public static async Task<List<ExtensionAnalytics>> HandleURLAsync(string url)
        {
            List<ExtensionAnalytics> results = new List<ExtensionAnalytics>();

            var cnts = await GetContentsAsync(url);
            var data = ExtractData(cnts);
            foreach (var line in data)
                results.Add(new ExtensionAnalytics(line));

            return results;
        }

        private static List<string> ExtractData(string[] cnts)
        {
            List<string> results = new List<string>();
            foreach (string line in cnts)
                if (line.Any(char.IsDigit))
                    results.Add(line);

            return results;
        }

        private static async Task<string[]> GetContentsAsync(string url)
        {
            var client = new HttpClient();
            string[] lines = (await client.GetStringAsync(url)).Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            return lines;
        }
    }
}
