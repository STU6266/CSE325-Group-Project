using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ReadingTracker.Components;
using ReadingTracker.Components.Account;
using ReadingTracker.Data;
using ReadingTracker.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

// Use persistent storage when running in a container.
var persistentDataPath = builder.Configuration["PersistentDataPath"];

if (!builder.Environment.IsDevelopment() &&
    string.IsNullOrWhiteSpace(persistentDataPath))
{
    throw new InvalidOperationException(
        "PersistentDataPath must be configured in production.");
}

string connectionString;

if (string.IsNullOrWhiteSpace(persistentDataPath))
{
    connectionString = builder.Configuration
        .GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException(
            "DefaultConnection not found.");
}
else
{
    Directory.CreateDirectory(persistentDataPath);

    var keysPath = Path.Combine(persistentDataPath, "keys");
    Directory.CreateDirectory(keysPath);

    builder.Services.AddDataProtection()
        .SetApplicationName("ReadingTracker")
        .PersistKeysToFileSystem(new DirectoryInfo(keysPath));

    connectionString =
        $"Data Source={Path.Combine(persistentDataPath, "app.db")};Cache=Shared";
}
builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = true;
        options.Stores.SchemaVersion = IdentitySchemaVersions.Version3;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

// Use sample books during local frontend development.
if (builder.Environment.IsDevelopment() &&
    builder.Configuration.GetValue("UseMockBookService", true))
{
    builder.Services.AddScoped<IBookService, MockBookService>();
}
else
{
    builder.Services.AddScoped<IBookService, BookService>();
}

var app = builder.Build();

// Apply existing migrations on a single production instance.
if (!app.Environment.IsDevelopment())
{
    await using var scope = app.Services.CreateAsyncScope();

    var db = scope.ServiceProvider
        .GetRequiredService<ApplicationDbContext>();

    await db.Database.MigrateAsync();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.Run();
