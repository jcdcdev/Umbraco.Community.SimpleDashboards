using Humanizer;
using Umbraco.Community.SimpleDashboards.Core.Models;
using Umbraco.Extensions;

namespace Umbraco.Community.SimpleDashboards.Web;

public abstract class SimpleDashboard : ISimpleDashboard
{
    public virtual string ViewPath => $"~/Views/Dashboards/{Alias}.cshtml";
    public virtual string ViewComponent => $"{Alias}Dashboard";
    public virtual string Label => Name;
    public string Alias => GetType().Name.TrimEnd("Dashboard");
    public string PathName => Alias.Kebaberize();
    public virtual int Weight => 100;
    public virtual string Name => Alias;
    public virtual string[] Sections => ["Umb.Section.Content"];
}
