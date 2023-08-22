## Quick Start

### Install Package
```csharp
dotnet add package Umbraco.Community.SimpleDashboards 
```

### Register Dashboard

By default this will display in the content section for Admins only.
```csharp
using Umbraco.Community.SimpleDashboards.Core; 
public class BasicDashboard : SimpleDashboard { }
```

### Create View

- Your view **must** go in `/Views/Dashboard`
- You view **must** be the name of your C# class (without `Dashboard`)
  - For example: `BasicDashboard.cs` => `/Views/Dashboard/Basic.cshtml` 
```csharp
@inherits Umbraco.Community.SimpleDashboards.DashboardViewPage

<h1>Hello Umbraco</h1>
<p>My Dashboard alias is: @Model.Dashboard.Alias</p>
```

## Detailed Register Dashboard

By adding a constructor you can define permissions, where to display (content sections) and the name 
```csharp
using Umbraco.Community.SimpleDashboards.Core;

// Control order of where dashboard shows 
[Umbraco.Cms.Core.Composing.Weight(-100)]
public class ExampleDashboard : SimpleDashboard
{
    public ExampleDashboard()
    {
        // Allow Admin User Group to see dashboard
        AddAccessRule(SimpleAccessRule.AllowAdminGroup);

        // Custom Section permissions
        Allow(x => x.Section("content"));
        Deny(x => x.Section("content"));

        // Custom User Groups
        Allow(x=>x.UserGroup("myGroup"));
        Deny(x=>x.UserGroup("myOtherGroup"));
        
        // Set dashboard name
        SetName("Dashboard Name");

        // Set culture specific dashboard name 
        SetName("Dashboard Name (cy-GB)", "cy-GB");
        
        // Show dashboard in the Media section
        AddSection(Umbraco.Cms.Core.Constants.Applications.Media);
        
        // Show dashboard in your custom section
        AddSection("mySection");
    }
}
```

## View Component Example

- Your View Component should match the name of your C# class plus `ViewComponent.cs`
  - For example: `BasicDashboard.cs` => `BasicDashboardViewComponent.cs`
- Your View Component **must** inherit either:
  - `DashboardViewComponent`
  - `DashboardAsyncViewComponent`

```csharp
public class ExampleDashboardViewComponent : DashboardAsyncViewComponent
{
    public override Task<IViewComponentResult> InvokeAsync(DashboardModel model)
    {
        // Complex business logic
        var viewModel = await _service.CreateViewModel(model);
        // ...
        return View("~/Views/MyPath/MyView.cshtml", viewModel);
    }
}
```