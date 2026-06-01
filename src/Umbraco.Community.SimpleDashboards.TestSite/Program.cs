var builder = WebApplication.CreateBuilder(args);

// register test-site helpers / sample data generators
builder.Services.AddHttpClient();
builder.Services.AddMemoryCache();
builder.Services.AddTransient<Umbraco.Community.SimpleDashboards.TestSite.Helpers.ISavedFormGenerator, Umbraco.Community.SimpleDashboards.TestSite.Helpers.SavedFormGenerator>();

builder.CreateUmbracoBuilder()
    .AddBackOffice()
    .AddWebsite()
    .AddDeliveryApi()
    .AddComposers()
    .Build();

var app = builder.Build();

await app.BootUmbracoAsync();

app.UseUmbraco()
    .WithMiddleware(u =>
    {
        u.UseBackOffice();
        u.UseWebsite();
    })
    .WithEndpoints(u =>
    {
        u.UseBackOfficeEndpoints();
        u.UseWebsiteEndpoints();
    });

await app.RunAsync();
