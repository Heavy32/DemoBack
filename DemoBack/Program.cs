using FlowCycle.Persistance;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using FlowCycle.Api;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container
        builder.Services.AddControllers(); // For API controllers only

        // Register infrastructure and business services
        builder.Services.AddInfrustructureLayer(builder.Configuration);
        builder.Services.AddBusinessServiceLayer();
        builder.Services.AddAutoMapperProfiles();

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

        // Enable Swagger
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "FlowCycle API v1");
            c.RoutePrefix = string.Empty; // Make Swagger UI the root page
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