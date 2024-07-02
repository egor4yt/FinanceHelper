﻿using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using FinanceHelper.Api.Filters;
using FinanceHelper.Api.HealthChecks;
using FinanceHelper.Api.Services;
using FinanceHelper.Api.Services.Interfaces;
using FinanceHelper.Api.Swagger;
using FinanceHelper.Shared;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace FinanceHelper.Api.Configuration;

/// <summary>
///     API configuration
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    ///     API configuration
    /// </summary>
    /// <param name="builder">Api instance refference</param>
    public static void ConfigureApi(this IHostApplicationBuilder builder)
    {
        ConfigureAuthorization(builder.Services);
        ConfigureInfrastructure(builder.Services, builder.Configuration);
        ConfigureSwagger(builder.Services, builder.Configuration);
    }

    private static void ConfigureAuthorization(IServiceCollection services)
    {
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.Cookie.Name = "id";
                options.Events = new CookieAuthenticationEvents
                {
                    OnRedirectToLogin = context =>
                    {
                        context.Response.StatusCode = 401;
                        return Task.CompletedTask;
                    },
                    OnRedirectToAccessDenied = context =>
                    {
                        context.Response.StatusCode = 401;
                        return Task.CompletedTask;
                    }
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

    private static void ConfigureSwagger(IServiceCollection services, IConfiguration configuration)
    {
        var swaggerDocOptions = new SwaggerDocOptions();
        configuration.GetSection(nameof(SwaggerDocOptions)).Bind(swaggerDocOptions);

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(swagger =>
        {
            swagger.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = swaggerDocOptions.Title,
                Version = "v1",
                Description = swaggerDocOptions.Description,
                TermsOfService = new Uri("https://github.com"),
                Contact = new OpenApiContact
                {
                    Name = swaggerDocOptions.Organization,
                    Email = swaggerDocOptions.Email
                },
                License = new OpenApiLicense
                {
                    Name = "MIT",
                    Url = new Uri("https://github.com/")
                }
            });

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            swagger.IncludeXmlComments(xmlPath);
        });
    }
}