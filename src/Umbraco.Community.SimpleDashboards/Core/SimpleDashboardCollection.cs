using Umbraco.Cms.Core.Composing;

namespace Umbraco.Community.SimpleDashboards.Core;

public class SimpleDashboardCollection(Func<IEnumerable<ISimpleDashboard>> items) : BuilderCollectionBase<ISimpleDashboard>(items);
