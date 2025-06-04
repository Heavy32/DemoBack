using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlowCycle.Api.Models.Storage.MapProfiles;
using FlowCycle.Persistance;
using FlowCycle.Persistance.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FlowCycle.Domain.Stock;

namespace FlowCycle.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrustructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(configuration["ConnectionStrings:Base"]);
            });

            // Repository registrations
            services.AddScoped<IStockItemRepository, StockItemRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();

            return services;
        }

        // Register all business/service layer classes (ending with Service)
        public static IServiceCollection AddBusinessServiceLayer(this IServiceCollection services)
        {
            services.AddScoped<IStorageItemService, StorageItemService>();
            services.AddScoped<IStockItemImportService, StockItemImportService>();
            return services;
        }

        public static IServiceCollection AddAutoMapperProfiles(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(StockProfiles));

            return services;
        }
    }
}

