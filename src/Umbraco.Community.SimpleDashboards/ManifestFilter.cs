using Umbraco.Cms.Core.Manifest;

namespace Umbraco.Community.SimpleDashboards
{
    internal class ManifestFilter : IManifestFilter
    {
        public void Filter(List<PackageManifest> manifests)
        {
            var assembly = typeof(ManifestFilter).Assembly;

            manifests.Add(new PackageManifest
            {
                PackageName = "Umbraco.Community.SimpleDashboards",
                Version = assembly.GetName().Version?.ToString(3) ?? "0.1.0",
                AllowPackageTelemetry = true,
                Scripts = new string[] { },
                Stylesheets = new string[] { }
            });
        }
    }
}
