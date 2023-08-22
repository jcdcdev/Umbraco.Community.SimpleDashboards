using Umbraco.Community.SimpleDashboards.Core;

namespace Umbraco.Community.SimpleDashboards.TestSite.Dashboards;

// Control order of where dashboard shows
[Umbraco.Cms.Core.Composing.Weight(-100)]
public class ExampleDashboard : SimpleDashboard
{
    public ExampleDashboard()
    {
        // Set dashboard name
        SetName("Example Dashboard Name (default)");
        // Set culture specific dashboard name
        SetName("Example Dashboard Name (en-GB)", "en-GB");

        // Show dashboard in the Media & Content sections
        AddSection(Cms.Core.Constants.Applications.Media);
        AddSection(Cms.Core.Constants.Applications.Content);
    }
}
