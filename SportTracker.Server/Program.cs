using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using SportTracker.Client.Pages;
using SportTracker.Client.Pages.Event;
using SportTracker.Client.Services;
using SportTracker.Client.Shared;
using SportTracker.Server.Components;
using SportTracker.Server.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder
    .Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder
    .Services.AddControllersWithViews()
    .AddJsonOptions(option =>
    {
        option.JsonSerializerOptions.Converters.Add(
            new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
        );
    });

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlite("Filename=../DB/SportTrackerServer.sqlite")
);
builder.Services.AddScoped<ISportEventRepository, SportEventRepository>();

// todo try to remove these
builder.Services.AddScoped<ISportEventService, SportEventService>();
builder.Services.AddScoped<IHttpService, HttpService>();
builder.Services.AddScoped(x =>
{
    var apiUrl = new Uri("http://localhost:5287");
    return new HttpClient() { BaseAddress = apiUrl };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// todo setup http (and disable in dev?)
app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Add).Assembly);

app.MapControllers();

app.Run();
