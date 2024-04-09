using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TrustyPortfolio.Data;
using TrustyPortfolio.Repositories;
using TrustyPortfolio.Components;
using MudBlazor.Services;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDbContext<PortfolioDbContext>(options => {
    options.UseNpgsql(builder.Configuration.GetConnectionString("PortfolioDbConnectionString"));
});

builder.Services.AddDbContext<AuthDbContext>(options => {
    options.UseNpgsql(builder.Configuration.GetConnectionString("AuthDbConnectionString"));
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AuthDbContext>();

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

app.UseAuthentication();
app.UseAuthorization();

app.Run();
