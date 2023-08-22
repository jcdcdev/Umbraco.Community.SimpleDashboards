using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Web.Common.Authorization;
using Umbraco.Cms.Web.Common.Filters;
using Umbraco.Community.SimpleDashboards.Core;

namespace Umbraco.Community.SimpleDashboards.Web;

[Authorize(Policy = AuthorizationPolicies.BackOfficeAccess)]
[DisableBrowserCache]
[Area(Constants.Area)]
public class SimpleDashboardController : Controller
{
    private readonly ICompositeViewEngine _viewEngine;
    private readonly ISimpleDashboardService _service;
    private readonly ILogger _logger;

    public SimpleDashboardController(ICompositeViewEngine viewEngine, ISimpleDashboardService service, ILogger<SimpleDashboardController> logger)
    {
        _viewEngine = viewEngine;
        _service = service;
        _logger = logger;
    }

    public IActionResult Render(string dashboard)
    {
        var dash = _service.GetDashboard(dashboard);
        if (dash == null)
        {
            _logger.LogWarning("Failed to find Dashboard {DashboardAlias}", dashboard);
            return Ok($"Unable to find Dashboard {dashboard}");
        }

        var model = new DashboardModel(dash);
        var path = $"~/Views/Dashboards/{dashboard}.cshtml";

        var result = _viewEngine.GetView(null, path, false);
        if (result.Success)
        {
            return PartialView(result.ViewName, model);
        }

        return ViewComponent($"{dashboard}Dashboard", new { Model = model });
    }
}
