using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Api.Common.OpenApi;
using Umbraco.Cms.Api.Management.OpenApi;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Infrastructure.Manifest;
using Umbraco.Community.SimpleDashboards.Core.Models;
using Umbraco.Community.SimpleDashboards.Web;

namespace Umbraco.Community.SimpleDashboards.Core;

public static class UmbracoBuilderExtensions
{
    public static void AddSimpleDashboards(this IUmbracoBuilder builder)
    {
        builder.SimpleDashboards();
        var types = builder.TypeLoader.GetTypes<ISimpleDashboard>();
        foreach (var type in types)
        {
            builder.SimpleDashboards().Append(type);
        }

        builder.AddBackOfficeOpenApiDocument(Constants.Api.ApiName, document => document
            .WithTitle(Constants.Api.GroupName)
            .WithBackOfficeAuthentication());

        builder.Services.AddSingleton<ISimpleDashboardService, SimpleDashboardService>();
        builder.Services.AddSingleton<IPackageManifestReader, PackageManifestReader>();
    }

    private static SimpleDashboardCollectionBuilder SimpleDashboards(this IUmbracoBuilder builder) => builder.WithCollectionBuilder<SimpleDashboardCollectionBuilder>();
}
