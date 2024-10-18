using jcdcdev.Umbraco.Core.Extensions;
using jcdcdev.Umbraco.Core.Web.Models.Manifests;
using Umbraco.Cms.Core.Manifest;
using Umbraco.Cms.Infrastructure.Manifest;

namespace Umbraco.Community.SimpleDashboards.Core;

public class SimpleDashboardPackageManifestReader(ISimpleDashboardService simpleDashboardService) : IPackageManifestReader
{
    public Task<IEnumerable<PackageManifest>> ReadPackageManifestsAsync()
    {
        var dashboards = simpleDashboardService.GetAll().ToList();
        if (!dashboards.Any())
        {
            return Task.FromResult<IEnumerable<PackageManifest>>(Array.Empty<PackageManifest>());
        }

        var extensions = new List<IManifest>();
        var packageManifest = new PackageManifest
        {
            Name = Constants.PackageName,
            Version = EnvironmentExtensions.CurrentAssemblyVersion().ToSemVer()?.ToString() ?? "0.1.0",
            AllowPublicAccess = false,
            AllowTelemetry = true,
            Extensions = []
        };

        extensions.Add(new EntryPointManifest
        {
            Name = "simple-dashboards.entrypoint",
            Alias = "simple-dashboards.entrypoint",
            Js = "/App_Plugins/Umbraco.Community.SimpleDashboards/dist/index.js"
        });

        foreach (var dashboard in dashboards)
        {
            foreach (var section in dashboard.Sections)
            {
                var uniqueAlias = $"{dashboard.Alias}-{section}";
                var uniqueName = $"{dashboard.Name} ({section})";
                var manifest = new DashboardManifest
                {
                    Alias = uniqueAlias,
                    Name = uniqueName,
                    ElementName = "simple-dashboard",
                    Weight = dashboard.Weight,
                    Meta = new DashboardManifest.MetaManifest
                    {
                        Label = dashboard.Label,
                        Pathname = dashboard.PathName
                    },
                    Conditions =
                    [
                        new ConditionManifest
                        {
                            Alias = "Umb.Condition.SectionAlias",
                            Match = section
                        }
                    ]
                };
                extensions.Add(manifest);
            }
        }

        packageManifest.Extensions = extensions.OfType<object>().ToArray();
        return Task.FromResult<IEnumerable<PackageManifest>>([packageManifest]);
    }
}
