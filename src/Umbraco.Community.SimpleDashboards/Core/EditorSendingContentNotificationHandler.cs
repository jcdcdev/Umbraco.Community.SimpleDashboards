using System.Globalization;
using Umbraco.Cms.Core.Dashboards;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Core.Security;
using Umbraco.Cms.Core.Services;

namespace Umbraco.Community.SimpleDashboards.Core;

public class EditorSendingContentNotificationHandler : INotificationHandler<SendingDashboardsNotification>
{
    private readonly ISimpleDashboardService _dashboardService;
    private readonly IBackOfficeSecurityAccessor _backOfficeSecurityAccessor;
    private readonly ILocalizationService _localizationService;
    private readonly ILocalizedTextService _localizedTextService;

    public EditorSendingContentNotificationHandler(
        ISimpleDashboardService dashboardService,
        IBackOfficeSecurityAccessor backOfficeSecurityAccessor,
        ILocalizationService localizationService,
        ILocalizedTextService localizedTextService)
    {
        _dashboardService = dashboardService;
        _backOfficeSecurityAccessor = backOfficeSecurityAccessor;
        _localizationService = localizationService;
        _localizedTextService = localizedTextService;
    }

    public void Handle(SendingDashboardsNotification notification)
    {
        foreach (var dashboard in notification.Dashboards)
        {
            if (dashboard.Alias == null)
            {
                continue;
            }

            if (dashboard.Label != $"[{dashboard.Alias}]")
            {
                continue;
            }


            if (!dashboard.Properties?.Any() ?? true)
            {
                continue;
            }

            var simpleDashboard = _dashboardService.GetDashboard(dashboard.Alias);
            if (simpleDashboard == null)
            {
                continue;
            }

            var backOfficeSecurity = _backOfficeSecurityAccessor.BackOfficeSecurity;

            var culture = backOfficeSecurity?.CurrentUser?.Language;
            if (!string.IsNullOrWhiteSpace(culture))
            {
                var info = _localizedTextService.ConvertToSupportedCultureWithRegionCode(new CultureInfo(culture));
                var language = _localizationService.GetLanguageByIsoCode(info.Name);
                culture = language?.IsDefault ?? false ? "*" : info.Name;
            }

            dashboard.Label = simpleDashboard.GetLabel(culture);
            dashboard.Properties = new List<IDashboardSlim> { new DashboardSlim { Alias = simpleDashboard.Alias, View = simpleDashboard.View } };
        }
    }
}
