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
using FlowCycle.Domain.Storage;
using FlowCycle.Domain.Costing;
using FlowCycle.Persistance.Repositories.Models;

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
            services.AddScoped<IStorageItemRepository, StorageItemRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();

            // Costing repositories
            services.AddScoped<ICostingMaterialRepository, CostingMaterialRepository>();
            services.AddScoped<ICostingRepository, CostingRepository>();
            services.AddScoped<ICostingLaborRepository, CostingLaborRepository>();
            services.AddScoped<ICostingOverheadRepository, CostingOverheadRepository>();
            services.AddScoped<ICostingMaterialTypeRepository, CostingMaterialTypeRepository>();

            // Unit of Work
            services.AddScoped<FlowCycle.Persistance.UnitOfWork.IUnitOfWork, FlowCycle.Persistance.UnitOfWork.UnitOfWork>();

            return services;
        }

        // Register all business/service layer classes (ending with Service)
        public static IServiceCollection AddBusinessServiceLayer(this IServiceCollection services)
        {
            services.AddScoped<IStorageItemService, StorageItemService>();
            services.AddScoped<IStorageItemImportService, StorageItemImportService>();
            services.AddScoped<IStorageItemExportService, StorageItemExportService>();

            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<ISupplierService, SupplierService>();

            // Costing services
            services.AddScoped<ICostingMaterialService, CostingMaterialService>();
            services.AddScoped<ICostingService, CostingService>();
            services.AddScoped<ICostingLaborService, CostingLaborService>();
            services.AddScoped<ICostingOverheadService, CostingOverheadService>();
            services.AddScoped<ICostingImportService, CostingImportService>();
            services.AddScoped<ICostingMaterialTypeService, CostingMaterialTypeService>();

            return services;
        }

        public static IServiceCollection AddAutoMapperProfiles(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(StorageProfiles));

            return services;
        }
    }
}

