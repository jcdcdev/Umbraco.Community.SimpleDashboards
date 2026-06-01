using jcdcdev.Umbraco.Core;
using Umbraco.Community.SimpleDashboards.Web;

namespace Umbraco.Community.SimpleDashboards.TestSite.Dashboards;

public class SavedFormsDashboard : SimpleDashboard
{
    public override string Name => "Saved Forms";
    public override string[] Sections => [Constants.Sections.Content];
}
