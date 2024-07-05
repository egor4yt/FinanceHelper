using System;
using System.IO;
using System.Reflection;
using FinanceHelper.Api.Configuration.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace FinanceHelper.Api.Configuration;

/// <summary>
///     Configure swagger options
/// </summary>
public class SwaggerConfiguration(IConfiguration configuration) : IConfigureOptions<SwaggerGenOptions>
{
    /// <inheritdoc />
    public void Configure(SwaggerGenOptions options)
    {
        var swaggerDocOptions = new SwaggerDocOptions();
        configuration.GetSection(nameof(SwaggerDocOptions)).Bind(swaggerDocOptions);

        options.SwaggerDoc("v1", new OpenApiInfo
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

        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.Http,
            Description = "Please provide a valid token",
            Name = "JWT Bearer authorization",
            In = ParameterLocation.Header,
            Scheme = "Bearer",
            BearerFormat = "JWT"
        });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                ArraySegment<string>.Empty
            }
        });

        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        options.IncludeXmlComments(xmlPath);
    }
}