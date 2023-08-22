using System.Collections.Concurrent;
using Microsoft.Extensions.Options;
using Umbraco.Cms.Core.Configuration.Models;
using Umbraco.Cms.Core.Hosting;
using Umbraco.Extensions;

namespace Umbraco.Community.SimpleDashboards.Core;

public class SimpleDashboardService : ISimpleDashboardService
{
    private readonly ConcurrentDictionary<string, ISimpleDashboard> _simpleDashboards;

    public SimpleDashboardService(SimpleDashboardCollection simpleDashboards, IOptions<GlobalSettings> options, IHostingEnvironment hostingEnvironment)
    {
        _simpleDashboards = new ConcurrentDictionary<string, ISimpleDashboard>();
        var umbracoArea = options.Value.GetUmbracoMvcArea(hostingEnvironment);

        foreach (var simpleDashboard in simpleDashboards)
        {
            var url = CreateUrl(simpleDashboard.Alias!, umbracoArea);
            simpleDashboard.SetView(url);
            _simpleDashboards.TryAdd(simpleDashboard.Alias!, simpleDashboard);
        }
    }

    public ISimpleDashboard? GetDashboard(string alias)
    {
        return _simpleDashboards.TryGetValue(alias, out var dashboard) ? dashboard : null;
    }

    public IEnumerable<ISimpleDashboard>? Get()
    {
        return _simpleDashboards.Values;
    }

    private string CreateUrl(string alias, string umbracoArea = "umbraco")
    {
        return $"/{umbracoArea}/{Cms.Core.Constants.Web.Mvc.BackOfficePathSegment}/{Constants.Area}/render/{alias}";
    }
}
