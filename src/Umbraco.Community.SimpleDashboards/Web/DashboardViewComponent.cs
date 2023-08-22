using Microsoft.AspNetCore.Mvc;

namespace Umbraco.Community.SimpleDashboards.Web;

public abstract class DashboardViewComponent : ViewComponent
{
    public abstract IViewComponentResult Invoke(DashboardModel model);
}