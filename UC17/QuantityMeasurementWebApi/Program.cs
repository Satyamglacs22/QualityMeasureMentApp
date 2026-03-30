using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using QuantityMeasurementAppRepositories.Context;
using QuantityMeasurementAppRepositories.Interfaces;
using QuantityMeasurementAppRepositories.Repositories;
using QuantityMeasurementAppServices.Interfaces;
using QuantityMeasurementAppServices.Services;
using QuantityMeasurementWebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

// ================== DATABASE ==================
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ================== DEPENDENCY INJECTION ==================
builder.Services.AddScoped<IQuantityRecordRepository, QuantityRecordRepository>();
builder.Services.AddScoped<IQuantityMeasurementService, QuantityMeasurementServiceImpl>();
builder.Services.AddScoped<IQuantityWebService, QuantityWebServiceImpl>();

// ================== CONTROLLERS + GLOBAL EXCEPTION HANDLER ==================
builder.Services.AddControllers(options =>
{
    options.Filters.Add<GlobalExceptionHandler>();
})
.ConfigureApiBehaviorOptions(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        IEnumerable<string> errors = context.ModelState.Values
            .SelectMany(v => v.Errors)
            .Select(e => e.ErrorMessage);

        var response = new
        {
            Timestamp = DateTime.UtcNow.ToString("o"),
            Status    = 400,
            Error     = "Validation Failed",
            Message   = string.Join("; ", errors),
            Path      = context.HttpContext.Request.Path.ToString()
        };

        return new Microsoft.AspNetCore.Mvc.BadRequestObjectResult(response);
    };
});

// ================== SWAGGER / OPENAPI ==================
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title       = "Quantity Measurement API",
        Version     = "v1",
        Description = "REST API for quantity measurement operations — UC17"
    });

    string xmlFile = Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory,
        "QuantityMeasurementAppWebAPI.xml");

    if (File.Exists(xmlFile))
        options.IncludeXmlComments(xmlFile);
});

// ================== HEALTH CHECKS ==================
builder.Services.AddHealthChecks();

var app = builder.Build();

// ================== AUTO MIGRATION ==================
using (IServiceScope scope = app.Services.CreateScope())
{
    AppDbContext db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

// ================== MIDDLEWARE PIPELINE ==================
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Quantity Measurement API v1");
    c.RoutePrefix = "swagger";
});

app.MapControllers();
app.MapHealthChecks("/health");

app.Run();

public partial class Program { }