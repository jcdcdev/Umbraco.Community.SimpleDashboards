using Umbraco.Community.SimpleDashboards.Core;

namespace Umbraco.Community.SimpleDashboards.Web;

public class SimpleDashboardRenderModel
{
    public string Body { get; set; }
    public static SimpleDashboardRenderModel Error => new SimpleDashboardRenderModel { Body = Constants.ErrorView };
}