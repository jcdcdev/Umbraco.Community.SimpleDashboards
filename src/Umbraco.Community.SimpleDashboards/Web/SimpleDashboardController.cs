using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Buffers;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Api.Common.Attributes;
using Umbraco.Cms.Api.Common.Filters;
using Umbraco.Cms.Api.Management.Filters;
using Umbraco.Cms.Core.Cache;
using Umbraco.Community.SimpleDashboards.Core;
using Umbraco.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Web.Common.Authorization;

namespace Umbraco.Community.SimpleDashboards.Web;

[ApiExplorerSettings(GroupName = "Simple Dashboards")]
[SimpleDashboardsVersionedRoute("")]
[MapToApi(Constants.Api.ApiName)]
[JsonOptionsName(Cms.Core.Constants.JsonOptionsNames.BackOffice)]
[ApiController]
[Authorize(Policy = AuthorizationPolicies.BackOfficeAccess)]
[AppendEventMessages]
[Produces("application/json")]
public class SimpleDashboardController(
    ICompositeViewEngine viewEngine,
    ISimpleDashboardService service,
    ILogger<SimpleDashboardController> logger,
    IViewComponentDescriptorProvider viewComponentDescriptorProvider,
    AppCaches appCaches)
    : Controller
{
    private readonly ILogger _logger = logger;
    private readonly IAppPolicyCache _runtimeCache = appCaches.RuntimeCache;

    [HttpGet("render/{dashboard}")]
    [Produces<SimpleDashboardRenderModel>]
    public async Task<IActionResult> Render(string dashboard)
    {
        var dash = service.GetByPath(dashboard);
        if (dash == null)
        {
            _logger.LogWarning("Failed to find Dashboard {DashboardAlias}", dashboard);
            return Ok(SimpleDashboardRenderModel.Error);
        }

        var model = new DashboardModel(dash);
        var path = $"~/Views/Dashboards/{model.Dashboard.Alias}.cshtml";
        var result = viewEngine.GetView(null, path, false);
        if (result.Success)
        {
            var body = await RenderAsync(result, model);
            return Ok(body);
        }

        var viewComponentName = dash.ViewComponent;
        if (ViewComponentExists(viewComponentName))
        {
            var body = await RenderAsync(viewComponentName, model);
            return Ok(body);
        }

        return await ReturnError(model);
    }

    private async Task<IActionResult> ReturnError(DashboardModel model)
    {
        var result = viewEngine.GetView(null, Constants.ErrorViewPath, false);
        var body = await RenderAsync(result, model);
        return Ok(body);
    }

    private async Task<SimpleDashboardRenderModel> RenderAsync(string viewComponentName, DashboardModel model)
    {
        var sp = HttpContext.RequestServices;

        var helper = new DefaultViewComponentHelper(
            sp.GetRequiredService<IViewComponentDescriptorCollectionProvider>(),
            HtmlEncoder.Default,
            sp.GetRequiredService<IViewComponentSelector>(),
            sp.GetRequiredService<IViewComponentInvokerFactory>(),
            sp.GetRequiredService<IViewBufferScope>());
        await using var writer = new StringWriter();
        var context = new ViewContext(ControllerContext, NullView.Instance, ViewData, TempData, writer, new HtmlHelperOptions());
        helper.Contextualize(context);
        var vcResult = await helper.InvokeAsync(viewComponentName, new { Model = model });
        vcResult.WriteTo(writer, HtmlEncoder.Default);
        await writer.FlushAsync();
        var body = writer.ToString();
        return new SimpleDashboardRenderModel
        {
            Body = body
        };
    }

    private async Task<SimpleDashboardRenderModel> RenderAsync(ViewEngineResult result, object? model)
    {
        if (result.View == null)
        {
            return SimpleDashboardRenderModel.Error;
        }

        var writer = new StringWriter();
        var viewContext = new ViewContext(new ActionContext(HttpContext, RouteData, ControllerContext.ActionDescriptor, ModelState), result.View, ViewData, TempData, writer, new HtmlHelperOptions())
        {
            ViewData =
            {
                Model = model
            }
        };

        await result.View.RenderAsync(viewContext);
        var body = writer.ToString();
        return new SimpleDashboardRenderModel
        {
            Body = body
        };
    }

    private bool ViewComponentExists(string viewComponentName)
    {
        return _runtimeCache.GetCacheItem(viewComponentName, () =>
        {
            var viewComponentDescriptors = viewComponentDescriptorProvider.GetViewComponents();
            return viewComponentDescriptors.Any(vc => vc.ShortName == viewComponentName);
        });
    }
}
