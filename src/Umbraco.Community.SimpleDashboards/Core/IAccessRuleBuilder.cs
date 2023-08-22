using Umbraco.Cms.Core.Dashboards;

namespace Umbraco.Community.SimpleDashboards.Core;

public interface IAccessRuleBuilder
{
    IAccessRule Section(string section);
    IAccessRule UserGroup(string userGroup);
}