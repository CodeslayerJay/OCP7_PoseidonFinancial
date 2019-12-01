using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Dot.Net.WebApi.Domain;

namespace Dot.Net.WebApi.Data
{
    public class LocalDbContext : DbContext
    {

        public LocalDbContext() { }
        public LocalDbContext(DbContextOptions<LocalDbContext> options) :base(options)
        { }

        public DbSet<CurvePoint> CurvePoints { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<RuleName> RuleNames { get; set; }
        public DbSet<BidList> BidList { get; set; }
        public DbSet<Trade> Trades { get; set; }
        public DbSet<User> Users { get; set;}

        public DbSet<Log> AppLogs { get; set; }
        public DbSet<LogType> LogTypes { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Override the default EF table mapping configuration
            builder.Entity<BidList>().ToTable("BidList");
            builder.Entity<CurvePoint>().ToTable("CurvePoint");
            builder.Entity<Rating>().ToTable("Rating");
            builder.Entity<Trade>().ToTable("Trade");
            builder.Entity<RuleName>().ToTable("RuleName");
            builder.Entity<User>().ToTable("Users");
        }
    }
}