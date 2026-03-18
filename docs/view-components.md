## View Component Example

- Your View Component should match the name of your C# class plus `ViewComponent.cs`
    - For example: `BasicDashboard.cs` => `BasicDashboardViewComponent.cs`
- Your View Component **must** inherit either:
    - `DashboardViewComponent`
    - `DashboardAsyncViewComponent`

```csharp
public class ExampleDashboardViewComponent : DashboardAsyncViewComponent
{
    public override Task<IViewComponentResult> InvokeAsync(DashboardViewModel model)
    {
        // Complex business logic
        var viewModel = await _service.CreateViewModel(model);
        // ...
        return View("~/Views/MyPath/MyView.cshtml", viewModel);
    }
}
```