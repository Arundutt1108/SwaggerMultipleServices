using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Add Swagger services (but no controllers, we'll use YAML files)
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Serve static files (YAML files from wwwroot/swagger)
app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    // Enable Swagger middleware
    app.UseSwagger();
    
    // Configure Swagger UI
    app.UseSwaggerUI(c =>
    {
        // Add endpoints for the JSON files (each service)
        c.SwaggerEndpoint("serviceA.json", "Service A API");
        c.SwaggerEndpoint("serviceB.json", "Service B API");

        // Customize the UI (optional)
        c.EnableDeepLinking();
        c.DisplayOperationId();
    });
}

app.UseAuthorization();

app.MapControllers();

app.Run();