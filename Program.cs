using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using suprimmil;
using suprimmil.Context;
using suprimmil.Models;
using suprimmil.Services;

var builder = WebApplication.CreateBuilder(args);

// Porta (Render)
var port = Environment.GetEnvironmentVariable("PORT");
if (!string.IsNullOrEmpty(port))
{
    builder.WebHost.ConfigureKestrel(options =>
    {
        options.ListenAnyIP(int.Parse(port));
    });
}

// Config
static void ConfigureEnvironmentVariables(ConfigurationManager configuration)
{
    Settings.DefaultDbConnection = configuration.GetConnectionString("DefaultDbConnection") ?? "";
}

ConfigureEnvironmentVariables(builder.Configuration);

// DbContext + retry (IMPORTANTE)
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var conn = builder.Configuration.GetConnectionString("DefaultDbConnection");

    options.UseNpgsql(conn, o =>
    {
        o.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(10),
            errorCodesToAdd: null
        );
    });
});

// Identity
builder.Services.AddIdentity<User, IdentityRole<int>>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;

    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

// Cookie
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/login";
    options.AccessDeniedPath = "/access-denied";
    options.Cookie.Name = "suprimmil.auth";
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromHours(8);
    options.SlidingExpiration = true;

    if (builder.Environment.IsDevelopment())
    {
        options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
    }
    else
    {
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    }
});

// Razor / Blazor
builder.Services.AddRazorComponents().AddInteractiveServerComponents();
builder.Services.AddRazorPages();

// Antiforgery
builder.Services.AddAntiforgery(options =>
{
    options.HeaderName = "RequestVerificationToken";
});

// Auth / policies
builder.Services.AddScoped<IUserClaimsPrincipalFactory<User>, AppUserClaimsPrincipalFactory>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy =>
        policy.RequireClaim("IsAdmin", "true")
    );
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddCascadingAuthenticationState();

// Services
builder.Services.AddScoped<ToastService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// MIGRATION WITH RETRY
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    var retries = 5;

    while (retries > 0)
    {
        try
        {
            Console.WriteLine("🚀 Applying migrations...");
            dbContext.Database.Migrate();
            Console.WriteLine("✅ Migrations applied!");
            break;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error applying migrations: {ex.Message}");
            retries--;
            Thread.Sleep(5000);
        }
    }
}

// Pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorPages();
app.MapRazorComponents<suprimmil.Components.App>().AddInteractiveServerRenderMode();

app.Run();