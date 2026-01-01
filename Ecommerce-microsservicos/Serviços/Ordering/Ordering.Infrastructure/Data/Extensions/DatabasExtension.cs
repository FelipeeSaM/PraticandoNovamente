namespace Ordering.Infrastructure.Data.Extensions
{
    public static class DatabasExtension
    {
        public static async Task InitialDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            dbContext.Database.MigrateAsync().GetAwaiter().GetResult();
        }
    }
}
