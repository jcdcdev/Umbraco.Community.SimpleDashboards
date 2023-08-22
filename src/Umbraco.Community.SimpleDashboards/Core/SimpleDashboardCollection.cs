using Umbraco.Cms.Core.Composing;

namespace Umbraco.Community.SimpleDashboards.Core;

public class SimpleDashboardCollection : BuilderCollectionBase<ISimpleDashboard>
{
    public SimpleDashboardCollection(Func<IEnumerable<ISimpleDashboard>> items) : base(items)
    {
    }
}