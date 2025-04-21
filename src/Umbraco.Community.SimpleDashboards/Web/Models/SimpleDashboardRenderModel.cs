using Umbraco.Community.SimpleDashboards.Core;

namespace Umbraco.Community.SimpleDashboards.Web.Models;

public class SimpleDashboardRenderModel
{
    public required string Body { get; set; }
    public static SimpleDashboardRenderModel Error => new() { Body = Constants.ErrorView };
}
