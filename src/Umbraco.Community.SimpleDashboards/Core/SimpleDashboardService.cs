using System.Collections.Concurrent;
using Humanizer;
using Umbraco.Community.SimpleDashboards.Core.Models;
using Umbraco.Extensions;

namespace Umbraco.Community.SimpleDashboards.Core;

public class SimpleDashboardService(SimpleDashboardCollection simpleDashboards) : ISimpleDashboardService
{
    public ISimpleDashboard? GetByAlias(string alias) => simpleDashboards.FirstOrDefault(x => x.HasAlias(alias));
    public ISimpleDashboard? GetByPath(string path) => simpleDashboards.FirstOrDefault(x => x.PathName.InvariantEquals(path));
    public IEnumerable<ISimpleDashboard> GetAll() => simpleDashboards;
}
