using Microsoft.AspNetCore.Mvc;
using Umbraco.Community.SimpleDashboards.Web;

namespace TestSite.Thirteen.Views.Components;

public class ExampleDashboardAsyncViewComponent : DashboardAsyncViewComponent
{
    public override async Task<IViewComponentResult> InvokeAsync(DashboardModel model)
    {
        // Simulate async workload
        await Task.Delay(TimeSpan.FromMilliseconds(1));
        return Content($"Hello {model.Dashboard.Alias}");
    }
}
