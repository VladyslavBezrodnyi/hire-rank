using HireRank.Common.Configurations;
using HireRank.Core.Entities;
using HireRank.Core.Store;
using HireRank.Infrastructure.Context;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Runtime.CompilerServices;

namespace HireRank.Infrastructure
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, string connectionString, string migrationsAssembly = "")
        {
            services.AddDbContext<HireRankContext>(options => options.UseSqlServer(connectionString, sql =>
            {
                if (!string.IsNullOrEmpty(migrationsAssembly))
                {
                    sql.MigrationsAssembly(migrationsAssembly);
                }
            }));

            services.AddScoped<IStore, EntityFrameworkStore>();

            return services;
        }

        public static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddIdentity<User, IdentityRole<Guid>>(options =>
                {
                    options.User.RequireUniqueEmail = true;
                    options.Password.RequiredLength = 6;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireDigit = true;
                })
                .AddRoles<IdentityRole<Guid>>()
                .AddEntityFrameworkStores<HireRankContext>()
                .AddDefaultTokenProviders();

            services.AddTokenAuthentication(configuration);

            return services;
        }

        public static void MigrateDb(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            serviceScope.ServiceProvider.GetRequiredService<HireRankContext>().Database.Migrate();
        }

        public static void SeedIdentities(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            var provider = scope.ServiceProvider;
            var context = provider.GetRequiredService<HireRankContext>();
            var userManager = provider.GetRequiredService<UserManager<User>>();
            var roleManager = provider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

            DbSeed.InitializeRolesAsync(userManager, roleManager).GetAwaiter().GetResult();
        }
        private static AuthenticationBuilder AddTokenAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var authConfiguration = new AuthConfiguration(configuration);
            return services
                .AddSingleton(authConfiguration)
                .AddAuthentication(options => {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, jwtBearerOptions => {
                    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = false,
                        IssuerSigningKey = authConfiguration.KEY,

                        ValidateIssuer = false,
                        ValidIssuer = authConfiguration.ISSUER,

                        ValidateAudience = false,
                        ValidAudience = authConfiguration.AUDIENCE,

                        ValidateLifetime = true,

                        ClockSkew = TimeSpan.Zero
                    };
                });
        }
    }
}
