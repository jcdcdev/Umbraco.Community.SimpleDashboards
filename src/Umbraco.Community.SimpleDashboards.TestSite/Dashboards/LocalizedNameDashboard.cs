using Umbraco.Community.SimpleDashboards.Web;

namespace Umbraco.Community.SimpleDashboards.TestSite.Dashboards;

public class LocalizedNameDashboard : SimpleDashboard
{
    public override Dictionary<string, string> LocalizedNames => new()
    {
        { "en", "Localized Dashboard" },
        { "sv", "Lokalisering Dashboard" }
    };
}
