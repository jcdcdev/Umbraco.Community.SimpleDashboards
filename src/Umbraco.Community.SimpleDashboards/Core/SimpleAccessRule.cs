using Umbraco.Cms.Core.Dashboards;

// ReSharper disable MemberCanBePrivate.Global
namespace Umbraco.Community.SimpleDashboards.Core;

public static class SimpleAccessRule
{
    public static IAccessRule GrantByUserGroup(string groupAlias)
    {
        return new AccessRule { Type = AccessRuleType.Grant, Value = groupAlias };
    }

    public static IAccessRule GrantBySection(string section)
    {
        return new AccessRule { Type = AccessRuleType.GrantBySection, Value = section };
    }

    public static IAccessRuleBuilder Allow()
    {
        return AccessRuleBuilder.Allow();
    }

    public static IAccessRuleBuilder Deny()
    {
        return AccessRuleBuilder.Deny();
    }

    public static IAccessRule AllowEditorGroup => GrantByUserGroup(Cms.Core.Constants.Security.EditorGroupAlias);
    public static IAccessRule AllowAdminGroup => GrantByUserGroup(Cms.Core.Constants.Security.AdminGroupAlias);
    public static IAccessRule AllowTranslatorGroup => GrantByUserGroup(Cms.Core.Constants.Security.TranslatorGroupAlias);
    public static IAccessRule AllowWriterGroup => GrantByUserGroup(Cms.Core.Constants.Security.WriterGroupAlias);

    public static IAccessRule AllowContentSection => GrantBySection(Cms.Core.Constants.Applications.Content);
    public static IAccessRule AllowUsersSection => GrantBySection(Cms.Core.Constants.Applications.Users);
    public static IAccessRule AllowFormsSection => GrantBySection(Cms.Core.Constants.Applications.Forms);
    public static IAccessRule AllowMediaSection => GrantBySection(Cms.Core.Constants.Applications.Media);
    public static IAccessRule AllowMembersSection => GrantBySection(Cms.Core.Constants.Applications.Members);
    public static IAccessRule AllowPackagesSection => GrantBySection(Cms.Core.Constants.Applications.Packages);
    public static IAccessRule AllowSettingsSection => GrantBySection(Cms.Core.Constants.Applications.Settings);
    public static IAccessRule AllowTranslationSection => GrantBySection(Cms.Core.Constants.Applications.Translation);

    public static IAccessRule DenyEditorGroup => Deny().UserGroup(Cms.Core.Constants.Security.EditorGroupAlias);
    public static IAccessRule DenyAdminGroup => Deny().UserGroup(Cms.Core.Constants.Security.AdminGroupAlias);
    public static IAccessRule DenyTranslatorGroup => Deny().UserGroup(Cms.Core.Constants.Security.TranslatorGroupAlias);
    public static IAccessRule DenyWriterGroup => Deny().UserGroup(Cms.Core.Constants.Security.WriterGroupAlias);

    public static IAccessRule DenyContentSection => Deny().Section(Cms.Core.Constants.Applications.Content);
    public static IAccessRule DenyUsersSection => Deny().Section(Cms.Core.Constants.Applications.Users);
    public static IAccessRule DenyFormsSection => Deny().Section(Cms.Core.Constants.Applications.Forms);
    public static IAccessRule DenyMediaSection => Deny().Section(Cms.Core.Constants.Applications.Media);
    public static IAccessRule DenyMembersSection => Deny().Section(Cms.Core.Constants.Applications.Members);
    public static IAccessRule DenyPackagesSection => Deny().Section(Cms.Core.Constants.Applications.Packages);
    public static IAccessRule DenySettingsSection => Deny().Section(Cms.Core.Constants.Applications.Settings);
    public static IAccessRule DenyTranslationSection => Deny().Section(Cms.Core.Constants.Applications.Translation);
}
