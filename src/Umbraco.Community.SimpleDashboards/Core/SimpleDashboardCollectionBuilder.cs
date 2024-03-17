using Umbraco.Cms.Core.Composing;

namespace Umbraco.Community.SimpleDashboards.Core;

public class SimpleDashboardCollectionBuilder : OrderedCollectionBuilderBase<SimpleDashboardCollectionBuilder, SimpleDashboardCollection, ISimpleDashboard>
{
    protected override SimpleDashboardCollectionBuilder This => this;
}
