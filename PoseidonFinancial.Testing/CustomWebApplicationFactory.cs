using Dot.Net.WebApi.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PoseidonFinancial.Testing
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Create a new service provider.
                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();


                // Remove the default db context registration
                var defaultContext = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<LocalDbContext>));

                if (defaultContext != null)
                {
                    services.Remove(defaultContext);
                }

                // Add a new database context using an in-memory database for testing.
                services.AddDbContext<LocalDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                    options.UseInternalServiceProvider(serviceProvider);
                });

                // Build the service provider.
                var sp = services.BuildServiceProvider();

                // Create a scope to obtain a reference to the database
                // context (LocalDbContext).
                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<LocalDbContext>();
                    var logger = scopedServices
                        .GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();
                    
                    // Ensure the database is created.
                    db.Database.EnsureCreated();

                    
                    // Seed our db with test data
                    try
                    {
                        SeedData.Initialize(db);
                    }
                    catch (Exception ex)
                    {

                        logger.LogError(ex, "An error occurred seeding the DB.");
                    }
                }
            });
        }
    }
}
