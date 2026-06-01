using Umbraco.Community.SimpleDashboards.TestSite.Models;
using Microsoft.Extensions.Caching.Memory;

namespace Umbraco.Community.SimpleDashboards.TestSite.Helpers;

public interface ISavedFormGenerator
{
    Task<List<SavedForm>> GenerateAsync(int count = 8);
}

public class SavedFormGenerator(IHttpClientFactory httpFactory, IMemoryCache cache) : ISavedFormGenerator
{
    public async Task<List<SavedForm>> GenerateAsync(int count = 8)
    {
        var rnd = new Random();

        var subjects = new[]
        {
            "Feature request",
            "Bug report",
            "Support needed",
            "Question about configuration",
            "UI suggestion",
            "Performance issue",
            "Integration request",
            "Documentation update"
        };

        var baseUrl = new Uri("https://lorem-api.com/api/lorem");

        var list = new List<SavedForm>(count);

        for (var i = 0; i < count; i++)
        {
            var id = i + 100;
            var subj = $"{subjects[rnd.Next(subjects.Length)]} (#{id})";
            string body;
            var perFormUrl = new UriBuilder(baseUrl)
            {
                Query = $"paragraphs={rnd.Next(1, 4)}&seed={id}"
            }.Uri;

            try
            {
                body = await cache.GetOrCreateAsync(perFormUrl, async entry =>
                {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60);
                    var client = httpFactory.CreateClient();
                    client.Timeout = TimeSpan.FromSeconds(5);
                    return await client.GetStringAsync(perFormUrl);
                }) ?? string.Empty;
            }
            catch
            {
                body = string.Empty;
            }

            list.Add(new SavedForm(subj, body));
        }

        return list;
    }
}
