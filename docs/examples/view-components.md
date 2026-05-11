## View Components

- Your View Component should match the name of your C# class plus `ViewComponent.cs`
    - For example: `BasicDashboard.cs` => `BasicDashboardViewComponent.cs`
- Your View Component **must** inherit either:
    - `DashboardViewComponent`
    - `DashboardAsyncViewComponent`

```csharp title="ExampleDashboardViewComponent.cs"
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

### Full example: SavedFormsDashboardViewComponent

The test site includes a concrete example of a dashboard view component you can study and reuse: `SavedFormsDashboardViewComponent`.

Files in the TestSite related to this example (GitHub links) with inline excerpts:

- [SavedFormsDashboard.cs](https://github.com/jcdcdev/Umbraco.Community.SimpleDashboards/blob/v17/src/Umbraco.Community.SimpleDashboards.TestSite/Dashboards/SavedFormsDashboard.cs)
 
Declares a `SimpleDashboard` with a name and the section(s) it appears in. This is the metadata the framework uses to show the dashboard in the back-office.

```csharp title="SavedFormsDashboard.cs"
using jcdcdev.Umbraco.Core;
using Umbraco.Community.SimpleDashboards.Web;

namespace Umbraco.Community.SimpleDashboards.TestSite.Dashboards;

public class SavedFormsDashboard : SimpleDashboard
{
    public override string Name => "Saved Forms";
    public override string[] Sections => [Constants.Sections.Content];
}
```

- [SavedFormsDashboardViewComponent.cs](https://github.com/jcdcdev/Umbraco.Community.SimpleDashboards/blob/v17/src/Umbraco.Community.SimpleDashboards.TestSite/ViewComponents/Dashboards/SavedFormsDashboardViewComponent.cs)
 
The ViewComponent that runs when the dashboard is rendered. It uses constructor DI to obtain `ISavedFormGenerator`, builds the `SavedFormsViewModel`, and returns the Razor view.

```csharp title="SavedFormsDashboardViewComponent.cs"
using Microsoft.AspNetCore.Mvc;
using Umbraco.Community.SimpleDashboards.Core.Models;
using Umbraco.Community.SimpleDashboards.TestSite.Helpers;
using Umbraco.Community.SimpleDashboards.TestSite.Models;
using Umbraco.Community.SimpleDashboards.Web;
using Umbraco.Community.SimpleDashboards.Web.Models;

namespace Umbraco.Community.SimpleDashboards.TestSite.ViewComponents.Dashboards;

public class SavedFormsDashboardViewComponent(ISavedFormGenerator generator) : DashboardAsyncViewComponent
{
    public override async Task<IViewComponentResult> InvokeAsync(DashboardViewModel model)
    {
        var forms = await generator.GenerateAsync();
        var vm = new SavedFormsViewModel(model.Dashboard, forms);
        return View(vm);
    }
}

public record SavedFormsViewModel(ISimpleDashboard Dashboard, List<SavedForm> Forms);
```

- [SavedFormGenerator.cs](https://github.com/jcdcdev/Umbraco.Community.SimpleDashboards/blob/v17/src/Umbraco.Community.SimpleDashboards.TestSite/Helpers/SavedFormGenerator.cs)
 
A small helper service (interface + implementation) that produces sample `SavedForm` data. The implementation fetches lorem text from an external API with some caching to simulate real data access.

```csharp title="SavedFormGenerator.cs"
using Umbraco.Community.SimpleDashboards.TestSite.Models;
using Microsoft.Extensions.Caching.Memory;

namespace Umbraco.Community.SimpleDashboards.TestSite.Helpers;

public interface ISavedFormGenerator
{
    Task<List<SavedForm>> GenerateAsync(int count = 8);
}

public class SavedFormGenerator(IHttpClientFactory httpFactory, IMemoryCache cache) : ISavedFormGenerator
{
    public async Task<List<SavedForm>> GenerateAsync(int count = 8)
    {
        var rnd = new Random();

        var subjects = new[]
        {
            "Feature request",
            "Bug report",
            "Support needed",
            "Question about configuration",
            "UI suggestion",
            "Performance issue",
            "Integration request",
            "Documentation update"
        };

        var baseUrl = new Uri("https://lorem-api.com/api/lorem");

        var list = new List<SavedForm>(count);

        for (var i = 0; i < count; i++)
        {
            var id = i + 100;
            var subj = $"{subjects[rnd.Next(subjects.Length)]} (#{id})";
            string body;
            var perFormUrl = new UriBuilder(baseUrl)
            {
                Query = $"paragraphs={rnd.Next(1, 4)}&seed={id}"
            }.Uri;

            try
            {
                body = await cache.GetOrCreateAsync(perFormUrl, async entry =>
                {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60);
                    var client = httpFactory.CreateClient();
                    client.Timeout = TimeSpan.FromSeconds(5);
                    return await client.GetStringAsync(perFormUrl);
                }) ?? string.Empty;
            }
            catch
            {
                body = string.Empty;
            }

            list.Add(new SavedForm(subj, body));
        }

        return list;
    }
}
```

- [SavedForm.cs](https://github.com/jcdcdev/Umbraco.Community.SimpleDashboards/blob/v17/src/Umbraco.Community.SimpleDashboards.TestSite/Models/SavedForm.cs)
 
A lightweight record that models a saved form item with a subject and content body. This is the model returned by the generator and consumed by the view.

```csharp title="SavedForm.cs"
namespace Umbraco.Community.SimpleDashboards.TestSite.Models;

public record SavedForm(string? Subject, string? ContentBody);
```

- [Default.cshtml (view)](https://github.com/jcdcdev/Umbraco.Community.SimpleDashboards/blob/v17/src/Umbraco.Community.SimpleDashboards.TestSite/Views/Shared/Components/SavedFormsDashboard/Default.cshtml)
 
The Razor view that renders the `SavedFormsViewModel`. It handles the empty-state and iterates the `Forms` list to render each saved form inside Umbraco UI components.

```razor title="Default.cshtml"
@model Umbraco.Community.SimpleDashboards.TestSite.ViewComponents.Dashboards.SavedFormsViewModel

@if (!Model.Forms.Any())
{
    <div class="uui-empty">No saved forms found.</div>
    return;
}

@foreach (var form in Model.Forms)
{
    <uui-box headline="Subject - @form.Subject">
        <div class="uui-text" style="white-space:pre-wrap;">@(!string.IsNullOrWhiteSpace(form.ContentBody) ? form.ContentBody : "(empty)")</div>
    </uui-box>
    <div style="margin-bottom: 20px"></div>
}
```

- [Program.cs](https://github.com/jcdcdev/Umbraco.Community.SimpleDashboards/blob/v17/src/Umbraco.Community.SimpleDashboards.TestSite/Program.cs)
 
Test site application startup — registers the sample generator with DI, sets up Umbraco backoffice/website, and wires middleware/endpoints so you can run the TestSite locally to see the dashboard.

```csharp title="Program.cs"
var builder = WebApplication.CreateBuilder(args);

// register test-site helpers / sample data generators
builder.Services.AddHttpClient();
builder.Services.AddMemoryCache();
builder.Services.AddTransient<Umbraco.Community.SimpleDashboards.TestSite.Helpers.ISavedFormGenerator, Umbraco.Community.SimpleDashboards.TestSite.Helpers.SavedFormGenerator>();

builder.CreateUmbracoBuilder()
    .AddBackOffice()
    .AddWebsite()
    .AddDeliveryApi()
    .AddComposers()
    .Build();

var app = builder.Build();

await app.BootUmbracoAsync();

app.UseUmbraco()
    .WithMiddleware(u =>
    {
        u.UseBackOffice();
        u.UseWebsite();
    })
    .WithEndpoints(u =>
    {
        u.UseBackOfficeEndpoints();
        u.UseWebsiteEndpoints();
    });

await app.RunAsync();
```

Use this example when you need a simple, realistic demonstration of:

- wiring a dashboard view component
- using constructor DI in a view component
- returning a strongly-typed view model from a dashboard component

You can copy the pattern into your own project and replace the generator with real data access logic.