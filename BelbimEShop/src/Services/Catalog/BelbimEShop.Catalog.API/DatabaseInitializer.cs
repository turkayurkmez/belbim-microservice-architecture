using BelbimEShop.Catalog.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace BelbimEShop.Catalog.API
{
    public static class DatabaseInitializer 
    {
        public static async Task InitializeAsync(IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<CatalogDbContext>();
                await context.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred while seeding the database.");
            }
        }
    }
}
