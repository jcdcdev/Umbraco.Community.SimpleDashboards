using Umbraco.Community.SimpleDashboards.Core;

namespace Umbraco.Community.SimpleDashboards.Web;

public class DashboardModel
{
    public readonly ISimpleDashboard Dashboard;

    public DashboardModel(ISimpleDashboard dashboard)
    {
        Dashboard = dashboard;
    }
}
