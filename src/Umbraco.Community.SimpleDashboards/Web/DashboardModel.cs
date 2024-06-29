using Umbraco.Community.SimpleDashboards.Core.Models;

namespace Umbraco.Community.SimpleDashboards.Web;

public class DashboardModel(ISimpleDashboard dashboard)
{
    public readonly ISimpleDashboard Dashboard = dashboard;
}
