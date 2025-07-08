using Microsoft.EntityFrameworkCore;
using MovieApi.Data;
using System.Diagnostics;

namespace MovieApi.Extensions
{
    public static class ApplicationExtensions
    {
        public static async Task SeedDataAsync(this IApplicationBuilder app) 
        {
            using (var Scope = app.ApplicationServices.CreateScope())
            {
                var serviceProvider = Scope.ServiceProvider;
                var context = serviceProvider.GetRequiredService<MovieContext>();

                // await context.Database.EnsureDeletedAsync();
                // await context.Database.MigrateAsync();

                try
                {
                    await SeedData.InitAsync(context);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                    throw;
                }
            }
        }
    }
}
