using Umbraco.Community.SimpleDashboards.Core;

namespace Umbraco.Community.SimpleDashboards.Web;

public class DashboardModel(ISimpleDashboard dashboard)
{
    public readonly ISimpleDashboard Dashboard = dashboard;
}
