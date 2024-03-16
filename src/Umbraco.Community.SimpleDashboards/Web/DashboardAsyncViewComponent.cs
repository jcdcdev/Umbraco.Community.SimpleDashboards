using Microsoft.AspNetCore.Mvc;

namespace Umbraco.Community.SimpleDashboards.Web;

public abstract class DashboardAsyncViewComponent : ViewComponent
{
    public abstract Task<IViewComponentResult> InvokeAsync(DashboardModel model);
}