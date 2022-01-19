using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using VDT.TransactionDaily.API.BL.Implements;
using VDT.TransactionDaily.API.BL.Interfaces;
using VDT.TransactionDaily.API.BLCore.Implements;
using VDT.TransactionDaily.API.BLCore.Interfaces;
using VDT.TransactionDaily.API.JWT;
using VDT.TransactionDaily.API.Models;
using VDT.TransactionDaily.API.Models.Others;

namespace VDT.TransactionDaily.API.Extensions
{
    public static class ConfigServicesExtensions
    {
        public static void AddJWTTokenServices(this IServiceCollection Services, IConfiguration Configuration)
        {
            // Add Jwt Setings
            var bindJwtSettings = new JwtSettings();
            Configuration.Bind("JsonWebTokenKeys", bindJwtSettings);
            Services.AddSingleton(bindJwtSettings);
            Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = bindJwtSettings.ValidateIssuerSigningKey,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(bindJwtSettings.IssuerSigningKey)),
                    ValidateIssuer = bindJwtSettings.ValidateIssuer,
                    ValidIssuer = bindJwtSettings.ValidIssuer,
                    ValidateAudience = bindJwtSettings.ValidateAudience,
                    ValidAudience = bindJwtSettings.ValidAudience,
                    RequireExpirationTime = bindJwtSettings.RequireExpirationTime,
                    ValidateLifetime = bindJwtSettings.RequireExpirationTime,
                    ClockSkew = TimeSpan.FromDays(1),
                };
            });
        }

        public static void AddInjectionServices(this IServiceCollection Services, ConfigurationManager Configuration)
        {
            Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            Services.AddTransient<IConfigService, ConfigService>();
            Services.AddScoped<IHttpContextService, HttpContextService>();
            Services.AddScoped<IAuthService, AuthService>();

            Services.AddDbContext<VdtContext>
                (options => options
                            .UseMySql(Configuration.GetConnectionString("Database"),
                                ServerVersion.Parse(Configuration.GetConnectionString("Version")), options =>
                                {
                                    options.EnableStringComparisonTranslations();
                                })
                            .EnableServiceProviderCaching()
                            .EnableSensitiveDataLogging()
                );
            Services.AddTransient<CoreService, CoreService>();

            Services.AddTransient(typeof(IBaseBL<>), typeof(BaseBL<>));
            Services.AddTransient<IUserBL, UserBL>();
            Services.AddTransient<IProductBL, ProductBL>();
        }
    }
}
