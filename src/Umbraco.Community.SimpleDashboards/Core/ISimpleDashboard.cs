﻿using Umbraco.Cms.Core.Dashboards;

namespace Umbraco.Community.SimpleDashboards.Core;

public interface ISimpleDashboard : IDashboard
{
    string? GetLabel(string? culture = "*");

    void Allow(Func<IAccessRuleBuilder, IAccessRule> func);
    void Deny(Func<IAccessRuleBuilder, IAccessRule> func);
    void SetView(string view);
    public void SetName(string name);
    public void SetName(string name, string culture);
}