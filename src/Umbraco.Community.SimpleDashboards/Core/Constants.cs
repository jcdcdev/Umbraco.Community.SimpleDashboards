using System.Reflection;

namespace Umbraco.Community.SimpleDashboards.Core;

public static class Constants
{
    public const string Area = "simpledashboards";

    public static string ExampleViewComponent(string name) =>
        $@"using Microsoft.AspNetCore.Mvc;
using Umbraco.Community.SimpleDashboards.Web;

namespace {_nameSpace}.Views.Components;

public class {name}ViewComponent : DashboardViewComponent
{{
    public override IViewComponentResult Invoke(DashboardModel model)
    {{
        return Content($""Hello {{model.Dashboard.Alias}}"");
    }}
}}";

    public static string ExampleView =
        @"@inherits Umbraco.Community.SimpleDashboards.Web.DashboardViewPage

<h1>Hello Umbraco</h1>
<p>My Dashboard is: @Model.Dashboard.Alias</p>";

    private static readonly string _nameSpace = Assembly.GetEntryAssembly()?.GetName()?.Name ?? "YourNamespace";
}
