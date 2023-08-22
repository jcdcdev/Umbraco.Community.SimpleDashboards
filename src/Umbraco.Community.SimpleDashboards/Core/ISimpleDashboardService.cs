namespace Umbraco.Community.SimpleDashboards.Core;

public interface ISimpleDashboardService
{
    ISimpleDashboard? GetDashboard(string alias);
    IEnumerable<ISimpleDashboard>? Get();
}
