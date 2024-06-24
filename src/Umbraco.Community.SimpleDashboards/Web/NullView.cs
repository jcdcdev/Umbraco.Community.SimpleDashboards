﻿using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace Umbraco.Community.SimpleDashboards.Web;

internal sealed class NullView : IView
{
    public static readonly NullView Instance = new NullView();

    public string Path => string.Empty;

    public Task RenderAsync(ViewContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        return Task.CompletedTask;
    }
}