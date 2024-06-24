# Umbraco.Community.SimpleDashboards

[![Umbraco Marketplace](https://img.shields.io/badge/Umbraco-Marketplace-%233544B1?style=flat&logo=umbraco)](https://marketplace.umbraco.com/package/umbraco.community.simpledashboards)
[![GitHub License](https://img.shields.io/github/license/jcdcdev/Umbraco.Community.SimpleDashboards?color=8AB803&label=License&logo=github)](https://github.com/jcdcdev/Umbraco.Community.SimpleDashboards/blob/v14/LICENSE)
[![NuGet Downloads](https://img.shields.io/nuget/dt/Umbraco.Community.SimpleDashboards?color=cc9900&label=Downloads&logo=nuget)](https://www.nuget.org/packages/Umbraco.Community.SimpleDashboards/)
[![Project Website](https://img.shields.io/badge/Project%20Website-jcdc.dev-jcdcdev?style=flat&color=3c4834&logo=data:image/svg+xml;base64,PHN2ZyB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciIHdpZHRoPSIxNiIgaGVpZ2h0PSIxNiIgZmlsbD0id2hpdGUiIGNsYXNzPSJiaSBiaS1wYy1kaXNwbGF5IiB2aWV3Qm94PSIwIDAgMTYgMTYiPgogIDxwYXRoIGQ9Ik04IDFhMSAxIDAgMCAxIDEtMWg2YTEgMSAwIDAgMSAxIDF2MTRhMSAxIDAgMCAxLTEgMUg5YTEgMSAwIDAgMS0xLTF6bTEgMTMuNWEuNS41IDAgMSAwIDEgMCAuNS41IDAgMCAwLTEgMG0yIDBhLjUuNSAwIDEgMCAxIDAgLjUuNSAwIDAgMC0xIDBNOS41IDFhLjUuNSAwIDAgMCAwIDFoNWEuNS41IDAgMCAwIDAtMXpNOSAzLjVhLjUuNSAwIDAgMCAuNS41aDVhLjUuNSAwIDAgMCAwLTFoLTVhLjUuNSAwIDAgMC0uNS41TTEuNSAyQTEuNSAxLjUgMCAwIDAgMCAzLjV2N0ExLjUgMS41IDAgMCAwIDEuNSAxMkg2djJoLS41YS41LjUgMCAwIDAgMCAxSDd2LTRIMS41YS41LjUgMCAwIDEtLjUtLjV2LTdhLjUuNSAwIDAgMSAuNS0uNUg3VjJ6Ii8+Cjwvc3ZnPg==)](https://jcdc.dev/umbraco-packages/simple-dashboards)

This packages aims to help developers quickly put together Umbraco Dashboards using C# only.

![Basic Dashboard in the Umbraco Office](https://raw.githubusercontent.com/jcdcdev/Umbraco.Community.SimpleDashboards/v14/docs/screenshot.png)

## Features

- C# dashboard creation
- No javascript or umbraco-package.json files required
- Supports both Views & View Components
- Easy to define section permissions

## Quick Start

### Install Package

```csharp
dotnet add package Umbraco.Community.SimpleDashboards 
```

### Register Dashboard

By default this will display in the content section for Admins only.

```csharp
using Umbraco.Community.SimpleDashboards.Core; 
public class BasicDashboard : SimpleDashboard { }
```

### Create View

- Your view **must** go in `/Views/Dashboard`
- You view **must** be the name of your C# class (without `Dashboard`)
    - For example: `BasicDashboard.cs` => `/Views/Dashboard/Basic.cshtml`

```csharp
@inherits Umbraco.Community.SimpleDashboards.Web.DashboardViewPage

<uui-box headline="Hello Umbraco">
    <p>My Dashboard is: @Model.Dashboard.Alias</p>
</uui-box>
```

### More Examples

[docs/examples.md](https://github.com/jcdcdev/Umbraco.Community.SimpleDashboards/blob/v14/docs/examples.md)

## Contributing

Contributions to this package are most welcome! Please read
the [Contributing Guidelines](https://github.com/jcdcdev/Umbraco.Community.SimpleDashboards/blob/v14/.github/CONTRIBUTING.md).

## Acknowledgments (thanks!)

- LottePitcher - [opinionated-package-starter](https://github.com/LottePitcher/opinionated-package-starter)