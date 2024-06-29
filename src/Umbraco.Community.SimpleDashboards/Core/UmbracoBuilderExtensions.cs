using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Infrastructure.Manifest;
using Umbraco.Community.SimpleDashboards.Core.Models;
using Umbraco.Community.SimpleDashboards.Web;

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

        builder.Services.ConfigureOptions<ConfigApiSwaggerGenOptions>();
        builder.Services.AddSingleton<ISimpleDashboardService, SimpleDashboardService>();
        builder.Services.AddSingleton<IPackageManifestReader, SimpleDashboardPackageManifestReader>();
    }

    private static SimpleDashboardCollectionBuilder SimpleDashboards(this IUmbracoBuilder builder) => builder.WithCollectionBuilder<SimpleDashboardCollectionBuilder>();
}
