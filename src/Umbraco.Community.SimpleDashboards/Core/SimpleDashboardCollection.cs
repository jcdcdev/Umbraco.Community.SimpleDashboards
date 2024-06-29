using Umbraco.Cms.Core.Composing;
using Umbraco.Community.SimpleDashboards.Core.Models;

namespace Umbraco.Community.SimpleDashboards.Core;

public class SimpleDashboardCollection(Func<IEnumerable<ISimpleDashboard>> items) : BuilderCollectionBase<ISimpleDashboard>(items);
