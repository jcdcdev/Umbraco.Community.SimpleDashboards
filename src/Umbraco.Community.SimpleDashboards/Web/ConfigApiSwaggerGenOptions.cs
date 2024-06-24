using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Umbraco.Community.SimpleDashboards.Core;

namespace Umbraco.Community.SimpleDashboards.Web;

public class ConfigApiSwaggerGenOptions : IConfigureOptions<SwaggerGenOptions>
{
    public void Configure(SwaggerGenOptions options)
    {
        options.SwaggerDoc(
            Constants.Api.ApiName,
            new OpenApiInfo
            {
                Title = "Simple Dashboards Api",
                Version = "Latest",
                Description = "API for Simple Dashboards"
            });
    }
}
