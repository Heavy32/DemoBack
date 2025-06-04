using FlowCycle.Persistance;
using FlowCycle.Domain.Stock;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Diagnostics;
using System.Reflection;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Http;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container
        builder.Services.AddControllers(); // For API controllers only

        // Add DbContext
        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            var connectionString = builder.Configuration["ConnectionStrings:Base"];
            Console.WriteLine($"Using connection string: {connectionString}");
            options.UseNpgsql(connectionString);
        });

        // Register services
        builder.Services.AddScoped<IStockItemImportService, StockItemImportService>();
        builder.Services.AddScoped<IStockItemExportService, StockItemExportService>();

        // Configure Swagger
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "FlowCycle API",
                Version = "v1",
                Description = "API for managing storage items"
            });

            // Configure file upload support
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            // Add support for file uploads
            //c.OperationFilter<FileUploadOperationFilter>();

            // Ensure all controllers are discovered
            c.DocInclusionPredicate((docName, apiDesc) => true);
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();

        // Enable Swagger with detailed error handling
        app.UseSwagger(c =>
        {
            c.SerializeAsV2 = false;
        });

        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "FlowCycle API v1");
            c.RoutePrefix = string.Empty; // Make Swagger UI the root page
            c.EnableDeepLinking();
            c.DisplayRequestDuration();
        });

        // Add global exception handling
        app.UseExceptionHandler(errorApp =>
        {
            errorApp.Run(async context =>
            {
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";
                var error = context.Features.Get<IExceptionHandlerFeature>();
                if (error != null)
                {
                    var ex = error.Error;
                    await context.Response.WriteAsJsonAsync(new { error = ex.Message, stackTrace = ex.StackTrace });
                }
            });
        });

        app.MapControllers(); // Map all API controllers

        app.Run();
    }
}