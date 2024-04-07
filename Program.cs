using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TrustyPortfolio.Data;
using TrustyPortfolio.Repositories;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Azure.Core;

var builder = WebApplication.CreateBuilder(args);



string currentEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

string authDbConnectionString;
string portfolioDbConnectionString;

if (string.Equals("Development", currentEnvironment)) {
    authDbConnectionString = builder.Configuration["AuthDbConnectionString"];
    portfolioDbConnectionString = builder.Configuration["PortfolioDbConnectionString"];
} else {
    SecretClientOptions options = new SecretClientOptions() {
        Retry =
        {
            Delay= TimeSpan.FromSeconds(2),
            MaxDelay = TimeSpan.FromSeconds(16),
            MaxRetries = 5,
            Mode = RetryMode.Exponential
         }
    };
    var client = new SecretClient(
    new Uri("https://trustyportfoliovault.vault.azure.net/"),
    new DefaultAzureCredential(), options);

    var temp = await client.GetSecretAsync("PortfolioAuthDbConnectionString");
    authDbConnectionString = temp.Value.Value;
    temp = await client.GetSecretAsync("PortfolioDbConnectionString");
    portfolioDbConnectionString = temp.Value.Value;
}




// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<PortfolioDbContext>(options =>
    options.UseNpgsql(portfolioDbConnectionString));

builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseNpgsql(authDbConnectionString));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AuthDbContext>();

builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<IBlogRepository, BlogPostRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IImageRepository, CloudinaryImageRepository>();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

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
