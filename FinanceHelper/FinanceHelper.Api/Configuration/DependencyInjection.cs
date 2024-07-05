using System.Globalization;
using System.Linq;
using System.Text;
using FinanceHelper.Api.Configuration.Options;
using FinanceHelper.Api.Filters;
using FinanceHelper.Api.HealthChecks;
using FinanceHelper.Api.Services;
using FinanceHelper.Api.Services.Interfaces;
using FinanceHelper.Shared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace FinanceHelper.Api.Configuration;

/// <summary>
///     API configuration
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    ///     API configuration
    /// </summary>
    /// <param name="builder">Api instance reference</param>
    public static void ConfigureApi(this IHostApplicationBuilder builder)
    {
        ConfigureAuthorization(builder.Services, builder.Configuration);
        ConfigureInfrastructure(builder.Services, builder.Configuration);
        ConfigureSwagger(builder.Services);
    }

    private static void ConfigureAuthorization(IServiceCollection services, IConfiguration configuration)
    {
        var jwtOptions = new JwtOptions();
        configuration.GetSection(nameof(JwtOptions)).Bind(jwtOptions);
        services.Configure<JwtOptions>(x =>
        {
            x.Audience = jwtOptions.Audience;
            x.Issuer = jwtOptions.Issuer;
            x.Key = jwtOptions.Key;
            x.TokenLifetimeInHours = jwtOptions.TokenLifetimeInHours;
        });

        services.AddAuthorization();
        services.AddAuthentication(x =>
        {
            x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtOptions.Issuer,
                ValidAudience = jwtOptions.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key))
            };
        });
    }

    private static void ConfigureInfrastructure(IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddHealthChecks().AddCheck<ApiHealthCheck>("API");
        services.AddControllers(options => options.Filters.Add<ApiExceptionFilterAttribute>());
        services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedLocalizations = configuration.GetSection(ConfigurationKeys.SupportedLocalization).Get<string[]>()!;
                var supportedCultures = supportedLocalizations.Select(x => new CultureInfo(x)).ToList();

                options.DefaultRequestCulture = new RequestCulture("ru");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            }
        );
    }

    private static void ConfigureSwagger(IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerConfiguration>();
    }
}