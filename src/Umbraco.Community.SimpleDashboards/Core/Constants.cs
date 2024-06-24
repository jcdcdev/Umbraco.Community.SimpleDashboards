using System.Reflection;

namespace Umbraco.Community.SimpleDashboards.Core;

public static class Constants
{
    public const string Area = "simpledashboards";

    public const string ExampleView =
        @"@inherits Umbraco.Community.SimpleDashboards.Web.DashboardViewPage

<h1>Hello Umbraco</h1>
<p>My Dashboard is: @Model.Dashboard.Alias</p>";

    public const string ErrorView =
        @"<div>
    <h1>Dashboard Not Found</h1>
</div>";

    private static readonly string NameSpace = Assembly.GetEntryAssembly()?.GetName()?.Name ?? "YourNamespace";
    public const string PackageName = "Simple Dashboards";
    public const string ErrorViewPath = "~/Views/Dashboards/ViewNotFound.cshtml";

    public static string ExampleViewComponent(string name) =>
        $@"using Microsoft.AspNetCore.Mvc;
using Umbraco.Community.SimpleDashboards.Web;

namespace {NameSpace}.Views.Components;

public class {name}ViewComponent : DashboardViewComponent
{{
    public override IViewComponentResult Invoke(DashboardModel model)
    {{
        return Content($""Hello {{model.Dashboard.Alias}}"");
    }}
}}";

    public class Api
    {
        public const string ApiName = "SimpleDashboards";
    }
}
