# Umbraco.Community.SimpleDashboards

[![Umbraco Version](https://img.shields.io/badge/Umbraco-10.4+-%233544B1?style=flat&logo=umbraco)](https://umbraco.com/products/umbraco-cms/)
[![NuGet](https://img.shields.io/nuget/vpre/Umbraco.Community.SimpleDashboards?color=0273B3)](https://www.nuget.org/packages/Umbraco.Community.SimpleDashboards)
[![GitHub license](https://img.shields.io/github/license/jcdcdev/Umbraco.Community.SimpleDashboards?color=8AB803)](https://github.com/jcdcdev/Umbraco.Community.SimpleDashboards/blob/main/LICENSE)
[![Downloads](https://img.shields.io/nuget/dt/Umbraco.Community.SimpleDashboards?color=cc9900)](https://www.nuget.org/packages/Umbraco.Community.SimpleDashboards/)

This packages aims to help developers quickly put together Umbraco Dashboards using C# only.

![Basic Dashboard in the Umbraco Office](https://raw.githubusercontent.com/jcdcdev/Umbraco.Community.SimpleDashboards/main/docs/screenshot.png)

## Features

- Simplifies C# based dashboard creation
- Supports both Views & View Components
- No package.manifest or lang/lang.xml files required!
- Variant support (culture specific names)
- Easy to define Access Rules

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

<h1>Hello Umbraco</h1>
<p>My Dashboard alias is: @Model.Dashboard.Alias</p>
```
### More Examples

[docs/examples.md](https://github.com/jcdcdev/Umbraco.Community.SimpleDashboards/blob/dev/docs/examples.md)

## Contributing

Contributions to this package are most welcome! Please read the [Contributing Guidelines](CONTRIBUTING.md).

## Acknowledgments (thanks!)

- LottePitcher - [opinionated-package-starter](https://github.com/LottePitcher/opinionated-package-starter)