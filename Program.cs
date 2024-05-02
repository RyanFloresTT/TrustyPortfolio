using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TrustyPortfolio.Data;
using TrustyPortfolio.Repositories;
using TrustyPortfolio.Components;
using MudBlazor.Services;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using Microsoft.AspNetCore.Components.Authorization;
using TrustyPortfolio.Components.Account;
using TrustyPortfolio.Models.Domain;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

builder.Services.AddAuthentication(options => {
    options.DefaultScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
})
    .AddIdentityCookies();

builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();


builder.Services.AddDbContext<PortfolioDbContext>(options => {
    options.UseNpgsql(builder.Configuration.GetConnectionString("PortfolioDbConnectionString"));
});

builder.Services.AddDbContext<AuthDbContext>(options => {
    options.UseNpgsql(builder.Configuration.GetConnectionString("AuthDbConnectionString"));
});

builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AuthDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<IBlogRepository, BlogPostRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IImageRepository, CloudinaryImageRepository>();

builder.Services.AddMudServices();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehabior", true);

StaticWebAssetsLoader.UseStaticWebAssets(builder.Environment, builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();
    
app.MapRazorComponents<App>()
   .AddInteractiveServerRenderMode();

app.MapAdditionalIdentityEndpoints();

app.Run();
