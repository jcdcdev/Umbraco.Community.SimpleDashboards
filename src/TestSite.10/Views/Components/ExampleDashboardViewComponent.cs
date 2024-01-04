using Microsoft.AspNetCore.Mvc;
using Umbraco.Community.SimpleDashboards.Web;

namespace TestSite.Ten.Views.Components;

public class ExampleDashboardViewComponent : DashboardViewComponent
{
    public override IViewComponentResult Invoke(DashboardModel model)
    {
        return Content($"Hello {model.Dashboard.Alias}");
    }
}
