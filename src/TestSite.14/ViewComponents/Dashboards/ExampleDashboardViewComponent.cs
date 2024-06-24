using Microsoft.AspNetCore.Mvc;
using Umbraco.Community.SimpleDashboards.Web;

namespace TestSite.Fourteen.ViewComponents.Dashboards;

public class ExampleDashboardViewComponent : DashboardViewComponent
{
    public override IViewComponentResult Invoke(DashboardModel model)
    {
        return Content($"Hello {model.Dashboard.Name}");
    }
}
