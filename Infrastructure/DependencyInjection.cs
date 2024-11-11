using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Caching.Distributed;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastucture(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<WarehouseDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("AppDatabase")));

            services.AddSingleton<IDistributedCache, RedisCache>();
            services.AddStackExchangeRedisCache(options => {
                options.Configuration = configuration.GetSection("RedisCacheOption:Configuration").Value;
                options.InstanceName = configuration.GetSection("RedisCacheOption:InstanceName").Value; ;
            });

            return services;
        }

    }
}
