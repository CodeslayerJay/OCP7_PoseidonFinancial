using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Dot.Net.WebApi.Data;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WebApi.AppUtilities;
using WebApi.Repositories;
using WebApi.Services;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using WebApi.Data;

namespace Dot.Net.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Repositories
            services.AddScoped<IBidListRepository, BidListRepository>();
            services.AddScoped<ICurvePointRepository, CurvePointRepository>();
            services.AddScoped<IRatingRepository, RatingRepository>();
            services.AddScoped<ITradeRepository, TradeRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRuleRepository, RuleRepository>();
            services.AddScoped<IAccessTokenRepository, AccessTokenRepository>();

            // Services
            services.AddScoped<IBidService, BidService>();
            services.AddScoped<ICurveService, CurveService>();
            services.AddScoped<IRatingService, RatingService>();
            services.AddScoped<ITradeService, TradeService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRuleService, RuleService>();

            // Tools
            services.AddScoped(typeof(IAppLogger<>), typeof(AppLogger<>));
            services.AddAutoMapper(typeof(MappingProfile));
            //services.AddMediatR(typeof(Startup).Assembly);


            // Contexts
            services.AddDbContext<LocalDbContext>(opts =>
                opts.UseSqlServer(AppConfig.ApiConnectionString));

            services.AddMvc();
            services.AddControllers();
            services.AddAuthorization();
            
            services.AddAuthentication(opts =>
            {
                opts.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = TokenConfig.ValidIssuer,
                    ValidAudience = TokenConfig.ValidAudience,
                    IssuerSigningKey = TokenConfig.GetKey(),
                    ClockSkew = TokenConfig.SkewTime,

                    // security switches
                    RequireExpirationTime = true,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = true
                };
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            SeedData.Initialize(app.ApplicationServices);
        }
    }
}
