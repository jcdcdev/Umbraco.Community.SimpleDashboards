using Umbraco.Community.SimpleDashboards.Web;

namespace TestSite.Dashboards;

public class ExampleDashboard : SimpleDashboard
{
    public override string Name => "Example Dashboard";

    public override int Weight => 500;

    // Show dashboard in the Media section
    public override string[] Sections => ["Umb.Section.Media", "Umb.Section.Content"];
}
