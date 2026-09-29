using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyBackendAPI.Data;
using MyBackendAPI.Models;
using MyBackendAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// ✅ Render (and most free PaaS hosts) inject the port to listen on via $PORT
var renderPort = Environment.GetEnvironmentVariable("PORT");
if (!string.IsNullOrEmpty(renderPort))
{
    builder.WebHost.UseUrls($"http://0.0.0.0:{renderPort}");
}

// ✅ MySQL Configuration
// Accepts either the ADO.NET format ("server=...;port=...;database=...;user=...;password=...;")
// or a URI-style connection string ("mysql://user:pass@host:port/db"), which is what
// Railway (and some other hosts) hand you by default.
static string NormalizeMySqlConnectionString(string raw)
{
    if (string.IsNullOrWhiteSpace(raw))
    {
        throw new InvalidOperationException(
            "ConnectionStrings:DefaultConnection is empty. Set the ConnectionStrings__DefaultConnection " +
            "environment variable on the host to your MySQL connection details.");
    }

    if (!raw.StartsWith("mysql://", StringComparison.OrdinalIgnoreCase))
    {
        return raw;
    }

    var uri = new Uri(raw);
    var userInfo = uri.UserInfo.Split(':', 2);
    var database = uri.AbsolutePath.TrimStart('/');
    return $"server={uri.Host};port={uri.Port};database={database};user={userInfo[0]};password={userInfo[1]};";
}

var connectionString = NormalizeMySqlConnectionString(
    builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        connectionString,
        new MySqlServerVersion(new Version(8, 0, 34))
    )
);

// ✅ Enable Controllers and Swagger
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
        options.JsonSerializerOptions.WriteIndented = true;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ✅ Auth: JWT bearer tokens issued by AuthController, verified here
builder.Services.AddSingleton<JwtTokenService>();

var jwtKey = builder.Configuration["Jwt:Key"];
if (!string.IsNullOrEmpty(jwtKey) && Encoding.UTF8.GetByteCount(jwtKey) >= 32)
{
    var jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? "DrdoProjectSample";
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtIssuer,
                ValidateAudience = true,
                ValidAudience = jwtIssuer,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromMinutes(1),
            };
        });
}
// If Jwt:Key isn't configured (or is too short), AddAuthentication is
// skipped entirely — [Authorize] endpoints then correctly 500 with a clear
// "no authentication handler configured" error instead of silently
// accepting unsigned/unverifiable tokens.

builder.Services.AddAuthorization();

// ✅ ENABLE CORS: Allow any frontend (frontend: localhost:5500, etc.)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// ✅ Apply pending EF Core migrations automatically on startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();

    // Optional one-time admin seed — set both SEED_ADMIN_EMAIL and
    // SEED_ADMIN_PASSWORD as environment variables on the host to create an
    // initial account (only when the Users table is still empty; never
    // overwrites or resets an existing user). Deliberately not hardcoded
    // here — this repo is public, and even a "demo" password shouldn't be
    // committed to it.
    var seedEmail = Environment.GetEnvironmentVariable("SEED_ADMIN_EMAIL");
    var seedPassword = Environment.GetEnvironmentVariable("SEED_ADMIN_PASSWORD");
    if (!string.IsNullOrWhiteSpace(seedEmail) && !string.IsNullOrWhiteSpace(seedPassword) && !db.Users.Any())
    {
        db.Users.Add(new User
        {
            Email = seedEmail.Trim().ToLowerInvariant(),
            PasswordHash = PasswordHasher.Hash(seedPassword),
            Role = "Admin",
        });
        db.SaveChanges();
    }
}

// ✅ Swagger only in development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Render terminates TLS at its edge, so redirecting to https inside the
// container would loop; only force it for local development.
if (string.IsNullOrEmpty(renderPort))
{
    app.UseHttpsRedirection();
}
app.UseStaticFiles();

// ✅ Add this before MapControllers()
app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

// ✅ Your controller mapping
app.MapControllers();

app.Run();
