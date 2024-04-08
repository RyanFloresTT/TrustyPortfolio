using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TrustyPortfolio.Data;
using TrustyPortfolio.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<PortfolioDbContext>(options =>
    options.UseNpgsql(builder.Configuration["PortfolioDbConnectionString"]));

builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseNpgsql(builder.Configuration["AuthDbConnectionString"]));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AuthDbContext>();

builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<IBlogRepository, BlogPostRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IImageRepository, CloudinaryImageRepository>();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehabior", true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsProduction()) {
    // Production-specific configurations
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
} else {
    // Development-specific configurations
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
