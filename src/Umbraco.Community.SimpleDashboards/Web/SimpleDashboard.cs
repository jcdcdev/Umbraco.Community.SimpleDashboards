using Humanizer;
using jcdcdev.Umbraco.Core;
using jcdcdev.Umbraco.Core.Web.Models.Manifests;
using Umbraco.Community.SimpleDashboards.Core.Models;
using Umbraco.Extensions;

namespace Umbraco.Community.SimpleDashboards.Web;

public abstract class SimpleDashboard : ISimpleDashboard
{
    public virtual string ViewPath => $"~/Views/Dashboards/{Alias}.cshtml";
    public virtual string ViewComponent => $"{Alias}Dashboard";
    public string Alias => GetType().Name.TrimEnd("Dashboard");
    public virtual Dictionary<string, string> LocalizedNames => [];
    public string PathName => Alias.Kebaberize();
    public virtual IConditionManifest[] Conditions => BuildConditions().ToArray();
    public bool HasLocalizedNames => LocalizedNames.Count != 0;
    public string Label => HasLocalizedNames ? $"#simpleDashboardTabs_{Alias.ToFirstLowerInvariant()}" : Name;
    public virtual int Weight => 100;
    public virtual string Name => Alias;
    public virtual string[] Sections => [Constants.Sections.Content];

    public bool HasAlias(string alias)
    {
        if (alias.InvariantEquals(Alias))
        {
            return true;
        }

        var aliases = new List<string>();
        foreach (var section in Sections.ToList())
        {
            aliases.Add(this.UniqueAlias(section));
        }

        return aliases.InvariantContains(alias);
    }

    protected List<IConditionManifest> BuildConditions()
    {
        var conditions = new List<IConditionManifest>();
        // TODO - Add when Umbraco has implemented oneOf for Sections
        // conditions.Add(ConditionManifest.SectionAlias(sections));
        return conditions;
    }
}

public static class SimpleWorkspaceViewExtensions
{
    public static string UniqueAlias(this ISimpleDashboard dashboard, string section)
    {
        return $"{dashboard.Alias}-{section}";
    }

    public static string UniqueName(this ISimpleDashboard dashboard, string section)
    {
        return $"{dashboard.Name} ({section})";
    }
}
