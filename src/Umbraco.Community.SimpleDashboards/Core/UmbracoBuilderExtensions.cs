using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Umbraco.Cms.Core.Configuration.Models;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Hosting;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Web.Common.ApplicationBuilder;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco.Community.SimpleDashboards.Web;
using Umbraco.Extensions;

namespace Umbraco.Community.SimpleDashboards.Core;

public static class UmbracoBuilderExtensions
{
    public static void AddSimpleDashboards(this IUmbracoBuilder builder)
    {
        var types = builder.TypeLoader.GetTypes<ISimpleDashboard>();
        foreach (var type in types)
        {
            builder.SimpleDashboards().Append(type);
        }

        builder.Services.AddSingleton<ISimpleDashboardService, SimpleDashboardService>();
        builder.AddNotificationHandler<SendingDashboardsNotification, EditorSendingContentNotificationHandler>();

        builder.Services.Configure<UmbracoPipelineOptions>(options =>
        {
            options.AddFilter(new UmbracoPipelineFilter(nameof(RenderController))
            {
                Endpoints = app => app.UseEndpoints(endpoints =>
                {
                    var globalSettings = app.ApplicationServices
                        .GetRequiredService<IOptions<GlobalSettings>>().Value;
                    var hostingEnvironment = app.ApplicationServices
                        .GetRequiredService<IHostingEnvironment>();

                    var backofficeArea = Cms.Core.Constants.Web.Mvc.BackOfficePathSegment;
                    var area = Constants.Area;
                    var rootSegment = $"{globalSettings.GetUmbracoMvcArea(hostingEnvironment)}/{backofficeArea}";
                    endpoints.MapAreaControllerRoute(
                        $"umbraco-{rootSegment}-{nameof(SimpleDashboardController)}".ToLowerInvariant(),
                        area,
                        $"{rootSegment}/simpledashboards/render/{{*dashboard}}",
                        new
                        {
                            controller = "SimpleDashboard",
                            action = "Render",
                            area
                        });
                })
            });
        });
    }

    private static SimpleDashboardCollectionBuilder SimpleDashboards(this IUmbracoBuilder builder) => builder.WithCollectionBuilder<SimpleDashboardCollectionBuilder>();
}
