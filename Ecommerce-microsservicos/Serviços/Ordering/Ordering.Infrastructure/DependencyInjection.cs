namespace Ordering.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfraStructureServices(this IServiceCollection services, IConfiguration config)
        {
            var connection = config.GetConnectionString("Database");

            services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
            services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

            services.AddDbContext<ApplicationDbContext>((sp, opt) => {
                opt.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                opt.UseSqlServer(connection);
            });

            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

            return services;
        }
    }
}
