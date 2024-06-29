using Umbraco.Cms.Core.Composing;
using Umbraco.Community.SimpleDashboards.Core.Models;

namespace Umbraco.Community.SimpleDashboards.Core;

public class SimpleDashboardCollectionBuilder : OrderedCollectionBuilderBase<SimpleDashboardCollectionBuilder, SimpleDashboardCollection, ISimpleDashboard>
{
    protected override SimpleDashboardCollectionBuilder This => this;
}
