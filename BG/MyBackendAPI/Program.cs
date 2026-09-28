using Microsoft.EntityFrameworkCore;
using MyBackendAPI.Data;

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
    scope.ServiceProvider.GetRequiredService<ApplicationDbContext>().Database.Migrate();
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

app.UseAuthorization();

// ✅ Your controller mapping
app.MapControllers();

app.Run();
