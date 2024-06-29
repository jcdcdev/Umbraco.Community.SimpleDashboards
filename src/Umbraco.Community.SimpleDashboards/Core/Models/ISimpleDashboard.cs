namespace Umbraco.Community.SimpleDashboards.Core.Models;

public interface ISimpleDashboard
{
    string ViewComponent { get; }
    string ViewPath { get; }
    string[] Sections { get; }
    string Alias { get; }
    int Weight { get; }
    string? Name { get; }
    string Label { get; }
    string PathName { get; }
}
