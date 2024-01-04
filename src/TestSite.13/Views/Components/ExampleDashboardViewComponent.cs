using Microsoft.AspNetCore.Mvc;
using Umbraco.Community.SimpleDashboards.Web;

namespace TestSite.Thirteen.Views.Components;

public class ExampleDashboardViewComponent : DashboardViewComponent
{
    public override IViewComponentResult Invoke(DashboardModel model)
    {
        return Content($"Hello {model.Dashboard.Alias}");
    }
}
