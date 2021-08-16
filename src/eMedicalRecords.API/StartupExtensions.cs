using System;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Npgsql;

namespace eMedicalRecords.API
{
    using Projections;
    using Infrastructure;
    using Infrastructure.Configurations;
    using Infrastructure.Securities;
    
    public static class StartupExtensions
    {
        public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var dbConfiguration = configuration.Get<DbConfiguration>();
            var builder = new NpgsqlConnectionStringBuilder()
            {
                Host = dbConfiguration.PostgresHostname,
                Database = dbConfiguration.PostgresDatabase,
                Username = dbConfiguration.PostgresUsername,
                Password = dbConfiguration.PostgresPassword,
                Port = int.Parse(dbConfiguration.PostgresPort)
            };
            
            services.AddDbContext<MedicalRecordContext>(options =>
            {
                options.UseNpgsql(builder.ConnectionString, sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                    sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorCodesToAdd: null!);
                });
            });

            return services;
        }

        public static IServiceCollection AddCustomMvc(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers()
                .AddNewtonsoftJson();
            return services;
        } 

        public static IServiceCollection AddCustomHealthCheck(this IServiceCollection services,
            IConfiguration configuration)
        {
            return services;
        }

        public static IServiceCollection AddCustomHostedService(this IServiceCollection services)
        {
            services.AddHostedService<ProjectionStartupService>();
            return services;
        }
        
        public static IServiceCollection AddCustomConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();
            services.Configure<DbConfiguration>(configuration);
            services.Configure<TemplateDatabaseSettings>(configuration.GetSection(nameof(TemplateDatabaseSettings)));
            
            services.AddSingleton<ITemplateDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<TemplateDatabaseSettings>>().Value);
            
            return services;
        }
        

        public static IServiceCollection AddJwt(this IServiceCollection services)
        {
            services.AddOptions();

            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("somethinglongerforthisdumbalgorithmisrequired"));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            var issuer = "issuer";
            var audience = "audience";

            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = issuer;
                options.Audience = audience;
                options.SigningCredentials = signingCredentials;
            });

            var tokenValidationParameters = new TokenValidationParameters
            {
                // The signing key must match!
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingCredentials.Key,
                // Validate the JWT Issuer (iss) claim
                ValidateIssuer = true,
                ValidIssuer = issuer,
                // Validate the JWT Audience (aud) claim
                ValidateAudience = true,
                ValidAudience = audience,
                // Validate the token expiry
                ValidateLifetime = true,
                // If you want to allow a certain amount of clock drift, set that here:
                ClockSkew = TimeSpan.FromHours(24),
                
            };
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = tokenValidationParameters;
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = (context) =>
                        {
                            var token = context.HttpContext.Request.Headers["Authorization"];
                            if (token.Count > 0 && token[0].StartsWith("Token ", StringComparison.OrdinalIgnoreCase))
                            {
                                context.Token = token[0].Substring("Token ".Length).Trim();
                            }

                            return Task.CompletedTask;
                        }
                    };

                });
            return services;
        }
    }
}