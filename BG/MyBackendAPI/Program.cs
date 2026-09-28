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
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
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
