using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Web.Common.Authorization;
using Umbraco.Cms.Web.Common.Filters;
using Umbraco.Community.SimpleDashboards.Core;
using Umbraco.Extensions;

namespace Umbraco.Community.SimpleDashboards.Web;

[Authorize(Policy = AuthorizationPolicies.BackOfficeAccess)]
[DisableBrowserCache]
[Area(Constants.Area)]
public class SimpleDashboardController : Controller
{
    private readonly ICompositeViewEngine _viewEngine;
    private readonly ISimpleDashboardService _service;
    private readonly ILogger _logger;
    private readonly IViewComponentDescriptorProvider _viewComponentDescriptorProvider;
    private readonly IAppPolicyCache _runtimeCache;

    public SimpleDashboardController(
        ICompositeViewEngine viewEngine,
        ISimpleDashboardService service,
        ILogger<SimpleDashboardController> logger,
        IViewComponentDescriptorProvider viewComponentDescriptorProvider,
        AppCaches appCaches)
    {
        _viewEngine = viewEngine;
        _service = service;
        _logger = logger;
        _viewComponentDescriptorProvider = viewComponentDescriptorProvider;
        _runtimeCache = appCaches.RuntimeCache;
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

        var viewComponentName = dash.ViewComponent;
        if (ViewComponentExists(viewComponentName))
        {
            return ViewComponent(viewComponentName, new { Model = model });
        }

        return PartialView("~/Views/Dashboards/ViewNotFound.cshtml", model);
    }

    private bool ViewComponentExists(string viewComponentName)
    {
        return _runtimeCache.GetCacheItem(viewComponentName, () =>
        {
            var viewComponentDescriptors = _viewComponentDescriptorProvider.GetViewComponents();
            return viewComponentDescriptors.Any(vc => vc.ShortName == viewComponentName);
        });
    }
}
