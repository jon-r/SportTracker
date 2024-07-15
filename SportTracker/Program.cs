using Microsoft.EntityFrameworkCore;
using SportTracker.Client.Pages;
using SportTracker.Components;
using SportTracker.Server.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlite("Filename=../DB/SportTrackerServer.sqlite"));
builder.Services.AddScoped<ISportEventRepository, SportEventRepository>();

var app = builder.Build();

// configure sqlite db TODO is this needed?
// using var scope = app.Services.CreateScope())
// {
//     var services = scope.ServiceProvider;

//     try
//     {
//         var appDbContext = services.GetRequiredService<AppDbContext>();

//     }
//     catch (Exception ex)
//     {
//         var appDbContext = services.GetRequiredService<ILogger<Program>>();
//         logger.LogError()
//     }
// }

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

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Home).Assembly);

app.MapControllers();

app.Run();
