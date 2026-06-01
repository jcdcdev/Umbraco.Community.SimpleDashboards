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

