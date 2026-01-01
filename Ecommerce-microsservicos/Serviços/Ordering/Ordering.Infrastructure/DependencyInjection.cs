namespace Ordering.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfraStructureServices(this IServiceCollection services, IConfiguration config)
        {
            var connection = config.GetConnectionString("Database");

            services.AddDbContext<ApplicationDbContext>(opt => {
                opt.UseSqlServer(connection);
            });

            return services;
        }
    }
}
