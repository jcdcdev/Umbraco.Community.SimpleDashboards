## Detailed Register Dashboard

By adding a constructor you can define permissions, where to display and the name of the dashboard.

```csharp
using Umbraco.Community.SimpleDashboards.Web;

public class ExampleDashboard : SimpleDashboard
{
    public override string Name => "Example Dashboard";

    public override int Weight => 500;

    // Show dashboard in the Media section
    public override string[] Sections => ["Umb.Section.Media", "Umb.Section.Content"];
}

```
