using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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

builder.Services.AddCors();

builder.Services.AddAuthentication();
var key = Encoding.ASCII.GetBytes(
    "50810017cb402db5d4e39d724384cc0dae83fec4b838b796f316a9849ced3639"
); // fixme get something better
builder
    .Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
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

// todo setup https
// app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.UseAuthorization();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Add).Assembly);

app.MapControllers();

app.Run();
