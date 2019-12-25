using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Dot.Net.WebApi.Domain;
using WebApi.Data;

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


        public DbSet<AccessToken> AccessTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AccessToken>().ToTable("AccessTokens").HasNoKey();
        }
    }
}