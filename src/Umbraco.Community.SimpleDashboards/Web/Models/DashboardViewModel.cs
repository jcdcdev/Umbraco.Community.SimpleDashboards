using Umbraco.Community.SimpleDashboards.Core.Models;

namespace Umbraco.Community.SimpleDashboards.Web.Models;

public class DashboardViewModel(ISimpleDashboard dashboard)
{
    public readonly ISimpleDashboard Dashboard = dashboard;
}
