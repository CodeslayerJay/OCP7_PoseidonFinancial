using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Dot.Net.WebApi.Domain;
using WebApi.Data;
using Dot.Net.WebApi;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;

namespace Dot.Net.WebApi.Data
{
    public class LocalDbContext : DbContext
    {

        public LocalDbContext(DbContextOptions<LocalDbContext> options) :base(options)
        { }

        
        public LocalDbContext()
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connString = Startup.IsTesting ?
                    Startup.StaticConfig.GetConnectionString("UAT") :
                    Startup.StaticConfig.GetConnectionString("Referential");

                optionsBuilder.UseSqlServer(connString);
                
            }
        }
        
        public DbSet<CurvePoint> CurvePoints { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<RuleName> RuleNames { get; set; }
        public DbSet<BidList> BidList { get; set; }
        public DbSet<Trade> Trades { get; set; }
        public DbSet<User> Users { get; set;}

        public DbSet<AppLog> Logs { get; set; }

        public DbSet<AccessToken> AccessTokens { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AccessToken>().ToTable("AccessTokens").HasKey(x => x.Id);

            base.OnModelCreating(builder);
        }
    }
}