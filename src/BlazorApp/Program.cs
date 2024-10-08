using BlazorApp;
using BlazorApp.Services;

using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<HeroImageService>();
builder.Services.AddBlazorise(options => options.Immediate = true)
    .AddBootstrapProviders()
    .AddFontAwesomeIcons();
builder.Services.AddScoped<ProjectService>();


await builder.Build().RunAsync();
