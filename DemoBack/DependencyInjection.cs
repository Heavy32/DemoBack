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

            services.AddScoped<IStockItemRepository, StockItemRepository>();

            return services;
        }

        public static IServiceCollection AddAutoMapperProfiles(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(StockProfiles));

            return services;
        }
    }
}

