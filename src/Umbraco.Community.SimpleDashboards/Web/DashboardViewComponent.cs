using Microsoft.AspNetCore.Mvc;
using Umbraco.Community.SimpleDashboards.Web.Models;

namespace Umbraco.Community.SimpleDashboards.Web;

public abstract class DashboardViewComponent : ViewComponent
{
    public abstract IViewComponentResult Invoke(DashboardViewModel model);
}
