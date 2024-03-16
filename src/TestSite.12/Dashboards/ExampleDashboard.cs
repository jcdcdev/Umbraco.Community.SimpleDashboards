using jcdcdev.Umbraco.Core.AccessRule;
using Umbraco.Cms.Core.Composing;
using Umbraco.Community.SimpleDashboards.Core;
using Constants = Umbraco.Cms.Core.Constants;

namespace TestSite.Twelve.Dashboards;

// Control order of where dashboard shows
[Weight(-100)]
public class ExampleDashboard : SimpleDashboard
{
    public ExampleDashboard()
    {
        // Set dashboard name
        SetName("Example Dashboard Name (default)");
        // Set culture specific dashboard name
        SetName("Example Dashboard Name (en-GB)", "en-GB");

        // Show dashboard in the Media & Content sections
        AddSection(Constants.Applications.Media);
        AddSection(Constants.Applications.Content);

        // Allow Editors
        AddAccessRule(SimpleAccessRule.AllowEditorGroup);

        // Allow custom User Group
        Allow(x => x.UserGroup("myGroup"));
        // Deny custom User Group
        Deny(x => x.UserGroup("myOtherGroup"));
    }
}
