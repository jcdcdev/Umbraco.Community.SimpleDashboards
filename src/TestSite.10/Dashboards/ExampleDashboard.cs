using jcdcdev.Umbraco.Core.AccessRule;
using Umbraco.Community.SimpleDashboards.Core;

namespace TestSite.Ten.Dashboards;

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
        AddSection(Umbraco.Cms.Core.Constants.Applications.Media);
        AddSection(Umbraco.Cms.Core.Constants.Applications.Content);

        // Allow Editors
        AddAccessRule(SimpleAccessRule.AllowEditorGroup);

        // Allow custom User Group
        Allow(x=>x.UserGroup("myGroup"));
        // Deny custom User Group
        Deny(x=>x.UserGroup("myOtherGroup"));
    }
}
