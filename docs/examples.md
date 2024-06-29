## Quick Start

### Install Package
```csharp
dotnet add package Umbraco.Community.SimpleDashboards 
```

### Register Dashboard

By default this will display in the content section for Admins only.
```csharp
using Umbraco.Community.SimpleDashboards.Web; 
public class BasicDashboard : SimpleDashboard { }
```

### Create View

- Your view **must** go in `/Views/Dashboard`
- You view **must** be the name of your C# class (without `Dashboard`)
  - For example: `BasicDashboard.cs` => `/Views/Dashboard/Basic.cshtml` 
```csharp
@inherits Umbraco.Community.SimpleDashboards.DashboardViewPage

<uui-box headline="Hello Umbraco">
    <p>My Dashboard is: @Model.Dashboard.Alias</p>
</uui-box>
```

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