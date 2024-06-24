using Umbraco.Community.SimpleDashboards.Core;

namespace TestSite.Fourteen.Dashboards;

public class ExampleDashboard : SimpleDashboard
{
    public ExampleDashboard()
    {
        SetName("Example Dashboard");
        SetWeight(500);
        // Show dashboard in the Media section
        AddSection("Umb.Section.Media");
        AddSection("Umb.Section.Content");
    }
}
