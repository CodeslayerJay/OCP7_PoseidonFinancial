using System;
using System.Collections.Generic;
using System.Text;

namespace PoseidonFinancial.Testing
{
    //public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    //{
    //    protected override void ConfigureWebHost(IWebHostBuilder builder)
    //    {
    //        builder.ConfigureServices(services =>
    //        {
    //            // Create a new service provider.
    //            var serviceProvider = new ServiceCollection()
    //                .AddEntityFrameworkInMemoryDatabase()
    //                .BuildServiceProvider();

    //            // Add a database context using an in-memory 
    //            // database for testing.
    //            services.AddDbContext<P3Referential>(options =>
    //            {
    //                options.UseInMemoryDatabase("InMemoryDbForTesting");
    //                options.UseInternalServiceProvider(serviceProvider);
    //            });

    //            // Build the service provider.
    //            var sp = services.BuildServiceProvider();

    //            // Create a scope to obtain a reference to the database
    //            // context (P3Referential).
    //            using (var scope = sp.CreateScope())
    //            {
    //                var scopedServices = scope.ServiceProvider;
    //                var db = scopedServices.GetRequiredService<P3Referential>();
    //                var logger = scopedServices
    //                    .GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

    //                // Ensure the database is created.
    //                db.Database.EnsureCreated();

    //                // Seed our db with test data
    //                try
    //                {

    //                    SeedData.Initialize(scopedServices);
    //                }
    //                catch (Exception ex)
    //                {

    //                    logger.LogError(ex, "An error occurred seeding the DB.");
    //                }
    //            }
    //        });
    //    }
    //}
}
