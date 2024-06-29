using Umbraco.Community.SimpleDashboards.Core.Models;

namespace Umbraco.Community.SimpleDashboards.Core;

public interface ISimpleDashboardService
{
    ISimpleDashboard? GetByAlias(string alias);
    ISimpleDashboard? GetByPath(string path);
    IEnumerable<ISimpleDashboard> GetAll();
}
