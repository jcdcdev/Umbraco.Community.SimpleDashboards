using Humanizer;
using Umbraco.Extensions;

namespace Umbraco.Community.SimpleDashboards.Core;

public abstract class SimpleDashboard : ISimpleDashboard
{
    private readonly List<string> _sections = new();
    private string? _name;
    private int _weight = 100;

    public string ViewPath => $"~/Views/Dashboards/{Alias}.cshtml";
    public string Alias => GetType().Name.TrimEnd("Dashboard");
    public string Label => Name;
    public string PathName => Alias.Kebaberize();
    public int Weight => _weight;

    public string Name => _name.IfNullOrWhiteSpace(Alias);

    public string[] Sections => _sections.Any()
        ? _sections.ToArray()
        : new[] { "Umb.Section.Content" };

    public string ViewComponent => $"{Alias}Dashboard";

    protected void SetWeight(int weight)
    {
        _weight = weight;
    }

    protected void AddSection(string section)
    {
        _sections.Add(section);
    }

    protected void SetName(string name)
    {
        _name = name;
    }
}
