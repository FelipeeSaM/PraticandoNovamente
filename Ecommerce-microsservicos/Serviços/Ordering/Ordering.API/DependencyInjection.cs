namespace Ordering.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Register application services here
            return services;
        }

        public static WebApplication UseApiServices(this WebApplication app)
        {
            // Configure middleware and endpoints here
            return app;
        }
    }
}
