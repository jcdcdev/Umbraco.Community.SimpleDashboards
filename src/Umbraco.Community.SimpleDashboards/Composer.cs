using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Community.SimpleDashboards.Core;

namespace Umbraco.Community.SimpleDashboards;

internal class Composer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        builder.AddSimpleDashboards();
    }
}
