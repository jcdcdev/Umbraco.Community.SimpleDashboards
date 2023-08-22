using Umbraco.Cms.Core.Dashboards;
using Umbraco.Extensions;

namespace Umbraco.Community.SimpleDashboards.Core;

public abstract class SimpleDashboard : ISimpleDashboard
{

    public string View => _view ?? "";
    private readonly List<IAccessRule> _rules = new();
    private readonly List<string> _sections = new();
    private readonly Dictionary<string, string> _names = new();
    private string? _view;
    public string? Alias => GetType().Name.TrimEnd("Dashboard");

    string[] IDashboard.Sections => _sections.Any()
        ? _sections.ToArray()
        : new[] { Cms.Core.Constants.Applications.Content };

    IAccessRule[] IDashboard.AccessRules => _rules.ToArray();

    public string? GetLabel(string? culture = "*")
    {
        return _names.TryGetValue(culture.IfNullOrWhiteSpace("*")!, out var name) ? name : Alias;
    }

    public void Allow(Func<IAccessRuleBuilder, IAccessRule> func)
    {
        var builder = AccessRuleBuilder.Allow();
        AddAccessRule(func(builder));
    }

    public void Deny(Func<IAccessRuleBuilder, IAccessRule> func)
    {
        var builder = AccessRuleBuilder.Deny();
        AddAccessRule(func(builder));    }

    public void SetView(string view)
    {
        _view = view;
    }

    protected void AddAccessRule(IAccessRule rule)
    {
        _rules.Add(rule);
    }

    protected void AddSection(string section)
    {
        _sections.Add(section);
    }

    public void SetName(string name)
    {
        SetName(name, "*");
    }

    public void SetName(string name, string culture)
    {
        _names.TryAdd(culture, name);
    }
}
