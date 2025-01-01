using Microsoft.AspNetCore.Mvc;
using Umbraco.Community.SimpleDashboards.Web.Models;

namespace Umbraco.Community.SimpleDashboards.Web;

public abstract class DashboardAsyncViewComponent : ViewComponent
{
    public abstract Task<IViewComponentResult> InvokeAsync(DashboardViewModel model);
}
