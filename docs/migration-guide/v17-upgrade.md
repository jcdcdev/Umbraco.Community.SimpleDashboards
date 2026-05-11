# v13 → v17 Guide

## Checklist

- Convert constructor/fluent configuration to property/manifest overrides on `SimpleDashboard`
- Replace `Allow` / `Deny` / `AddAccessRule` with `IConditionManifest[]` (use `ConditionManifest.Create(...)`)
- Migrate one dashboard first 
- Verify in backoffice
- Then batch‑migrate the rest

## Before → After

| v13 (legacy / constructor)                                   | v17 (manifest / property)                                                                                                      |
|--------------------------------------------------------------|--------------------------------------------------------------------------------------------------------------------------------|
| `SetName("Title");`                                          | `public override string Name => "Title";`                                                                                      |
| `SetName("Title","en-GB");`                                  | ⚠️ [TODO - Issue #211](https://github.com/jcdcdev/Umbraco.Community.SimpleDashboards/issues/211)                               |
| `AddSection(Constants.Applications.Media);`                  | `public override string[] Sections => new[] { Constants.Applications.Media };`                                                 |
| `AddAccessRule(SimpleAccessRule.AllowAdminGroup);`           | `Conditions` with `ConditionManifest.Create("Umb.Condition.CurrentUser.GroupId", Constants.Security.AdminGroupKey.ToString())` |
| `Allow(x => x.UserGroup("myGroup"));`                        | Add `ConditionManifest` using the group's GUID/key (or resolve alias→GUID at startup)                                          |
| `Deny(x => x.UserGroup("other"));`                           | Add `ConditionManifest` using the group's GUID/key (or resolve alias→GUID at startup)                                          |
| `[Umbraco.Cms.Core.Composing.Weight(-100)]` or ctor ordering | `public override int Weight => -100;`                                                                                          |     
| Views in `/Views/Dashboard/Name.cshtml`                      | ✅ unchanged — naming/location rules remain the same                                                                            |
| `DashboardViewComponent` / `DashboardAsyncViewComponent`     | ✅ unchanged — view components still work                                                                                       |

## Minimal examples

### Legacy (v13)

```csharp title="LegacyExampleDashboard.cs"
[Umbraco.Cms.Core.Composing.Weight(-100)]
public class LegacyExampleDashboard : SimpleDashboard
{
	public LegacyExampleDashboard()
	{
		SetName("Example Dashboard Name (default)");
		SetName("Example Dashboard Name (en-GB)", "en-GB");
		AddSection(Constants.Applications.Media);
		AddSection(Constants.Applications.Content);
		AddAccessRule(SimpleAccessRule.AllowEditorGroup);
		Allow(x => x.UserGroup("myGroup"));
		Deny(x => x.UserGroup("myOtherGroup"));
	}
}
```

### Migrated (v17)

```csharp title="ExampleDashboard.cs"
using jcdcdev.Umbraco.Core.Web.Models.Manifests;
using Umbraco.Cms.Core;

public class ExampleDashboard : SimpleDashboard
{
	public override string Name => "Example Dashboard Name (default)";
	public override int Weight => -100;
	public override string[] Sections => new[] { Constants.Applications.Media, Constants.Applications.Content };

	public override IConditionManifest[] Conditions => new[]
	{
		// built‑in Admin group example
		ConditionManifest.Create("Umb.Condition.CurrentUser.GroupId", Constants.Security.AdminGroupKey.ToString()),

		// custom group: replace with resolved GUID
		ConditionManifest.Create("Umb.Condition.CurrentUser.GroupId", "<my-group-guid>")
	};
}
```

## Quick notes

- Views: keep `/Views/Dashboard/{ClassNameWithoutDashboard}.cshtml`.
- Group aliases: v17 condition operands usually expect GUIDs (group Key). If you used aliases, add a small startup
  helper to resolve alias→Key and populate `Conditions`.
- Ordering: Override `Weight`.
- Troubleshooting: If a dashboard is missing, verify `Sections` contains correct aliases and each
  `ConditionManifest` operand is valid (GUID vs alias mismatch is the most common issue).

---


## Support & Contact

- Report issues: [GitHub issues](https://github.com/jcdcdev/Umbraco.Community.SimpleDashboards/issues)
- Contact: [Contact page](https://jcdc.dev/contact)


