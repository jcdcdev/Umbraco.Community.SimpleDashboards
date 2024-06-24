using Umbraco.Cms.Web.Common.Routing;

namespace Umbraco.Community.SimpleDashboards.Web;

public class RelationsManagerVersionedRouteAttribute(string template) : BackOfficeRouteAttribute($"simpledashboards/api/v{{version:apiVersion}}/{template.TrimStart('/')}");