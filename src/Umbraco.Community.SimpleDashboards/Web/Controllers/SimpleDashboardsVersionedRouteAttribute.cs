using Umbraco.Cms.Web.Common.Routing;

namespace Umbraco.Community.SimpleDashboards.Web.Controllers;

public class SimpleDashboardsVersionedRouteAttribute(string template) : BackOfficeRouteAttribute($"simpledashboards/api/v{{version:apiVersion}}/{template.TrimStart('/')}");
