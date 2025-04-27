using FlowCycle.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

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
            options.UseNpgsql(builder.Configuration["ConnectionStrings:Base"]);
        });

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

        app.MapControllers(); // Map all API controllers

        app.Run();
    }
}