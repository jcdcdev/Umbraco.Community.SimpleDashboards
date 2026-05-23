using System.Globalization;
using System.Reflection;
using jcdcdev.Umbraco.Core.Extensions;
using jcdcdev.Umbraco.Core.Web.Models.Manifests;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Manifest;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Infrastructure.Manifest;
using Umbraco.Community.SimpleDashboards.Web;
using Umbraco.Extensions;

namespace Umbraco.Community.SimpleDashboards.Core;

public class PackageManifestReader(
    ISimpleDashboardService simpleDashboardService,
    ILocalizedTextService localizedTextService,
    ILogger<PackageManifestReader> logger) : IPackageManifestReader
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
            Version = Assembly.GetAssembly(typeof(PackageManifestReader))?.GetName().Version?.ToSemVer()?.ToString() ?? "0.1.0",
            AllowPublicAccess = false,
            AllowTelemetry = true,
            Extensions = []
        };

        extensions.Add(new BackofficeEntryPointManifest
        {
            Name = "simple-dashboards.entrypoint",
            Alias = "simple-dashboards.entrypoint",
            Js = "/App_Plugins/Umbraco.Community.SimpleDashboards/dist/index.js"
        });


        foreach (var dashboard in dashboards)
        {
            foreach (var section in dashboard.Sections)
            {
                var conditions = dashboard.Conditions.Any() ? dashboard.Conditions : [ConditionManifest.SectionAlias(section)];

                var manifest = new DashboardManifest
                {
                    Alias = dashboard.UniqueAlias(section),
                    Name = dashboard.UniqueName(section),
                    ElementName = "simple-dashboard",
                    Weight = dashboard.Weight,
                    Meta = new DashboardManifest.MetaManifest
                    {
                        Label = dashboard.Label,
                        Pathname = dashboard.PathName
                    },
                    Conditions = conditions
                };

                extensions.Add(manifest);
            }
        }

        var cultures = localizedTextService.GetSupportedCultures().ToList();

        // Umbraco's translations for UK English are marked as `en` not `en-GB`
        try
        {
            cultures.Add(new CultureInfo("en"));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error adding en fallback culture");
        }

        foreach (var culture in cultures)
        {
            var cultureCode = culture.Name.ToLowerInvariant();
            var items = new Dictionary<string, string>();
            foreach (var dashboard in dashboards.Where(x => x.HasLocalizedNames))
            {
                if (!dashboard.LocalizedNames.TryGetValue(cultureCode, out var localizedName))
                {
                    var fallbackCultureCode = culture.TwoLetterISOLanguageName;
                    if (fallbackCultureCode != cultureCode)
                    {
                        if (!dashboard.LocalizedNames.TryGetValue(fallbackCultureCode, out localizedName))
                        {
                            continue;
                        }
                    }
                }

                if (localizedName.IsNullOrWhiteSpace())
                {
                    continue;
                }

                items.Add(dashboard.Alias.ToFirstLowerInvariant(), localizedName);
            }

            if (!items.Any())
            {
                continue;
            }

            var localizationManifest = new LocalizationManifest
            {
                Alias = $"simpleDashboardTabs.{cultureCode}",
                Name = $"Simple Dashboard Tabs ({cultureCode})",
                Meta = new()
                {
                    Culture = cultureCode,
                    Localizations = new Dictionary<string, Dictionary<string, string>>
                    {
                        ["simpleDashboardTabs"] = items
                    }
                }
            };

            extensions.Add(localizationManifest);
        }

        packageManifest.Extensions = extensions.OfType<object>().ToArray();
        return Task.FromResult<IEnumerable<PackageManifest>>([packageManifest]);
    }
}
