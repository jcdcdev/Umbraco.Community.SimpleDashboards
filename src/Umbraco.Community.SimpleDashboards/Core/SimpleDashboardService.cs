using System.Collections.Concurrent;
using Humanizer;
using Microsoft.Extensions.Logging;
using Umbraco.Community.SimpleDashboards.Core.Models;

namespace Umbraco.Community.SimpleDashboards.Core;

public class SimpleDashboardService : ISimpleDashboardService
{
    private readonly ConcurrentDictionary<string, ISimpleDashboard> _simpleDashboards;

    public SimpleDashboardService(SimpleDashboardCollection simpleDashboards, ILogger<SimpleDashboardService> logger)
    {
        _simpleDashboards = new ConcurrentDictionary<string, ISimpleDashboard>();
        foreach (var simpleDashboard in simpleDashboards)
        {
            if (!_simpleDashboards.TryAdd(simpleDashboard.Alias.Kebaberize(), simpleDashboard))
            {
                logger.LogWarning("SimpleDashboard with alias {Alias} already exists, skipping", simpleDashboard.Alias);
            }
        }
    }

    public ISimpleDashboard? GetByAlias(string alias) => GetByPath(alias.Kebaberize());
    public ISimpleDashboard? GetByPath(string path) => _simpleDashboards.TryGetValue(path.ToLowerInvariant(), out var dashboard) ? dashboard : null;

    public IEnumerable<ISimpleDashboard> GetAll() => _simpleDashboards.Values;
}
