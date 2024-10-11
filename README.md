# SwaggerMultipleServices
README: .NET 8 Application with Swagger UI Without Controllers
This guide will help you create a .NET 8 application that supports Swagger UI without using traditional controllers. The Swagger UI will include a dropdown to select a service, which will be rendered from local JSON files.

Prerequisites:

.NET 8 SDK
A code editor, such as Visual Studio Code or Visual Studio
1. Create a New .NET 8 Web Application
To begin, create a new .NET 8 web application using the command line:
dotnet new web -n SwaggerWithoutControllers
Navigate to the project directory:
cd SwaggerWithoutControllers

2. Add Swagger and OpenAPI Dependencies - You need to add the necessary NuGet packages to support Swagger:
dotnet add package Swashbuckle.AspNetCore

3. Modify Program.cs to Support Swagger
We’ll now configure Swagger in the Program.cs file, but since we won’t be using controllers, we’ll generate the Swagger documentation from local JSON files.

Open the Program.cs file and add the following code:
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Add Swagger services (but no controllers, we'll use JSON files)
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Serve static files (JSON files from wwwroot/swagger)
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

4. Prepare Swagger JSON Files
Next, we need to provide the JSON files that define the Swagger documentation for each service. Create a wwwroot/swagger folder in your project structure to store these files:
SwaggerWithoutControllers
│
└───wwwroot
    └───swagger
        ├───service1.json
        └───service2.json
        
Ensure that each json file follows the OpenAPI specification. You can either generate these files from existing APIs or manually write them.

For example, the content of swagger/serviceA.json might look like:
{
  "openapi": "3.0.0",
  "info": {
    "title": "Service 1 API",
    "version": "v1"
  },
  "paths": {
    "/endpoint1": {
      "get": {
        "summary": "Get data from endpoint 1",
        "responses": {
          "200": {
            "description": "Successful response"
          }
        }
      }
    }
  }
}
5. Enable Static File Serving
To serve the JSON files, you need to ensure that static file serving is enabled in your app. This is done by adding app.UseStaticFiles() in the Program.cs file, which we have already done in Step 3.

6. Run the Application
Run the application using the following command:
dotnet run
Once the application starts, navigate to http://localhost:5138/swagger. You should see the Swagger UI with a dropdown that lets you select between different services, and each will load its documentation from the corresponding JSON file.

7. Customizing Swagger UI
You can further customize the Swagger UI by passing additional configuration options through the UseSwaggerUI method. For example, you can set the default UI theme, customize supported HTTP methods, or add additional functionality.

8. Publish the Application (Optional)
If you want to publish the application, use the following command:
dotnet publish -c Release

Conclusion:
You have now successfully created a .NET 8 application that supports Swagger UI without controllers. The Swagger UI allows users to select different services from a dropdown, and each service's documentation is loaded from separate JSON files. This structure allows for a flexible API documentation solution where services are decoupled from the traditional controller setup.