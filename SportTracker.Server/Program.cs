using Microsoft.EntityFrameworkCore;
using SportTracker.Server.Components;
using SportTracker.Server.Models;
using SportTracker.Server.Models.SportEvents;
using SportTracker.Server.Models.Users;
using SportTracker.Server.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents().AddInteractiveServerComponents();

builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<AppDbContext>(opts =>
    opts.UseSqlite("Filename=../DB/SportTrackerServer.sqlite;Mode=ReadWrite;")
);

builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<ISportEventRepository, SportEventRepository>();

builder
    .Services.AddAuthentication()
    .AddCookie(opts =>
    {
        opts.LoginPath = "/login";
        opts.SlidingExpiration = true;
        opts.ExpireTimeSpan = TimeSpan.FromDays(7);
        opts.Cookie.Name = "token";
    });
builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState();

builder.Services.AddHostedService<BackupWorkerService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

app.Run();
