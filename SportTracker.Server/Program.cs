using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using SportTracker.Client.Pages.Event;
using SportTracker.Server.Components;
using SportTracker.Server.Models;
using SportTracker.Server.Services;
using SportTracker.Shared.Services;

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
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<ISportEventRepository, SportEventRepository>();
builder.Services.AddScoped<ISportEventService, SportEventServerService>();
builder.Services.AddScoped<IJwtService, JwtService>();

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
