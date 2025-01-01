using jcdcdev.Umbraco.Core;
using jcdcdev.Umbraco.Core.Web.Models.Manifests;
using Umbraco.Community.SimpleDashboards.Web;

namespace Umbraco.Community.SimpleDashboards.TestSite.Dashboards;

public class ConditionalDashboard : SimpleDashboard
{
    public override IConditionManifest[] Conditions => [ConditionManifest.SectionAlias(Constants.Sections.Settings)];
}
