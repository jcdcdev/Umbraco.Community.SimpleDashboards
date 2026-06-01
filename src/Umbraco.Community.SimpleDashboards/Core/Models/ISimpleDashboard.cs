using jcdcdev.Umbraco.Core.Web.Models.Manifests;

namespace Umbraco.Community.SimpleDashboards.Core.Models;

public interface ISimpleDashboard
{
    string ViewComponent { get; }
    string ViewPath { get; }
    string[] Sections { get; }
    string Alias { get; }
    int Weight { get; }
    string? Name { get; }
    Dictionary<string, string> LocalizedNames { get; }
    string PathName { get; }
    IConditionManifest[] Conditions { get; }
    bool HasLocalizedNames { get; }
    string Label { get; }
    bool HasAlias(string alias);
}
