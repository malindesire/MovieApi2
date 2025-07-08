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
