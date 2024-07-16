using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SportTracker.Client.Services;
using SportTracker.Client.Shared;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddScoped<ISportEventService, SportEventService>();
builder.Services.AddScoped<IHttpService, HttpService>();
builder.Services.AddScoped(x =>
{
    var apiUrl = new Uri("http://localhost:5287");
    return new HttpClient() { BaseAddress = apiUrl };
});

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

await builder.Build().RunAsync();
