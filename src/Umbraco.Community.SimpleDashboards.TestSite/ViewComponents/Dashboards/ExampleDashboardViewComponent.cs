using Microsoft.AspNetCore.Mvc;
using Umbraco.Community.SimpleDashboards.Web;
using Umbraco.Community.SimpleDashboards.Web.Models;

namespace Umbraco.Community.SimpleDashboards.TestSite.ViewComponents.Dashboards;

public class ExampleDashboardViewComponent : DashboardViewComponent
{
    public override IViewComponentResult Invoke(DashboardViewModel model)
    {
        return Content($"Hello {model.Dashboard.Name}");
    }
}
