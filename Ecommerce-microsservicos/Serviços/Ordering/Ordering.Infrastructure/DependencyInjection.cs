using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfraStructureServices(this IServiceCollection services, IConfiguration config)
        {
            var connection = config.GetConnectionString("Database");
            // Register application services here
            return services;
        }
    }
}
